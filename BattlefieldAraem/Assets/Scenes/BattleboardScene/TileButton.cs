using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image outlineImage;
    [SerializeField] Image fillImage;
    [SerializeField] bool isEnabled;
    Color transparent = new Color(0, 0, 0, 0);
    Vector3 reusableVector = new Vector3(0, 0, 0);

    int tileType;
    int tileID;
    Transform myTransform;

    //public int type;

    bool pointerDown;
    float pointerDownTimer;
    float requiredHoldTime = 0.4f;

    public void SetTileID(int id)
    {
        tileID = id;
    }

    public int GetTileId()
    {
        return tileID;
    }

    public void ResetTile()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = 0;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(isEnabled)
        {
            pointerDown = true;
        }        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerDown)
        {
            ResetTile();
        }
    }

    public void SetTileType(int v)
    {
        tileType = v;
    }

    public int GetTileType()
    {
        return tileType;
    }

    public void SetTilePosition(float x, float y)
    {
        reusableVector.x = x;
        reusableVector.y = y;
        myTransform.localPosition = reusableVector;
    }

    public void SetTilePosition(Vector3 v)
    {
        myTransform.localPosition = v;
    }

    public Vector3 GetTilePosition()
    {
        return myTransform.localPosition;
    }

    public void SetTileOutlineColor(Color c)
    {
        outlineImage.color = c;
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                //LayoutBoardController.Instance.TileClick(gameObject);
                ResetTile();
            }

            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    void Awake()
    {
        myTransform = transform;
        outlineImage.color = transparent;
    }
}
