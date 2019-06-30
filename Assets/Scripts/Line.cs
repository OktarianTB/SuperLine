using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Line : MonoBehaviour
{
    private float minDistance = 0.1f;
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

    public void UpdateLine(Vector2 mousePosition)
    {
        if(points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePosition);
            return;
        }

        if(Vector2.Distance(points.Last(), mousePosition) > minDistance)
        {
            SetPoint(mousePosition);
        }
    }

    private void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        ManageCapacity();

        if (points.Count > 1)
        {
            edgeCollider.points = points.ToArray();
        }

    }

    private void ManageCapacity()
    {
        lineManager = FindObjectOfType<LineManager>();

        if (!lineManager)
        {
            Debug.LogWarning("Line Manager can't be found by Line Selector");
        }
        else
        {
            lineManager.RemoveCapacity();
        }
    }

}
