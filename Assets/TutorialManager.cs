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

    public void Start()
    {
        if (_settings.selectedLevel != 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        ShowItm(_stage);

        if (_stage == 0)
        {
            MoveOnAfter(5);
        }
        if (_stage == 1)
        {
            MoveOnAfter(4);
        }
        if (_stage == 2)
        {
            
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
