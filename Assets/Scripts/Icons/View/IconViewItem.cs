using UnityEngine;
using UnityEngine.UI;

public class IconViewItem : MonoBehaviour
{
    [SerializeField] private IconsSO _iconsSO;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    public IconsSO IconSO => _iconsSO;
    public void SetIconSO(IconsSO iconsSO, IconView iconView = null, IconViewItem iconViewItem = null)
    {
        _iconsSO = iconsSO;

        _image ??= GetComponent<Image>();
        _button ??= GetComponent<Button>();
        _image.sprite = _iconsSO.Sprite;
        _button.onClick.AddListener(() =>
        {
            if (iconView is not null)
                iconView.gameObject.SetActive(false);
            if (iconViewItem is not null)
                iconViewItem.SetIconSO(_iconsSO);
        });
    }
}