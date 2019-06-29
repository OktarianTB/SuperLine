using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{

    public GameObject normalLinePrefab;
    public GameObject boostLinePrefab;
    public bool isNormalLine = true;

    bool currentlyDrawingALine = false;
    float halfHeight;
    float halfWidth;

    Line activeLine;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (IsValidMousePosition(mousePosition))
        {
            ManageInput(mousePosition);
        }
        else if(currentlyDrawingALine)
        {
            StopDrawing();
        }
    }

    private void ManageInput(Vector2 position)
    {
        if (Input.GetMouseButtonDown(0) && !currentlyDrawingALine)
        {
            StartDrawing();
        }

        if (Input.GetMouseButtonUp(0) && currentlyDrawingALine)
        {
            StopDrawing();
        }

        if (activeLine != null)
        {
           activeLine.UpdateLine(position);
        }

    }

    private void StartDrawing()
    {
        GameObject linePrefab = isNormalLine ? normalLinePrefab : boostLinePrefab;
        GameObject line = Instantiate(linePrefab);
        activeLine = line.GetComponent<Line>();
        currentlyDrawingALine = true;
    }

    private void StopDrawing()
    {
        activeLine = null;
        currentlyDrawingALine = false;
    }

    bool IsValidMousePosition(Vector2 position)
    {
        if(position.y >= halfHeight - 1.3f || position.y <= - halfHeight + 0.5f)
        {
            return false;
        }
        else if (position.x > halfWidth - 0.5f || position.x < - halfWidth + 0.5f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
