using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Weapons/Card")]
public class CardSO : ScriptableObject
{
    [Header("UI")]
    public string cardName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Upgrade")]
    public UpgradeType type;
    public float value;
}

