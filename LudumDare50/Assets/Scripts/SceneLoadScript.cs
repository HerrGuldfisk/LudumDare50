using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadScript : MonoBehaviour
{
    [SerializeField] string sceneToLoad;

    public void LoadNewScene(){
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReloadCurrentScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
