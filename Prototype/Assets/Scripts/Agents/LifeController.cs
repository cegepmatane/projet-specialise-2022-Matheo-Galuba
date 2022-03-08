using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private Collider zoneCollider;
    [SerializeField]
    private float life = 100.0f;

    private bool isInsideZone = true;
    private List<Collider> reachableGameObjects = new List<Collider>();

    public void OnTriggerEnter(Collider other)
    {
        if (other == zoneCollider)
        {
            isInsideZone = true;
        }
        if (other.gameObject.tag == "Agent")
        {
            reachableGameObjects.Add(other);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == zoneCollider)
        {
            isInsideZone = false;
        }
        if (other.gameObject.tag == "Agent")
        {
            reachableGameObjects.Remove(other);
        }
    }

    public void fixedUpdate()
    {
        if (isInsideZone)
        {
            takeDamage(0.5f);
        }
    }

    public void takeDamage(float damage)
    {
        life -= damage;
    }

    public float getLife()
    {
        return life;
    }
}
