using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.parent)
                {
                    if (hit.transform.parent.gameObject.GetComponent<Character>())
                    {
                        Debug.Log("Hit Character");
                    }
                    else
                    {
                        Debug.Log("has parent");
                    }
                }
                else
                {
                    if(hit.transform.name == "FieldTile")
                    {
                        Debug.Log("Field Tile");
                    }
                    else
                    {
                        Debug.Log("no parent");
                    }
                    
                }
                
            }
        }
    }
}
