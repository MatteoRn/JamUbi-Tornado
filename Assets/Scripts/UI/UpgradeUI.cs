using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public GameObject panel;
    public UpdateCard[] allCards;
    public Button[] cardButtons;

    public Cardmanager cardManager;

    void Start()
    {
        panel.SetActive(false);
    }

    public void ShowCards()
    {
        Time.timeScale = 0f;

        panel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            UpdateCard card = allCards[Random.Range(0, allCards.Length)];
            int index = i;

            cardButtons[i].GetComponentInChildren<Text>().text = card.name;

            cardButtons[i].onClick.RemoveAllListeners();
            cardButtons[i].onClick.AddListener(() => SelectCard(card));
        }
    }

    void SelectCard(UpdateCard card)
    {
        cardManager.ApplyCard(card.currentCard);
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
