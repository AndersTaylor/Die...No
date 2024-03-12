using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public bool pickedUpKey;
    private GameObject sceneTransition;
    private GameObject escMenu;
    
    void Start()
    {
        escMenu = GameObject.FindGameObjectWithTag("EscMenu");
        escMenu.SetActive(false);
        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition");
    }

    //AT Esc Menu
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            escMenu.SetActive(!escMenu.activeSelf);
    }

    public IEnumerator LoadNextScene()
    {
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            GetComponent<AudioSource>().Play();
            sceneTransition.GetComponent<Animator>().SetBool("EndScene", true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Level 1");
        }
        else
        {
            yield return new WaitForSeconds(3);
            GetComponent<AudioSource>().Play();
            sceneTransition.GetComponent<Animator>().SetBool("EndScene", true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    [HideInInspector] public string sceneName;
    public IEnumerator LoadSceneName()
    {
        GetComponent<AudioSource>().Play();
        sceneTransition.GetComponent<Animator>().SetBool("EndScene", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}