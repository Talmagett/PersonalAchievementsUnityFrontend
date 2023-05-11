using TMPro;
using UnityEngine;

public class MultipleProgressTypeView : MonoBehaviour
{
    [SerializeField] private SliderTextView _sliderText;
    [SerializeField] private TMP_InputField _currentIF;
    [SerializeField] private TMP_InputField _targetIF;

    private void OnEnable()
    {
        _sliderText.OnChanged += Redraw;
    }
    private void OnDisable()
    {
        _sliderText.OnChanged -= Redraw;
    }

    public void OnCurrentChange(string value)
    {
        if (int.TryParse(value, out int result))
        {
            _sliderText.SetValue(result);
        }
    }
    public void OnTargetChange(string value)
    {
        if (int.TryParse(value, out int result))
        {
            if (result <= 0)
                return;
            _sliderText.SetMaxValue(result);
        }
    }
    private void Redraw(Vector2 value)
    {
        _currentIF.text = value.x.ToString();
        _targetIF.text = value.y.ToString();
    }
}