using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nav_sprint {

[CreateAssetMenu (fileName = "New CARD", menuName = "Card")]
public class Card : ScriptableObject
{

    public Sprite cardArt;
    // ...
    public string cardName; 
    public int cardHealth;
    public int cardDamage;
    public int cardManaCost;

    // something

    public CardType cardType;   // Unit, Building, Support
    public CardFaction cardFaction; // Undead, Faint, Neutral
    public RangeType rangeType; // Melee, Ranged

    //enums values

    public enum CardType
    {
        Unit,
        Building,
        Support,
    }

    public enum CardFaction
    {
        Undead,
        Faint,
        Neutral,
    }

    public enum RangeType
    {
        Melee,
        Ranged,
    }
}

}