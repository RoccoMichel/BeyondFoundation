using UnityEngine;

public class BuildingCommercial : Building
{
    public float baseIncome = 250;
    public override void Upgrade()
    {
        base.Upgrade();
        baseIncome += 100 * level;
    }

    protected override void NewDay()
    {
        GameController.Instance.AddMoney(baseIncome);
        GameController.Instance.AddCitizens(2);
    }
}
