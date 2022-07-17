using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using DG.Tweening;

public class InputShelf : PickupHolder
{
    [SerializeField]
    private Transform _diceParent;

    [SerializeField]
    private GameObject _dicePrefab;

    [SerializeField]
    private Transform _meshParent;

    [SerializeField]
    private GameDice[] _diceSlots;

    [SerializeField]
    private float _spawnInTime;

    [SerializeField]
    private float _spawnDelay;

    [SerializeField]
    private float _destroyWaitTime;

    private Queue<Recipe.DiceColorNumberCombo> _tutorialQueue;

    private int ShelfWidth => _diceSlots == null ? 0 : _diceSlots.Length;

    public static System.Action OnTakenEvent;

    public override void RemovePickup(PickupObject obj)
    {
        for (int i=0; i<_diceSlots.Length; i++)
        {
            if (obj == _diceSlots[i])
            {
                OnTakenEvent?.Invoke();
                _diceSlots[i] = null;
                break;
            }
        }
    }

    public void SetShelfWidth(int width)
    {
        _meshParent.localScale = new Vector3(width, 1, 1);
        _diceSlots = new GameDice[width];
    }

    public void SetTutorialQueue(IEnumerable<Recipe.DiceColorNumberCombo> inp)
    {
        _tutorialQueue = new Queue<Recipe.DiceColorNumberCombo>(inp);
    }

    public void RefillDice(LevelConfig levelData)
    {
        int delayCount = 0;
        float refillDelay = 0;

        for (int i=0; i<ShelfWidth; i++)
        {
            if (_diceSlots[i] != null)
            {
                bool didDestroy = _diceSlots[i].Decay();

                if (didDestroy)
                {
                    _diceSlots[i] = null;
                }

                refillDelay = _destroyWaitTime;
            }
        }

        for (int i=0; i<ShelfWidth; i++)
        {
            if (_diceSlots[i] != null)
                continue;
            
            float delay = (delayCount * _spawnDelay) + refillDelay;
            SpawnSingleDie(levelData, i, delay);
            delayCount ++;
        }
    }

    public void SpawnSingleDie(LevelConfig levelData, int slotNum, float delayAmount)
    {
        DiceColor color = levelData.GetRandomColor();
        int value = levelData.GetRandomNumber();

        if (_tutorialQueue.Count > 0)
        {
            var combo = _tutorialQueue.Dequeue();
            color = combo.color;
            value = combo.number;
        }
        GameObject newObj = Instantiate(_dicePrefab, _diceParent);
        GameDice dice = newObj.GetComponent<GameDice>();
        dice.SetHolder(this);
        dice.SetColorAndValue(color, value);

        _diceSlots[slotNum] = dice;
        Vector3 finalPos = new Vector3(slotNum, dice.transform.localScale.y * 0.5f, 0);
        float finalScale = dice.transform.localScale.y;

        dice.transform.localPosition = finalPos + Vector3.up;
        dice.transform.localScale = Vector3.zero;

        dice.transform.DOScale(finalScale, _spawnInTime).SetEase(Ease.OutExpo).SetDelay(delayAmount);
        dice.transform.DOLocalMove(finalPos, _spawnInTime).SetEase(Ease.OutBounce).SetDelay(delayAmount);
    }
}
