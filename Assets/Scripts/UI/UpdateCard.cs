using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateCard : MonoBehaviour
{
    public TMP_Text cardNameText;
    public TMP_Text cardDescriptionText;
    public Image cardIconImage;

    public CardSO currentCard;
    public void SetCard(CardSO card)
    {
        currentCard = card;

        cardNameText.text = card.cardName;
        cardDescriptionText.text = card.description;
        cardIconImage.sprite = card.icon;
    }

    public void OnCardSelected()
    {
        Cardmanager cardManager = FindAnyObjectByType<Cardmanager>();

        if (cardManager != null)
        {
            cardManager.ApplyCard(currentCard);
            Debug.Log(currentCard.cardName);
        }
    }
}
