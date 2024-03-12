using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KeyFollow : MonoBehaviour
{
    public float speed = 1;

    private PickUp pickUpScript;

    void Start()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            pickUpScript = parentTransform.GetComponent<PickUp>();
        }
    }
}