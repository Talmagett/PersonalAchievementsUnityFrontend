using UnityEngine;

public class TabUI : MonoBehaviour
{
    [SerializeField] protected GameObject[] _tabGObjects;
    public virtual void SelectTab(int id)
    {
        for (int i = 0; i < _tabGObjects.Length; i++)
        {
            _tabGObjects[i].SetActive(id == i);
        }
    }
}