using UnityEngine;
using TMPro;

public class AgentLifeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject agent;
    [SerializeField]
    private float textVerticalOffset = 0.0f;

    private TextMeshProUGUI text;

    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        float life = agent.GetComponent<LifeController>().getLife();
        text.text = life.ToString("F0") + "%";

        Vector3 screenPos = Camera.main.WorldToScreenPoint(agent.transform.position);
        transform.position = screenPos + new Vector3(0.0f, textVerticalOffset, 0.0f);
    }
}
