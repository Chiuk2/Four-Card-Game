using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public GameObject[] cardObjects;

    protected int[] cardValues = new int[53];
    protected int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetCardValues();
    }

    public virtual void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < cardObjects.Length; i++)
        {
            num = i;

            num %= 13;

            if (num > 10 || num == 0)
            {
                num = 10;
            }

            cardValues[i] = num++;
        }
    }

    public void Shuffle()
    {
        for (int i = cardObjects.Length - 1; i > 0; --i)
        {
            int randomCard = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardObjects.Length -1) + 1;
            GameObject face = cardObjects[i];
            cardObjects[i] = cardObjects[randomCard];
            cardObjects[randomCard] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[randomCard];
            cardValues[randomCard] = value;
        }
        currentIndex = 1;
    }

    public int DealCard(CardScript cardScript)
    {
        if (currentIndex > 52)
            currentIndex = 0;
;
        cardScript.SetCard(cardObjects[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetValueOfCard();
    }

    public GameObject GetCardBack()
    {
        return cardObjects[0];
    }
}
