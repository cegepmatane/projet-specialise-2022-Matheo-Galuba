using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyChecker : MonoBehaviour
{
    public List<GameObject> reachableGameObjects = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Agent")
        {
            reachableGameObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Agent")
        {
            reachableGameObjects.Remove(other.gameObject);
        }
    }
}
