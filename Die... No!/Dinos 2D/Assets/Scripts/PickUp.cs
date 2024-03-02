using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //AT Goes on every key. Sends the value to the GameController to distribute the value to scene listeners.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().pickedUpKey = true;
            gameObject.SetActive(false);
        }
    }
}
