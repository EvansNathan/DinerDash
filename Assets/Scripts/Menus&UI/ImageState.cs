using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageState : MonoBehaviour
{
    public bool isActive = false;
    public Image self;
    public Sprite happyImage;
    public Sprite transImg;

    public float happinessModifier;

    public void Activate()
    {
        self.sprite = happyImage;
        isActive = true;
        self.fillAmount = 1;
        happinessModifier = 1;
    }

    public void Deactivate()
    {
        self.sprite = transImg;
        isActive = false;
    }
}
