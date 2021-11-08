using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{
    public TMP_Text tmpText;

    string GetAmountString(int amount)
    {
        string returnString = "";
        if (amount > 9)
        {
            if (amount > 99)
            {
                returnString = amount.ToString();
            }
            else
            {
                returnString = amount + "  ";
            }
        }
        else
        {
            returnString = amount + "    ";
        }
        return returnString;
    }

    public void SetText(int race1Amount, int race2Amount, int race3Amount, int race4Amount, int race5Amount)
    {
        tmpText.text = "<size=200%><sprite=6></size>" + GetAmountString(race1Amount) + "<sprite=2><size=200%><sprite=7></size>" + GetAmountString(race2Amount) + "<sprite=2><size=200%><sprite=8></size>" + GetAmountString(race3Amount) + "<sprite=2><size=200%><sprite=9></size>" + GetAmountString(race4Amount) + "<sprite=2><size=200%><sprite=10></size>" + GetAmountString(race5Amount);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetText(0, 0, 0, 0, 0);
    }
}
