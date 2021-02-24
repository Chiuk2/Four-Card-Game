using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPGameManager : GameManager
{
    public AudioClip holdBtnSound;

    public Button holdBtn1;
    public Button holdBtn2;
    public Button holdBtn3;
    public Button holdBtn4;
    public Button holdBtn5;
    public Button holdBtn6;
    public Button holdBtn7;

    public Text holdText1;
    public Text holdText2;
    public Text holdText3;
    public Text holdText4;
    public Text holdText5;
    public Text holdText6;
    public Text holdText7;

    public GameObject hideCard1;
    public GameObject hideCard2;
    public GameObject hideCard3;
    public GameObject hideCard4;
    public GameObject hideCard5;
    public GameObject hideCard6;
    public GameObject hideCard7;
    public GameObject pokerChips1;
    public GameObject pokerChips2;
    public GameObject pokerChips3;

    public HandCheckerScript handChecker;
    public SPPlayerScript sPokerPlayerScript;
    public SPPlayerScript sPokerDealerScript;

    protected List<int> notHeldCards = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
    protected int dealCount = 0;
    protected List<int> playerFinalValue;
    protected List<int> dealerFinalValue;
    
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
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
        holdBtn6.onClick.AddListener(() => HoldBtnClicked(6, holdText6));
        holdBtn7.onClick.AddListener(() => HoldBtnClicked(7, holdText7));
    }

    private void Update()
    {
        if (dealCount == 1 && notHeldCards.Count == 2)
        {
            notHeldCards.ForEach(x => DisableHeldButton(x));
            dealBtn.gameObject.SetActive(true);
        }
        else if (dealCount == 1 && notHeldCards.Count != 2)
        {
            ResetBtns(true);
            dealBtn.gameObject.SetActive(false);
        }
    }

    protected override void DealClicked()
    {
        List<int> sortedPlayerHand;
        List<int> sortedDealerHand;

        audioS.PlayOneShot(dealSound);

        if (++dealCount == 2)
        {
            sPokerPlayerScript.RemoveCards(notHeldCards);
            sPokerDealerScript.OpponentHandReveal();

            sortedPlayerHand = handChecker.SortHand(sPokerPlayerScript.fiveHand);
            sortedDealerHand = handChecker.SortHand(sPokerDealerScript.fiveHand);

            pot = betAmount * 2;
            playerFinalValue = CheckFinalHand(sortedPlayerHand, sPokerPlayerScript);
            dealerFinalValue = CheckFinalHand(sortedDealerHand, sPokerPlayerScript);
            scoreText.text = "HAND: " + UpdateHandText(playerFinalValue[0]);
            dealerScoreText.text = "OPP. HAND: " + UpdateHandText(dealerFinalValue[0]);

            if (handChecker.CompareHands(playerFinalValue, sortedPlayerHand, dealerFinalValue, sortedDealerHand))
            {
                gameText.text = "You Win!";
                sPokerPlayerScript.AdjustMoney(pot);
                currencyText.text = "$" + sPokerPlayerScript.GetMoney().ToString();
            }
            else
                gameText.text = "Dealer Wins";
            
            RoundOver();
        }
        else
        {
            StartRound();
            sPokerPlayerScript.ResetHand();
            sPokerDealerScript.ResetHand();
            GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
            sPokerPlayerScript.StartHand();
            sPokerDealerScript.StartHand();
        }
    }

    protected override void RoundOver()
    {
        takeAwayChips();
        HideDealerCards(false);
        betBtn.gameObject.SetActive(true);
        autoBetBtn.gameObject.SetActive(true);     
        pot = 0;
        ResetBtns(false);
        dealCount = 0;
        gameText.gameObject.SetActive(true);

        if (!autoBetSet)
        {
            betAmount = 20;
            betsText.text = "Bets: " + betAmount.ToString();
        }
    }

    protected virtual void StartRound()
    {
        pot = 0;
        scoreText.text = "HAND: --";
        dealerScoreText.text = "OPP. HAND: --";
        gameText.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(false);
        autoBetBtn.gameObject.SetActive(false);
        sPokerPlayerScript.AdjustMoney(-betAmount);
        currencyText.text = "$" + sPokerPlayerScript.GetMoney().ToString();
        ResetBtnTexts();
        HideDealerCards(true);
        ResetBtns(true);
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
        holdBtn6.gameObject.SetActive(onOff);
        holdBtn7.gameObject.SetActive(onOff);
        if (!onOff)
            notHeldCards = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
    }

    protected void DisableHeldButton(int btnNumber)
    {
        switch (btnNumber)
        {
            case 1: holdBtn1.gameObject.SetActive(false);
                break;
            case 2:
                holdBtn2.gameObject.SetActive(false);
                break;
            case 3:
                holdBtn3.gameObject.SetActive(false);
                break;
            case 4:
                holdBtn4.gameObject.SetActive(false);
                break;
            case 5:
                holdBtn5.gameObject.SetActive(false);
                break;
            case 6:
                holdBtn6.gameObject.SetActive(false);
                break;
            case 7:
                holdBtn7.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    protected virtual void HideDealerCards(bool onOff)
    {
        hideCard1.SetActive(onOff);
        hideCard2.SetActive(onOff);
        hideCard3.SetActive(onOff);
        hideCard4.SetActive(onOff);
        hideCard5.SetActive(onOff);
        hideCard6.SetActive(onOff);
        hideCard7.SetActive(onOff);
    }

    protected void takeAwayChips()
    {
        pokerChips1.SetActive(false);
        pokerChips2.SetActive(false);
        pokerChips3.SetActive(false);
    }

    protected override void BetClicked()
    {
        if (betAmount >= 300)
        {
            pokerChips1.SetActive(true);
        }
        if (betAmount >= 600)
        {
            pokerChips2.SetActive(true);
        }
        if (betAmount >= 900)
        {
            pokerChips3.SetActive(true);
        }

        if (betAmount + 20 < sPokerPlayerScript.GetMoney())
        {
            betAmount += 20;
        }
        else if (betAmount + 20 >= sPokerPlayerScript.GetMoney())
        {
            betAmount = sPokerPlayerScript.GetMoney();
        }
        betsText.text = "Bets: " + betAmount.ToString();
        audioS.PlayOneShot(chipBtnSound);
    }

    protected virtual void ResetBtnTexts()
    {
        holdText1.gameObject.SetActive(false);
        holdText2.gameObject.SetActive(false);
        holdText3.gameObject.SetActive(false);
        holdText4.gameObject.SetActive(false);
        holdText5.gameObject.SetActive(false);
        holdText6.gameObject.SetActive(false);
        holdText7.gameObject.SetActive(false);
    }

    protected virtual List<int> CheckFinalHand(List<int> sortedHand, SPPlayerScript playerScript)
    {
        List<int> handValue = new List<int>();

        if (handChecker.IsFullHouse(sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 7, handChecker.FullHouseValue(sortedHand) };
        }
        else if (handChecker.IsTwoPair(sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 3, handChecker.TwoPValue(sortedHand) };
        }
        else if (handChecker.SevenPair(sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 2, handChecker.PairValue(sortedHand) };
        }
        else if (handChecker.IsThreeOfAKind(sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 4, handChecker.ThreeKindValue(sortedHand) };
        }
        else if (handChecker.IsFourOfAKind(sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 8, handChecker.ThreeKindValue(sortedHand) };
        }
        else if (handChecker.IsRoyalFlush(playerScript.fiveHand, sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 10, 0 };
        }
        else if (handChecker.IsStraightFlush(playerScript.fiveHand, sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 9, sortedHand[4] };
        }
        else if (handChecker.IsStraight(sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 5, sortedHand[4] };
        }
        else if (handChecker.IsFlush(playerScript.fiveHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 6, sortedHand[4] };
        }
        else
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 1, sortedHand[4] };
        }
        
        return handValue;
    }

    protected string UpdateHandText(int hand)
    {
        switch (hand)
        {
            case 1: return "High Card";
            case 2: return "Pair";
            case 3: return "Two Pairs";
            case 4: return "Three of a Kind";
            case 5: return "Straight";
            case 6: return "Flush";
            case 7: return "Full House";
            case 8: return "Four of a Kind";
            case 9: return "Straight Flush";
            case 10: return "Royal Flush!";
            default: return "";
        }
    }
}
