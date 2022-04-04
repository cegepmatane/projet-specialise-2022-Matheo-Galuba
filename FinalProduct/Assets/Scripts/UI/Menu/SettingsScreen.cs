using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScreen : MonoBehaviour
{
    [Header("Map Settings")]
    public TMP_Dropdown _mapDropdown;

    [Header("Population Settings")]
    [SerializeField]
    private Slider _populationSlider;
    [SerializeField]
    private TMP_Text _populationReactiveText;

    private void Start()
    {
        _populationReactiveText.text = _populationSlider.value.ToString();
    }

    public void OnMapDropdownValueChanged()
    {
        int value = _mapDropdown.value;
        GameManager.Instance.SetMapType((Map)value);
    }

    public void OnPopulationSliderValueChanged()
    {
        int value = (int)_populationSlider.value;
        GameManager.Instance.SetPopulation(value);
        _populationReactiveText.text = value.ToString();
    }
}
