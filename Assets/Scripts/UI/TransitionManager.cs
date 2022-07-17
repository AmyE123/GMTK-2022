using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    private static TransitionManager _instance;

    [SerializeField]
    private bool _startVisible;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private CanvasGroup _grp;

    private string _sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;    
    }

    public static void StartTransition(string sceneName)
    {
        if (_instance == null)
            _instance = FindObjectOfType<TransitionManager>();

        _instance.StartTransition2(sceneName);
    }

    private void StartTransition2(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
