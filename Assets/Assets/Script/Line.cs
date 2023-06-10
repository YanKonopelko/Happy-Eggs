using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rb;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    private float pointsMinDistance = 0.1f;
    private float CircleColliderRaduis;
    private const float allowedDistance = 0.2f;


    private Vector2 fix;
    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }


    public void UsePhysics(bool usePhysic)
    {
        rb.isKinematic = !usePhysic;
    }


    public void SetLineColor(Gradient LineColor)
    {
        lineRenderer.colorGradient = LineColor;
    }


    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    
    public void AddPoint(Vector2 newPoint)
    {
        //if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            //return;
        if (pointsCount > 1)
        {
            var lastPos = GetLastPoint();
            Vector2 point = lastPos;
            fix = FixPos(lastPos);
            Debug.Log(fix);
            Debug.Log(lastPos);

            if ( (lastPos.x < newPoint.x && fix.x == 1) || (lastPos.x > newPoint.x && fix.x == -1) || fix.x == 0  )
                point.x = newPoint.x;
            if ( (lastPos.y < newPoint.y && fix.y == 1) || (lastPos.y > newPoint.y && fix.y == -1) || fix.y == 0 )
                point.y = newPoint.y;
            Debug.Log(point);

            if (lastPos == point || fix == new Vector2(2,2)) return;
            
            
            newPoint = point;
        }
        
        //InkManager.inkAmount -= InkManager.currentPrice;

        
        points.Add(newPoint);
        pointsCount++;

        // Add CircleCollider to the point
        

        //LineRenderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

        //EdgeCollider
        
    }

    public void EndLine(float LineWidth)
    {
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        gameObject.layer = LayerMask.NameToLayer("CantDrawOver");
        edgeCollider.edgeRadius =LineWidth / 2f;
        CircleColliderRaduis = LineWidth / 2f;
        UsePhysics(true);
        if (pointsCount > 1)
        {
            edgeCollider.points = points.ToArray();
            foreach (var t in edgeCollider.points)
            {
                CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
                circleCollider.offset = t;
                circleCollider.radius = CircleColliderRaduis;
            }
        }
        
    }
    public void SetPointMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }
    
    private Vector2 FixPos(Vector2 from)
    {
        Vector2 point = new Vector2(0, 0);
        
        Debug.DrawRay(from, Vector2.down * allowedDistance, Color.black,1);
        Debug.DrawRay(from, Vector2.right * allowedDistance, Color.black,1);
        Debug.DrawRay(from, Vector2.left * allowedDistance, Color.black,1);
        Debug.DrawRay(from, Vector2.up * allowedDistance, Color.black,1);
        
        if (Physics2D.Raycast(from, Vector2.down, allowedDistance))
            point.y = 1;
            if (Physics2D.Raycast(from, Vector2.up, allowedDistance))
            point.y = -1;

        if (Physics2D.Raycast(from, Vector2.down, allowedDistance) &&
            Physics2D.Raycast(from, Vector2.up, allowedDistance))
            point.y = 2;
                 
        if (Physics2D.Raycast(from, Vector2.left, allowedDistance))
            point.x = 1;
        if (Physics2D.Raycast(from, Vector2.right, allowedDistance))
            point.x = -1;
         
        if(Physics2D.Raycast(from, Vector2.left, allowedDistance) &&
           Physics2D.Raycast(from, Vector2.right, allowedDistance))
            point.x = 2;
        return point;
    }

  
}
