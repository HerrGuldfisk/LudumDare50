using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeBar : MonoBehaviour
{
    Image fill;

    private void Awake()
    {
        fill = GetComponent<Image>();
    }

    public void AddLife(float value)
    {
        fill.fillAmount += value;
    }

    public void SetLife(float value)
    {
        fill.fillAmount = value;
    }
}
