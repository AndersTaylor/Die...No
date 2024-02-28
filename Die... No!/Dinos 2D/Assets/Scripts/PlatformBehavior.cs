using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public GameObject button;

    private float moveSpeed = 3f;
    private ButtonBehavior buttonScript;
    private bool moving;
    public Vector3 end;
    public Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
       buttonScript = button.GetComponent<ButtonBehavior>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonScript.GetPressed && moving == false)
        {
            MovePlatform();
        }
        if (!buttonScript.GetPressed && moving == false)
        {
            ReturnPlatform();
        }
        else
        {
            moving = false;
        }
    }

    void MovePlatform()
    {
        moving = true;
        transform.position = Vector3.MoveTowards(transform.position, end, moveSpeed * Time.deltaTime);

    }

    void ReturnPlatform()
    {
        moving = true;
        transform.position = Vector3.MoveTowards(transform.position, start, moveSpeed * Time.deltaTime);

    }
}
