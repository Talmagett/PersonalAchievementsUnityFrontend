using TMPro;
using UnityEngine;

public class InfinityProgressTypeView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _currentIF;
    private int _progress;
    public int Value => _progress;
    public void SetValue(int value)
    {
        _progress = value;
        Validate();
    }
    public void OnFinishEnter(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _progress = 0;
        }
        else
        {
            if (int.TryParse(value, out int result))
                _progress = result;
        }
        Validate();
    }
    private void Validate()
    {
        if (_progress < 0)
            _progress = 0;
        _currentIF.text = _progress.ToString();
    }
    public void Increment()
    {
        _progress++;
        Validate();
    }
    public void Decrement()
    {
        _progress--;
        Validate();
    }
}