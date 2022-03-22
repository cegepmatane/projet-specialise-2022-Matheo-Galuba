using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private float viewDistance = 10f;
    [SerializeField]
    [Range(3, 30)]
    private uint viewRayNumber = 9;
    [SerializeField]
    private bool showRays = false;

    // Attack parameters
    [Header("Attack")]
    [SerializeField]
    private Collider reachableEnemiesChecker;
    [SerializeField]
    private float attackDamage = 1f;
    private EnemyChecker enemyChecker;

    public void Start()
    {
        enemyChecker = GetComponentsInChildren<EnemyChecker>()[0];
    }

    public void setTeamId(int teamId)
    {
        GetComponent<Unity.MLAgents.Policies.BehaviorParameters>().TeamId = teamId;
    }

    public override void OnEpisodeBegin()
    {
        // Nothing for the moment
    }

    // Method called when the neural network request observation data
    public override void CollectObservations(VectorSensor sensor)
    {
        // Lunch 3 raycast in fornt of the agent with an angle of 30°
        // Make a layer mask to ignore the "Environment" layer
        int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        layerMask = ~layerMask;
        // Eyes position
        Vector3 raycastOrigin = transform.position + transform.forward * 0.51f;
        // Raycast output
        RaycastHit hit;

        for (int i = 0; i < viewRayNumber; i++)
        {
            // Compute the raycast direction
            Vector3 raycastDirection = Quaternion.AngleAxis(-viewField / 2f + (viewField / (viewRayNumber - 1)) * i, transform.up) * transform.forward;
            Physics.Raycast(raycastOrigin, raycastDirection, out hit, viewDistance, layerMask);
            if (showRays)
            {
                if (hit.collider)
                    Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.red);
                else
                    Debug.DrawRay(raycastOrigin, raycastDirection * viewDistance, Color.green);
            }
            sensor.AddObservation(hit.distance);
        }

        // Observe the distance to the center of the arena
        float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.z, 2));
        sensor.AddObservation(distance);
    }

    // Method called when the agent receive data from the neural network
    public override void OnActionReceived(ActionBuffers actions)
    {
        // === Uncomment to debug the agent === //
        // Debug.Log("OnActionReceived");
        // This is to see the output of the neural network with continuous actions
        // Debug.Log("Action: " + actions.ContinuousActions[0]);
        // This is to see the output of the neural network with discrete actions
        // Debug.Log("Action: " + actions.DiscreteActions[0]);

        // Recieve the action from the neural network
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        int attackAction = actions.DiscreteActions[0];

        // Move the agent
        transform.Translate(0f, 0f, moveY * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, moveX * 180f * Time.deltaTime, 0f);

        // Attack
        if (attackAction == 1)
        {
            attack();
        }
    }

    // Methode called when the user override the agent actions with heuristic mode
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        ActionSegment<int> descreteActions = actionsOut.DiscreteActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");

        descreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    // Method called when the agent attack an enemy
    private void attack()
    {
        // === Uncomment to debug the agent === //
        // Debug.Log("Attack");

        List<GameObject> reachableEnemies = enemyChecker.reachableGameObjects;

        if (reachableEnemies.Count > 0)
        {
            foreach (GameObject enemy in reachableEnemies)
            {
                AddReward(100f);
                enemy.GetComponent<LifeController>().takeDamage(attackDamage);

            }
        }
    }

    public void punish(float punishment)
    {
        AddReward(-punishment);
    }
}
