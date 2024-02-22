using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //AT attach this script to your empty parent object storing the different sprites on different layers
    // (i.e. 3 tiled sprites for the fore, mid, and backgrounds)
    
    private float length;
    private SpriteRenderer[] childSprites;
    private float startPos;
    private new GameObject camera;
    
    public float parallaxEffect; //AT set to a number between 0 and 1. 1 = no parallax. >1 makes the backgrounds move opposite direction

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        startPos = transform.position.x;
        childSprites = GetComponentsInChildren<SpriteRenderer>(); //AT get all the sprites in the children of the attached empty game object
    }

    void Update()
    {
        SetSpritesPos();
    }

    void SetSpritesPos()
    {
        for (int i = 0; i < childSprites.Length; i++)
        {
            length = childSprites[i].bounds.size.x;
            
            float temp = (camera.transform.position.x * (1 - parallaxEffect));
            float dist = (camera.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            if (temp > startPos + length)
                startPos += length;
            else if (temp < startPos - length)
                startPos -= length;
        }
    }
}
