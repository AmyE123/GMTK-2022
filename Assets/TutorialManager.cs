using GameData;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    LevelController _levelController;

    [SerializeField]
    private GameObject _levelTimer;

    [SerializeField]
    private GameSettings _settings;

    [SerializeField]
    private GameObject[] _textBits;

    float _time;

    int _stage;

    bool _exitedLevel = false;

    public void Start()
    {
        if (_settings.selectedLevel != 0)
        {
            Destroy(gameObject);
            return;
        }

        InputShelf.OnTakenEvent += DiceTakenFromInput;
        Oven.BakeCompleteEvent += OvenBakeEvent;
        Oven.ItemAddedEvent += OvenEnterEvent;
        Oven.ItemRemovedEvent += OvenRemoveEvent;
        OutputShelf.OnGivenEvent += DeliveryMadeEvent;
    }

    void DiceTakenFromInput()
    {
        if (_stage == 2)
            _stage ++;
    }

    void OvenBakeEvent()
    {
        if (_stage == 3)
            _stage ++;
    }

    void OvenEnterEvent()
    {

    }

    void OvenRemoveEvent()
    {

    }
    
    void DeliveryMadeEvent()
    {
        if (_stage == 4)
            _stage ++;
    }

    private void Update()
    {
        ShowItm(_stage);

        if (_stage < 4)
        {
            _levelController.IncreaseRefill();
        }

        _levelController.IncreaseTimeLimit();

        if (_stage == 0)
        {
            MoveOnAfter(5);
        }
        if (_stage == 1)
        {
            MoveOnAfter(4);
        }
        if (_stage > 4 && _stage < 16)
        {
            MoveOnAfter(0.5f);
        }

        if (_stage == 16)
        {
            _settings.selectedLevel ++;

            var nextLevel = _settings.GetCurrentLevel();

            if (nextLevel == null)
                TransitionManager.StartTransition("TitleScene");
            else
                TransitionManager.StartTransition("Environment");
            _stage++;
        }
    
    }

    private void MoveOnAfter(float seconds)
    {
        _time += Time.deltaTime;
        
        if (_time >= 5)
        {
            _time = 0;
            _stage++;
        }
    }

    private void ShowItm(int idx)
    {
        for(int i=0; i<_textBits.Length;i++)
            _textBits[i].SetActive(i == idx);
    }
}
