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

        Gizmos.color = Color.yellow;

        int populationPerArea = populationSize / spawnAreaColliders.Count;

        foreach (BoxCollider spawnAreaCollider in spawnAreaColliders)
        {
            float minDistanceBetweenAgents = Mathf.Sqrt(spawnAreaCollider.size.x * spawnAreaCollider.size.z / populationPerArea);
            int numberOfRows = Mathf.CeilToInt(spawnAreaCollider.size.x / minDistanceBetweenAgents);
            int numberOfColumns = Mathf.CeilToInt(spawnAreaCollider.size.z / minDistanceBetweenAgents);
            int agentGeneratedCounter = 0;

            for (int i = 0; i <= numberOfRows; ++i)
            {
                float x = i * spawnAreaCollider.size.x / numberOfRows + spawnAreaCollider.transform.position.x - spawnAreaCollider.size.x / 2;

                for (int j = 0; j < numberOfColumns; ++j)
                {
                    if (agentGeneratedCounter < populationPerArea)
                    {
                        float z = j * spawnAreaCollider.size.z / numberOfColumns + spawnAreaCollider.transform.position.z - spawnAreaCollider.size.z / 2;

                        Vector3 generationPosition = new Vector3(x, transform.position.y, z);
                        Quaternion generationRotation = Quaternion.Euler(0, 0, 0);
                        GameObject agent = Instantiate(agentPrefab, generationPosition, generationRotation);
                        agent.transform.parent = transform;
                        agent.name = "Agent " + i;
                        agent.GetComponent<AgentController>().setTeamId(i);
                        agents.Add(agent);
                        GameObject agentLifeUI = Instantiate(agentLifeUIPrefab);
                        agentLifeUI.transform.SetParent(canvas.transform);
                        agentLifeUI.name = "AgentLifeUI " + i;
                        agentLifeUI.GetComponent<AgentLifeUI>().SetAgent(agent);

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

        int populationPerArea = populationSize / spawnAreaColliders.Count;

        foreach (BoxCollider spawnAreaCollider in spawnAreaColliders)
        {
            float minDistanceBetweenAgents = Mathf.Sqrt(spawnAreaCollider.size.x * spawnAreaCollider.size.z / populationPerArea);
            int numberOfRows = Mathf.CeilToInt(spawnAreaCollider.size.x / minDistanceBetweenAgents);
            int numberOfColumns = Mathf.CeilToInt(spawnAreaCollider.size.z / minDistanceBetweenAgents);
            int agentGeneratedCounter = 0;

            for (int i = 0; i <= numberOfRows; ++i)
            {
                float x = i * spawnAreaCollider.size.x / numberOfRows + spawnAreaCollider.transform.position.x - spawnAreaCollider.size.x / 2;

                for (int j = 0; j < numberOfColumns; ++j)
                {
                    if (agentGeneratedCounter < populationPerArea)
                    {
                        float z = j * spawnAreaCollider.size.z / numberOfColumns + spawnAreaCollider.transform.position.z - spawnAreaCollider.size.z / 2;

                        Vector3 generationPosition = new Vector3(x, transform.position.y, z);
                        // Draw a point at the generation position
                        Gizmos.DrawSphere(spawnAreaCollider.center + generationPosition, 0.1f);
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
