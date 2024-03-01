using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueCreater2 : MonoBehaviour
{

    public int currentLine = 0;
    public string[] lines;
    public TextMeshProUGUI textComponent;
    public GameObject button;

    private GameObject dialogueImage;
    private GameObject dinoImage;
    private GameObject character;
    private GameObject dino;
    private Rigidbody2D characterRB;
    private Collider2D buttonCollider;
    private bool buttonClicked;
    private ButtonBehavior buttonScript;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        dialogueImage = GameObject.FindWithTag("DialogueImage");
        dinoImage = GameObject.FindWithTag("DinoImage");
        character = GameObject.FindWithTag("Player");
        dino = GameObject.FindWithTag("Dino");
        buttonScript = button.GetComponent<ButtonBehavior>();
        playerController = character.GetComponent<PlayerController>();
        
        playerController.Still();
        playerController.enabled = false;

        characterRB = character.GetComponent<Rigidbody2D>();

        ShowNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null)
        {
            character = GameObject.FindWithTag("Player");
            playerController = character.GetComponent<PlayerController>();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (buttonClicked)
            {
                ShowNextLineEnd();
            }
            else
            {
                ShowNextLine();
            }
        }

        if (!buttonClicked && buttonScript.GetPressed && buttonScript.GetDinoPressed)
        {
            buttonClicked = true;
            ShowNextLineEnd();
            dialogueImage.SetActive(true);
            dinoImage.SetActive(true);
            
            playerController.Still();
            playerController.enabled = false;
            characterRB.velocity = Vector2.zero;
            
            dino.GetComponent<DinoFollower>().Stay();
            dino.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void ShowNextLine()
    {
        if (currentLine < 6)
        {
            textComponent.text = lines[currentLine];
            currentLine++;
        }

        else
        {
            playerController.enabled = true;
            textComponent.text = "";
            dialogueImage.SetActive(false);
            dinoImage.SetActive(false);

        }
    }

    void ShowNextLineEnd()
    {
        if (currentLine < lines.Length)
        {
            textComponent.text = lines[currentLine];
            currentLine++;
        }
        else
        {
            textComponent.text = "";
            dialogueImage.SetActive(false);
            playerController.enabled = true;
            dinoImage.SetActive(false);
        }
    }
}


