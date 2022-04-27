using System;
using UnityEngine;

public class PointCollector : Singleton<PointCollector>
{
    public static Action OnPointsEarned;

    private int totalPoints = 0;
    private int collectedPoints = 0;
    private int multiplier = 1;

    private void Start()
    {
        if (PlayerPrefs.HasKey("totalPoints"))
        {
            totalPoints = PlayerPrefs.GetInt("totalPoints");
        }
        else
        {
            PlayerPrefs.SetInt("totalPoints", 0);
        }
    }

    public void CollectPoints(int amount)
    {
        collectedPoints += amount;
        OnPointsEarned?.Invoke();
    }

    public void SetMultiplier(int amount)
    {
        multiplier = amount;
    }

    public int GetCollectedPoints()
    {
        return collectedPoints;
    }

    public int GetPoints()
    {
        return collectedPoints * multiplier;
    }


    public int GetTotalPoints()
    {
        return totalPoints;
    }

    public int GetUpdatedPoints()
    {
        return PlayerPrefs.GetInt("totalPoints") + GetPoints();
    }

    public void SetTotalCoins()
    {
        totalPoints = PlayerPrefs.GetInt("totalPoints") + GetPoints();
    }
}
