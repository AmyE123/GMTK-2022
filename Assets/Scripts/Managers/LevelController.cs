using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelConfig _levelConfig;

    [SerializeField]
    private OvenController _ovenController;

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

    [SerializeField]
    private int _currentHappyPoints;

    public int HappyPoints => _currentHappyPoints;

    public float RefillTimerPercent => _timeUntilNextRefill / _levelConfig.RefillTime;

    public float LevelTimePercent => 1 - Mathf.Clamp01(_timeRemaining / _levelConfig.TotalLevelTime);

    public void Start()
    {
        _orderUI.SetLevel(_levelConfig);
        _timeRemaining = _levelConfig.TotalLevelTime;
        _customerShelf.SetShelfWidth(_levelConfig.MaxOrders);
        _diceShelf.SetShelfWidth(_levelConfig.MaxDice);
        _ovenController.SetLevel(_levelConfig);
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

    public void OnDeliveryComplete(FinishedFood food)
    {
        int freshnessBonus = Mathf.Clamp(((int) food.Freshness + 1) * 5, 0, 10);
        _currentHappyPoints += food.Recipe.BasePoints + freshnessBonus;
        // TODO burst and text and particles
    }

    public void OnPlayerHoldChanged(IEnumerable<PickupObject> currentlyHolding)
    {
        _orderUI.OnPlayerHoldChanged(currentlyHolding);
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
