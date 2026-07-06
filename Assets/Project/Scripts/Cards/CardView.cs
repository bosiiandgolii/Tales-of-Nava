using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Nav_sprint;
using System;

public class CardView : MonoBehaviour
{
    public Card cardData;

    public Image cardArtImage;
    public Image cardFactionBackground;

    public TextMeshProUGUI cardNameText;

    public TextMeshProUGUI cardHealthText;

    public TextMeshProUGUI cardDamageText;

    public TextMeshProUGUI cardManaCostText;

    public Image [] typeImages;
    public Image [] rangeImages;
    
    void Start()
    {
        UpdateCardView();


    } 

    public void UpdateCardView(){
        
        // update card stats
        cardNameText.text = cardData.cardName;
        cardHealthText.text = cardData.cardHealth.ToString();
        cardDamageText.text = cardData.cardDamage.ToString();
        cardManaCostText.text = cardData.cardManaCost.ToString();

        // update mathing card background color based on card faction
        cardFactionBackground.color = cardData.cardFaction switch {
            Card.CardFaction.Undead => Color.darkSalmon,
            Card.CardFaction.Faint => Color.darkCyan,
            Card.CardFaction.Neutral => Color.darkGreen,
            _ => Color.whiteSmoke,
        };

        // update card type image
        typeImages[(int)cardData.cardType].gameObject.SetActive(true);
        
        // update card range image
        rangeImages[(int)cardData.rangeType].gameObject.SetActive(true);

        //update card image
        cardArtImage.sprite = cardData.cardArt;
    }
}
