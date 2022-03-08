using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentController : Agent
{
    // Move speed
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 5f;
    // Vision parameters
    [Header("Vision")]
    [SerializeField]
    private float viewField = 90f;
    [SerializeField]
    [Range(3, 30)]
    private uint viewRayNumber = 9;
    [SerializeField]
    private bool showRays = false;

    public override void OnEpisodeBegin()
    {
        // Reset the agent position
        transform.localPosition = new Vector3(0f, 1f, 0f);
    }

    // Method called when the neural network request observation data
    public override void CollectObservations(VectorSensor sensor)
    {
        // Lunch 3 raycast in fornt of the agent with an angle of 30°
        // Make a layer mask to ignore the "Environment" layer
        int layerMask = 1 << LayerMask.NameToLayer("Environment");
        layerMask = ~layerMask;
        // Eyes position
        Vector3 raycastOrigin = transform.position + transform.forward * 0.51f;
        // Raycast output
        RaycastHit hit;

        for (int i = 0; i < viewRayNumber; i++)
        {
            // Compute the raycast direction
            Vector3 raycastDirection = Quaternion.AngleAxis(-viewField / 2f + (viewField / (viewRayNumber - 1)) * i, transform.up) * transform.forward;
            Physics.Raycast(raycastOrigin, raycastDirection, out hit, 10.0f, layerMask);
            if (showRays)
            {
                if (hit.collider)
                    Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.red);
                else
                    Debug.DrawRay(raycastOrigin, raycastDirection * 10.0f, Color.green);
            }
            sensor.AddObservation(hit.distance);
        }
    }

    // Method called when the agent receive data from the neural network
    public override void OnActionReceived(ActionBuffers actions)
    {
        // === Uncomment to debug the agent === //
        Debug.Log("OnActionReceived");
        // This is to see the output of the neural network with continuous actions
        Debug.Log("Action: " + actions.ContinuousActions[0]);
        // This is to see the output of the neural network with discrete actions
        // Debug.Log("Action: " + actions.DiscreteActions[0]);

        // Recieve the action from the neural network
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        // Move the agent
        transform.Translate(0f, 0f, moveY * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, moveX * 180f * Time.deltaTime, 0f);
    }

    // Methode called when the user override the agent actions with heuristic mode
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }
}
