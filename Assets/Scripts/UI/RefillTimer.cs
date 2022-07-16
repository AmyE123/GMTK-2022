using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class RefillTimer : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        [SerializeField]
        private LevelController _levelController;

        public void Update()
        {
            _fillImage.fillAmount = _levelController.RefillTimerPercent;
        }
    }
}