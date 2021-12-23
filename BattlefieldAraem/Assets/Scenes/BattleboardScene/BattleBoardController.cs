using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBoardController : MonoBehaviour
{
    public bool isLayoutBoard;
    public Image battleboardImage;
    public Transform pieceTileLayerTransform;
    public Transform buttonTileLayerTransform;
    public GameObject tilePiecePrefab;
    public GameObject tileButtonPrefab;
    [Space]
    [Header("Don't Assign")]
    public Color transparent = new Color(0, 0, 0, 0);
    public static BattleBoardController Instance { get; private set; }

    List<GameObject> tileButtonList = new List<GameObject>();
    List<GameObject> tilePieceList = new List<GameObject>();
    Vector3 tileVector = new Vector3(0, 0, 0);
    public TileButton tbSelected;
    public TilePiece tpSelected;

    GameObject FindTileButton(float x, float y)
    {
        foreach (GameObject tile in tileButtonList)
        {
            if(tile.GetComponent<TileButton>())
            {
                Transform tbTransform = tile.GetComponent<TileButton>().myTransform;
                if(x == tbTransform.localPosition.x && y == tbTransform.localPosition.y)
                {
                    return tile;
                }
            }
        }
        return null;
    }

    GameObject FindTilePiece(float x, float y)
    {
        foreach (GameObject tile in tilePieceList)
        {
            if (tile.GetComponent<TilePiece>())
            {
                Transform tbTransform = tile.GetComponent<TilePiece>().myTransform;
                if (x == tbTransform.localPosition.x && y == tbTransform.localPosition.y)
                {
                    return tile;
                }
            }
        }
        return null;
    }

    GameObject FindTrayTileByValue(int value)
    {
        foreach(GameObject tile in tilePieceList)
        {
            TilePiece tp = tile.GetComponent<TilePiece>();
            if(tp.type == TileTypes.myLayoutTrayTile && tp.value == value)
            {
                return tile;
            }
        }
        return null;
    }

    void ClearAllOutlines()
    {
        foreach(GameObject tile in tileButtonList)
        {
            tile.GetComponent<TileButton>().outlineImage.color = transparent;
        }
    }

    void SetPieceTile(GameObject pt, int count, int v, bool show = true)
    {
        TilePiece pieceTile = pt.GetComponent<TilePiece>();
        if (show)
        {
            string value = v.ToString();
            if (v == 10)
            {
                value = "S";
            }
            else if (v == 11)
            {
                value = "B";
            }
            else if (v == 12)
            {
                value = "F";
            }
            pieceTile.countText.text = count.ToString();
            pieceTile.valueText.text = value.ToString();
        }
        pieceTile.count = count;
        pieceTile.value = v;
    }

    void SpawnTile(float x, float y, int type, Color color, bool piece, bool enabled = true)
    {
        GameObject tile;
        tileVector.x = x;
        tileVector.y = y;

        if (piece)
        {
            tile = Instantiate(tilePiecePrefab, pieceTileLayerTransform) as GameObject;
            TilePiece tp = tile.GetComponent<TilePiece>();
            tp.type = type;
            tp.color = color;
            tp.pieceImage.color = color;
            tile.transform.localPosition = tileVector;
            tilePieceList.Add(tile);
        }
        else
        {
            tile = Instantiate(tileButtonPrefab, buttonTileLayerTransform) as GameObject;
            tile.GetComponent<TileButton>().type = type;
            tile.transform.localPosition = tileVector;
            tileButtonList.Add(tile);
        }
    }

    void LayoutSpawnMyFieldTiles(bool piece)
    {
        float x = -462.7f;
        float y = -153.2f;
        int type = TileTypes.myLayoutFieldTile;

        for (int a = 0; a < 4; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                if (piece)
                {
                    SpawnTile(x, y, type, transparent, piece);
                }
                else
                {
                    SpawnTile(x, y, type, transparent, piece);
                }
                x = x + 102.9f;
            }
            x = -462.7f;
            y = y - 102f;
        }
    }

    void LayoutSpawnMyTrayTiles(bool piece, Color color)
    {
        float x = -462.7f;
        float y = -561.2f;
        int type = TileTypes.myLayoutTrayTile;

        SpawnTile(x, y, type, color, piece);
        x = 463.4f;
        SpawnTile(x, y, type, color, piece);
        x = -462.7f;
        y = y - 102f;

        for (int a = 0; a < 10; a++)
        {
            SpawnTile(x, y, type, color, piece);
            x = x + 102.9f;
        }
    }

    void LayoutInitMyTrayTiles(Color color)
    {
        LayoutSpawnMyTrayTiles(false,color);
        LayoutSpawnMyTrayTiles(true,color);
        foreach (GameObject tile in tilePieceList)
        {
            //tile.AddComponent<TrayTile>();
            //tile.GetComponent<PieceTile>().pieceImage.color = color;
        }
        GameObject[] tileArray = tilePieceList.ToArray();

        SetPieceTile(tileArray[0], 6, 11);
        SetPieceTile(tileArray[1], 1, 12);
        SetPieceTile(tileArray[2], 1, 1);
        SetPieceTile(tileArray[3], 1, 2);
        SetPieceTile(tileArray[4], 2, 3);
        SetPieceTile(tileArray[5], 3, 4);
        SetPieceTile(tileArray[6], 4, 5);
        SetPieceTile(tileArray[7], 4, 6);
        SetPieceTile(tileArray[8], 4, 7);
        SetPieceTile(tileArray[9], 5, 8);
        SetPieceTile(tileArray[10], 8, 9);
        SetPieceTile(tileArray[11], 1, 10);
    }

    void LayoutSpawnFieldtileFromTrayTile(TileButton tbClicked, TilePiece tpClicked)
    {
        //if the clicked field tile already has a piece on it
        if(tpClicked)
        {
            //put the clicked tile back in the tray






































            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////// Trouble here //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            GameObject trayTile = FindTrayTileByValue(tpSelected.value);
            if (trayTile)
            {
                TilePiece tp = trayTile.GetComponent<TilePiece>();
                tp.count++;
                SetPieceTile(trayTile, tp.count, tp.value);
                tp.pieceImage.color = tp.color;

                SetPieceTile(tpClicked.gameObject, 0, tpSelected.value);
            }
            else
            {
                ClearAllOutlines();
                tbSelected = null;
                tpSelected = null;
            }
        }
        else
        {
            tileVector.x = tbClicked.myTransform.localPosition.x;
            tileVector.y = tbClicked.myTransform.localPosition.y;
            SpawnTile(tileVector.x, tileVector.y, TileTypes.myLayoutFieldTile, tpSelected.color, true);
            GameObject[] tileArray = tilePieceList.ToArray();
            SetPieceTile(tileArray[tileArray.Length - 1], 0, tpSelected.value);
            tileArray[tileArray.Length - 1].GetComponent<TilePiece>().countText.text = "";
            tpSelected.count--;
            if (tpSelected.count < 1)
            {
                tpSelected.pieceImage.color = transparent;
                tpSelected.countText.text = "";
                tpSelected.valueText.text = "";
                tpSelected.count = 0;
            }
            tpSelected.countText.text = tpSelected.count.ToString();
        }
    }

    void LayoutSwapFieldtileForFieldTile(TileButton tbClicked, TilePiece tpClicked)
    {
        int tempValue;
        if(tpSelected)
        {
            tempValue = tpSelected.value;
            if(tpClicked)
            {
                SetPieceTile(tpSelected.gameObject, 0, tpClicked.value);
                tpSelected.countText.text = "";

                SetPieceTile(tpClicked.gameObject, 0, tempValue);
                tpClicked.countText.text = "";
            }
            else
            {
                //if there is a piece on the selected tile and
                //there isn't a piece on the clicked tile
                //set the clicked tile to the selected tile vaule
                tpSelected.myTransform.localPosition = tbClicked.myTransform.localPosition;
            }
            
        }
        else
        {
            //if there is no piece on the selected tile
            if (tpClicked)
            {
                //and there is a piece on the clicked tile
                tpClicked.myTransform.localPosition = tbSelected.myTransform.localPosition;
            }
            else
            {
                //and there is no piece on the clicked tile
                ClearAllOutlines();
            }
        }
    }

    void LayoutPutFieldtileBackInTray()
    {
        GameObject trayTile = FindTrayTileByValue(tpSelected.value);
        if(trayTile)
        {
            TilePiece tpClicked = trayTile.GetComponent<TilePiece>();
            tpClicked.count++;
            SetPieceTile(trayTile, tpClicked.count, tpClicked.value);
            tpClicked.pieceImage.color = tpClicked.color;

            if (tpSelected.gameObject)
            {
                tilePieceList.Remove(tpSelected.gameObject);
                Destroy(tpSelected.gameObject);
            }
        }
        else
        {
            ClearAllOutlines();
            tbSelected = null;
            tpSelected = null;
        }
    }

    void LayoutProcessTileClick(TileButton tbClicked, TilePiece tpClicked)
    {
        int typeSelected;
        int typeClicked;
        if(tpSelected)
        {
            typeSelected = tpSelected.type;
        }
        else
        {
            typeSelected = tbSelected.type;
        }

        if(tpClicked)
        {
            typeClicked = tpClicked.type;
        }
        else
        {
            typeClicked = tbClicked.type;
        }

        switch(typeSelected)
        {
            case TileTypes.myLayoutTrayTile:
                switch(typeClicked)
                {
                    case TileTypes.myLayoutTrayTile:
                        ClearAllOutlines();
                        break;
                    case TileTypes.myLayoutFieldTile:
                        LayoutSpawnFieldtileFromTrayTile(tbClicked, tpClicked);
                        break;
                }
                break;
            case TileTypes.myLayoutFieldTile:
                switch (typeClicked)
                {
                    case TileTypes.myLayoutTrayTile:
                        LayoutPutFieldtileBackInTray();
                        break;
                    case TileTypes.myLayoutFieldTile:
                        LayoutSwapFieldtileForFieldTile(tbClicked, tpClicked);
                        break;
                }
                break;
        }
        tbSelected = null;
        tpSelected = null;
    }

    void ProcessTileClick(TileButton tbClicked, TilePiece tpClicked)
    {
        if(isLayoutBoard)
        {
            LayoutProcessTileClick(tbClicked, tpClicked);
        }
        else
        {

        }
    }


    public void TileClick(GameObject goClicked)
    {
        TileButton tbClicked = goClicked.GetComponent<TileButton>();
        Vector3 vectorClicked = tbClicked.myTransform.localPosition;
        GameObject goClickedPiece = FindTilePiece(vectorClicked.x, vectorClicked.y);        

        //if a tile has been selected
        if(tbSelected)
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
            Vector3 tfSelected = tbSelected.myTransform.localPosition;
            GameObject goSelectedPiece = FindTilePiece(tfSelected.x, tfSelected.y);
            
            ProcessTileClick(tbClicked, tpClicked);
            ClearAllOutlines();
        }
        //else assign clicked tile to selected tile
        else
        {
            tbSelected = tbClicked;
            if(goClickedPiece)
            {
                tpSelected = goClickedPiece.GetComponent<TilePiece>();
            }
            else
            {
                tpSelected = null;
            }
            tbSelected.outlineImage.color = Color.green;
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
        if(isLayoutBoard)
        {
            LayoutInitMyTrayTiles(Color.red);
            LayoutSpawnMyFieldTiles(false);
        }
        else
        {

        }        
    }

    /*void SpawnTileFromTrayToField(GameObject sourceTile, GameObject destinationTile)
    {
        tileVector = destinationTile.GetComponent<TilePiece>().myTransform.position;
        GameObject tile = Instantiate(tilePiecePrefab, pieceTileLayerTransform) as GameObject;
        tile.GetComponent<TilePiece>().type = TileTypes.myLayoutFieldTile;
        tile.transform.localPosition = tileVector;
        tilePieceList.Add(tile);

        TilePiece tpSource = sourceTile.GetComponent<TilePiece>();
        tpSource.count = tpSource.count - 1;
        if(tpSource.count < 1)
        {
            if(sourceTile)
            { }
        }
    }

    void MyLayoutTrayPieceTileClick(GameObject clickedTile)
    {
        Vector3 tileVector = selectedTileButton.myTransform.position;;
        Vector3 clickedPos;

        if(selectedTilePiece)
        {

        }
        
        if(selectedTileButton)
        {
            ClearAllOutlines();
            TilePiece tpClickedTilePiece = clickedTile.GetComponent<TilePiece>();
            if (selectedTileButton.GetComponent<TilePiece>())
            {
                TilePiece tpSelectedTilePiece = selectedTileButton.GetComponent<TilePiece>();
            }
            else
            {
                TileButton tbSelectedTileButton = selectedTileButton.GetComponent<TileButton>();
            }
            
        }
        else
        {
            clickedPos = clickedTile.GetComponent<TilePiece>().myTransform.position;
            selectedTileButton = FindTileButton(clickedPos.x, clickedPos.y).GetComponent<TileButton>();
        }
        selectedTileButton = null;
        selectedTilePiece = null;
    }*/

    /*List<GameObject> SpawnOpponentTrayTiles()
    {
        float x = -462.7f;
        float y = 662.8f;
        
        List<GameObject> tileList = new List<GameObject>();

        for(int a = 0; a < 10; a++)
        {
            tileList.Add(SpawnTile(x,y,true));
            x = x + 102.9f;
        }
        x = -462.7f;
        y = y - 102f;
        tileList.Add(SpawnTile(x,y,true));
        x = 463.4f;
        tileList.Add(SpawnTile(x,y,true));
        //y = y - 102f;

        return tileList;
    }

    List<GameObject> SpawnOpponentFieldTiles(bool piece)
    {
        float x = -462.7f;
        float y = 458.8f;
        Vector3 tileVector = new Vector3(0, 0, 0);
        List<GameObject> tileList = new List<GameObject>();

        for (int a = 0; a < 4; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                if (piece)
                {
                    tileList.Add(SpawnTile(x, y,true));
                }
                else
                {
                    tileList.Add(SpawnTile(x, y,false));
                }                
                x = x + 102.9f;
            }
            x = -462.7f;
            y = y - 102f;
        }
        return tileList;
    }

    List<GameObject> SpawnNeutralTiles()
    {
        float x = -462.7f;
        float y = 50.8f;

        List<GameObject> tileList = new List<GameObject>();

        for (int a = 0; a < 2; a++)
        {
            for (int b = 0; b < 2; b++)
            {
                tileList.Add(SpawnTile(x,y,false));
                x = x + 102.9f;
            }
            for (int b = 0; b < 2; b++)
            {
                x = x + 102.9f;
            }
            for (int b = 0; b < 2; b++)
            {
                tileList.Add(SpawnTile(x,y,false));
                x = x + 102.9f;
            }
            for (int b = 0; b < 2; b++)
            {
                x = x + 102.9f;
            }
            for (int b = 0; b < 2; b++)
            {
                tileList.Add(SpawnTile(x,y,false));
                x = x + 102.9f;
            }
            y = y - 102f;
            x = -462.7f;
        }

        return tileList;
    }*/
}
