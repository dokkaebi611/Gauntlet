using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;
    private void Start()
    {
        
    }

    void Update()
    {
        if(target != null)
        {
            Vector3 tragetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, tragetPosition, ref velocity, smoothTime);
        }
    }
}
