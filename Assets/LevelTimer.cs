using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    private LevelController _level;

    [SerializeField]
    private Text _text;

    // Update is called once per frame
    void Update()
    {
        if (_level == null)
            _level = FindObjectOfType<LevelController>();

        _text.text = Mathf.CeilToInt(_level.LevelTimeRemaining).ToString();
    }
}
