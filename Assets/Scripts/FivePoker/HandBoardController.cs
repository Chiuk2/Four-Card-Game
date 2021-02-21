using System.Collections;
using System.Collections.Generic;
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
    private int betAreaOn = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetHandText(int handIndex)
    {
        handBoardTexts[handIndex - 1].color = Color.white;
        handBoardOn = handIndex;
    }

    public void ResetBoard()
    {
        if (handBoardOn == 0)
            return;
        handBoardTexts[handBoardOn - 1].color = new Color(0,0,0);
        handBoardOn = 0;
    }
}
