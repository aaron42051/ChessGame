using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        i = CurrentX;
        j = CurrentY;

        // left 
        if (i - 2 >= 0)
        {
            // left up
            if (j + 1 < 8)
            {
                c = BoardManager.Instance.Chessmans[i - 2, j + 1];
                if (c == null)
                {
                    r[i - 2, j + 1] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i - 2, j + 1] = true;
                }
            }

            // left down
            if (j - 1 >= 0)
            {
                c = BoardManager.Instance.Chessmans[i - 2, j - 1];
                if (c == null)
                {
                    r[i - 2, j - 1] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i - 2, j - 1] = true;
                }
            }
        }

        // right
        if (i + 2 < 8)
        {
            // right up
            if (j + 1 < 8)
            {
                c = BoardManager.Instance.Chessmans[i + 2, j + 1];
                if (c == null)
                {
                    r[i + 2, j + 1] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i + 2, j + 1] = true;
                }
            }

            // right down
            if (j - 1 >= 0)
            {
                c = BoardManager.Instance.Chessmans[i + 2, j - 1];
                if (c == null)
                {
                    r[i + 2, j - 1] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i + 2, j - 1] = true;
                }
            }
        }

        // down
        if (j - 2 >= 0)
        {
            // down left
            if (i - 1 >= 0)
            {
                c = BoardManager.Instance.Chessmans[i - 1, j - 2];
                if (c == null)
                {
                    r[i - 1, j - 2] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i - 1, j - 2] = true;
                }
            }

            // down right
            if (i + 1 < 8)
            {
                c = BoardManager.Instance.Chessmans[i + 1, j - 2];
                if (c == null)
                {
                    r[i + 1, j - 2] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i + 1, j - 2] = true;
                }
            }
        }

        // up
        if (j + 2 < 8)
        {
            // up left
            if (i - 1 >= 0)
            {
                c = BoardManager.Instance.Chessmans[i - 1, j + 2];
                if (c == null)
                {
                    r[i - 1, j + 2] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i - 1, j + 2] = true;
                }
            }

            // up right
            if (i + 1 < 8)
            {
                c = BoardManager.Instance.Chessmans[i + 1, j + 2];
                if (c == null)
                {
                    r[i + 1, j + 2] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[i + 1, j + 2] = true;
                }
            }
        }

        return r;
    }
}
