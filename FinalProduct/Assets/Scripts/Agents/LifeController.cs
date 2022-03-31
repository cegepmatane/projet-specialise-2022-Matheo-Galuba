using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private float life = 100.0f;

    // private Renderer renderer = null;
    private IEnumerator takeDamageCoroutine;
    private bool isInvincible = false;
    private EnemyChecker enemyChecker;
    private Animator animator;

    public void Start()
    {
        enemyChecker = GetComponentInChildren<EnemyChecker>();

        animator = GetComponent<Animator>();
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
            GetComponent<AgentController>().AddReward(-50f);
            StartCoroutine(damageCoroutine());
        }
    }

    public float getLife()
    {
        return life;
    }

    IEnumerator damageCoroutine()
    {
        isInvincible = true;

        for (float f = 1f; f >= 0; f -= 0.04f)
        {
            animator.SetTrigger("Take Damage");
            yield return new WaitForSeconds(0.01f);
        }

        isInvincible = false;
    }
}
