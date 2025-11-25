using UnityEngine;

public class BuildingHousing : Building
{
    public int residence;
    public int maxResidence = 5;

    public override void Place()
    {
        base.Place();
        TryAddResidence(Random.Range(1, 10));
    }
    public override void Upgrade()
    {
        base.Upgrade();
        maxResidence += 10 * level;
        TryAddResidence(Random.Range(1, 10));
    }

    /// <summary>
    /// Try to add as many people to this household as possible, returns the rest.
    /// </summary>
    /// <param name="amount">The amount of People looking housing</param>
    /// <returns>Returns the amount of Citizens that could not find a residence</returns>
    public int TryAddResidence(int amount)
    {
        int left = amount;

        for (int i = 0; i < amount; i++)
        {
            if (residence >= maxResidence) break;
            left--;
            residence++;
        }

        GameController.Instance.AddCitizens(amount - left);
        return left;
    }
}