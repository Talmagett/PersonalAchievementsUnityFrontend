using Newtonsoft.Json;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrudAchievementView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _titleIF;
    [SerializeField] private TMP_InputField _descriptionIF;

    [SerializeField] private IconViewItem _iconViewItem;

    [SerializeField] private ProgressTypeView _progressType;
    [SerializeField] private Toggle _isUnlockedToggle;
    [SerializeField] private Toggle _isPrivateToogle;
    [SerializeField] private GameObject _isGlobalGO;
    [SerializeField] private Toggle _isGlobalToggle;
    [SerializeField] private MultipleProgressTypeView _multipleProgressType;
    [SerializeField] private TasksProgressTypeView _tasksProgressType;
    [SerializeField] private InfinityProgressTypeView _infinityProgressTypeView;
    [SerializeField] private TMP_InputField _notesIF;
    [SerializeField] private GameObject _deleteBtnGO;

    public event Action<AchievementDto> OnClick;
    public event Action<int> OnDelete;
    private bool _isEdit;
    private AchievementDto _achievementDto;
    public void Save()
    {
        if (!ValidateInput())
            return;
        if (_achievementDto is null || _achievementDto.id is null)
        {
            _achievementDto = new AchievementDto();
        }
        _achievementDto.iconId = _iconViewItem.IconSO.IconId;
        _achievementDto.name = _titleIF.text;
        _achievementDto.description = string.IsNullOrWhiteSpace(_descriptionIF.text) ? null : _descriptionIF.text;
        _achievementDto.notes = string.IsNullOrWhiteSpace(_notesIF.text) ? null : _notesIF.text;
        _achievementDto.progressType = (AchievementDto.ProgressType)_progressType.SelectedItem;
        switch (_achievementDto.progressType)
        {
            case AchievementDto.ProgressType.Single:
                _achievementDto.isUnlocked = _isUnlockedToggle.isOn;
                break;
            case AchievementDto.ProgressType.Multiple:
                _achievementDto.progressCurrent = _multipleProgressType.SliderTextView.Current;
                _achievementDto.progressTarget = _multipleProgressType.SliderTextView.MaxTarget;
                _achievementDto.isUnlocked = _achievementDto.progressCurrent >= _achievementDto.progressTarget;
                break;
            case AchievementDto.ProgressType.Tasks:
                string data = JsonConvert.SerializeObject(_tasksProgressType.GetAllTasks(), formatting: Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                _achievementDto.tasks = data;
                break;
            case AchievementDto.ProgressType.Infinite:
                _achievementDto.progressCurrent = _infinityProgressTypeView.Value;
                break;
            default:
                break;
        }
        _achievementDto.isGlobal = _isGlobalToggle.isOn;
        _achievementDto.isPrivate = _isPrivateToogle.isOn;
        OnClick?.Invoke(_achievementDto);
        ClearData();
    }
    public void SetData(AchievementDto? achievementDto)
    {
        _achievementDto = achievementDto;
        _isGlobalGO.SetActive(AuthController.UserRole == AuthController.Role.Admin);

        if (achievementDto is null)
        {
            ClearData();
            _deleteBtnGO.SetActive(_isEdit);
            return;
        }
        _titleIF.text = achievementDto.name;
        _descriptionIF.text = achievementDto.description;
        _notesIF.text = achievementDto.notes;
        _progressType.SelectTab((int)achievementDto.progressType);
        _isGlobalToggle.isOn = achievementDto.isGlobal;
        _isPrivateToogle.isOn = achievementDto.isPrivate;

        switch (achievementDto.progressType)
        {
            case AchievementDto.ProgressType.Single:
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

        _isEdit = true;
        _deleteBtnGO.SetActive(_isEdit);
    }
    public void Delete()
    {
        if (_isEdit && _achievementDto.id is not null)
            OnDelete?.Invoke((int)_achievementDto.id);
    }
    public void ClearData()
    {
        _titleIF.text = "";
        _descriptionIF.text = "";
        _notesIF.text = "";
        _progressType.SelectTab(0);
        _isUnlockedToggle.isOn = false;
        _isPrivateToogle.isOn = false;
        _isEdit = false;
        _achievementDto = null;
        _deleteBtnGO.SetActive(false);

        _multipleProgressType.OnMaxTargetChange("10");
        _multipleProgressType.OnCurrentChange("3");
        _infinityProgressTypeView.SetValue(0);
        _tasksProgressType.SetTasks(null);
    }
    private bool ValidateInput()
    {
        bool isCorrect = true;
        if (string.IsNullOrWhiteSpace(_titleIF.text))
        {
            MessageController.Instance.AlertInputValidation(_titleIF);
            isCorrect = false;
        }

        return isCorrect;
    }
}