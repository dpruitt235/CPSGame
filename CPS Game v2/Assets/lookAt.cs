using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public Transform mapTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public float RotationSpeed = 3.0f;
    void Start()
    {
        _cameraOffset = transform.position - mapTransform.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            _cameraOffset = camTurnAngle * _cameraOffset;
            Vector3 newPos = mapTransform.position + _cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
            transform.LookAt(mapTransform);
        }
        
    }

    //void Update()
    //{
    //    if(Input.GetAxis("Mouse X") > 0)
    //    {
    //        transform.position+= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime *speed,
    //            0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
    //    }
    //    else if (Input.GetAxis("Mouse X") < 0)
    //    {
    //        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
    //            0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
    //    }
    //}
}
