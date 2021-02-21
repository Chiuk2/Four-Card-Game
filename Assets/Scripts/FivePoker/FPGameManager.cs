using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPGameManager : GameManager
{
    public Button holdBtn1;
    public Button holdBtn2;
    public Button holdBtn3;
    public Button holdBtn4;
    public Button holdBtn5;

    public Text holdText1;
    public Text holdText2;
    public Text holdText3;
    public Text holdText4;
    public Text holdText5;

    public FPokerPlayerScript fPokerPlayerScript;
    public HandCheckerScript handChecker;
    public HandBoardController handBoardController;

    private int dealCount = 0;
    private List<int> notHeldCards = new List<int>() {1, 2, 3, 4, 5};
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
        holdBtn1.onClick.AddListener(() => HoldBtnClicked(1, holdText1));
        holdBtn2.onClick.AddListener(() => HoldBtnClicked(2, holdText2));
        holdBtn3.onClick.AddListener(() => HoldBtnClicked(3, holdText3));
        holdBtn4.onClick.AddListener(() => HoldBtnClicked(4, holdText4));
        holdBtn5.onClick.AddListener(() => HoldBtnClicked(5, holdText5));
    }

    protected override void DealClicked()
    {
        if (++dealCount == 2)
        {
            if (notHeldCards.Count != 5)
            {
                GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
                notHeldCards.ForEach(i => fPokerPlayerScript.StartCardNotHeld(i));
            }
            else
            {
                fPokerPlayerScript.ResetHand();
                GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
                fPokerPlayerScript.StartHand();
            }
            ResetBtns(false);
            dealCount = 0;
        }
        else
        {
            ResetBtnTexts();
            ResetBtns(true);
            fPokerPlayerScript.ResetHand();
            GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
            fPokerPlayerScript.StartHand();
        }
        
        gameText.gameObject.SetActive(false);
        handBoardController.ResetBoard();
        List<int> sortedHand = handChecker.SortHand(fPokerPlayerScript.hand);
        List<int> testHand = new List<int>() { 1, 2, 3, 4, 5 };
        CheckCurrentHand(sortedHand);
        //sortedHand.ForEach(i => Debug.Log("Show sorted list: " + i));
        //Debug.Log("Is Straight Hand? " + handChecker.IsStraight(sortedHand));
        //Debug.Log("Is there flush? " + handChecker.IsFlush(fPokerPlayerScript.hand));
        //Debug.Log("Is there full house " + handChecker.IsFullHouse(sortedHand));
        //Debug.Log("Is there two pairs " + handChecker.IsTwoPair(sortedHand));
        //Debug.Log("Is there jack or better pair " + handChecker.IsPair(sortedHand));
        //Debug.Log("Is there three of a kind " + handChecker.IsThreeOfAKind(sortedHand));
        //Debug.Log("Is there four of a kind " + handChecker.IsFourOfAKind(sortedHand));
        //Debug.Log("Is there straight flush? " + handChecker.IsStraightFlush(fPokerPlayerScript.hand, sortedHand));

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

    protected virtual void HoldBtnClicked(int btn, Text holdText)
    {
        if (notHeldCards.Contains(btn))
        {
            notHeldCards.Remove(btn);
            holdText.gameObject.SetActive(true);
        }
        else
        {
            notHeldCards.Add(btn);
            holdText.gameObject.SetActive(false);
        }
    }

    protected virtual void ResetBtns(bool onOff)
    {
        holdBtn1.gameObject.SetActive(onOff);
        holdBtn2.gameObject.SetActive(onOff);
        holdBtn3.gameObject.SetActive(onOff);
        holdBtn4.gameObject.SetActive(onOff);
        holdBtn5.gameObject.SetActive(onOff);
        if (!onOff)
            notHeldCards = new List<int>() { 1, 2, 3, 4, 5 };
    }

    protected virtual void ResetBtnTexts()
    {
        holdText1.gameObject.SetActive(false);
        holdText2.gameObject.SetActive(false);
        holdText3.gameObject.SetActive(false);
        holdText4.gameObject.SetActive(false);
        holdText5.gameObject.SetActive(false);
    }

    protected virtual void CheckCurrentHand(List<int> sortedHand)
    {
        if (handChecker.IsFullHouse(sortedHand))
        {
            UpdateHandBoard("Full House");
            Debug.Log("Hit Full House");
        }
        else if (handChecker.IsTwoPair(sortedHand))
        {
            UpdateHandBoard("Two Pair");
            Debug.Log("Hit Two Pair");
        }
        else if (handChecker.IsPair(sortedHand))
        {
            UpdateHandBoard("Jack Or Better");
            Debug.Log("Hit Jack Or Better");
        }
        else if (handChecker.IsThreeOfAKind(sortedHand))
        {
            UpdateHandBoard("Three Of A Kind");
            Debug.Log("3 of a kind");
        }
        else if (handChecker.IsFourOfAKind(sortedHand))
        {
            UpdateHandBoard("Four Of A Kind");
            Debug.Log("4 of a kind");
        }
        else if (handChecker.IsRoyalFlush(fPokerPlayerScript.hand, sortedHand))
        {
            UpdateHandBoard("Royal Flush");
            Debug.Log("royal flush");
        }
        else if (handChecker.IsStraightFlush(fPokerPlayerScript.hand, sortedHand))
        {
            UpdateHandBoard("Straight Flush");
            Debug.Log("straight flush");
        }
        else if (handChecker.IsStraight(sortedHand))
        {
            UpdateHandBoard("Straight");
            Debug.Log("straight");
        }
        else if (handChecker.IsFlush(fPokerPlayerScript.hand))
        {
            UpdateHandBoard("Flush");
            Debug.Log("flush");
        }

    }

    protected void UpdateHandBoard(string hand)
    {
        switch (hand)
        {
            case "Royal Flush": 
                handBoardController.SetHandText(1);
                break;
            case "Straight Flush":
                handBoardController.SetHandText(2);
                break;
            case "Four Of A Kind":
                handBoardController.SetHandText(3);
                break;
            case "Full House":
                handBoardController.SetHandText(4);
                break;
            case "Flush":
                handBoardController.SetHandText(5);
                break;
            case "Straight":
                handBoardController.SetHandText(6);
                break;
            case "Three Of A Kind":
                handBoardController.SetHandText(7);
                break;
            case "Two Pair":
                handBoardController.SetHandText(8);
                break;
            case "Jack Or Better":
                handBoardController.SetHandText(9);
                break;
            default:
                break;
        }
    }
}
