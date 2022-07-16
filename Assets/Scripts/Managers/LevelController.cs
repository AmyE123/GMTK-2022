using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelConfig _levelConfig;

    [SerializeField]
    private InputShelf _diceShelf;

    public void Start()
    {
        _diceShelf.SetShelfWidth(_levelConfig.MaxDice);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            _diceShelf.RefillDice(_levelConfig);
    }
}
