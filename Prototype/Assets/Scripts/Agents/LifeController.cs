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

    private bool isOutsideZone = false;
    private Renderer renderer = null;
    private Color originalColor;
    private IEnumerator takeDamageCoroutine;
    private bool isInvincible = false;
    private List<Collider> reachableGameObjects = new List<Collider>();

    public void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == zoneCollider)
        {
            isOutsideZone = false;
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
            isOutsideZone = true;
        }
        if (other.gameObject.tag == "Agent")
        {
            reachableGameObjects.Remove(other);
        }
    }

    public void Update()
    {
        if (isOutsideZone)
        {
            takeDamage(zoneDamage);
        }
    }

    public void takeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }

        life -= damage;
        if (life <= 0.0f)
        {
            life = 0.0f;
            Destroy(gameObject);
        }
        else
        {
            GetComponent<AgentController>().punish(damage);
            StartCoroutine(redFlash());
        }
    }

    public float getLife()
    {
        return life;
    }

    IEnumerator redFlash()
    {
        isInvincible = true;

        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            renderer.material.color = Color.Lerp(originalColor, Color.red, f);
            yield return new WaitForSeconds(0.01f);
        }

        // Cool down
        yield return new WaitForSeconds(0.5f);

        isInvincible = false;
    }
}
