using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public int value = 0;

    public int GetValueOfCard()
    {
        return value;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public string GetObjectName()
    {
        return GetComponent<MeshRenderer>().material.name;
    }

    public void SetCard(GameObject newCard)
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = newCard.GetComponent<Renderer>().sharedMaterial;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 180));
    }

    public void ResetCard()
    {
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));
        value = 0;
    }
}
