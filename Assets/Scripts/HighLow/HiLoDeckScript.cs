using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiLoDeckScript : DeckScript
{
        void Start()
    {
        GetCardValues();
    }

    public override void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < cardObjects.Length; i++)
        {
            num = i;

            num %= 13;

            if (num == 0)
                num = 13;

            cardValues[i] = num++;
        }

        currentIndex = 1;
    }
}
