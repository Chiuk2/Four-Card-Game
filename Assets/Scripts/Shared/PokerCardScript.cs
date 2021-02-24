using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerCardScript : CardScript
{
    public override void ResetCard()
    {
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
        value = 0;
    }

    public override void SetCard(GameObject newCard)
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = newCard.GetComponent<Renderer>().sharedMaterial;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 180));
    }
}
