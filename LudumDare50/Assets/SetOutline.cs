using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutline : MonoBehaviour
{
    [SerializeField] Sprite normal;
    [SerializeField] Sprite outlined;

    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleOutline(bool outlineOn)
    {
        if (outlineOn) spriteRenderer.sprite = outlined;
        else spriteRenderer.sprite = normal;
    }
}
