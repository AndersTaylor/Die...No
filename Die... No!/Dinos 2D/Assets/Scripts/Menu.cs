using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 
    public void LoadScene()
    { 
        GameObject gc = GameObject.FindGameObjectWithTag("GameController"); 
        gc.GetComponent<GameController>().StartCoroutine(nameof(GameController.LoadNextScene)); 
    }

    public static void LoadSceneName(string scene)
    {
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        gc.GetComponent<GameController>().sceneName = scene;
        gc.GetComponent<GameController>().StartCoroutine(nameof(GameController.LoadSceneName)); 
    }
}