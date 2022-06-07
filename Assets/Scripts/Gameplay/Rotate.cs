using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed;
    
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);
    }
}
