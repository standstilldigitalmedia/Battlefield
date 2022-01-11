using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    [SerializeField] GameObject clickableTile;
    [SerializeField] GameObject center;
    [SerializeField] GameObject spotLight;
    [SerializeField] Material yellowMat;
    [SerializeField] Material whiteMat;
    [SerializeField] Material greenMat;
    Transform myTransform;

    public void MakeTileGreen()
    {
        clickableTile.GetComponent<Renderer>().material = greenMat;
    }

    public void MakeTileWhite()
    {
        clickableTile.GetComponent<Renderer>().material = whiteMat;
    }

    public void MakeTileYellow()
    {
        clickableTile.GetComponent<Renderer>().material = yellowMat;
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
        MakeTileWhite();
        UnHighlight();
    }
}
