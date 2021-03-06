﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public CardScript cardScript;
    public DeckScript deckScript;

    public int handValue = 0;
    public GameObject[] hand;
    public AudioClip moneySound;
    protected AudioSource audioS;

    public int cardIndex = 0;

    private int money = 1000;
    private List<CardScript> aceList = new List<CardScript>();

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        handValue += cardValue;

        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        AceCheck();
        cardIndex++;
        return handValue;
    }

    public void AdjustMoney(int amount)
    {
        if (amount > 0)
            audioS.PlayOneShot(moneySound);
        money += amount;
    }

    public int GetMoney()
    {
        return money;
    }

    public void AceCheck()
    {
        foreach(CardScript ace in aceList)
        {
            if (handValue > 21 && ace.GetValueOfCard() == 11)
            {
                ace.SetValue(1);
                handValue -= 10; 
            }
            else if (handValue + 10 < 22 && ace.GetValueOfCard() == 1)
            {
                ace.SetValue(11);
                handValue += 10;
            }
        }
    }

    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}
