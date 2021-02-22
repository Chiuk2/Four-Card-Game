using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandCheckerScript : MonoBehaviour
{
    public List<int> SortHand(GameObject[] hand)
    {
        List<int> sortedHand = new List<int>();

        for (int i = 0; i < hand.Length; i++)
        {
            sortedHand.Add(hand[i].GetComponent<CardScript>().GetValueOfCard());
        }

        sortedHand.Sort();
        return sortedHand;
    }

    public bool IsStraight(List<int> hand)
    {
        if (hand.Count == 5)
        {
            int i, testValue;
            // Check for Ace
            if (hand[0] == 1)
            {
                bool a = hand[1] == 2 && hand[2] == 3 && hand[3] == 4 && hand[4] == 5;
                bool b = hand[1] == 10 && hand[2] == 11 && hand[3] == 12 && hand[4] == 13;

                return (a || b);
            }
            else
            {
                testValue = hand[0] + 1;

                for (i = 1; i < 5; i++)
                {
                    if (hand[i] != testValue)
                        return (false);

                    testValue++;
                }

                return (true);
            }
        }
        else if (hand.Count == 7)
        {
            // Implement for Seven Cards
            return (false);
        }
        return (false);
    }

    public bool IsFlush(GameObject[] hand)
    {
        if (hand.Length == 5)
        {
            //Check suit for first card
            string firstSuit;
            if (hand[0].GetComponent<MeshRenderer>().material.name.Contains("Clubs"))
            {
                firstSuit = "Clubs";
            }
            else if (hand[0].GetComponent<MeshRenderer>().material.name.Contains("Hearts"))
            {
                firstSuit = "Hearts";
            }
            else if (hand[0].GetComponent<MeshRenderer>().material.name.Contains("Diamonds"))
            {
                firstSuit = "Diamonds";
            }
            else
            {
                firstSuit = "Spades";
            }

            foreach (GameObject card in hand)
            {
                Debug.Log("First Suit: " + firstSuit);
                Debug.Log("Card material name: " + card.GetComponent<MeshRenderer>().material.name);
                if (!card.GetComponent<MeshRenderer>().material.name.Contains(firstSuit))
                    return (false);
            }
            return (true);
        }
        else if (hand.Length == 7)
        {
            // Implement for seven cards
            return false;
        }
        return false;
    }

    public bool IsPair(List<int> hand)
    {
        if (hand.GroupBy(h => h)
                .Where(g => g.Count() == 2)
                .Count() == 1)
        {
            return hand.Where(y => y > 10 || y == 1)
                        .GroupBy(g => g)
                        .Where(h => h.Count() == 2)
                        .Any();
        }
        
        return false;
    }

    public bool IsTwoPair(List<int> hand)
    {
        return hand.GroupBy(x => x)
                    .Where(y => y.Count() == 2)
                    .Count() == 2;
    }

    public bool IsThreeOfAKind(List<int> hand)
    {
        return hand.GroupBy(x => x)
                    .Where(y => y.Count() == 3)
                    .Any();
    }

    public bool IsFourOfAKind(List<int> hand)
    {
        return hand.GroupBy(x => x)
                    .Where(y => y.Count() == 4)
                    .Any();

    }

    public bool IsFullHouse(List<int> hand)
    {
        return IsPair(hand) && IsThreeOfAKind(hand);
    }

    public bool IsStraightFlush(GameObject[] hand, List<int> sortedHand)
    {
        return IsStraight(sortedHand) && IsFlush(hand);
    }

    public bool IsRoyalFlush(GameObject[] hand, List<int> sortedHand)
    {
        return IsStraight(sortedHand) && IsFlush(hand) 
            && sortedHand.Contains(1) && sortedHand.Contains(13);
    }
}
