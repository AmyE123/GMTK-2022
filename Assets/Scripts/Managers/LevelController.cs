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
    private OutputShelf _customerShelf;

    [SerializeField]
    private OrderList _orderUI;

    [SerializeField]
    private float _timeRemaining;

    [SerializeField]
    private float _timeUntilNextRefill;

    [SerializeField]
    private float _timeUntilNextCustomer;

    public float RefillTimerPercent => _timeUntilNextRefill / _levelConfig.RefillTime;

    public void Start()
    {
        _customerShelf.SetShelfWidth(_levelConfig.MaxOrders);
        _diceShelf.SetShelfWidth(_levelConfig.MaxDice);
        DoRefill();
    }

    public void Update()
    {
        _timeRemaining -= Time.deltaTime;
        _timeUntilNextRefill -= Time.deltaTime;
        _timeUntilNextCustomer -= Time.deltaTime;

        if (_timeUntilNextRefill <= 0)
            DoRefill();

        if (_timeUntilNextCustomer <= 0)
            DoNewCustomer();
    }

    private void DoRefill()
    {
        _timeUntilNextRefill = _levelConfig.RefillTime;
        _diceShelf.RefillDice(_levelConfig);
    }

    private void DoNewCustomer()
    {
        bool didSpawn = _customerShelf.SpawnRandomNewCustomer(_levelConfig);
        _timeUntilNextCustomer = didSpawn ? _levelConfig.OrderSpawnTime : 5;
    }
}
