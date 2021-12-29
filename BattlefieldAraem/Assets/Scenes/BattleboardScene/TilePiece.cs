using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TilePiece : TileButton
{
    [SerializeField] Image pieceImage;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] TextMeshProUGUI rangeText;
    [SerializeField] TextMeshProUGUI specialText;

    public void SetTilePieceImage(Image img)
    {
        pieceImage.sprite = img.sprite;
    }

    public void SetTileAttackText(int value)
    {
        attackText.text = value.ToString();
    }

    public void SetTileDefenseText(int value)
    {
        defenseText.text = value.ToString();
    }

    public void SetTileMoveText(int value)
    {
        moveText.text = value.ToString();
    }

    public void SetTileRangeText(int value)
    {
        rangeText.text = value.ToString();
    }

    public void SetTileSpecialText(string txt)
    {
        specialText.text = txt;
    }

}
