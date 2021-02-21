using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPokerPlayerScript : HiLoPlayerScript
{
    public override void StartHand()
    {
        GetCard();
        GetCard();
        GetCard();
        GetCard();
        GetCard();
    }
}
