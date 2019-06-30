using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSelector : MonoBehaviour
{

    LineManager lineManager;

    private void Start()
    {
        lineManager = FindObjectOfType<LineManager>();

        if (!lineManager)
        {
            Debug.LogWarning("Line Manager can't be found by Line Selector");
        }
    }

    public void SelectBoostLine()
    {
        lineManager.isNormalLine = false;
    }

    public void SelectNormalLine()
    {
        lineManager.isNormalLine = true;
    }

}
