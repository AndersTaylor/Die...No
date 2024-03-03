using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public Sprite pressedSprite;
    public Sprite defaultSprite;
    private SpriteRenderer sr; 
    private bool isPressed;
    private bool dinoPressed;
    

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    int dinosEnter = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dino"))
        {
            sr.sprite = pressedSprite;
            isPressed = true;
        }

        if (other.CompareTag("Dino"))
        {
            dinoPressed = true;
            dinosEnter++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dino"))
        {
            dinosEnter--;
        }
        
        if (other.CompareTag("Player") && !dinoPressed)
        {
            sr.sprite = defaultSprite; 
            isPressed = false; 
        }

        if (other.CompareTag("Dino") && dinoPressed && dinosEnter <= 0)
        {
            dinoPressed = false;
            sr.sprite = defaultSprite; 
            isPressed = false; 
        }
    }

    public bool GetPressed
    {
        get { return isPressed; }
    }
    public bool GetDinoPressed
    {
        get { return dinoPressed; }
    }

}
