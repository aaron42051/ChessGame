using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
    // allow us to access an instance of this from anywhere
    public static BoardHighlights Instance { set; get; }

    public GameObject highlightPrefab;
    private List<GameObject> highlights;

    private void Start()
    {
        // set instance to this, instantiate list of highlights
        Instance = this;
        highlights = new List<GameObject>();
    }


    // each highlight will be on or off
    // get a new highlight object
    private GameObject GetHighlightObject()
    {
        // return the first highlight that's not active (reuse highlight objects)
        GameObject gObj = highlights.Find(g => !g.activeSelf);

        // if there are none available to use, instantiate another one and add it
        if (gObj == null)
        {
            gObj = Instantiate(highlightPrefab);
            highlights.Add(gObj);
        }

        return gObj;
    }


    // for each possible move, get a highlight object and set it active, then move it to the right position
    public void HighlightAllowedMoves(bool[,] moves)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {

                    GameObject gObj = GetHighlightObject();
                    gObj.SetActive(true);
                    gObj.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    // for each gameobject in highlights, set them inactive
    public void HideHighlights()
    {
        foreach(GameObject gObj in highlights)
        {
            gObj.SetActive(false);
        }
    }
}
