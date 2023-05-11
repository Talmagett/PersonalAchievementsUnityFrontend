using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private Transform _loadingIconTransform;
    [Button]
    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
        if (value)
            _loadingIconTransform.DORotate(Vector3.forward * 180, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        else
        {
            _loadingIconTransform.DORewind();
            _loadingIconTransform.DOKill();
        }
    }
}