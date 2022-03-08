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
    }
}
