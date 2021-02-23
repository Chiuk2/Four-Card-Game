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

    public bool IsFlush(GameObject[] hand)
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
            if (!card.GetComponent<MeshRenderer>().material.name.Contains(firstSuit))
                return (false);
        }
        return (true);
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
        return IsThreeOfAKind(hand) && SevenPair(hand);
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

    public bool SevenPair(List<int> hand)
    {
        return hand.GroupBy(h => h)
                 .Where(g => g.Count() == 2)
                 .Count() == 1;
    }

    public int PairValue(List<int> hand)
    {
        return hand.GroupBy(h => h)
                     .Where(g => g.Count() == 2)
                     .Select(x => x.Key).First();
    }

    public int TwoPValue(List<int> hand)
    {
        return hand.GroupBy(x => x)
                    .Where(y => y.Count() == 2)
                    .Select(y => y.Key)
                    .OrderByDescending(x => x).First();
    }

    public int ThreeKindValue(List<int> hand)
    {
        return hand.GroupBy(x => x)
                    .Where(y => y.Count() == 3)
                    .Select(y => y.Key)
                    .OrderByDescending(x => x).First();
    }

    public int FourKindValue(List<int> hand)
    {
        return hand.GroupBy(x => x)
                    .Where(y => y.Count() == 4)
                    .Select(y => y.Key)
                    .OrderByDescending(x => x).First();
    }

    public int FullHouseValue(List<int> hand)
    {
        return ThreeKindValue(hand);
    }

    public bool CompareHands(List<int> playerValue, List<int> playerList, 
        List<int> opValue, List<int> opList)
    {
        List<int> playerListChecked = playerList;
        List<int> opListChecked = opList;

        if (playerList.Contains(0))
        {
            playerListChecked = ExchangeAceValue(playerList);
            playerListChecked.Sort();
        }
        if (opListChecked.Contains(0))
        {
            opListChecked = ExchangeAceValue(opList);
            opListChecked.Sort();
        }

        if (playerValue[0] == 10)
            return true;

        if (playerValue[0] > opValue[0])
        {
            return true;
        }
        else if (playerValue[0] < opValue[0])
        {
            return false;
        }
        else if (playerValue[0] == opValue[0])
        {
            if (playerValue[1] > opValue[1])
            {
                return true;
            }
            else if (playerValue[1] < opValue[1])
            {
                return false;
            }
            else if (playerValue[1] == opValue[1])
            {
                for (int i = 0; i < 5; i++)
                {
                    if (playerListChecked[i] > opListChecked[i])
                        return true;
                    else if (playerListChecked[i] < opListChecked[i])
                        return false;
                }
            }
        }
        return false;
    }

    public List<int> ExchangeAceValue(List<int> hand)
    {
        while (hand.Contains(1))
        {
            hand[hand.IndexOf(1)] = 14;
        }

        return hand;
    }
}
