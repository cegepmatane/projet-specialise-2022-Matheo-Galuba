using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    void OnMapDropdownValueChanged(int value)
    {
        GameManager.Instance.SetMapType((Map)value);
    }

    void OnPopulationSliderValueChanged(int value)
    {
        GameManager.Instance.SetPopulation((int)value);
    }
}
