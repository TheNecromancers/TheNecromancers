using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class CompanionMovement : MonoBehaviour
{

    private Transform companionAnchor;
    public float time = 0.5f; // how fast it'll catch up, 0.3 seconds

    private Vector3 velocity;

    void Start()
    {
        companionAnchor = GameObject.FindGameObjectWithTag("CompanionAnchor").transform;
    }

    void Update()
    {
        float length = 1.0f; // Desired length of the ping-pong
        Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.forward;
        //direction perpendicular to player forward direction
        Vector3 left = Vector3.Cross(dir, Vector3.up).normalized;
        Vector3 offset = (Mathf.PingPong(Time.time, length) - length) * left;
        transform.position = Vector3.SmoothDamp(transform.position, companionAnchor.position + offset, ref velocity, time);
    }
}
 
