using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateOnStart : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);   
    }
}
