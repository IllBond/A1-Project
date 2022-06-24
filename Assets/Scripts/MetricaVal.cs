using System;
using UnityEngine;
using UnityEngine.UI;

public class MetricaVal : MonoBehaviour
{
    public float value;
    public Slider slider;
    public Text val;

    void Awake()
    {
        value = slider.value;
        val.text = slider.value.ToString();
        slider.onValueChanged.AddListener(delegate { SetValSlider(); });
    }

    public void SetValSlider()
    {
        value = (float)Math.Round(slider.value, 1);
        slider.value = value;
        val.text = value.ToString();
    }
}
