using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayCollisionCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OneWayCollision"))
            other.gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer.Equals(6))
            other.gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
        
    }
}
