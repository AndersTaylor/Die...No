using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public Sprite pressedSprite;
    public Sprite defaultSprite;
    private SpriteRenderer sr; 
    private bool isPressed; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Dino"))
        {
            sr.sprite = pressedSprite;
            isPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Dino"))
        {

            sr.sprite = defaultSprite; 
            isPressed = false; 
        }
    }

    public bool GetPressed
    {
        get { return isPressed; }
    }
}
