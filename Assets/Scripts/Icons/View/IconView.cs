using UnityEngine;

public class IconView : MonoBehaviour
{
    [SerializeField] private Transform _iconsParent;
    [SerializeField] private IconViewItem _iconViewItem;
    [SerializeField] private IconsSO[] _iconsSO;
    [SerializeField] private IconViewItem _achievementIcon;
    public void CreateIcons()
    {
        while (_iconsParent.childCount > 0)
            DestroyImmediate(_iconsParent.GetChild(0).gameObject);

        foreach (var item in _iconsSO)
        {
            Instantiate(_iconViewItem, _iconsParent).SetIconSO(item, this, _achievementIcon);
        }
    }
}