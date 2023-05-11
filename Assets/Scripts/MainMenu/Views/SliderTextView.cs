using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _valuesText;

    public int Current => (int)_slider.value;
    public int MaxTarget => (int)_slider.maxValue;

    private Slider _slider;
    public event Action<Vector2> OnChanged;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        Redraw();
    }

    public void OnValueChanged(float value)
    {
        OnChanged?.Invoke(new Vector2(_slider.value, _slider.maxValue));
        Redraw();
    }
    public void Redraw()
    {
        _valuesText.text = $"{_slider.value}/{_slider.maxValue}";
    }
    public void SetValue(float value)
    {
        _slider.value = value;
        Redraw();
    }
    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
        Redraw();
    }
}