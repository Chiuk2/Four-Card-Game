using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;
    public Button autoBetBtn;
    public Button quitBtn;
    public Button noBtn;
    public Button yesBtn;

    public Text standText;
    public Text currencyText;
    public Text dealerScoreText;
    public Text betsText;
    public Text scoreText;
    public Text gameText;
    public Text autoBetText;

    public GameObject quitBackDrop;
    public GameObject quitPopUp;

    public GameObject hideCard;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    private int standClicks = 0;
    protected int pot = 0;
    protected int betAmount = 20;
    protected bool autoBetSet = false;
    // Start is called before the first frame update
    void Start()
    {
        gameText.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(false);
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtn.onClick.AddListener(() => BetClicked());
        autoBetBtn.onClick.AddListener(() => AutoBetClicked());
        quitBtn.onClick.AddListener(() => QuitClicked());
        noBtn.onClick.AddListener(() => NoClicked());
        yesBtn.onClick.AddListener(() => YesClicked());
    }

    private void StandClicked()
    {
        standBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(false);
        standClicks++;
        if (standClicks > 1)
            RoundOver();
        dealerScoreText.gameObject.SetActive(true);
        hideCard.SetActive(false);
        HitDealer();
    }


    private void HitClicked()
    {
        if (playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20)
                RoundOver();
        }
    }

    protected virtual void DealClicked()
    {
        playerScript.ResetHand();
        dealerScript.ResetHand();
        hideCard.SetActive(true);
        gameText.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(true);
        hitBtn.gameObject.SetActive(true);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        scoreText.text = "Hand: " + playerScript.handValue.ToString();
        dealerScoreText.text = "Dealer Hand: " + dealerScript.handValue.ToString();

        dealBtn.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(false);
        autoBetBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standText.text = "Stand";

        pot = betAmount * 2;
        betsText.text = "Bets: " + betAmount.ToString();
        playerScript.AdjustMoney(-betAmount);
        currencyText.text = "$" + playerScript.GetMoney().ToString();
    }

    protected virtual void BetClicked()
    {
        betAmount += 20;
        betsText.text = "Bets: " + betAmount.ToString();
    }

    protected virtual void AutoBetClicked()
    { 
        if (autoBetSet)
        {
            autoBetSet = !autoBetSet;
            autoBetText.color = Color.white;
            betAmount = 20;
            betsText.text = "Bets: " + betAmount.ToString();
        }
        else
        {
            autoBetSet = !autoBetSet;
            autoBetText.color = Color.black;
        }
    }

    protected void QuitClicked()
    {
        quitBackDrop.SetActive(true);
        quitPopUp.SetActive(true);
    }

    protected void NoClicked()
    {
        quitBackDrop.SetActive(false);
        quitPopUp.SetActive(false);
    }

    protected void YesClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HitDealer()
    {
        while(dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerScoreText.text = "Dealer Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue >= 20)
                RoundOver();
        }
        RoundOver();
    }

    protected virtual void RoundOver()
    {
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool roundOver = true;

        if (playerBust && dealerBust)
        {
            gameText.text = "All Bust: return Bets";
            playerScript.AdjustMoney(pot / 2);
        }
        else if (playerBust || (!dealerBust && playerScript.handValue < dealerScript.handValue))
        {
            gameText.text = "Dealer Wins!";
        }
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            gameText.text = "You Win!";
            playerScript.AdjustMoney(pot);
        }
        else if (playerScript.handValue == dealerScript.handValue)
        {
            gameText.text = "Tie: return Bets";
            playerScript.AdjustMoney(pot / 2);
        }
        else if (!dealerBust && dealerScript.handValue > 16)
        {
            HitDealer();
            RoundOver();
        }
        else
        {
            roundOver = false;
        }

        if (roundOver)
        {
            gameText.gameObject.SetActive(true);
            standBtn.gameObject.SetActive(false);
            hitBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            betBtn.gameObject.SetActive(true);
            autoBetBtn.gameObject.SetActive(true);
            
            currencyText.text = "$" + playerScript.GetMoney().ToString();
            standClicks = 0;
            pot = 0;
            if (!autoBetSet)
            {
                betAmount = 20;
                betsText.text = "Bets: " + betAmount.ToString();
            }
        }
    }
}
