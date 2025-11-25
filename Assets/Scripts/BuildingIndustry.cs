using UnityEngine;

public class BuildingIndustry : Building
{
    public float baseIncome = 100;
    public Event profitEvent;
    public override void Upgrade()
    {
        base.Upgrade();
    }

    protected override void NewDay()
    {
        base.NewDay();
        GameController.Instance.AddMoney(baseIncome);
    }
}
