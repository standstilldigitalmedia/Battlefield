using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image outlineImage;
    [SerializeField] Image fillImage;

    int type;
    bool isEnabled = true;
    Transform myTransform;

    //public int type;

    bool pointerDown;
    float pointerDownTimer;
    float requiredHoldTime = 0.2f;

    public void SetType(int v)
    {
        type = v;
    }

    public int GetTileType()
    {
        return type;
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

    public void SetOutlineColor(Color c)
    {
        outlineImage.color = c;
    }

    void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject goPointerDown = BattleBoardController.Instance.FindTilePiece(myTransform.localPosition.x, myTransform.localPosition.y);
        if(goPointerDown)
        {
            TilePiece tpPointerDown = goPointerDown.GetComponent<TilePiece>();
            if(tpPointerDown.GetTileType() == TileTypes.myLayoutTrayTile && tpPointerDown.GetCount() < 1)
            {
                pointerDown = false;
                return;
            }
        }
        if (isEnabled)
        {
            pointerDown = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(pointerDown)
        {
            Reset();
        }        
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime)
            {
                BattleBoardController.Instance.TileClick(gameObject);
                Reset();
            }

            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    void Awake()
    {
        myTransform = transform;
        outlineImage.color = BattleBoardController.Instance.transparent;
    }
}
