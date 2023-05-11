using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CrudAchievementController;

public class CrudAchievementView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _titleIF;
    [SerializeField] private TMP_InputField _descriptionIF;

    [SerializeField] private ProgressTypeView _progressType;
    [SerializeField] private Toggle _isUnlockedToggle;
    [SerializeField] private MultipleProgressTypeView _multipleProgressType;
    [SerializeField] private TMP_InputField _notesIF;
    public event Action<CrudAchievement> OnClick;
    private bool _isEdit;
    private int _editingAchievementId;
    public void Save()
    {
        if (!ValidateInput())
            return;
        CrudAchievement achievement = new CrudAchievement();
        achievement.name = _titleIF.text;
        achievement.description = string.IsNullOrWhiteSpace(_descriptionIF.text) ? null : _descriptionIF.text;
        achievement.notes = string.IsNullOrWhiteSpace(_notesIF.text) ? null : _notesIF.text;
        achievement.progressType = (ProgressType)_progressType.SelectedItem;
        switch (achievement.progressType)
        {
            case ProgressType.Single:
                achievement.isUnlocked = _isUnlockedToggle.isOn;
                break;
            case ProgressType.Multiple:
                break;
            case ProgressType.Tasks:
                break;
            case ProgressType.Infinite:
                break;
            default:
                break;
        }
        achievement.isGlobal = false;
        achievement.lockedIconId = 1;
        achievement.unlockedIconId = 2;
        achievement.isPrivate = false;

        /*
    public string? OwnerId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public int? GlobalId { get; set; }
    public int? ProgressTarget { get; set; }
    public int? ProgressCurrent { get; set; }
    public string? Tasks { get; set; }
         */

        achievement.id = _isEdit ? _editingAchievementId : null;
        achievement.ownerId = null;
        achievement.creatorId = null;
        achievement.tasks = null;
        achievement.id = null;
        OnClick?.Invoke(achievement);
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