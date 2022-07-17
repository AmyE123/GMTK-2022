using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField]
    private GameSettings _settings;

    [SerializeField]
    private AudioSource _source;

    // Update is called once per frame
    void Update()
    {
        _source.volume = _settings.soundVolume;
    }
}
