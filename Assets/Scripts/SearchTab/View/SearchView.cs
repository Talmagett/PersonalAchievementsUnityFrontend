using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchView : MonoBehaviour
{
    [System.Serializable]
    public class UserDto
    {
        public string id;
        public string username;
        public string ownedAchievements;
    }
    [SerializeField] private TMP_InputField _searchIF;
    [SerializeField] private Transform _searchResultParent;
    [SerializeField] private SearchUserElementView _searchUserElementView;
    [SerializeField] private SearchPersonInfoView _searchPersonInfoView;
    public event Action<string> OnClick;
    public void Search()
    {
        if (_searchIF.text is null)
            return;

        OnClick?.Invoke(_searchIF.text);
    }
    public void ShowResult(List<UserDto> users)
    {
        while (_searchResultParent.childCount > 0)
        {
            DestroyImmediate(_searchResultParent.GetChild(0).gameObject);
        }
        foreach (var user in users)
        {
            Instantiate(_searchUserElementView, _searchResultParent).SetData(user, _searchPersonInfoView);
        }
    }
}