using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public float musicVolume;
    public float soundVolume;

    public LevelConfig[] levels;
    public int selectedLevel;

    public LevelConfig[] LevelList => levels;

    public LevelConfig GetCurrentLevel()
    {
        if (selectedLevel >= LevelList.Length)
            return null;
            
        return LevelList[selectedLevel];
    }

    public void SaveToPrefs()
    {
        PlayerPrefs.SetString("GameSettings", JsonUtility.ToJson(this));
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        List<LevelConfig> tmpLevelDatas = new List<LevelConfig>(levels);

        if (PlayerPrefs.HasKey("GameSettings"))
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("GameSettings"), this);

        levels = tmpLevelDatas.ToArray();
    }
}
