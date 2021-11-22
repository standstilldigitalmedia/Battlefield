using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyImage
{
    public Sprite currency0;
    public Sprite currency1;
    public Sprite currency2;
    public Sprite currency3;
    public Sprite currency4;
}

public class CurrencyPanel : MonoBehaviour
{
    [SerializeField] TMP_Text race1Currency;
    [SerializeField] TMP_Text race2Currency;
    [SerializeField] TMP_Text race3Currency;
    [SerializeField] TMP_Text race4Currency;
    [SerializeField] TMP_Text race5Currency;

    [SerializeField] Image race1CurrencyImage;
    [SerializeField] Image race2CurrencyImage;
    [SerializeField] Image race3CurrencyImage;
    [SerializeField] Image race4CurrencyImage;
    [SerializeField] Image race5CurrencyImage;

    Sprite[] currencySprites;

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
        race1Currency.text = GetAmountString(race1Amount);
        race2Currency.text = GetAmountString(race2Amount);
        race3Currency.text = GetAmountString(race3Amount);
        race4Currency.text = GetAmountString(race4Amount);
        race5Currency.text = GetAmountString(race5Amount);

        //tmpText.text = "<size=200%><sprite=6></size>" + GetAmountString(race1Amount) + "<sprite=2><size=200%><sprite=7></size>" + GetAmountString(race2Amount) + "<sprite=2><size=200%><sprite=8></size>" + GetAmountString(race3Amount) + "<sprite=2><size=200%><sprite=9></size>" + GetAmountString(race4Amount) + "<sprite=2><size=200%><sprite=10></size>" + GetAmountString(race5Amount);
    }

    void SetRace1Images()
    {
        race1CurrencyImage.sprite = currencySprites[0];
        race2CurrencyImage.sprite = currencySprites[1];
        race3CurrencyImage.sprite = currencySprites[2];
        race4CurrencyImage.sprite = currencySprites[3];
        race5CurrencyImage.sprite = currencySprites[4];
    }

    void SetRace2Images()
    {
        race1CurrencyImage.sprite = currencySprites[5];
        race2CurrencyImage.sprite = currencySprites[6];
        race3CurrencyImage.sprite = currencySprites[7];
        race4CurrencyImage.sprite = currencySprites[8];
        race5CurrencyImage.sprite = currencySprites[9];
    }

    void SetRace3Images()
    {
        race1CurrencyImage.sprite = currencySprites[10];
        race2CurrencyImage.sprite = currencySprites[11];
        race3CurrencyImage.sprite = currencySprites[12];
        race4CurrencyImage.sprite = currencySprites[13];
        race5CurrencyImage.sprite = currencySprites[14];
    }

    void SetRace4Images()
    {
        race1CurrencyImage.sprite = currencySprites[15];
        race2CurrencyImage.sprite = currencySprites[16];
        race3CurrencyImage.sprite = currencySprites[17];
        race4CurrencyImage.sprite = currencySprites[18];
        race5CurrencyImage.sprite = currencySprites[19];
    }

    void SetRace5Images()
    {
        race1CurrencyImage.sprite = currencySprites[20];
        race2CurrencyImage.sprite = currencySprites[21];
        race3CurrencyImage.sprite = currencySprites[22];
        race4CurrencyImage.sprite = currencySprites[23];
        race5CurrencyImage.sprite = currencySprites[24];
    }

    // Start is called before the first frame update
    void Start()
    {
        SetText(1, 22, 333, 4, 56);
        currencySprites = Resources.LoadAll<Sprite>("currencySprite");
        SetRace4Images();
    }
}
