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

    [SerializeField]
    private float _transitionSpeed = 0.6f;

    private string _sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        
        _image.fillAmount = 1;
        _image.fillClockwise = false;
        _grp.blocksRaycasts = true;
        _grp.alpha = 1;

        _image.DOFillAmount(0, _transitionSpeed).SetEase(Ease.Linear).SetDelay(0.1f).OnComplete(() => 
        {
            _grp.blocksRaycasts = false;
        });
    }

    public static void StartTransition(string sceneName)
    {
        if (_instance == null)
            _instance = FindObjectOfType<TransitionManager>();

        _instance.StartTransition2(sceneName);
    }

    private void StartTransition2(string sceneName)
    {
        _grp.alpha = 1;
        _grp.blocksRaycasts = true;
        _image.fillAmount = 0;
        _image.fillClockwise = true;

        _image.DOFillAmount(1, _transitionSpeed).SetEase(Ease.Linear).OnComplete(() => 
        {

            SceneManager.LoadScene(sceneName);

        });
    }
}
