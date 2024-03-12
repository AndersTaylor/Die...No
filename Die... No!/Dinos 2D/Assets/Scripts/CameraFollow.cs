using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class CameraFollow : MonoBehaviour
{
    public float dampTime = 0.15f;
    
    private Transform target;
    private Vector3 velocity = Vector3.zero;
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update () 
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
