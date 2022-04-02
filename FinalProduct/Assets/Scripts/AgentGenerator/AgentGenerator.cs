using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGenerator : MonoBehaviour
{
    // Generation settings
    [Header("Generation")]
    [SerializeField]
    protected int populationSize = 10;
    [SerializeField]
    protected GameObject agentPrefab;
    [SerializeField]
    protected Collider zoneCollider;

    // UI settings
    [Header("UI")]
    [SerializeField]
    protected Canvas canvas;
    [SerializeField]
    protected GameObject agentLifeUIPrefab;

    protected List<GameObject> agents = new List<GameObject>();

    public void Start()
    {
        spawnAgents();
    }

    public virtual void spawnAgents()
    {
        // To be implemented by the child class
    }

    public void setPopulationSize(int populationSize)
    {
        this.populationSize = populationSize;
    }
}
