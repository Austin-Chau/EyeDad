using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour {

    public float lineLength;

    private void Update()
    {
        Vector3 rightDirection = Quaternion.AngleAxis(GetComponent<Camera>().fieldOfView + 20, transform.up) * transform.forward;
        Vector3 leftDirection = Quaternion.AngleAxis(-GetComponent<Camera>().fieldOfView - 20, transform.up) * transform.forward;

        DrawLine(transform.position, transform.position - rightDirection);
        DrawLine(transform.position, transform.position - leftDirection);
    }

    //https://answers.unity.com/questions/8338/how-to-draw-a-line-using-script.html
    private void DrawLine(Vector3 start, Vector3 end, float duration = 0.05f)
    {
        GameObject myLine = new GameObject("VisionLine");
        myLine.transform.parent = GameObject.Find("VisionLines").transform;
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        myLine.layer = LayerMask.GetMask("Default");
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.red;
        lr.startWidth = .1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(myLine, duration);
    }
}
