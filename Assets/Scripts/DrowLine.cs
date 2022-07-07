using UnityEngine;
using System;
using System.Collections.Generic;
public class DrowLine : MonoBehaviour
{

    private LineRenderer Line;
    [SerializeField] private Color[] colors;
    public Vector3 startPos;
    public int colorNum =0;
    private Vector3 lastPos;

    void Start()
    {
        Line = GetComponent<LineRenderer>();
        Line.startWidth = 0.2f;
        Line.endWidth = 0.2f;
        Line.positionCount = 0;
        Line.startColor = Line.endColor = colors[colorNum];
        lastPos = startPos;
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && InkManager.inkAmount > 0 && InputManager.onAllowedPosition)
        {

            Vector3 currentPoint = InputManager.GetWorldPosition(Input.mousePosition);
            if (currentPoint != lastPos || lastPos == startPos)
            {
                lastPos = currentPoint;
                InkManager.inkAmount -= Time.deltaTime * 10;
                Line.positionCount++;
                Line.SetPosition(Line.positionCount - 1, currentPoint);
            }
        }
        else if(Input.GetMouseButtonUp(0) || !InputManager.onAllowedPosition)
        {


            var coll = gameObject.AddComponent<PolygonCollider2D>();

            List<Vector2> points = new List<Vector2>();
            Vector3 pos = new Vector3();
            for (int i = 0; i < Line.positionCount; i++)
            {
                pos = new Vector3(0, 0.1f, 0);
                if (i != 0 && Math.Abs(Line.GetPosition(i).y - Line.GetPosition(i - 1).y) > Math.Abs(Line.GetPosition(i).x - Line.GetPosition(i - 1).x))
                    pos = new Vector3(0.1f, 0, 0);
                points.Add(Line.GetPosition(i) +pos);
            }
            for (int i = Line.positionCount-1; i >= 0; i--)
            {
                pos = new Vector3(0, 0.1f, 0);
                if (i != Line.positionCount - 1 &&  Math.Abs(Line.GetPosition(i).y - Line.GetPosition(i + 1).y) > Math.Abs(Line.GetPosition(i).x - Line.GetPosition(i + 1).x))
                    pos = new Vector3(0.1f, 0, 0);
                points.Add(Line.GetPosition(i) - pos);
            }
            points[points.Count - 1] = points[0]; 
            var a = points.ToArray();
            coll.points = a;
            gameObject.AddComponent<Rigidbody2D>();
            GetComponent<DrowLine>().enabled = false;

        }
    }

    
}
