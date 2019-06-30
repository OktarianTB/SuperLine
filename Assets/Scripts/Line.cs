using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Line : MonoBehaviour
{
    float minDistance = 0.1f;
    public float boostSpeed = 8f;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public LineType lineType = LineType.Normal;
    public enum LineType
    {
        Normal,
        Boost
    }

    List<Vector2> points;
    LineManager lineManager;
    SurfaceEffector2D surfaceEffector;

    public void UpdateLine(Vector2 mousePosition)
    {
        if(points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePosition);
            return;
        }

        float distance = Vector2.Distance(points.Last(), mousePosition);
        if (distance > minDistance)
        {
            ManageCapacity(distance);
            SetPoint(mousePosition);
        }
    }

    private void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        if(points.Count == 2 && lineType == LineType.Boost)
        {
            ManageBoostDirection();
        }

        if (points.Count > 1)
        {
            edgeCollider.points = points.ToArray();
        }

    }

    private void ManageBoostDirection() //Depending on initial direction of the drawing, the boost direction needs to be changed accordingly
    {
        surfaceEffector = GetComponent<SurfaceEffector2D>();

        if(points[0].x < points[1].x)
        {
            surfaceEffector.speed = boostSpeed;
        }
        else
        {
            surfaceEffector.speed = -boostSpeed;
        }

    }

    private void ManageCapacity(float amount)
    {
        lineManager = FindObjectOfType<LineManager>();

        if (!lineManager)
        {
            Debug.LogWarning("Line Manager can't be found by Line Selector");
        }
        else
        {
            lineManager.RemoveCapacity(amount);
        }
    }

}
