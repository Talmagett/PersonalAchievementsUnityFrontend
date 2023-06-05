using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static SearchView;

public class SearchPersonInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private SliderTextView _achievementsCountSlider;
    [SerializeField] private Button _viewAchievementsButton;
    [SerializeField] private GameObject _banningArea;
    private string _id = null;
    [Inject] private MainMenuController _mainMenuController;
    public string Id => _id;
    public void SetData(UserDto userDto)
    {
        _usernameText.text = userDto.username;
        _id = userDto.id;
        string[] minMax = userDto.ownedAchievements.Split('/');
        if (int.TryParse(minMax[0], out int current))
        {
            _achievementsCountSlider.SetValue(current);
        }
        if (int.TryParse(minMax[1], out int max))
        {
            _achievementsCountSlider.SetMaxValue(max);
            _viewAchievementsButton.interactable = max > 0;
        }
        _banningArea.SetActive(AuthController.UserRole==AuthController.Role.Admin);
        gameObject.SetActive(true);
    }

    public void ViewAchievementOfUser()
    {
        if (_id == null)
            return;

        gameObject.SetActive(false);
        _mainMenuController.ShowAchievements(_id);
    }
}