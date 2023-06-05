using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static LikeController;

public class SingleAchievementView : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _notesText;

    [Space]
    [SerializeField] private GameObject _singleProgressUnlockedGO;
    [SerializeField] private Toggle _isUnlockedToggle;

    [SerializeField] private GameObject _targetProgressGO;
    [SerializeField] private SliderTextView _sliderTextView;

    [SerializeField] private GameObject _infinityProgressGO;
    [SerializeField] private TMP_Text _infinityText;

    [Space]
    [SerializeField] private GameObject _isGlobalGO;
    [SerializeField] private GameObject _isPrivateGO;
    [SerializeField] private GameObject _isPublicGO;
    [SerializeField] private Toggle _likeToggle;
    [SerializeField] private TMP_Text _likesCount;
    [SerializeField] private GameObject _editBtnGO;
    [Inject] private MainMenuController _mainMenuController;

    public event Action<LikeDto> OnLikeClick;
    private AchievementDto _achievementDto;
    public event Action<int> OnAchievementShow;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    public void SetData(AchievementDto achievementDto)
    {
        _achievementDto = achievementDto;
        _titleText.text = achievementDto.name;
        _descriptionText.text = achievementDto.description;

        _notesText.text = achievementDto.notes;
        _singleProgressUnlockedGO.SetActive(achievementDto.progressType == AchievementDto.ProgressType.Single);
        _targetProgressGO.SetActive(achievementDto.progressType == AchievementDto.ProgressType.Multiple);
        _infinityProgressGO.SetActive(achievementDto.progressType == AchievementDto.ProgressType.Infinite);

        switch (achievementDto.progressType)
        {
            case AchievementDto.ProgressType.Single:
                _isUnlockedToggle.isOn = achievementDto.isUnlocked;
                break;
            case AchievementDto.ProgressType.Multiple:
                _sliderTextView.SetMaxValue(achievementDto.progressTarget.Value);
                _sliderTextView.SetValue(achievementDto.progressCurrent.Value);
                //slider
                break;
            case AchievementDto.ProgressType.Tasks:
                break;
            case AchievementDto.ProgressType.Infinite:
                _infinityText.text = achievementDto.progressCurrent.ToString();
                break;
            default:
                break;
        }
        _isGlobalGO.SetActive(achievementDto.isGlobal);
        _isPrivateGO.SetActive(achievementDto.isPrivate);
        _isPublicGO.SetActive(!achievementDto.isPrivate);
        _likeToggle.interactable = achievementDto.ownerId != AuthController.Id;
        OnAchievementShow?.Invoke((int)achievementDto.id);
    }
    public void SetLikes(LikeDto likeDto)
    {
        _likeToggle.isOn = likeDto.isLiked;
        _likesCount.text = likeDto.likesCount.ToString();
    }
    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
    }
    public void Edit()
    {
        _mainMenuController.ShowCrud(_achievementDto);
    }
    public void Like()
    {
        OnLikeClick?.Invoke(new LikeDto() { isLiked = _likeToggle.isOn, achievementId = (int)_achievementDto.id });
    }
}