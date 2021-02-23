using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HandBoardController : MonoBehaviour
{
    public GameObject[] betPanels;

    public Text[] handBoardTexts;

    public Text[] betAreaOne;

    public Text[] betAreaTwo;

    public Text[] betAreaThree;

    public Text[] betAreaFour;

    public Text[] betAreaFive;

    private int handBoardOn = 0;
    private int[] betAreaOn = {0,0};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetHandText(int handIndex)
    {
        handBoardTexts[handIndex].color = Color.white;
        handBoardOn = handIndex;
    }

    public void ResetBoard()
    {
        if (handBoardOn == 0)
            return;
        handBoardTexts[handBoardOn].color = new Color(0,0,0);
        switch (betAreaOn[0])
        {
            case 0: betAreaOne[betAreaOn[1]].color = new Color(0, 0, 0);
                break;
            case 1:
                betAreaTwo[betAreaOn[1]].color = new Color(0, 0, 0);
                break;
            case 2:
                betAreaThree[betAreaOn[1]].color = new Color(0, 0, 0);
                break;
            case 3:
                betAreaFour[betAreaOn[1]].color = new Color(0, 0, 0);
                break;
            case 4:
                betAreaFive[betAreaOn[1]].color = new Color(0, 0, 0);
                break;
        }
        handBoardOn = 0;
        betAreaOn = new int[] { 0, 0 };
    }

    public void UpdateBetPanel(int betIndex)
    {
        betPanels[betIndex].SetActive(true);
        if (betIndex != 0)
            betPanels[betIndex - 1].SetActive(false);
        else
            betPanels[4].SetActive(false);

    }

    public void MaxBetPanel(int betIndex, int prevBetIndex)
    {
        Debug.Log("PrevBetIndex: " + prevBetIndex);
        betPanels[betIndex].SetActive(true);
        betPanels[prevBetIndex - 1].SetActive(false);
    }

    public int SetBetText(int betPanelIndex, int betIndex)
    {
        int winnings = 0;

        switch (betPanelIndex)
        {
            case 0: betAreaOne[betIndex].color = Color.white;
                betAreaOn = new int[] { betPanelIndex, betIndex };
                winnings = Int32.Parse(betAreaOne[betIndex].text);
                break;
            case 1:
                betAreaTwo[betIndex].color = Color.white;
                betAreaOn = new int[] { betPanelIndex, betIndex };
                winnings = Int32.Parse(betAreaTwo[betIndex].text);
                break;
            case 2:
                betAreaThree[betIndex].color = Color.white;
                betAreaOn = new int[] { betPanelIndex, betIndex };
                winnings = Int32.Parse(betAreaThree[betIndex].text);
                break;
            case 3:
                betAreaFour[betIndex].color = Color.white;
                betAreaOn = new int[] { betPanelIndex, betIndex };
                winnings = Int32.Parse(betAreaFour[betIndex].text);
                break;
            case 4:
                betAreaFive[betIndex].color = Color.white;
                betAreaOn = new int[] { betPanelIndex, betIndex };
                winnings = Int32.Parse(betAreaFive[betIndex].text);
                break;
            default:
                break;
        }

        return winnings;
    }
}
