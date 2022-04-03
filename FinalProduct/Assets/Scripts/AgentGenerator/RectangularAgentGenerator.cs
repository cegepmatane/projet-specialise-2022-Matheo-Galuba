using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RectangularAgentGenerator : AgentGenerator
{
    // Generation settings
    [Header("Rectangular Generation")]
    [SerializeField]
    protected List<BoxCollider> spawnAreaColliders = new List<BoxCollider>();

    public override void spawnAgents()
    {
        if (spawnAreaColliders.Count <= 0)
        {
            return;
        }

        int populationPerArea = Mathf.CeilToInt((float)populationSize / (float)spawnAreaColliders.Count);
        int agentGeneratedCounter = 0;

        foreach (BoxCollider spawnAreaCollider in spawnAreaColliders)
        {
            float minDistanceBetweenAgents = Mathf.Sqrt(spawnAreaCollider.size.x * spawnAreaCollider.size.z / populationPerArea);
            int numberOfRows = Mathf.CeilToInt(spawnAreaCollider.size.x / minDistanceBetweenAgents);
            int numberOfColumns = Mathf.CeilToInt(spawnAreaCollider.size.z / minDistanceBetweenAgents) - 1;
            int agentGeneratedInAreaCounter = 0;

            for (int i = 0; i < numberOfRows; ++i)
            {
                float x = i * spawnAreaCollider.size.x / (numberOfRows - 1) + spawnAreaCollider.transform.position.x - spawnAreaCollider.size.x / 2;

                for (int j = 0; j < numberOfColumns; ++j)
                {
                    if (agentGeneratedInAreaCounter < populationPerArea && agentGeneratedCounter < populationSize)
                    {
                        float z = j * spawnAreaCollider.size.z / (numberOfColumns - 1) + spawnAreaCollider.transform.position.z - spawnAreaCollider.size.z / 2;

                        Vector3 generationPosition = new Vector3(x, transform.position.y, z);
                        Quaternion generationRotation = Quaternion.Euler(0, 0, 0);
                        GameObject agent = Instantiate(agentPrefab, spawnAreaCollider.center + generationPosition, generationRotation);
                        agent.transform.parent = transform;
                        agent.name = "Agent " + agentGeneratedCounter;
                        agent.GetComponent<AgentController>().setTeamId(i);
                        agents.Add(agent);
                        GameObject agentLifeUI = Instantiate(agentLifeUIPrefab);
                        agentLifeUI.transform.SetParent(canvas.transform);
                        agentLifeUI.name = "AgentLifeUI " + agentGeneratedCounter;
                        agentLifeUI.GetComponent<AgentLifeUI>().SetAgent(agent);

                        agentGeneratedInAreaCounter++;
                        agentGeneratedCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (spawnAreaColliders.Count <= 0)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        int populationPerArea = Mathf.CeilToInt((float)populationSize / (float)spawnAreaColliders.Count);
        int agentGeneratedCounter = 0;

        foreach (BoxCollider spawnAreaCollider in spawnAreaColliders)
        {
            float minDistanceBetweenAgents = Mathf.Sqrt(spawnAreaCollider.size.x * spawnAreaCollider.size.z / populationPerArea);
            int numberOfRows = Mathf.CeilToInt(spawnAreaCollider.size.x / minDistanceBetweenAgents);
            int numberOfColumns = Mathf.CeilToInt(spawnAreaCollider.size.z / minDistanceBetweenAgents) - 1;
            int agentGeneratedInAreaCounter = 0;

            for (int i = 0; i < numberOfRows; ++i)
            {
                float x = i * spawnAreaCollider.size.x / (numberOfRows - 1) + spawnAreaCollider.transform.position.x - spawnAreaCollider.size.x / 2;

                for (int j = 0; j < numberOfColumns; ++j)
                {
                    if (agentGeneratedInAreaCounter < populationPerArea && agentGeneratedCounter < populationSize)
                    {
                        float z = j * spawnAreaCollider.size.z / (numberOfColumns - 1) + spawnAreaCollider.transform.position.z - spawnAreaCollider.size.z / 2;

                        Vector3 generationPosition = new Vector3(x, transform.position.y, z);
                        // Draw a point at the generation position
                        Gizmos.DrawSphere(spawnAreaCollider.center + generationPosition, 0.1f);
                        agentGeneratedInAreaCounter++;
                        agentGeneratedCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
#endif
}
