using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashBar : MonoBehaviour
{
    Image fill;

    private void Awake()
    {
        fill = GetComponent<Image>();
    }

    public void AddDash(float value)
    {
        fill.fillAmount += value;
    }

    public void SetDash(float value)
    {
        fill.fillAmount = value;
    }
}
