using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private Transform target;
    
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }
    private void Update()
    {
        Vector3 position = target.position;
        position.z = -10.0f;
        position.y = 0;
        position.x = position.x < 0 ? 0 : position.x;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}