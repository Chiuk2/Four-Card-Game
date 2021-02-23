using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiLoPlayerScript : MonoBehaviour
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

    public virtual void StartHand()
    {
        GetCard();
        
        // Set if 2 more cards are wanted
        //GetCard();  
        //GetCard();
    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
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

    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
        }
        cardIndex = 0;
        handValue = 0;
    }
}
