using UnityEngine;
using TMPro;

public class AgentLifeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject agent;
    [SerializeField]
    private float textVerticalOffset = 0.0f;
    [SerializeField]
    private Camera mainCamera;

    private TextMeshProUGUI text;
    private RectTransform sliderTransform;

    public void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        sliderTransform = transform.Find("Foreground inside").GetComponent<RectTransform>();
        mainCamera = FindObjectOfType<Camera>();
    }

    public void Update()
    {
        if (agent != null)
        {
            float life = agent.GetComponent<LifeController>().getLife();
            text.text = life.ToString("F0") + "%";
            sliderTransform.anchorMax = new Vector2(life / 100, 1);

            Vector3 screenPosition = mainCamera.WorldToScreenPoint(agent.transform.position);
            transform.position = screenPosition + new Vector3(0.0f, textVerticalOffset, 0.0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAgent(GameObject agent)
    {
        this.agent = agent;
    }
}
