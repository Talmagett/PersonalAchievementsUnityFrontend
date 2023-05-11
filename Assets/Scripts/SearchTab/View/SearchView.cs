using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static SearchView;

public class SearchView : MonoBehaviour
{

    [System.Serializable]
    public class UserDto
    {
        public string username;
        public string count;
    }
    [SerializeField] private TMP_InputField _searchIF;
    [SerializeField] private Transform _searchResultParent;
    [SerializeField] private SearchUserElementView _searchUserElementView;
    public event Action<string> OnClick;
    public void Search()
    {
        if (_searchIF.text is null)
            return;

        OnClick?.Invoke(_searchIF.text);
    }
    public void ShowResult(IEnumerable<UserDto> users)
    {

    }
}