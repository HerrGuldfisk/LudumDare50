using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basics.Helpers;

public class StartGameButton : MonoBehaviour
{
    public void OnClick()
    {
        LevelManager.LoadLevel(1);
    }
}
