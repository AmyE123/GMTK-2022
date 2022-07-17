using DG.Tweening;
using GameData;
using UnityEngine;

public class OvenController : MonoBehaviour
{
    [SerializeField] 
    private GameObject _ovenPrefab;

    [SerializeField] 
    private Transform _ovenParent;

    [SerializeField] 
    private float _ovenDistance = 1f;

    [SerializeField] 
    private Oven[] _ovenSlots;

    [SerializeField] 
    private float _spawnInTime;

    private int _ovenCount; 

    public void SetLevel(LevelConfig levelConfig)
    {
        _ovenCount = levelConfig.OvenCount;
        InitializeOvens(_ovenCount, levelConfig);
    }

    void InitializeOvens(int ovenCount, LevelConfig levelConfig)
    {
        for (int i = 0; i < ovenCount; i++)
        {
            SpawnOven(i, levelConfig);
        }            
    }

    private void SpawnOven(int slotNum, LevelConfig levelConfig)
    {
        GameObject newObj = Instantiate(_ovenPrefab, _ovenParent);
        Vector3 finalPos = new Vector3(-slotNum * _ovenDistance, 0, 0);       

        Oven oven = newObj.GetComponent<Oven>();
        oven.SetLevelConfig(levelConfig);

        float finalScale = oven.transform.localScale.y;

        _ovenSlots[slotNum] = oven;

        oven.transform.localPosition = finalPos + new Vector3(0, -3, 0);

        oven.transform.DOScale(finalScale, _spawnInTime).SetEase(Ease.OutExpo);
        oven.transform.DOLocalMove(finalPos, _spawnInTime).SetEase(Ease.OutBack);
    }
}
