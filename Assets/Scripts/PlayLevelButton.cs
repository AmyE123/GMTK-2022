using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class PlayLevelButton : MonoBehaviour
{
    [SerializeField] Text _titleText;
    [SerializeField] Text _descText;

    private LevelConfig _level;
    private int _levelIdx;

    public void Init(LevelConfig level, int idx)
    {
        _level = level;
        _levelIdx = idx;
        _titleText.text = level.LevelName.ToUpper();
        _descText.text = level.Description;
    }

    public void ButtonPress()
    {
        TransitionManager.StartTransition("SampleScene");
    }
}
