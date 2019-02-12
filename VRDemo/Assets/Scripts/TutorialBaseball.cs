using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBaseball : MonoBehaviour {

    void Update()
    {
        if (transform.parent.tag == "Controller")
            transform.position = transform.parent.transform.position;
    }
}
