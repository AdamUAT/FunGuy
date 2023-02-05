using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class LabeledSlider : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    TextMeshProUGUI valueText;

    [SerializeField]
    UnityEvent<float> onValueChanged;

    // Start is called before the first frame update
    void Start()
    {
        SetLabel(slider.value);
        // Listen to the slider for changes to its value
        slider.onValueChanged.AddListener(delegate {UpdateValue(); });
    }

    public void SetValue(float sliderValue)
    {
        slider.value = sliderValue;
    }

    public void UpdateValue()
    {
        SetLabel(slider.value);
        onValueChanged.Invoke(slider.value);
    }

    private void SetLabel(float sliderValue)
    {
        valueText.text = String.Format("{0:0}%", sliderValue * 100.0 );
    }
}
