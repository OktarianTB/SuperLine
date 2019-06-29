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

    List<GameObject> lines;
    Line activeLine;
    Player player;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        lines = new List<GameObject>();

        player = FindObjectOfType<Player>();
        if (!player)
        {
            Debug.LogWarning("Player hasn't been found");
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(IsValidMousePosition(mousePosition) && !player.gameHasStarted)
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

        DrawLine(position);

    }

    private void DrawLine(Vector2 position)
    {
        if (activeLine != null)
        {
            activeLine.UpdateLine(position);
        }
    }

    private void StartDrawing()
    {
        GameObject linePrefab = isNormalLine ? normalLinePrefab : boostLinePrefab;
        GameObject line = Instantiate(linePrefab);
        lines.Add(line);

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

    public void RemoveLastLine()
    {
        if(lines.Count < 1 || player.gameHasStarted)
        {
            return;
        }

        GameObject lineToBeRemoved = lines[lines.Count - 1];
        lines.Remove(lineToBeRemoved);
        Destroy(lineToBeRemoved);
        
    }

}
