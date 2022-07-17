using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] GameObject _buttonPrefab;
    [SerializeField] GameSettings _settings;

    public void Start()
    {
        int i=0;

        foreach (LevelConfig lvl in _settings.LevelList)
        {
            GameObject newObj = Instantiate(_buttonPrefab, transform);
            newObj.GetComponent<PlayLevelButton>().Init(lvl, i);
            i++;
        }  
    }
}
