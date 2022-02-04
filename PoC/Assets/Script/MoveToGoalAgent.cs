using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class MoveToGoalAgent : Agent
{
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("OnActionReceived");
        Debug.Log("Action: " + actions.DiscreteActions[0]);
    }
}
