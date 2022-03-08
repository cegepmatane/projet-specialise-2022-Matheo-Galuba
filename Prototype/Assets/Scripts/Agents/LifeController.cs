using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private Collider zoneCollider;
    [SerializeField]
    private float life = 100.0f;
    [SerializeField]
    private float zoneDamage = 1.0f;

    private bool isInsideZone = true;
    private Renderer renderer = null;
    private Color originalColor;
    private IEnumerator takeDamageCoroutine;
    private List<Collider> reachableGameObjects = new List<Collider>();

    public void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        StartCoroutine(takeZoneDamage());
    }

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

    public void takeDamage(float damage)
    {
        life -= damage;
        if (life <= 0.0f)
        {
            life = 0.0f;
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(redFlash());
        }
    }

    public float getLife()
    {
        return life;
    }

    IEnumerator takeZoneDamage()
    {
        while (true)
        {
            if (isInsideZone == false)
            {
                Debug.Log("Damage");
                takeDamage(zoneDamage);

                // Red color effect
                // for (float f = 1f; f >= 0; f -= 0.1f)
                // {
                //     renderer.material.color = Color.Lerp(originalColor, Color.red, f);
                //     yield return new WaitForSeconds(0.1f);
                // }

                // Cooldown
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    IEnumerator redFlash()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            renderer.material.color = Color.Lerp(originalColor, Color.red, f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
