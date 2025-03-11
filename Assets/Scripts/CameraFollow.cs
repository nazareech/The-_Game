using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float minX, maxX, minY, maxY;
    [SerializeField] Transform target;

    [SerializeField] float followSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target)return;

        transform.position = Vector3.Lerp(transform.position,
           new Vector3(
           Mathf.Clamp(target.position.x, minX, maxX),
           Mathf.Clamp(target.position.y, minY, maxY),
           -10),
           followSpeed * Time.deltaTime);
    }
}
