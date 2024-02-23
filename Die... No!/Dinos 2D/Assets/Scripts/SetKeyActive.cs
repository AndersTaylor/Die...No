using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKeyActive : MonoBehaviour
{
    private bool guyDiscovered = false;
    private GameObject character;
    private SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!guyDiscovered && character.transform.position.y >= 5.057f && character.transform.position.x >= 43f)
        {
            guyDiscovered = true;
            sr.enabled = true;
        }

    }
}
