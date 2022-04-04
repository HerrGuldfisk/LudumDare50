using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basics.Tweening;
using Basics.Helpers;

public class StartController : MonoBehaviour
{
    public RectTransform mask;
    public Fireplace firePlace;

    private int clickCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }

    public void OnClick()
    {
        if(clickCounter == 0)
        {
            clickCounter++;
            // firePlace.shouldBurn = true;
            Destroy(gameObject, 2.5f);
            LeanTween.size(mask, new Vector2(1500, 1500), 2f).setEaseInCubic();
        }
    }
}
