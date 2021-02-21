using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPokerPlayerScript : HiLoPlayerScript
{
    public override void StartHand()
    {
        GetCard();
        GetCard();
        GetCard();
        GetCard();
        GetCard();
    }

    public void StartCardNotHeld(int tossCard)
    {
        tossCard -= 1;
        Debug.Log("Hand Index: " + tossCard);
        //hand[tossCard].GetComponent<CardScript>().transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
        int cardValue = deckScript.DealCard(hand[tossCard].GetComponent<CardScript>());
        hand[tossCard].GetComponent<Renderer>().enabled = true;
        cardIndex++;
    }
}
