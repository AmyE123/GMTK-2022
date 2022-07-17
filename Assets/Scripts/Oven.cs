using DG.Tweening;
using GameData;
using System.Collections.Generic;
using UnityEngine;
using WorldToCanvas;

public class Oven : MonoBehaviour
{
    private Recipe _currentItem;
    private LevelConfig _levelConfig;
    private float _timeLeft;

    [SerializeField]
    private GameObject _uiPrefab;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _ovenFinishedSFX;

    [SerializeField]
    private GameObject _finishedFoodPrefab;

    [SerializeField]
    private Transform _doorTransform;

    [SerializeField]
    private Transform _mainOven;

    [SerializeField]
    private ParticleController _particles;

    private float _wobbleFactor;

    private float _wobblePhase;

    public bool HasItemReady => _currentItem != null && _timeLeft <= 0;

    public bool IsBaking => _currentItem != null && _timeLeft > 0;

    public bool IsEmpty => _currentItem == null;

    public Recipe CurrentItem => _currentItem;

    public float BakePercent => 1 - Mathf.Clamp01(_timeLeft / _currentItem.BakeTime);

    [SerializeField]
    private Transform _closedDoorTransform;

    [SerializeField]
    private Transform _openDoorTransform;

    private bool _paused = false;

    public void SetPaused(bool yesno) => _paused = yesno;

    public static System.Action ItemAddedEvent;
    public static System.Action BakeCompleteEvent;
    public static System.Action ItemRemovedEvent;

    void Start()
    {
        W2C.InstantiateAs<OvenTimer>(_uiPrefab).Init(this);
    }

    private void Awake()
    {
        _audioSource = GameObject.Find("SFXManager").GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (_paused)
            return;

        if (_currentItem != null && _timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            
            if (_timeLeft <= 0)
                OnBakeComplete();
        }

        if (IsBaking)
        {
            _particles.isEmitting = true;
            _doorTransform.rotation = Quaternion.Lerp(_doorTransform.rotation, _closedDoorTransform.rotation, Time.deltaTime * 15);
        }
        else
        {
            _particles.isEmitting = false;
            _doorTransform.rotation = Quaternion.Lerp(_doorTransform.rotation, _openDoorTransform.rotation, Time.deltaTime * 15);
        }
    }

    private void OnBakeComplete()
    {
        Debug.Log($"Finished baking: {_currentItem.name}");
        // play some animation perhaps?
        _audioSource.PlayOneShot(_ovenFinishedSFX);
        BakeCompleteEvent?.Invoke();
    }

    public FinishedFood TakeOutItem()
    {
        if (_currentItem == null || _timeLeft > 0)
            return null;

        Recipe item = _currentItem;
        _currentItem = null;

        GameObject newObj = Instantiate(_finishedFoodPrefab, transform.position, _finishedFoodPrefab.transform.rotation);
        FinishedFood food = newObj.GetComponent<FinishedFood>();
        food.SetRecipe(item);
        ItemRemovedEvent?.Invoke();
        return food;
    }

    public bool GiveDice(List<GameDice> diceList)
    {
        if (_currentItem != null)
        {
            W2CManager.CreateError(transform.position, "oven in use!");
            Debug.Log("Oven is full!");
            return false;
        }

        List<Recipe> matchingRecipies = OrderChecker.GetMatchingRecepies(_levelConfig, diceList);

        if (matchingRecipies.Count == 0)
        {
            W2CManager.CreateError(transform.position, "wrong recipe!");
            Debug.Log("No matching recipes");
            return false;
        }

        if (matchingRecipies.Count == 1)
        {
            BeginBaking(matchingRecipies[0]);
        }
        else
        {
            foreach (Customer customer in FindObjectOfType<OutputShelf>().CustomersInOrderTheyCame) // yes I know but gamejam
            {
                bool foundCustomer = false;

                foreach (Recipe rec in matchingRecipies)
                {
                    if (rec == customer.RecipeRequest)
                        foundCustomer = true; 
                }

                if (foundCustomer)
                {
                    BeginBaking(customer.RecipeRequest);
                    break;
                }
            }
        }

        ItemAddedEvent?.Invoke();
        return true;
    }

    private void BeginBaking(Recipe rec)
    {
        Debug.Log($"Started baking: {rec.name}");
        _currentItem = rec;
        _timeLeft = rec.BakeTime;
    }

    public void SetLevelConfig(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }
}
