using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelConfig _levelConfig;

    [SerializeField]
    private InputShelf _diceShelf;

    [SerializeField]
    private float _timeRemaining;

    [SerializeField]
    private float _timeUntilNextRefill;

    public float RefillTimerPercent => _timeUntilNextRefill / _levelConfig.RefillTime;

    public void Start()
    {
        _diceShelf.SetShelfWidth(_levelConfig.MaxDice);
        DoRefill();
    }

    public void Update()
    {
        _timeRemaining -= Time.deltaTime;
        _timeUntilNextRefill -= Time.deltaTime;

        if (_timeUntilNextRefill <= 0)
            DoRefill();
    }

    private void DoRefill()
    {
        _timeUntilNextRefill = _levelConfig.RefillTime;
        _diceShelf.RefillDice(_levelConfig);
    }
}
