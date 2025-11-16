using TMPro;
using UnityEngine;

public class BuildingMenu : Menu
{
    [Header("References")]
    [SerializeField] private TMP_Text labelDisplay;
    [SerializeField] private TMP_Text descriptionDisplay;
    [HideInInspector] public Building building;

    private void Start()
    {
        RefreshVisuals();
    }

    public void Upgrade()
    {
        building.Upgrade();
        RefreshVisuals();
    }

    public void Sell()
    {
        building.Sell();
        DestroySelf();
    }

    private void RefreshVisuals()
    {
        labelDisplay.text = building.label;
        descriptionDisplay.text = $@"
Level : {building.level}
Value: {building.value}
----------------------
" + building.description;
    }
}
