using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image outlineImage;
    public Image fillImage;

    [Space] 
    [Header("Don't Assign")]
    public bool isEnabled = true;
    public Transform myTransform;
    public int type;

    bool pointerDown;
    float pointerDownTimer;
    float requiredHoldTime = 0.5f;

    void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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

    void Start()
    {
        myTransform = transform;
        outlineImage.color = BattleBoardController.Instance.transparent;
    }
}
