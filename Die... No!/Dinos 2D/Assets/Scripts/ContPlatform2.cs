using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConPlatform : MonoBehaviour
{
    public GameObject button;
    public GameObject button2;

    private float moveSpeed = 3f;
    private ButtonBehavior buttonScript;
    private ButtonBehavior buttonScript2;
    private bool moving;
    public Vector3 end;
    public Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        buttonScript = button.GetComponent<ButtonBehavior>();
        buttonScript2 = button2.GetComponent<ButtonBehavior>();

        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonScript.GetPressed && buttonScript2.GetPressed && moving == false)
        {
            MovePlatform();
            if (transform.position == end)
            {
                Debug.Log("end");
                ReturnPlatform();
            }
            if (transform.position == start)
            {
                MovePlatform();
            }
        }
        if (!buttonScript.GetPressed && moving == false)
        {
            ReturnPlatform();
        }
        if (!buttonScript2.GetPressed && moving == false)
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
