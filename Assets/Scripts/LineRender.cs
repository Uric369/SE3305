using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour

{
    LineRenderer line;
    public Transform startPoint;
    public Transform endPoint;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startColor = Color.gray;
        line.endColor = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
        line.startColor = Color.gray;
        line.endColor = Color.gray;
    }
}
