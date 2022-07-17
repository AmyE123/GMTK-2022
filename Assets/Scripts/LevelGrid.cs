using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using UnityEngine.UI;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] GameObject _buttonPrefab;
    [SerializeField] GameSettings _settings;
    [SerializeField] private ScrollRect scrollRect;

    public void Start()
    {
        int i=0;

        foreach (LevelConfig lvl in _settings.LevelList)
        {
            GameObject newObj = Instantiate(_buttonPrefab, transform);
            newObj.GetComponent<PlayLevelButton>().Init(lvl, i);
            i++;
        } 

        scrollRect.normalizedPosition = Vector2.one;
    }
}
