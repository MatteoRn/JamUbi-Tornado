using UnityEngine;

public class CarLevelSystems : MonoBehaviour
{
    [Header("Level Settings")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    [Header("XP Curve")]
    public float xpMultiplier = 1.3f;

    void Start()
    {
        Debug.Log($"Level system initialized ? Level {level}");
    }
    public void AddXP(int amount)
    {
        currentXP += amount;
        CheckLevelUp();
    }

    void CheckLevelUp()
    {
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);

        Debug.Log($"LEVEL UP ! Nouveau niveau : {level}");
    }
}
