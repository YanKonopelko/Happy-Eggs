using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject LinePrefabs;
    public LayerMask CantDrawOverLayer;
    int CantDrawOverLayerIndex;


    public float LinePointsMinDistance;
    public float LineWidth;
    public Gradient LineColor;


    Line currentLine;


    private void Start()
    {
        CantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if ((Input.touchCount > 0 || Input.GetMouseButton(0) ) && InkManager.inkAmount > 0)
            Draw();

        if (Input.GetMouseButtonUp(0)||InkManager.inkAmount <=0)
            EndDraw();
    }


    private void BeginDraw()
    {
        currentLine = Instantiate(LinePrefabs).GetComponent<Line>();
        currentLine.transform.parent = transform;
        currentLine.gameObject.layer = LayerMask.NameToLayer("CantDrawOver");

        currentLine.SetLineColor(LineColor);
        currentLine.SetLineWidth(LineWidth);
        currentLine.SetPointMinDistance(LinePointsMinDistance);
        currentLine.UsePhysics(false);
    }

    private void Draw()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(MousePos, LineWidth / 3f, Vector2.zero, 1f, CantDrawOverLayer);

        if (!hit)
            currentLine.AddPoint(MousePos);
            //EndDraw();
        //else
            //currentLine.AddPoint(MousePos);
    }

    private void EndDraw()
    {
        if(currentLine != null)
        {
            if(currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                var modi =  currentLine.gameObject.AddComponent<NavMeshModifier>();
                modi.overrideArea = true;
                modi.area = 3;
                currentLine.EndLine(LineWidth);
                currentLine = null;
                GameSceneManager.instanse.StartGame();

                Destroy(this);
            }
        }
        
    }
}
