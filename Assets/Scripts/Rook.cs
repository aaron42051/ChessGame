using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman
{
    public override bool[,] PossibleMove()
    {
        // set of all available moves
        bool[,] r = new bool[8, 8];

        // chessman instance to instatiate to the currently checked tile
        Chessman c;
        int i, j;

        // Forward
        i = CurrentX;
        j = CurrentY;

        // go in one direction
        while (true)
        {
            j++;
            if (j >= 8)
            {
                break;
            }

            // get the piece at that spot
            c = BoardManager.Instance.Chessmans[i, j];

            // if no piece, valid move (and keep moving)
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                // if enemy piece, valid move, but stop here
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }

        // Backward
        j = CurrentY;

        while(true)
        {
            j--;
            if (j < 0)
            {
                break;
            }

            // get the piece at that spot
            c = BoardManager.Instance.Chessmans[i, j];

            // if no piece, valid move (and keep moving)
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                // if enemy piece, valid move, but stop here
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }

        // Left
        j = CurrentY;

        while (true)
        {
            i--;
            if (i < 0)
            {
                break;
            }

            // get the piece at that spot
            c = BoardManager.Instance.Chessmans[i, j];

            // if no piece, valid move (and keep moving)
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                // if enemy piece, valid move, but stop here
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }

        // Right
        i = CurrentX;

        while (true)
        {
            i++;
            if (i >= 8)
            {
                break;
            }

            // get the piece at that spot
            c = BoardManager.Instance.Chessmans[i, j];

            // if no piece, valid move (and keep moving)
            if (c == null)
            {
                r[i, j] = true;
            }
            else
            {
                // if enemy piece, valid move, but stop here
                if (isWhite != c.isWhite)
                {
                    r[i, j] = true;
                }
                break;
            }
        }




        return r;
    }
}
