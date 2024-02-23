using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueCreater : MonoBehaviour
{

    public int currentLine = 0;
    public string[] lines;
    public TextMeshProUGUI textComponent;
    public int currentLineEnd = 8;

    
    private GameObject dialogueImage;
    private GameObject mushImage;
    private GameObject guyImage;
    private GameObject character;
    private bool guyDiscovered;
    private Rigidbody2D characterRB;


    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        dialogueImage = GameObject.FindWithTag("DialogueImage");
        mushImage = GameObject.FindWithTag("MushImage");
        guyImage = GameObject.FindWithTag("GuyImage");
        character = GameObject.FindWithTag("Player");

        character.GetComponent<PlayerController>().enabled = false;
        characterRB = character.GetComponent<Rigidbody2D>();
        guyImage.SetActive(false);

        ShowNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (guyDiscovered)
            {
                ShowNextLineEnd();
            }
            else
            {
                ShowNextLine();
            }
        }
        if (!guyDiscovered && character.transform.position.y >= 5.057f && character.transform.position.x >= 43f)
        {
            guyDiscovered = true;
            ShowNextLineEnd();
            dialogueImage.SetActive(true);
            character.GetComponent<PlayerController>().enabled = false;
            characterRB.velocity = Vector2.zero;
            guyImage.SetActive(true);
            mushImage.SetActive(false);
            //characterRB.angularVelocity = Vector2.zero;
        }


    }
    void ShowNextLine()
    {
        if (currentLine < 9)
        {
            // Display the current line and increment the index
            textComponent.text = lines[currentLine];
            currentLine++;
        }

        else
        {
            // If there are no more lines, clear the text
            character.GetComponent<PlayerController>().enabled = true;
            textComponent.text = "";
            dialogueImage.SetActive(false);
            mushImage.SetActive(false);

        }
    }

    void ShowNextLineEnd()
    {
        if (currentLineEnd < lines.Length)
        {
            textComponent.text = lines[currentLineEnd];
            currentLineEnd++;
        }
        else
        {
            textComponent.text = "";
            dialogueImage.SetActive(false);
            character.GetComponent<PlayerController>().enabled = true;
            guyImage.SetActive(false);
        }
    }

}
