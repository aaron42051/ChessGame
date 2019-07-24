using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    // 2d array of Chess pieces, called Chessmans
    public Chessman[,] Chessmans{set; get;}
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Quaternion flipDirection = Quaternion.Euler(0, 180, 0);

    public bool isWhiteTurn = true;

    private void Start()
    {
        Instance = this; 
        SpawnAllChessmans();
    }

    // Update is called once per frame
    void Update () {
        UpdateSelection();
        DrawChessboard();
        checkClick();
	}

    // constantly update the current hovered position
    private void UpdateSelection()
    {
        // is there a main camera to use?
        if (!Camera.main)
        {
            Debug.Log("No main camera");
            return;
        }

        // if we hit something
        RaycastHit hit;


        // 1. ray provided is from camera to screen point (mouse position)
        // 2. out is the result of the collision
        // 3. 25 is max distance of the ray
        // 4. layer mask (have the ray only hit the chess board and not the piece
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane")))
        {
            // if ray (mouseover) hits something

            // set the current selection
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;

        } else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }


    // render some debug lines for the chessboard and a hover marker
    private void DrawChessboard()
    {
        // the width and height of the board (it's an 8x8)
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            // (0, 0, i)
            Vector3 start = Vector3.forward * i;

            // from i to i + 8
            Debug.DrawLine(start, start + widthLine);

            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        // draw mouse hover 
        if (selectionX >= 0 && selectionY >= 0)
        {
            // draw line from corner to corner
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            // draw line going other direction (form an X)
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }


    // renders a chess piece

    // piece index is set in Unity editor: King(White: 0, Black: 6), Queen (White: 1, Black: 7), Rook, Bishop, Knight, Pawn

    // (piece index/type, x, y, direction (Q identity is white, flipDirection is black))
    private void SpawnChessman(int index, int x, int y, Quaternion direction)
    {
        GameObject gObj = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), direction) as GameObject;

        // give the piece a transform and add it to the list
        gObj.transform.SetParent(transform);
        Chessmans[x, y] = gObj.GetComponent<Chessman>();
        Chessmans[x,y].SetPosition(x, y);
        activeChessman.Add(gObj);
    }

    // get the center of the tile you want (for placing pieces)
    // set (col index, row index)
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    // spawn all the pieces
    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];

        // spawn white

        // King
        SpawnChessman(0, 4, 0, Quaternion.identity);

        // Queen
        SpawnChessman(1, 3, 0, Quaternion.identity);

        // Rooks
        SpawnChessman(2, 0, 0, Quaternion.identity);
        SpawnChessman(2, 7, 0, Quaternion.identity);

        // Bishops
        SpawnChessman(3, 2, 0, Quaternion.identity);
        SpawnChessman(3, 5, 0, Quaternion.identity);

        // Knights
        SpawnChessman(4, 1, 0, Quaternion.identity);
        SpawnChessman(4, 6, 0, Quaternion.identity);

        // Pawns
        for (int i = 0; i < 8; i++) {
            SpawnChessman(5, i, 1, Quaternion.identity);
        }


        // King
        SpawnChessman(6, 4, 7, flipDirection);

        // Queen
        SpawnChessman(7, 3, 7, flipDirection);

        // Rooks
        SpawnChessman(8, 0, 7, flipDirection);
        SpawnChessman(8, 7, 7, flipDirection);

        // Bishops
        SpawnChessman(9, 2, 7, flipDirection);
        SpawnChessman(9, 5, 7, flipDirection);

        // Knights
        SpawnChessman(10, 1, 7, flipDirection);
        SpawnChessman(10, 6, 7, flipDirection);

        // Pawns
        for (int i = 0; i < 8; i++) {
            SpawnChessman(11, i, 6, flipDirection);
        }
    }

    private void checkClick() {

        // if there was a mouse click
        if (Input.GetMouseButtonDown(0))
        {

            // if you clicked on the board
            if(selectionX >= 0 && selectionY >= 0) {

                // if you haven't clicked on a piece yet
                if (selectedChessman == null)
                {
                    // Select the piece
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    // move the piece
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
    }

    // select the given chess piece
    private void SelectChessman(int x, int y)
    {
        // don't select if nothing is there
        if(Chessmans[x, y] == null)
        {
            return;
        }

        // don't select the piece if it's not your turn
        if (Chessmans[x, y].isWhite != isWhiteTurn) {
            return;
        }

        // set the allowed moves
        allowedMoves = Chessmans[x, y].PossibleMove();
        selectedChessman = Chessmans[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    // move the selected chess piece
    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x, y])
        {
            Chessman c = Chessmans[x, y];

            // if there's a piece there and it's not your team
            if (c != null && c.isWhite != isWhiteTurn)
            {
                if(c.GetType() == typeof(King))
                {
                    // End the game
                    return;
                }

                // capture a piece
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }

            // remove the piece from it's original position in the 2d array
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;

            // get the position of the selected piece
            selectedChessman.transform.position = GetTileCenter(x, y);

            // 
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
        }
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
    }

}
