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

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        dialogueImage = GameObject.FindWithTag("DialogueImage");
        dinoImage = GameObject.FindWithTag("DinoImage");
        character = GameObject.FindWithTag("Player");
        dino = GameObject.FindWithTag("Dino");
        buttonScript = button.GetComponent<ButtonBehavior>();

        character.GetComponent<PlayerController>().enabled = false;

        characterRB = character.GetComponent<Rigidbody2D>();

        ShowNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null)
        {
            character = GameObject.FindWithTag("Player");
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
            character.GetComponent<PlayerController>().enabled = false;
            characterRB.velocity = Vector2.zero;
            dino.GetComponent<DinoFollower>().Stay();
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Dino"))
    //    {
    //        buttonClicked = true;
    //        ShowNextLineEnd();
    //        dialogueImage.SetActive(true);
    //        character.GetComponent<PlayerController>().enabled = false;
    //        characterRB.velocity = Vector2.zero;
    //        dinoImage.SetActive(true);
    //        dino.GetComponent<DinoFollower>().enabled = false;
    //    }
    //}

    void ShowNextLine()
    {
        if (currentLine < 6)
        {
            textComponent.text = lines[currentLine];
            currentLine++;
        }

        else
        {
            character.GetComponent<PlayerController>().enabled = true;
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
            character.GetComponent<PlayerController>().enabled = true;
            dinoImage.SetActive(false);
        }
    }
}


