using UnityEngine;
using DG.Tweening;
public class MainMenuView : TabUI
{
    [SerializeField] private RectTransform _focusRT;
    public override void SelectTab(int id)
    {
        base.SelectTab(id);
        _focusRT.DOLocalMove(Vector2.zero,0.2f);
    }
}