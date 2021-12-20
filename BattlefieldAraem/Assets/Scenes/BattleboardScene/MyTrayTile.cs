using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MyTrayTile : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] TextMeshProUGUI countText;

    public int value;
    public int count;

    Image pieceImage;

    private void Start()
    {
        pieceImage = GetComponent<Image>();
    }
}
