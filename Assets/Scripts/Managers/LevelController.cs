using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameSettings _gameSettings;

    private LevelConfig _levelConfig;

    [SerializeField]
    private OvenController _ovenController;

    [SerializeField]
    private TutorialManager _tutorialManager;

    [SerializeField]
    private InputShelf _diceShelf;

    [SerializeField]
    private ObjectShelf _objectShelf;

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

    private int _deliveryCount;

    [SerializeField]
    private PlayerMovement _player;

    [SerializeField]
    private WinScreen _winScreen;

    public int HappyPoints => _currentHappyPoints;

    public float RefillTimerPercent => _timeUntilNextRefill / _levelConfig.RefillTime;

    public float LevelTimePercent => 1 - Mathf.Clamp01(_timeRemaining / _levelConfig.TotalLevelTime);

    public float LevelTimeRemaining => Mathf.Clamp(_timeRemaining, 0, 9999);

    private bool _timeUp;
    private bool _paused;

    public void SetPaused(bool yesno) => _paused = yesno;

    public void Start()
    {
        _levelConfig = _gameSettings.GetCurrentLevel();
        
        _orderUI.SetLevel(_levelConfig);
        _timeRemaining = _levelConfig.TotalLevelTime;
        _customerShelf.SetShelfWidth(_levelConfig.MaxOrders);
        _objectShelf.SetWidth(_levelConfig.ShelfSpace);
        _diceShelf.SetShelfWidth(_levelConfig.MaxDice);
        _ovenController.SetLevel(_levelConfig);

        _diceShelf.SetTutorialQueue(_levelConfig.TutorialDiceQueue);
        _customerShelf.SetTutorialQueue(_levelConfig.TutorialRequestQueue);
        
        DoRefill();
    }

    public void IncreaseRefill() => _timeUntilNextRefill = _levelConfig.RefillTime;
    public void IncreaseTimeLimit() => _timeRemaining = _levelConfig.TotalLevelTime;

    public void Update()
    {
        if (_timeUp || _paused)
            return;

        _timeRemaining -= Time.deltaTime;
        _timeUntilNextRefill -= Time.deltaTime;
        _timeUntilNextCustomer -= Time.deltaTime;

        if (_timeUntilNextRefill <= 0)
            DoRefill();

        if (_timeUntilNextCustomer <= 0)
            DoNewCustomer();

        if (_timeRemaining <= 0)
            OnTimeUp();
    }

    private void OnTimeUp()
    {
        _timeUp = true;
        _winScreen.EnableScreen(_deliveryCount, _currentHappyPoints);
        _player.StopMoving();
    }

    public void OnDeliveryComplete(FinishedFood food)
    {
        int freshnessBonus = Mathf.Clamp(((int) food.Freshness + 1) * 5, 0, 10);
        _currentHappyPoints += food.Recipe.BasePoints;
        _deliveryCount ++;
        // TODO burst and text and particles
        WorldToCanvas.W2CManager.CreateText(food.transform.position, $"+{food.Recipe.BasePoints} HAPPY POINTS!");
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
