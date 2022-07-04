using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrowLine : MonoBehaviour
{

    private LineRenderer Line;
    [SerializeField] private GameObject collLine;
    public Vector3 startPos;
    void Start()
    {
        Line = GetComponent<LineRenderer>();
        Line.startWidth = 0.2f;
        Line.endWidth = 0.2f;
        Line.positionCount = 0;
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && InputManager.inkAmount > 0)
        {

            Vector3 currentPoint = InputManager.GetWorldPosition(Input.mousePosition);
            if (currentPoint != startPos)
            {
                startPos = currentPoint;
                InputManager.inkAmount -= Time.deltaTime * 10;
                Line.positionCount++;
                Line.SetPosition(Line.positionCount - 1, currentPoint);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            var obj = Instantiate(collLine,transform.position,Quaternion.identity);

            //obj.GetComponent<Rigidbody2D>().MovePosition(Line.transform.position);
            transform.SetParent(obj.transform.transform);

            var coll = obj.GetComponent<EdgeCollider2D>();
            List<Vector2> points = new List<Vector2>();
            for (int i = 0; i < Line.positionCount; i++)
            {
                points.Add(Line.GetPosition(i));
            }
            coll.SetPoints(points);

            GetComponent<DrowLine>().enabled = false;

        }
    }

    
}
