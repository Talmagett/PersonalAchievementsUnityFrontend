using TMPro;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public class MultipleProgressTypeView : MonoBehaviour
{
    [SerializeField] private SliderTextView _sliderText;
    [SerializeField] private TMP_InputField _currentIF;
    [SerializeField] private TMP_InputField _targetIF;
    public SliderTextView SliderTextView => _sliderText;
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
            Redraw();
        }
    }
    public void OnMaxTargetChange(string value)
    {
        if (int.TryParse(value, out int result))
        {
            if (result <= 1)
                return;
            _sliderText.SetMaxValue(result);
        }
    }
    private void Redraw(Vector2 value)
    {
        _currentIF.text = value.x.ToString();
        _targetIF.text = value.y.ToString();
    }
    public void Redraw()
    {
        _currentIF.text = _sliderText.Current.ToString();
        _targetIF.text = _sliderText.MaxTarget.ToString();
    }
}