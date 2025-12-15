using UnityEngine;

public class BuildingIndustry : Building
{
    public float baseIncome = 100;
    public override void Upgrade()
    {
        base.Upgrade();
        baseIncome += 100 * level;
    }

    protected override void NewDay()
    {
        base.NewDay();
        GameController.Instance.AddMoney(baseIncome);
    }
}
