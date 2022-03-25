using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform agentGenerator;
    [SerializeField]
    private float cameraSmoothFactor = 0.5f;

    // Viewmode settings
    [Header("Viewmode")]
    [SerializeField]
    private Vector3 firstPersonCameraOffset = new Vector3(0, 1.5f, 0);
    [SerializeField]
    private float firstPersonCameraRotation = -45f;
    private bool firstPersonView = false;

    // Zoom settings
    [Header("Zoom")]
    [SerializeField]
    private float minZoom = 2f;
    [SerializeField]
    private float maxZoom = 50f;
    [SerializeField]
    private float zoomSpeed = 100f;
    private float zoom = 25f;
    private float zoomRef = 0;

    // Rotation settings
    [Header("Rotation")]
    [SerializeField]
    private float rotationSpeed = 100f;
    private Quaternion targetRotation;

    private Vector3 cameraOffset;
    private int followedAgentIndex = 0;
    private Transform targetAgent;

    void Start()
    {
        cameraOffset = transform.position - agentGenerator.position;
    }

    void LateUpdate()
    {
        // Target managment
        if (targetAgent == null)
        {
            if (agentGenerator.childCount <= 0)
            {
                // Set targetAgent to a new transform at position 0, 0, 0
                targetAgent = agentGenerator.transform;
            }
            else
            {
                targetAgent = agentGenerator.GetChild(followedAgentIndex);
            }
        }

        // Target switching
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            followedAgentIndex = (followedAgentIndex + 1) % agentGenerator.childCount;
            targetAgent = agentGenerator.GetChild(followedAgentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            followedAgentIndex = (followedAgentIndex - 1 + agentGenerator.childCount) % agentGenerator.childCount;
            targetAgent = agentGenerator.GetChild(followedAgentIndex);
        }

        // Viewmode switching
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!firstPersonView)
            {
                firstPersonView = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (firstPersonView)
            {
                firstPersonView = false;
            }
        }

        if (firstPersonView)
        {
            // Folow the target
            transform.position = Vector3.Lerp(transform.position, targetAgent.TransformPoint(firstPersonCameraOffset), cameraSmoothFactor);

            // Copy the target's rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAgent.rotation * Quaternion.Euler(firstPersonCameraRotation, 0, 0), cameraSmoothFactor);
        }
        else
        {
            // Rotate around the target when the right mouse button is pressed according to the mouse movement
            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(targetAgent.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed);
                transform.RotateAround(targetAgent.position, transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed);

                cameraOffset = transform.position - targetAgent.position;
            }

            // Zoom in and out when the scroll wheel is moved
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                zoom = Mathf.Clamp(zoom - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed, minZoom, maxZoom);
                cameraOffset = cameraOffset.normalized * zoom;
            }

            // Move the camera to the target agent
            transform.position = Vector3.Lerp(transform.position, targetAgent.position + cameraOffset, cameraSmoothFactor);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetAgent.position - transform.position), cameraSmoothFactor);
        }
    }
}
