using UnityEngine;
using DG.Tweening;
public class MainMenuView : TabUI
{
    [SerializeField] private RectTransform _focusRT;
    [SerializeField] private Transform[] _tabbingObjects;
    public override void SelectTab(int id)
    {
        base.SelectTab(id);
        _focusRT.transform.SetParent(_tabbingObjects[id]);
        _focusRT.DOLocalMove(Vector2.zero,0.2f);
    }
}