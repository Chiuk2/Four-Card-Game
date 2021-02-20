using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiLoGameManager : GameManager
{
    public Button highBtn1;
    public Button highBtn2;
    public Button highBtn3;
    public Button lowBtn1;
    public Button lowBtn2;
    public Button lowBtn3;

    public HiLoPlayerScript hlPlayerScript;
    public HiLoDealerScript hlDealerScript;

    // Start is called before the first frame update
    void Start()
    {
        showCards(false);
        gameText.gameObject.SetActive(false);
        highBtn1.gameObject.SetActive(false);
        lowBtn1.gameObject.SetActive(false);
        highBtn2.gameObject.SetActive(false);
        lowBtn2.gameObject.SetActive(false);
        highBtn3.gameObject.SetActive(false);
        lowBtn3.gameObject.SetActive(false);
        hideCard.SetActive(false);

        dealBtn.onClick.AddListener(() => DealClicked());
        betBtn.onClick.AddListener(() => BetClicked());
        autoBetBtn.onClick.AddListener(() => AutoBetClicked());
        quitBtn.onClick.AddListener(() => QuitClicked());
        noBtn.onClick.AddListener(() => NoClicked());
        yesBtn.onClick.AddListener(() => YesClicked());
        highBtn2.onClick.AddListener(() => SelectHigh(0));  // Change to appropriate index
        lowBtn2.onClick.AddListener(() => SelectLow(0));    // Change to appropriate index

        // Uncomment for more player cards
        //highBtn1.onClick.AddListener(() => SelectHigh(0));
        //highBtn3.onClick.AddListener(() => SelectHigh(2));
        //lowBtn1.onClick.AddListener(() => SelectLow(0));
        //lowBtn3.onClick.AddListener(() => SelectLow(2));
    }


    protected override void DealClicked()
    {
        hlPlayerScript.ResetHand();
        hlDealerScript.ResetHand();
        gameText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        hlPlayerScript.StartHand();
        hlDealerScript.StartHand();
        hideCard.SetActive(true);
        showCards(true);

        scoreText.text = "Hand: --";
        dealBtn.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(false);
        autoBetBtn.gameObject.SetActive(false);

        highBtn2.gameObject.SetActive(true);
        lowBtn2.gameObject.SetActive(true);

        // Uncomment for more player cards
        //highBtn1.gameObject.SetActive(true);
        //lowBtn1.gameObject.SetActive(true);
        //highBtn3.gameObject.SetActive(true);
        //lowBtn3.gameObject.SetActive(true);

        pot = betAmount * 2;
        betsText.text = "Bets: " + betAmount.ToString();
        hlPlayerScript.AdjustMoney(-betAmount);
        currencyText.text = "$" + hlPlayerScript.GetMoney().ToString();
    }

    protected override void RoundOver()
    {
        gameText.gameObject.SetActive(true);
        dealBtn.gameObject.SetActive(true);
        betBtn.gameObject.SetActive(true);
        autoBetBtn.gameObject.SetActive(true);
        highBtn1.gameObject.SetActive(false);
        lowBtn1.gameObject.SetActive(false);
        highBtn2.gameObject.SetActive(false);
        lowBtn2.gameObject.SetActive(false);
        highBtn3.gameObject.SetActive(false);
        lowBtn3.gameObject.SetActive(false);

        currencyText.text = "$" + hlPlayerScript.GetMoney().ToString();
        pot = 0;
        if (!autoBetSet)
        {
            betAmount = 20;
            betsText.text = "Bets: " + betAmount.ToString();
        }
    }

    private void SelectHigh(int selectCard)
    {
        int cardValue = hlPlayerScript.hand[selectCard].GetComponent<CardScript>().GetValueOfCard();
        int dealerCardValue = hlDealerScript.hand[0].GetComponent<CardScript>().GetValueOfCard();

        scoreText.text = "Hand: " + cardValue;
        hideCard.SetActive(false);
        dealerScoreText.text = "Dealer Hand: " + hlDealerScript.handValue.ToString();
        
        if (cardValue <= dealerCardValue)
        {
            gameText.text = "You Win!";
            hlPlayerScript.AdjustMoney(pot);
            RoundOver();
        }
        else if (cardValue > dealerCardValue)
        {
            gameText.text = "Dealer Wins!";
            RoundOver();
        }
    }

    private void SelectLow(int selectCard)
    {
        int cardValue = hlPlayerScript.hand[selectCard].GetComponent<CardScript>().GetValueOfCard();
        int dealerCardValue = hlDealerScript.hand[0].GetComponent<CardScript>().GetValueOfCard();

        scoreText.text = "Hand: " + cardValue;
        hideCard.SetActive(false);
        dealerScoreText.text = "Dealer Hand: " + hlDealerScript.handValue.ToString();
        dealerScoreText.gameObject.SetActive(true);

        if (cardValue >= dealerCardValue)
        {
            gameText.text = "You Win!";
            hlPlayerScript.AdjustMoney(pot);
            RoundOver();
        }
        else if (cardValue < dealerCardValue)
        {
            gameText.text = "Dealer Wins!";
            RoundOver();
        }
    }

    void showCards(bool doShow)
    {
        foreach (GameObject card in hlPlayerScript.hand)
        {
            card.SetActive(doShow);
        }
        hlDealerScript.hand[0].SetActive(doShow);
    }
}
