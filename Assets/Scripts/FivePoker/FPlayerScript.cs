using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPlayerScript : SPPlayerScript
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
