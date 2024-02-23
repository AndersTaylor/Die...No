using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueCreater : MonoBehaviour
{

    public int currentLine = 0;
    public string[] lines;
    public TextMeshProUGUI textComponent;
    public Vector2 newPosition;

    private GameObject dialogueImage;


    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        dialogueImage = GameObject.FindWithTag("DialogueImage");

        //RectTransform textRectTransform = textComponent.rectTransform;

        //textRectTransform.anchoredPosition = newPosition;

        ShowNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
            textComponent.text = "";
            Destroy(dialogueImage);

        }
    }

}
