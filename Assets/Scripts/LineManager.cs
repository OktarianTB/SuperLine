using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineManager : MonoBehaviour
{

    public GameObject normalLinePrefab;
    public GameObject boostLinePrefab;
    public bool isNormalLine = true; // If it's not a normal line, then it's a boost line

    public TextMeshProUGUI normalLineCapacityText;
    public TextMeshProUGUI boostLineCapacityText;

    public float normalLineMaxCapacity = 150f;
    public float boostLineMaxCapacity = 50f;
    public float normalLineCapacity;
    public float boostLineCapacity;
    float amountRemoved = 0;

    bool currentlyDrawingALine = false;
    bool canUseNormalLine = true;
    bool canUseBoostLine = true;

    float halfHeight;
    float halfWidth;

    List<GameObject> lines;
    List<float> lineDistances;
    Line activeLine;
    Player player;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        lines = new List<GameObject>();
        lineDistances = new List<float>();

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
        lineDistances.Add(amountRemoved);
        amountRemoved = 0;

        activeLine = null;
        currentlyDrawingALine = false;
    }

    bool IsValidMousePosition(Vector2 position) // Checks if the mouse is within the playable screen area
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

    public void RemoveLastLine() // Similar to Ctrl + Z
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

    public void RemoveCapacity(float amount) // Removes capacity based on the distance of the line 
    {
        if (isNormalLine)
        {
            normalLineCapacity -= amount;
            amountRemoved += amount;

            if(normalLineCapacity <= 0)
            {
                canUseNormalLine = false;
                normalLineCapacity = 0;
                StopDrawing();
            }
        }
        else
        {
            boostLineCapacity -= amount;
            amountRemoved += amount;

            if (boostLineCapacity <= 0)
            {
                canUseBoostLine = false;
                boostLineCapacity = 0;
                StopDrawing();
            }
        }
    }

    private void AddToCapacity(GameObject lineToBeRemoved) // Restores the line drawing capacity of the removed line
    {
        Line lineComponent = lineToBeRemoved.GetComponent<Line>();

        if(lineComponent.lineType == Line.LineType.Normal)
        {
            normalLineCapacity += lineDistances[lineDistances.Count - 1];
            lineDistances.Remove(lineDistances.Last());

            normalLineCapacity = Mathf.Clamp(normalLineCapacity, 0, normalLineMaxCapacity);
            canUseNormalLine = true;
        }
        else if(lineComponent.lineType == Line.LineType.Boost)
        {
            boostLineCapacity += lineDistances[lineDistances.Count - 1];
            lineDistances.Remove(lineDistances.Last());

            boostLineCapacity = Mathf.Clamp(boostLineCapacity, 0, boostLineMaxCapacity);
            canUseBoostLine = true;
        }
    }

    private void UpdateCapacityText()
    {
        if(normalLineCapacity == 0)
        {
            normalLineCapacityText.text = "0%";
        }
        else
        {
            float normalLinePercentage = Mathf.Round(normalLineCapacity / normalLineMaxCapacity * 100);
            normalLineCapacityText.text = normalLinePercentage + "%";
        }

        if (boostLineCapacity == 0)
        {
            boostLineCapacityText.text = "0%";
        }
        else
        {
            float boostLinePercentage = Mathf.Round(boostLineCapacity / boostLineMaxCapacity * 100);
            boostLineCapacityText.text = boostLinePercentage + "%";
        }
    }

}
