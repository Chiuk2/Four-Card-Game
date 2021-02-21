using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPGameManager : GameManager
{

    public FPokerPlayerScript fPokerPlayerScript;
    public HandCheckerScript handChecker;
    // Start is called before the first frame update
    void Start()
    {
        gameText.gameObject.SetActive(false);
        dealBtn.onClick.AddListener(() => DealClicked());
        betBtn.onClick.AddListener(() => BetClicked());
        autoBetBtn.onClick.AddListener(() => AutoBetClicked());
        quitBtn.onClick.AddListener(() => QuitClicked());
        noBtn.onClick.AddListener(() => NoClicked());
        yesBtn.onClick.AddListener(() => YesClicked());
    }

    protected override void DealClicked()
    {
        fPokerPlayerScript.ResetHand();
        gameText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        fPokerPlayerScript.StartHand();
        List<int> sortedHand = handChecker.SortHand(fPokerPlayerScript.hand);
        List<int> testHand = new List<int>() { 1,2,3,4,5 };
        sortedHand.ForEach(i => Debug.Log("Show sorted list: " + i));
        Debug.Log("Is Straight Hand? " + handChecker.IsStraight(sortedHand));
        Debug.Log("Is there flush? " + handChecker.IsFlush(fPokerPlayerScript.hand));
        Debug.Log("Is there full house " + handChecker.IsFullHouse(testHand));
        Debug.Log("Is there two pairs " + handChecker.IsTwoPair(testHand));
        Debug.Log("Is there jack or better pair " + handChecker.IsPair(testHand));
        Debug.Log("Is there three of a kind " + handChecker.IsThreeOfAKind(testHand));
        Debug.Log("Is there four of a kind " + handChecker.IsFourOfAKind(testHand));
        Debug.Log("Is there straight flush? " + handChecker.IsStraightFlush(fPokerPlayerScript.hand, testHand));

    }

    protected override void RoundOver()
    {

    }

    protected override void BetClicked()
    {

    }

    protected override void AutoBetClicked()
    {

    }
}
