using TMPro;
using UnityEngine;
using static SearchView;

public class SearchUserElementView : MonoBehaviour
{
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private TMP_Text _countText;
    public void SetData(UserDto userDto)
    {
        _usernameText.text = userDto.username;
        _countText.text = userDto.count;
    }
}