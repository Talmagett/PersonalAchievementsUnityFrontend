using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private GameObject _isSingleGO;
    [SerializeField] private GameObject _isUnlockedGO;
    [SerializeField] private SliderTextView _slider;
    [SerializeField] private Button _button;
    private SingleAchievementView _singleAchievementView;
    private AchievementDto _achievementDto;
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    public void SetData(AchievementDto achievementDto, SingleAchievementView singleAchievementView)
    {
        _singleAchievementView = singleAchievementView;
        _achievementDto = achievementDto;
        _titleText.text = _achievementDto.name;
        switch (_achievementDto.progressType)
        {
            case AchievementDto.ProgressType.Single:
                _isSingleGO.SetActive(true);
                _isUnlockedGO.SetActive(_achievementDto.isUnlocked);
                break;
            case AchievementDto.ProgressType.Multiple:
                break;
            case AchievementDto.ProgressType.Tasks:
                break;
            case AchievementDto.ProgressType.Infinite:
                break;
            default:
                break;
        }

        _button.onClick.AddListener(() => ShowCard(achievementDto));
    }
    public void ShowCard(AchievementDto achievementDto)
    {
        _singleAchievementView.SetData(achievementDto);
        _singleAchievementView.SetVisible(true);
    }
}