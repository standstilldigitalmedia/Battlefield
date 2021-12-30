using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Header("Uma")]
    [SerializeField] GameObject uma;

    int attackValue;
    int defenseValue;
    int moveValue;
    int rangeValue;
    string specialStringValue;

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
}
