using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayCollisionCheck : MonoBehaviour
{
    public GameObject oneWayCollision;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(6))
            oneWayCollision.GetComponent<CompositeCollider2D>().isTrigger = false;
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(6))
            oneWayCollision.GetComponent<CompositeCollider2D>().isTrigger = true;
        
    }
}
