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

    List<Vector2> points;

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

        if(points.Count > 1)
        {
            edgeCollider.points = points.ToArray();
        }

    }


}
