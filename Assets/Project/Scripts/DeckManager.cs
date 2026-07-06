using System.Collections.Generic;
using System.Data.Common;
using Nav_sprint;
using UnityEngine;
using UnityEngine.Categorization;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    private int currentIndex = 0;
    
    void Start()
    {
        Card[] cards = Resources.LoadAll<Card>("Cards");

        allCards.AddRange(cards);

        HandManager hand = FindAnyObjectByType<HandManager>();
        for (int i = 0; i < 6; i++)
        {
            DrawCard(hand);
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        return;

        Card nextCard = allCards [currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex +1 ) % allCards.Count;
    }
}
