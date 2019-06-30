using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineManager : MonoBehaviour
{

    public GameObject normalLinePrefab;
    public GameObject boostLinePrefab;
    public bool isNormalLine = true;

    public TextMeshProUGUI normalLineCapacityText;
    public TextMeshProUGUI boostLineCapacityText;

    public float normalLineMaxCapacity = 150f;
    public float boostLineMaxCapacity = 50f;
    float normalLineCapacity;
    float boostLineCapacity;

    bool currentlyDrawingALine = false;
    bool canUseNormalLine = true;
    bool canUseBoostLine = true;
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

        normalLineCapacity = normalLineMaxCapacity;
        boostLineCapacity = boostLineMaxCapacity;

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

        UpdateCapacityText();
    }

    private void ManageInput(Vector2 position)
    {
        if (Input.GetMouseButtonDown(0) && !currentlyDrawingALine)
        {
            if (isNormalLine && canUseNormalLine || !isNormalLine && canUseBoostLine)
            {
                StartDrawing();
            }
                
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

        AddToCapacity(lineToBeRemoved);

        lines.Remove(lineToBeRemoved);
        Destroy(lineToBeRemoved);
        
    }

    public void RemoveCapacity()
    {
        if (isNormalLine)
        {
            normalLineCapacity = Mathf.Clamp(normalLineCapacity - 1, 0, normalLineMaxCapacity);
            if(normalLineCapacity <= 0)
            {
                canUseNormalLine = false;
                StopDrawing();
            }
        }
        else
        {
            boostLineCapacity = Mathf.Clamp(boostLineCapacity - 1, 0, boostLineMaxCapacity);
            if(boostLineCapacity <= 0)
            {
                canUseBoostLine = false;
                StopDrawing();
            }
        }
    }

    private void AddToCapacity(GameObject lineToBeRemoved) // Restores the line drawing capacity of the removed line
    {
        Line lineComponent = lineToBeRemoved.GetComponent<Line>();

        if(lineComponent.lineType == Line.LineType.Normal)
        {
            normalLineCapacity += lineComponent.lineRenderer.positionCount;
            normalLineCapacity = Mathf.Clamp(normalLineCapacity, 0, normalLineMaxCapacity);
            canUseNormalLine = true;
        }
        else if(lineComponent.lineType == Line.LineType.Boost)
        {
            boostLineCapacity += lineComponent.lineRenderer.positionCount;
            boostLineCapacity = Mathf.Clamp(boostLineCapacity, 0, boostLineMaxCapacity);
            canUseBoostLine = true;
        }
    }

    private void UpdateCapacityText()
    {
        float normalLinePercentage = Mathf.Round(normalLineCapacity / normalLineMaxCapacity * 100);
        float boostLinePercentage = Mathf.Round(boostLineCapacity / boostLineMaxCapacity * 100);

        normalLineCapacityText.text = normalLinePercentage + "%";
        boostLineCapacityText.text = boostLinePercentage + "%";
    }

}
