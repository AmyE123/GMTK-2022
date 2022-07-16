using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using DG.Tweening;

public class InputShelf : MonoBehaviour
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

    private int ShelfWidth => _diceSlots == null ? 0 : _diceSlots.Length;

    public void SetShelfWidth(int width)
    {
        _meshParent.localScale = new Vector3(width, 1, 1);
        _diceSlots = new GameDice[width];
    }

    public void RefillDice(LevelConfig levelData)
    {
        for (int i=0; i<ShelfWidth; i++)
        {
            if (_diceSlots[i] != null)
                continue;

            SpawnSingleDie(levelData, i);
        }
    }

    public void SpawnSingleDie(LevelConfig levelData, int slotNum)
    {
        DiceColor color = levelData.GetRandomColor();
        int value = levelData.GetRandomNumber();

        GameObject newObj = Instantiate(_dicePrefab, _diceParent);
        GameDice dice = newObj.GetComponent<GameDice>();
        dice.SetColorAndValue(color, value);

        _diceSlots[slotNum] = dice;
        Vector3 finalPos = new Vector3(slotNum, dice.transform.localScale.y * 0.5f, 0);
        float finalScale = dice.transform.localScale.y;

        dice.transform.localPosition = finalPos + Vector3.up;
        dice.transform.localScale = Vector3.zero;

        dice.transform.DOScale(finalScale, _spawnInTime).SetEase(Ease.OutExpo).SetDelay(_spawnDelay * slotNum);
        dice.transform.DOLocalMove(finalPos, _spawnInTime).SetEase(Ease.OutBounce).SetDelay(_spawnDelay * slotNum);
    }
}
