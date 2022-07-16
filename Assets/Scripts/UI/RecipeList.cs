using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class RecipeList : MonoBehaviour
{
    [SerializeField]
    private GameObject _iconPrefab;

    [SerializeField]
    private RectTransform _parentRect;

    public void SetLevel(LevelConfig level)
    {
        foreach (Recipe r in level.AllRecipies)
        {
            GameObject newObj = Instantiate(_iconPrefab, _parentRect);
            newObj.GetComponent<RecipeUI>().SetRecipe(r);
        }
    }
}
