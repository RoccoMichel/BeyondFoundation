using UnityEngine;

public class BuildingHousing : Building
{
    public int residence;
    public int maxResidence;

    public override void Upgrade()
    {
        base.Upgrade();
        maxResidence += 10 * level;
    }
}
