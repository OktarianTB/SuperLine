using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSelector : MonoBehaviour
{

    LineManager lineManager;

    private void Start()
    {
        lineManager = FindObjectOfType<LineManager>();
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
