using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPGameManager : GameManager
{
    public AudioClip holdBtnSound, changeBetSound;
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
    public Text winMultiplierText;

    public FPokerPlayerScript fPokerPlayerScript;
    public HandCheckerScript handChecker;
    public HandBoardController handBoardController;

    private int dealCount = 0;
    private int betIndex = 0;
    protected int winMuliplier = 2;
    private List<int> notHeldCards = new List<int>() {1, 2, 3, 4, 5};
    
    // Start is called before the first frame update
    void Start()
    {
        pot = 0;
        gameText.gameObject.SetActive(false);
        audioS = GetComponent<AudioSource>();
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
        handBoardController.UpdateBetPanel(betIndex++);
        winMultiplierText.text = "WIN " + winMuliplier.ToString() + "X";
    }

    protected override void DealClicked()
    {
        List<int> sortedHand;

        audioS.PlayOneShot(dealSound);
        handBoardController.ResetBoard();

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
            sortedHand = handChecker.SortHand(fPokerPlayerScript.hand);
            pot = 0;
            CheckCurrentHand(sortedHand);
            RoundOver();
        }
        else
        {
            if (fPokerPlayerScript.GetMoney() <= 0)
            {
                GameOver();
                return;
            }
            else if (fPokerPlayerScript.GetMoney() < betIndex)
            {
                gameText.text = "Not Enough Funds";
                gameText.gameObject.SetActive(true);
                dealCount = 0;
                return;
            }

            StartRound();
            fPokerPlayerScript.ResetHand();
            GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
            fPokerPlayerScript.StartHand();
            sortedHand = handChecker.SortHand(fPokerPlayerScript.hand);
            CheckCurrentHand(sortedHand);
        }

    }

    protected override void RoundOver()
    {
        betBtn.gameObject.SetActive(true);
        autoBetBtn.gameObject.SetActive(true);
        winMultiplierText.text = "WIN " + winMuliplier.ToString() + "X" + pot;
        fPokerPlayerScript.AdjustMoney(pot * winMuliplier);
        currencyText.text = "$" + fPokerPlayerScript.GetMoney().ToString();
        
        if (pot > 0)
        {
            audioS.PlayOneShot(jackpotSound);
        }

        pot = 0;
        ResetBtns(false);
        dealCount = 0;
        gameText.gameObject.SetActive(true);
    }

    protected virtual void StartRound()
    {
        pot = 0;
        gameText.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(false);
        autoBetBtn.gameObject.SetActive(false);
        winMultiplierText.text = "WIN " + winMuliplier.ToString() + "X";
        fPokerPlayerScript.AdjustMoney(-betIndex);
        currencyText.text = "$" + fPokerPlayerScript.GetMoney().ToString();
        ResetBtnTexts();
        ResetBtns(true);
    }

    protected override void BetClicked()
    {
        // Bet One Button

        if (betIndex > 4)
            betIndex = 0;

        handBoardController.UpdateBetPanel(betIndex++);
        betsText.text = "Bet: " + betIndex;
        audioS.PlayOneShot(changeBetSound);
    }

    protected override void AutoBetClicked()
    {
        int prevIndex = betIndex > 4 ? 4 : betIndex;
        betIndex = 4;
        handBoardController.MaxBetPanel(betIndex, prevIndex);
        betsText.text = "Bet: " + (betIndex + 1);
        betIndex++;
        audioS.PlayOneShot(changeBetSound);
    }

    protected override void GameOver()
    {
        gameText.text = "GAME OVER";
        gameText.gameObject.SetActive(true);
        dealBtn.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(false);
        autoBetBtn.gameObject.SetActive(false);
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
        audioS.PlayOneShot(holdBtnSound);
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
        }
        else if (handChecker.IsTwoPair(sortedHand))
        {
            UpdateHandBoard("Two Pair");
        }
        else if (handChecker.IsPair(sortedHand))
        {
            UpdateHandBoard("Jack Or Better");
        }
        else if (handChecker.IsThreeOfAKind(sortedHand))
        {
            UpdateHandBoard("Three Of A Kind");
        }
        else if (handChecker.IsFourOfAKind(sortedHand))
        {
            UpdateHandBoard("Four Of A Kind");
        }
        else if (handChecker.IsRoyalFlush(fPokerPlayerScript.hand, sortedHand))
        {
            UpdateHandBoard("Royal Flush");
        }
        else if (handChecker.IsStraightFlush(fPokerPlayerScript.hand, sortedHand))
        {
            UpdateHandBoard("Straight Flush");
        }
        else if (handChecker.IsStraight(sortedHand))
        {
            UpdateHandBoard("Straight");
        }
        else if (handChecker.IsFlush(fPokerPlayerScript.hand))
        {
            UpdateHandBoard("Flush");
        }

    }

    protected void UpdateHandBoard(string hand)
    {
        switch (hand)
        {
            case "Royal Flush": 
                handBoardController.SetHandText(0);
                pot += handBoardController.SetBetText(betIndex - 1, 0);
                break;
            case "Straight Flush":
                handBoardController.SetHandText(1);
                pot += handBoardController.SetBetText(betIndex - 1, 1);
                break;
            case "Four Of A Kind":
                handBoardController.SetHandText(2);
                pot += handBoardController.SetBetText(betIndex - 1, 2);
                break;
            case "Full House":
                handBoardController.SetHandText(3);
                pot += handBoardController.SetBetText(betIndex - 1, 3);
                break;
            case "Flush":
                handBoardController.SetHandText(4);
                pot += handBoardController.SetBetText(betIndex - 1, 4);
                break;
            case "Straight":
                handBoardController.SetHandText(5);
                pot += handBoardController.SetBetText(betIndex - 1, 5);
                break;
            case "Three Of A Kind":
                handBoardController.SetHandText(6);
                pot += handBoardController.SetBetText(betIndex - 1, 6);
                break;
            case "Two Pair":
                handBoardController.SetHandText(7);
                pot += handBoardController.SetBetText(betIndex - 1, 7);
                break;
            case "Jack Or Better":
                handBoardController.SetHandText(8);
                pot += handBoardController.SetBetText(betIndex - 1, 8);
                break;
            default:
                break;
        }
    }
}
