using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public GameObject button;

    private float moveSpeed = 3f;
    private ButtonBehavior buttonScript;
    private bool moving;
    private Vector3 end = new Vector3(0.947f, -4, 0);
    private Vector3 start = new Vector3(0.947f, 2, 0);

    // Start is called before the first frame update
    void Start()
    {
       buttonScript = button.GetComponent<ButtonBehavior>();
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
