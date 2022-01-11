using UnityEngine;
using TMPro;
using UMA.CharacterSystem;

public class Character : MonoBehaviour
{
    [Header("Overhead Panel")]
    [SerializeField] GameObject overheadPanel;
    [SerializeField] TextMeshPro overheadAttackText;
    [SerializeField] TextMeshPro overheadDefenseText;
    [SerializeField] TextMeshPro overheadMoveText;
    [SerializeField] TextMeshPro overheadRangeText;
    [SerializeField] TextMeshPro overheadSpecialText;

    [Header("Side Panel")]
    [SerializeField] GameObject sidePanel;
    [SerializeField] TextMeshPro sideAttackText;
    [SerializeField] TextMeshPro sideDefenseText;
    [SerializeField] TextMeshPro sideMoveText;
    [SerializeField] TextMeshPro sideRangeText;
    [SerializeField] TextMeshPro sideSpecialText;

    [Header("Spotlight")]
    [SerializeField] GameObject spotLight;

    [Header("Uma")]
    [SerializeField] DynamicCharacterAvatar uma;

    int attackValue;
    int defenseValue;
    int moveValue;
    int rangeValue;
    string specialStringValue;
    Transform myTransform;

    public void SetAttackValue(int at)
    {
        attackValue = at;
    }

    public void SetDefenseValue(int df)
    {
        defenseValue = df;
    }

    public void SetMoveValue(int mv)
    {
        moveValue = mv;
    }

    public void SetRangeValue(int rg)
    {
        rangeValue = rg;
    }

    public void ActivateOverheadPanel()
    {
        overheadPanel.gameObject.SetActive(true);
        sidePanel.gameObject.SetActive(false);
        overheadAttackText.text = attackValue.ToString();
        overheadDefenseText.text = defenseValue.ToString();
        overheadMoveText.text = moveValue.ToString();
        overheadRangeText.text = rangeValue.ToString();
        overheadSpecialText.text = specialStringValue;
    }

    public void ActivateSidePanel()
    {
        overheadPanel.gameObject.SetActive(false);
        sidePanel.gameObject.SetActive(true);
        sideAttackText.text = attackValue.ToString();
        sideDefenseText.text = defenseValue.ToString();
        sideMoveText.text = moveValue.ToString();
        sideRangeText.text = rangeValue.ToString();
        sideSpecialText.text = specialStringValue;
    }

    public void DeactivatePanels()
    {
        overheadPanel.gameObject.SetActive(false);
        sidePanel.gameObject.SetActive(false);
    }

    public void SetShirtColor(Color c)
    {
        uma.SetColor("Outfit", c);
        uma.UpdateColors(true);
    }

    public void SetPantsColor(Color c)
    {
        uma.SetColor("Pants1", c);
        uma.UpdateColors(true);
    }

    public void SetSkinColor(Color c)
    {
        uma.SetColor("Skin", c);
        uma.UpdateColors(true);
    }

    public void Highlight()
    {
        spotLight.SetActive(true);
    }

    public void UnHighlight()
    {
        spotLight.SetActive(false);
    }

    public void SetPostion(Vector3 pos)
    {
        myTransform.localPosition = pos;
    }

    public Vector3 GetPosition()
    {
        return myTransform.localPosition;
    }

    private void Start()
    {
        myTransform = transform;
    }
}
