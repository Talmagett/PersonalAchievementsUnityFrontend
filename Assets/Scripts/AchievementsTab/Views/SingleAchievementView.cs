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
    [SerializeField] private GameObject _singleProgressUnlockedGO;
    [SerializeField] private Toggle _isUnlockedToggle;

    [SerializeField] private GameObject _isGlobalGO;
    [SerializeField] private GameObject _isPrivateGO;
    [SerializeField] private GameObject _isPublicGO;
    [SerializeField] private Toggle _likeToggle;
    [SerializeField] private TMP_Text _likesCount;
    [SerializeField] private GameObject _editBtnGO;
    [Inject] private MainMenuController _mainMenuController;

    public event Action<LikeDto> OnLikeClick;
    private AchievementDto _achievementDto;

    public void SetData(AchievementDto achievementDto)
    {
        _achievementDto = achievementDto;
        _titleText.text = achievementDto.name;
        _descriptionText.text = achievementDto.description;

        _notesText.text = achievementDto.notes;
        //_iconImage.sprite = achievementDto.isUnlocked ? achievementDto.unlockedIconId : achievementDto.lockedIconId;

        switch (achievementDto.progressType)
        {
            case AchievementDto.ProgressType.Single:
                _singleProgressUnlockedGO.SetActive(true);
                _isUnlockedToggle.isOn = achievementDto.isUnlocked;
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
        _isGlobalGO.SetActive(achievementDto.isGlobal);
        _isPrivateGO.SetActive(achievementDto.isPrivate);
        _isPublicGO.SetActive(!achievementDto.isPrivate);
        _likeToggle.interactable = achievementDto.ownerId != AuthController.Id;
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