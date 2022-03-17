using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField]
    private float narrowingSpeed = 1.0f;
    [SerializeField]
    private float startSize = 60.0f;
    [SerializeField]
    private float endSize = 10.0f;

    private List<GameObject> agentsOutsideOfTheZone = new List<GameObject>();

    public void Start()
    {
        transform.localScale = new Vector3(startSize, startSize, startSize);
    }

    public void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x - narrowingSpeed * Time.deltaTime, transform.localScale.y - narrowingSpeed * Time.deltaTime, transform.localScale.z - narrowingSpeed * Time.deltaTime);
        if (transform.localScale.x <= endSize)
        {
            Destroy(gameObject);
        }

        if (agentsOutsideOfTheZone.Count > 0)
        {
            foreach (GameObject agent in agentsOutsideOfTheZone)
            {
                if (agent == null)
                {
                    agentsOutsideOfTheZone.Remove(agent);
                }
                else
                {
                    agent.GetComponent<LifeController>().takeDamage(5.0f);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Agent")
        {
            agentsOutsideOfTheZone.Remove(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Agent")
        {
            agentsOutsideOfTheZone.Add(other.gameObject);
        }
    }
}
