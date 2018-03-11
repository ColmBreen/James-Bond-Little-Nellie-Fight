using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public GameObject targetGameObject;
    public Vector3 target = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float maxSpeed = 5.0f;
    public float mass = 1;
    public Vector3 force = Vector3.zero;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
        Gizmos.DrawLine(transform.position, target);
    }

    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }

    private void Update()
    {
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
            force = SeekForce(target);
        }

        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        if (velocity.magnitude > 0.0001f)
        {
            transform.LookAt(transform.position + velocity);
            velocity *= 0.99f;
        }
        transform.position += velocity * Time.deltaTime;
    }
}
