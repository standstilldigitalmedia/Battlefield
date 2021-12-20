using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseTile : MonoBehaviour
{
    Image outlineImage;
    Color transparent = new Color(0, 0, 0, 0);

    private void Start()
    {
        outlineImage = GetComponent<Image>();
    }

    public void HideTile()
    {
        outlineImage.color = transparent;
    }
}
