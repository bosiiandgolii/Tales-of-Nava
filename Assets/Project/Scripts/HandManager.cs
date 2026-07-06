using UnityEngine;
using Nav_sprint;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;


public class HandManager : MonoBehaviour
{
    // setting
    public float fanSpread = 7f;
    public float cardSpacing = 100f;
    public float verticalSpacing = 100f;
    public static int maxCardInHand = 6;

    public GameObject cardPrefab; // prefab
    public Transform handTransform; // root of hand position
    public List<GameObject> cardsInHand = new List<GameObject>(); // list of card object in hand
    void Start()
    {
    
    }

    public void AddCardToHand( Card cardData)
    {
        if (cardsInHand.Count < maxCardInHand){
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        newCard.GetComponent<CardView>().cardData = cardData;

        UpdateHandVisuals();
        }
    }

    // autoupdate for easy testing
    void Update()
    {
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;
        
        // if only one card in hand place correctly
        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizePosition = (2f * i / (cardCount - 1) - 1f);

            float verticalOffset = verticalSpacing *  ( 1 * normalizePosition * normalizePosition);

            // final card position set
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
