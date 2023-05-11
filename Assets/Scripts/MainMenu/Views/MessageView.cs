using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    [SerializeField] private RectTransform _popupRTransform;
    [SerializeField] private TextMeshProUGUI _messageTitleTMP;
    [SerializeField] private TextMeshProUGUI _messageTextTMP;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _submitButton;
    public Button SubmitButton => _submitButton;
    public void SetTitle(string title)
    {
        _messageTitleTMP.text = title;
    }
    public void SetDescription(string text)
    {
        _messageTextTMP.text = text;
        var childHeight = _messageTextTMP.preferredHeight;
        Vector2 parentSize = _popupRTransform.sizeDelta;
        parentSize.y = Mathf.Clamp(childHeight + 400, 560, Screen.height - 400);
        _popupRTransform.sizeDelta = parentSize;
    }
    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon ?? _icon.sprite;
    }
}
