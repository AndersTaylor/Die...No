using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public bool pickedUpKey;
    private GameObject sceneTransition;
    
    void Start()
    {
        sceneTransition = GameObject.FindGameObjectWithTag("SceneTransition");
    }
    
    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3);
        sceneTransition.GetComponent<Animator>().SetBool("EndScene", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
