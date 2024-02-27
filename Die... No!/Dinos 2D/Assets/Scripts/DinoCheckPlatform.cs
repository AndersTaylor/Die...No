using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoCheckPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OneWayCollision"))
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("OneWayCollision"))
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
