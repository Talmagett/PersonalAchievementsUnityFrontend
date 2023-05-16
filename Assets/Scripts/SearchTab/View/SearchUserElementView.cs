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
    public void SetData(UserDto userDto, SearchPersonInfoView onClick)
    {
        _usernameText.text = userDto.username;
        _countText.text = userDto.ownedAchievements;
        _userBtn.onClick.AddListener(() => onClick.SetData(userDto.username,userDto.ownedAchievements));
    }
    private void OnDestroy()
    {
        _userBtn.onClick.RemoveAllListeners();
    }
}