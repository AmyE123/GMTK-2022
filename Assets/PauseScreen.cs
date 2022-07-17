using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    PlayerMovement _player;

    [SerializeField]
    LevelController _level;

    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _level = FindObjectOfType<LevelController>();
    }
    
    public void OpenMenu()
    {
        Start();
        gameObject.SetActive(true);
        _player.SetPaused(true);
        _level.SetPaused(true);
        
        foreach(Oven oven in FindObjectsOfType<Oven>())
            oven.SetPaused(true);
    }

    public void ButtonPressMenu()
    {
        TransitionManager.StartTransition("TitleScene");
    }

    public void ButtonPressRetry()
    {
        TransitionManager.StartTransition("Environment");
    }

    public void ButtonPressContinue()
    {
        gameObject.SetActive(false);
        _player.SetPaused(false);
        _level.SetPaused(false);
        
        foreach(Oven oven in FindObjectsOfType<Oven>())
            oven.SetPaused(false);
    }
}
