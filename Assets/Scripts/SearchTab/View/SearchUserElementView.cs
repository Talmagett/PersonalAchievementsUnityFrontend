using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SearchView;

public class SearchUserElementView : MonoBehaviour
{
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Button _userBtn;
    private string _id;
    public void SetData(UserDto userDto, SearchPersonInfoView onClick)
    {
        _id = userDto.id;
        _usernameText.text = userDto.username;
        _countText.text = userDto.ownedAchievements;
        _userBtn.onClick.AddListener(() => onClick.SetData(userDto));
    }
    private void OnDestroy()
    {
        _userBtn.onClick.RemoveAllListeners();
    }
}