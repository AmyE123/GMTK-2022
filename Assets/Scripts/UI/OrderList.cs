using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class OrderList : MonoBehaviour
{
    [SerializeField]
    private GameObject _iconPrefab;

    [SerializeField]
    private RectTransform _parentRect;

    private LevelConfig _level;

    private List<RecipeUI> _childUI = new List<RecipeUI>();

    private IEnumerable<PickupObject> _lastKnownPickups;

    public void SetLevel(LevelConfig lvl)
    {
        _level = lvl;
    }

    public RecipeUI AddOrder(Customer customer)
    {
        GameObject newObj = Instantiate(_iconPrefab, _parentRect);
        RecipeUI newUI = newObj.GetComponent<RecipeUI>();
        newUI.SetCustomer(customer);
        _childUI.Add(newUI);
        
        if (_lastKnownPickups != null)
            OnPlayerHoldChanged(_lastKnownPickups);

        return newUI;
    }

    public void OnPlayerHoldChanged(IEnumerable<PickupObject> pickups)
    {
        _lastKnownPickups = pickups;

        List<GameDice> dice = new List<GameDice>();
        bool holdingNonDice = false;

        foreach (PickupObject obj in pickups)
        {
            if (obj is GameDice)
                dice.Add(obj as GameDice);
            else
                holdingNonDice = true;
        }

        // No valid recipes if holding a non-dice pickup
        if (holdingNonDice)
            dice.Clear();

        List<Recipe> validRecs = OrderChecker.GetMatchingRecepies(_level, dice);

        foreach (RecipeUI ui in _childUI)
            ui.SetPlayerValidRecipes(validRecs);
    }
}
