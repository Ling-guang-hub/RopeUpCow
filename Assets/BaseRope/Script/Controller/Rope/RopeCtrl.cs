// Project  RopeUp
// FileName  RopeCtrl.cs
// Author  AX
// Desc
// CreateAt  2025-06-19 09:06:51 
//


using System;
using UnityEngine;

public class RopeCtrl : MonoBehaviour
{
    public RopeState currentState;

    public Transform startPoint;

    public GameObject ropeLoopObj;

    public float lineWidth;
    
    private LineRenderer _lineRenderer;
    
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = lineWidth;
        startPoint = transform;
    }


    void Update()
    {
        _lineRenderer.SetPosition(0, startPoint.position);
        _lineRenderer.SetPosition(1, ropeLoopObj.transform.position);
        
    }


    private void Start()
    {
        
    }

}


