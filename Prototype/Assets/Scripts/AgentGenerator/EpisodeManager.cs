using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeManager : MonoBehaviour
{
    public void Update()
    {
        // check the number of children
        if (transform.childCount <= 1)
        {
            // if there is only one child, it means that the episode is over
            // so we can destroy the episode manager
            resetScene();
        }
    }

    private void resetScene()
    {
        // Destroy all children objects
        foreach (Transform child in transform)
        {
            transform.gameObject.GetComponent<AgentController>().AddReward(20.0f);
            Destroy(child.gameObject);
        }

        // Destroy all the ui objects
        Canvas canvas = FindObjectsOfType<Canvas>()[0];
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }

        // Call the AgentGenerator to generate new agents
        GetComponent<AgentGenerator>().spawnAgents();
    }
}
