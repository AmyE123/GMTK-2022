using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerContextualUI : W2C
{
    [SerializeField]
    private CanvasGroup _grp;

    [SerializeField]
    private TMP_Text _actionText;


    PlayerPickupObjectDetection _playerPickupDetector;

    // Start is called before the first frame update
    void Start()
    {
        WorldToCanvas.W2CManager.Initialize(this);
        _playerPickupDetector = FindObjectOfType<PlayerPickupObjectDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerPickupDetector.ContextualTarget == null)
        {
            _grp.alpha = 0;
        }
        else
        {
            _grp.alpha = 1;
            _actionText.text = _playerPickupDetector.ContextualAction;
            SetPosition(_playerPickupDetector.ContextualTarget.position);
        }
    }
}
