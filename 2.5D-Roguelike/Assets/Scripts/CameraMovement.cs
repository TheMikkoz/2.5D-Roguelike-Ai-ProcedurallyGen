using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float duration;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target.transform.position);
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, duration);
    }
}
