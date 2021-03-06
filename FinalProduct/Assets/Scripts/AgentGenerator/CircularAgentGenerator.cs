using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircularAgentGenerator : AgentGenerator
{
    // Generation settings
    [Header("Circular Generation")]
    [SerializeField]
    private float radius = 20f;

    public override void spawnAgents()
    {
        // Compute the agent height to offset the generation position
        float agentHeight = agentPrefab.GetComponentInChildren<Renderer>().bounds.size.y;

        for (int i = 0; i < populationSize; ++i)
        {
            float angle = i * Mathf.PI * 2 / populationSize;
            Vector3 generationPosition = transform.position + new Vector3(Mathf.Cos(angle) * radius, agentHeight / 2, Mathf.Sin(angle) * radius);
            Quaternion generationRotation = Quaternion.Euler(0, -angle * Mathf.Rad2Deg - 90, 0);
            GameObject agent = Instantiate(agentPrefab, generationPosition, generationRotation);
            agent.transform.parent = transform;
            agent.name = "Agent " + i;
            agent.GetComponent<AgentController>().setTeamId(i);
            agents.Add(agent);
            GameObject agentLifeUI = Instantiate(agentLifeUIPrefab);
            agentLifeUI.transform.SetParent(canvas.transform);
            agentLifeUI.name = "AgentLifeUI " + i;
            agentLifeUI.GetComponent<AgentLifeUI>().SetAgent(agent);
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < populationSize; ++i)
        {
            float angle = i * Mathf.PI * 2 / populationSize;
            Vector3 generationPosition = new Vector3(Mathf.Cos(angle) * radius, 0.0f, Mathf.Sin(angle) * radius);
            // Draw a point at the generation position
            Gizmos.DrawSphere(transform.position + generationPosition, 0.1f);
        }
    }
#endif
}
