using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBoardController : MonoBehaviour
{
    [SerializeField] bool isLayoutBoard;
    [SerializeField] Image battleboardImage;
    [SerializeField] Transform pieceTileLayerTransform;
    [SerializeField] Transform buttonTileLayerTransform;
    [SerializeField] GameObject tilePiecePrefab;
    [SerializeField] GameObject tileButtonPrefab;
    [Space]
    [Header("Don't Assign")]
    public Color transparent = new Color(0, 0, 0, 0);
    public Vector3 reusableVector = new Vector3(0, 0, 0);
    public static BattleBoardController Instance { get; private set; }

    List<GameObject> tileButtonList = new List<GameObject>();
    List<GameObject> tilePieceList = new List<GameObject>();

    public TileButton tbSelected;
    public TilePiece tpSelected;

    void ResetSelected()
    {
        ClearAllOutlines();
        tbSelected = null;
        tpSelected = null;
    }

    GameObject FindTileButton(float x, float y)
    {
        foreach (GameObject tile in tileButtonList)
        {
            if (tile.GetComponent<TileButton>())
            {
                Vector3 position = tile.GetComponent<TileButton>().GetPosition();
                if (x == position.x && y == position.y)
                {
                    return tile;
                }
            }
        }
        return null;
    }

    public GameObject FindTilePiece(float x, float y)
    {
        foreach (GameObject tile in tilePieceList)
        {
            if (tile.GetComponent<TilePiece>())
            {
                Vector3 position = tile.GetComponent<TilePiece>().GetPosition();
                if (x == position.x && y == position.y)
                {
                    return tile;
                }
            }
        }
        return null;
    }

    GameObject FindTrayTileByValue(int value)
    {
        foreach (GameObject tile in tilePieceList)
        {
            TilePiece tp = tile.GetComponent<TilePiece>();
            if (tp.GetTileType() == TileTypes.myLayoutTrayTile && tp.GetValue() == value)
            {
                return tile;
            }
        }
        return null;
    }

    void ClearAllOutlines()
    {
        foreach (GameObject tile in tileButtonList)
        {
            tile.GetComponent<TileButton>().SetOutlineColor(transparent);
        }
    }

    TileButton InstantiateTileButton(Vector3 tv, int type)
    {
        GameObject tile = Instantiate(tileButtonPrefab, buttonTileLayerTransform) as GameObject;
        TileButton tb = tile.GetComponent<TileButton>();
        tb.SetPosition(tv);
        tb.SetType(type);
        tileButtonList.Add(tile);
        return tile.GetComponent<TileButton>();
    }

    TilePiece InstantiateTilePiece(Vector3 tv, int count, int value, Color color, int type)
    {
        GameObject tile = Instantiate(tilePiecePrefab, pieceTileLayerTransform) as GameObject;
        TilePiece tp = tile.GetComponent<TilePiece>();
        tp.SetPosition(tv);
        tp.SetCount(count);
        tp.SetValue(value);
        tp.SetColor(color);
        tp.SetType(type);
        tilePieceList.Add(tile);
        return tp;
    }

    void LayoutInstantiateMyFieldTiles()
    {
        reusableVector.x = -462.7f;
        reusableVector.y = -153.2f;

        for (int a = 0; a < 4; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                InstantiateTileButton(reusableVector, TileTypes.myLayoutFieldTile);
                reusableVector.x = reusableVector.x + 102.9f;
            }
            reusableVector.x = -462.7f;
            reusableVector.y = reusableVector.y - 102f;
        }
    }

    void LayoutInstantiateMyTrayTiles(Color color)
    {
        reusableVector.x = -462.7f;
        reusableVector.y = -561.2f;
        int type = TileTypes.myLayoutTrayTile;

        int[] valueArray = { 11, 12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] countArray = { 6, 1, 1, 1, 2, 3, 4, 4, 4, 5, 8, 1 };

        InstantiateTileButton(reusableVector, type);
        InstantiateTilePiece(reusableVector, countArray[0], valueArray[0], color, type);
        reusableVector.x = 463.4f;
        InstantiateTileButton(reusableVector, type);
        InstantiateTilePiece(reusableVector, countArray[1], valueArray[1], color, type);
        reusableVector.x = -462.7f;
        reusableVector.y = reusableVector.y - 102f;

        for (int a = 2; a < 12; a++)
        {
            InstantiateTileButton(reusableVector, type);
            InstantiateTilePiece(reusableVector, countArray[a], valueArray[a], color, type);
            reusableVector.x = reusableVector.x + 102.9f;
        }
    }

    void LayoutAddRemoveTrayTile(int value, bool add)
    {
        GameObject goTrayTile = FindTrayTileByValue(value);

        if (goTrayTile)
        {
            //remove one tile from the tray
            //find the tile to remove
            TilePiece tpTrayTile = goTrayTile.GetComponent<TilePiece>();

            if (add)
            {
                //add one to the tray tile's count
                tpTrayTile.AddToCount();
            }
            else
            {
                //subtract one from the tray tile's count
                tpTrayTile.TakeFromCount();
            }
        }
    }

    int LayoutAddRemoveFieldTile(TileButton tbTarget, Color color, int value)
    {
        int existingTileValue = 0;
        GameObject goTarget = FindTilePiece(tbTarget.GetPosition().x, tbTarget.GetPosition().y);
        if (goTarget)
        {
            TilePiece tpTarget = goTarget.GetComponent<TilePiece>();
            //if value == 0, destroy the piece on this field tile
            if (value == 0)
            {
                if (tpTarget)
                {
                    Destroy(tpTarget.gameObject);
                }
            }
            else
            {
                //if there is a piece on this tile, assign it's value to the return variable existingTileValue and then
                //make the target tile the value that was passed in
                if (tpTarget)
                {
                    Debug.Log("tp target");
                    existingTileValue = tpTarget.GetValue();
                    tpTarget.SetValue(value);
                }
                //if there is no piece on this tile, spawn a new one
                else
                {
                    Debug.Log("we should't be able to get here");
                }
            }
        }
        else
        {
            InstantiateTilePiece(tbTarget.GetPosition(), 0, value, color, TileTypes.myLayoutFieldTile);
            //SpawnTile(tbTarget.myTransform.localPosition.x, tbTarget.myTransform.localPosition.y, TileTypes.myLayoutFieldTile, color, true);
            //SetPieceTile()
        }
        return existingTileValue;
    }

    void LayoutProcessTileClick(TileButton tbClicked, TilePiece tpClicked)
    {
        int typeSelected;
        int typeClicked;
        if (tpSelected)
        {
            typeSelected = tpSelected.GetTileType();
        }
        else
        {
            typeSelected = tbSelected.GetTileType();
        }

        if (tpClicked)
        {
            typeClicked = tpClicked.GetTileType();
        }
        else
        {
            typeClicked = tbClicked.GetTileType();
        }

        switch (typeSelected)
        {
            case TileTypes.myLayoutTrayTile:
                switch (typeClicked)
                {
                    case TileTypes.myLayoutTrayTile:
                        ClearAllOutlines();
                        break;
                    case TileTypes.myLayoutFieldTile:
                        LayoutAddRemoveTrayTile(tpSelected.GetValue(), false);
                        int fieldTileValue = LayoutAddRemoveFieldTile(tbClicked, tpSelected.GetColor(), tpSelected.GetValue());
                        if (fieldTileValue > 0)
                        {
                            LayoutAddRemoveTrayTile(fieldTileValue, true);
                        }
                        break;
                }
                break;
            case TileTypes.myLayoutFieldTile:
                switch (typeClicked)
                {
                    case TileTypes.myLayoutTrayTile:
                        LayoutAddRemoveTrayTile(tpSelected.GetValue(), true);
                        if (tpSelected)
                        {
                            tilePieceList.Remove(tpSelected.gameObject);
                            Destroy(tpSelected.gameObject);
                        }
                        break;
                    case TileTypes.myLayoutFieldTile:
                        int tempValue;
                        if (tpSelected)
                        {
                            tempValue = tpSelected.GetValue();
                            if (tpClicked)
                            {
                                tpSelected.SetValue(tpClicked.GetValue());
                                tpClicked.SetValue(tempValue);
                            }
                            else
                            {
                                tpSelected.SetPosition(tbClicked.GetPosition());
                            }
                        }
                        else if (tpClicked)
                        {
                            tempValue = tpClicked.GetValue();
                            if (tpSelected)
                            {
                                tpClicked.SetValue(tpSelected.GetValue());
                                tpSelected.SetValue(tempValue);
                            }
                            else
                            {
                                tpClicked.SetPosition(tbSelected.GetPosition());
                            }
                        }
                        else
                        {
                            ResetSelected();
                        }
                        //LayoutSwapFieldtileForFieldTile(tbClicked, tpClicked);
                        break;
                }
                break;
        }
        ResetSelected();
    }

    public void TileClick(GameObject goClicked)
    {
        TileButton tbClicked = goClicked.GetComponent<TileButton>();
        Vector3 vectorClicked = tbClicked.GetPosition();
        GameObject goClickedPiece = FindTilePiece(vectorClicked.x, vectorClicked.y);

        //if a tile has been selected
        if (tbSelected)
        {
            TilePiece tpClicked;
            if (goClickedPiece)
            {
                tpClicked = goClickedPiece.GetComponent<TilePiece>();
            }
            else
            {
                tpClicked = null;
            }
            Vector3 tfSelected = tbSelected.GetPosition();
            GameObject goSelectedPiece = FindTilePiece(tfSelected.x, tfSelected.y);

            if (isLayoutBoard)
            {
                LayoutProcessTileClick(tbClicked, tpClicked);
            }
            else
            {

            }
            ClearAllOutlines();
        }
        //else assign clicked tile to selected tile
        else
        {
            tbSelected = tbClicked;
            if (goClickedPiece)
            {
                tpSelected = goClickedPiece.GetComponent<TilePiece>();
            }
            else
            {
                tpSelected = null;
            }
            tbSelected.SetOutlineColor(Color.green);
        }
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

    private void Start()
    {
        if (isLayoutBoard)
        {
            LayoutInstantiateMyTrayTiles(Color.red);
            LayoutInstantiateMyFieldTiles();
        }
        else
        {

        }
    }
}
