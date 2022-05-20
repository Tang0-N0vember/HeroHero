using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Camera _camera;
    void Update()
    {
        
        if (_camera == null)
            _camera = Camera.main;
        if (_camera == null)
            return;
        

        transform.LookAt(_camera.transform);
        transform.Rotate(Vector3.up * 180);
    }
}
