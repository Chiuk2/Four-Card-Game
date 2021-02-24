using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FGameManager : SPGameManager
{
    protected List<int> notFiveHeldCards = new List<int>() { 1, 2, 3, 4, 5 };

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
    }

    // Update is called once per frame
    private void Update()
    {
    }

    protected override void DealClicked()
    {
        List<int> sortedPlayerHand;
        List<int> sortedDealerHand;

        audioS.PlayOneShot(dealSound);

        if (++dealCount == 2)
        {
            if (notFiveHeldCards.Count != 5)
            {
                GameObject.Find("Deck").GetComponent<HiLoDeckScript>().Shuffle();
                notFiveHeldCards.ForEach(i => sPokerPlayerScript.StartCardNotHeld(i));
            }
            else
            {
                sPokerPlayerScript.ResetHand();
                GameObject.Find("Deck").GetComponent<HiLoDeckScript>().Shuffle();
                sPokerPlayerScript.StartHand();
            }

            sortedPlayerHand = handChecker.SortHand(sPokerPlayerScript.hand);
            sortedDealerHand = handChecker.SortHand(sPokerDealerScript.hand);

            pot = betAmount * 2;
            playerFinalValue = CheckFinalHand(sortedPlayerHand, sPokerPlayerScript);
            dealerFinalValue = CheckFinalHand(sortedDealerHand, sPokerDealerScript);
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

    protected override void HideDealerCards(bool onOff)
    {
        hideCard1.SetActive(onOff);
        hideCard2.SetActive(onOff);
        hideCard3.SetActive(onOff);
        hideCard4.SetActive(onOff);
        hideCard5.SetActive(onOff);
    }

    protected override void ResetBtns(bool onOff)
    {
        holdBtn1.gameObject.SetActive(onOff);
        holdBtn2.gameObject.SetActive(onOff);
        holdBtn3.gameObject.SetActive(onOff);
        holdBtn4.gameObject.SetActive(onOff);
        holdBtn5.gameObject.SetActive(onOff);
        if (!onOff)
            notFiveHeldCards = new List<int>() { 1, 2, 3, 4, 5 };
    }

    protected override void ResetBtnTexts()
    {
        holdText1.gameObject.SetActive(false);
        holdText2.gameObject.SetActive(false);
        holdText3.gameObject.SetActive(false);
        holdText4.gameObject.SetActive(false);
        holdText5.gameObject.SetActive(false);
    }

    protected override void HoldBtnClicked(int btn, Text holdText)
    {
        if (notFiveHeldCards.Contains(btn))
        {
            notFiveHeldCards.Remove(btn);
            holdText.gameObject.SetActive(true);
        }
        else
        {
            notFiveHeldCards.Add(btn);
            holdText.gameObject.SetActive(false);
        }
        audioS.PlayOneShot(holdBtnSound);
    }

    protected override List<int> CheckFinalHand(List<int> sortedHand, SPPlayerScript playerScript)
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
        else if (handChecker.IsRoyalFlush(playerScript.hand, sortedHand))
        {
            if (sortedHand.Contains(1))
                sortedHand = handChecker.ExchangeAceValue(sortedHand);
            handValue = new List<int>() { 10, 0 };
        }
        else if (handChecker.IsStraightFlush(playerScript.hand, sortedHand))
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
        else if (handChecker.IsFlush(playerScript.hand))
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
}
