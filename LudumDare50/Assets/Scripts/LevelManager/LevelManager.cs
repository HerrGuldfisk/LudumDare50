using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basics.Helpers
{
    public class LevelManager : MonoBehaviour
    {

        public static void LoadLevel()
        {
            if (SceneManager.sceneCountInBuildSettings - 1 > SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadSceneAsync(0);
            }
        }

        public static void LoadLevel(int index)
        {
            if(index >= 0 && index < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
            }
            else
            {
                Debug.LogWarning($"No scene in build settings with index: {index}");
            }
        }
    }
}

