using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battleboard : MonoBehaviour
{
    public static Battleboard Instance { get; private set; }
    [SerializeField] Image battleboardImage;
    [SerializeField] Transform battleboardPanelTransform;
    [SerializeField] GameObject baseTilePrefab;
    
    Vector3 tileVector = new Vector3(0, 0, 0);
    void Start()
    {
        float x = -260f;
        float y = 375f;
       
        for (int a = 0; a < 14; a++)
        {
            for(int b = 0; b < 10; b++)
            {
                tileVector.x = x;
                tileVector.y = y;
                GameObject tile = Instantiate(baseTilePrefab, battleboardPanelTransform) as GameObject;
                tile.transform.localPosition = tileVector;
                x = x + 57.8f;
            }
            x = -260f;
            y = y - 57.7f;
        }

        /*x = -260f;
        y = 375f;

        for (int c = 0; c < 10; c++)
        {
            for (int d = 0; d < 14; d++)
            {
                Vector3 tileVector = new Vector3(x, y, 0);
                GameObject tile = Instantiate(pieceTilePrefab, battleboardPanelTransform) as GameObject;
                tile.transform.localPosition = new Vector3(x, y, 0);
                tile.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                tile.GetComponent<PieceTile>().tileText.text = "";
                y = y - 57.7f;
            }
            y = 375f;
            x = x + 57.8f;
        }*/
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
