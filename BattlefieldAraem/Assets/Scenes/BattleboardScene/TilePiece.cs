using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TilePiece : MonoBehaviour
{
    [SerializeField] Image pieceImage;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI valueText;
    Transform myTransform;
    Color color;
    Color disabledColor;

    int value;
    int count;
    int type;

    public void AddToCount()
    {
        count++;
        countText.text = count.ToString();
        pieceImage.color = color;
    }

    public void TakeFromCount()
    {
        count--;
        if(count > 0)
        {
            countText.text = count.ToString();
            pieceImage.color = color;
        }
        else
        {
            countText.text = "";
            pieceImage.color = disabledColor;
        }
    }

    public void SetCount(int v)
    {
        count = v;
        if(v > 0)
        {
            countText.text = v.ToString();
            pieceImage.color = color;
        }
        else
        {
            countText.text = "";
            pieceImage.color = disabledColor;
        }
    }

    public int GetCount()
    {
        return count;
    }

    public void SetValue(int v)
    {
        if(v == 0)
        {
            if(gameObject)
            {
                Destroy(gameObject);
                return;
            }
        }
        value = v;
        string tv;
        if(v > 0)
        {
            if (v == 10)
            {
                tv = "S";
            }
            else if (v == 11)
            {
                tv = "B";
            }
            else if (v == 12)
            {
                tv = "F";
            }
            else
            {
                tv = v.ToString();
            }
            valueText.text = tv;
        }
        else
        {
            valueText.text = "";
        }
        
    }

    public int GetValue()
    {
        return value;
    }

    public void SetType(int v)
    {
        type = v;
    }

    public int GetTileType()
    {
        return type;
    }

    public void SetColor(Color c)
    {
        color = c;
        disabledColor = new Color(c.r, c.g, c.b, 0.5f);
        pieceImage.color = color;
    }

    public Color GetColor()
    {
        return color;
    }

    public void SetPosition(float x, float y)
    {
        BattleBoardController.Instance.reusableVector.x = x;
        BattleBoardController.Instance.reusableVector.y = y;
        myTransform.localPosition = BattleBoardController.Instance.reusableVector;
    }

    public void SetPosition(Vector3 v)
    {
        myTransform.localPosition = v;
    }

    public Vector3 GetPosition()
    {
        return myTransform.localPosition;
    }

    public void EnableTile()
    {
        pieceImage.color = color;
    }

    public void DisableTile()
    {
        pieceImage.color = disabledColor;
    }

    private void Awake()
    {
        myTransform = transform;
    }
}
