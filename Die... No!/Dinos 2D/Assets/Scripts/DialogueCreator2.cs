using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueCreater2 : MonoBehaviour
{

    public int currentLine = 0;
    public string[] lines;
    public TextMeshProUGUI textComponent;

    private GameObject dialogueImage;
    private GameObject dinoImage;
    private GameObject character;
    private Rigidbody2D characterRB;


    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        dialogueImage = GameObject.FindWithTag("DialogueImage");
        dinoImage = GameObject.FindWithTag("DinoImage");
        character = GameObject.FindWithTag("Player");

        character.GetComponent<PlayerController>().enabled = false;
        //characterRB = character.GetComponent<Rigidbody2D>();

        ShowNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }


    }

    void ShowNextLine()
    {
        if (currentLine < lines.Length)
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
            dinoImage.SetActive(false);

        }
    }


}
