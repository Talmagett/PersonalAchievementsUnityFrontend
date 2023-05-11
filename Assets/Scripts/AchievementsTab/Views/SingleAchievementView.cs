using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SingleAchievementView : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _notesText;
    [SerializeField] private Toggle _isUnlockedToggle;
    [SerializeField] private GameObject _isGlobalGO;
    [SerializeField] private GameObject _isPrivateGO;

    public void SetData()
    {

    }
    public SingleAchievementView SetTitle(string title)
    {
        _titleText.text = title;
        return this;
    }
    public SingleAchievementView SetDescription(string value)
    {
        _descriptionText.text = value;
        return this;
    }
    public SingleAchievementView SetImage(Sprite icon)
    {
        _iconImage.sprite = icon;
        return this;
    }
    public SingleAchievementView SetNotes(string value)
    {
        _notesText.text = value;
        return this;
    }
    public SingleAchievementView SetUnlocked(bool value)
    {
        _isUnlockedToggle.isOn = value;
        return this;
    }
    public SingleAchievementView SetGlobal(bool value)
    {
        _isGlobalGO.SetActive(value);
        return this;
    }
    public SingleAchievementView SetPrivate(bool value)
    {
        _isPrivateGO.SetActive(value);
        return this;
    }
    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
    }
}
