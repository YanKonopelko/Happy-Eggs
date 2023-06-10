using System.Collections;
using UnityEngine;
public class LineCheck :MonoBehaviour
{
    private Transform _transform;
    private Vector3 lastPos;
    private void Start()
    {
        _transform = transform;
        lastPos = _transform.position;
    }
    
}
