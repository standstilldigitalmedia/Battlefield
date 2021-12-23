using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilePiece : MonoBehaviour
{
    public Image pieceImage;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI valueText;
    [Space]
    [Header("Don't Assign")]
    public Transform myTransform;
    public Color color;

    public int value;
    public int count;
    public int type;

    private void Start()
    {
        //outlineImage.color = transparent;
        //pieceImage.color = transparent;
        //valueText.text = "";
        //countText.text = "";
        myTransform = transform;
    }
}
