using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private Collider zoneCollider;
    [SerializeField]
    private float life = 100.0f;

    private bool isInsideZone = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other == zoneCollider)
        {
            isInsideZone = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == zoneCollider)
        {
            isInsideZone = false;
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
}
