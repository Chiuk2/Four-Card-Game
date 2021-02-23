using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SPPlayerScript : HiLoPlayerScript
{
    public GameObject[] fiveHand;
    public override void StartHand()
    {
        GetCard();
        GetCard();
        GetCard();
        GetCard();
        GetCard();
        GetCard();
        GetCard();
    }

    public void StartCardNotHeld(int tossCard)
    {
        tossCard -= 1;
        int cardValue = deckScript.DealCard(hand[tossCard].GetComponent<CardScript>());
        hand[tossCard].GetComponent<Renderer>().enabled = true;
        cardIndex++;
    }

    public void OpponentHandReveal()
    {
        List<GameObject> newHand = new List<GameObject>(hand);
        List<int> opIndexList = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        for (int i = 0; i < 2; i++)
        {
            int randomNum = Random.Range(0, opIndexList.Count - 1);
            hand[opIndexList[randomNum] - 1].transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
            newHand.Remove(hand[opIndexList[randomNum] - 1]);
            opIndexList.RemoveAt(randomNum);
        }
        fiveHand = newHand.ToArray();
    }

    public void RemoveCards(List<int> index)
    {
        List<GameObject> newHand = new List<GameObject>(hand);

        index.ForEach(x =>
        {
            hand[x - 1].transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
            newHand.Remove(hand[x - 1]);
        });

        fiveHand = newHand.ToArray();
    }
}
