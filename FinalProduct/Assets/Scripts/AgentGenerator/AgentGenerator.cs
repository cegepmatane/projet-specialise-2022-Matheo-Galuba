using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGenerator : MonoBehaviour
{
    // Generation settings
    [Header("Generation")]
    [SerializeField]
    protected int populationSize;
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
        // Get the game manager and set the population size
        populationSize = FindObjectOfType<GameManager>().GetPopulation();
        spawnAgents();
    }

    public virtual void spawnAgents()
    {
        // To be implemented by the child class
    }
}
