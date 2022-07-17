using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _deliveryCount;

    [SerializeField]
    private TMP_Text _happyCount;

    public void EnableScreen(int deliveries, int happyPoints)
    {
        gameObject.SetActive(true);
        _deliveryCount.text = deliveries.ToString();
        _happyCount.text = happyPoints.ToString();
    }

    public void ButtonPressMenu()
    {
        TransitionManager.StartTransition("TitleScene");
    }

    public void ButtonPressRetry()
    {
        TransitionManager.StartTransition("SampleScene");
    }

    public void ButtonPressContinue()
    {
        TransitionManager.StartTransition("SampleScene");
    }
}
