using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private float life = 100.0f;

    private Renderer renderer = null;
    private Color originalColor;
    private IEnumerator takeDamageCoroutine;
    private bool isInvincible = false;
    private EnemyChecker enemyChecker;

    public void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        enemyChecker = GetComponentInChildren<EnemyChecker>();
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

        isInvincible = false;
    }
}
