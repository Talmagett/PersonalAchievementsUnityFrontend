using System;
using TMPro;
using UnityEngine;
using static ProfileController;

public class ProfileView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameIF;
    [SerializeField] private TMP_InputField _emailIF;
    [SerializeField] private TMP_InputField _firstNameIF;
    [SerializeField] private TMP_InputField _lastNameIF;
    [SerializeField] private TMP_InputField _phoneNumberIF;

    public event Action OnSignOutClicked;
    public event Action<UserAccountDataDto> OnSaveClicked;
    public void SignOut()
    {
        ClearData();
        OnSignOutClicked?.Invoke();
    }
    public void Show(UserAccountDataDto result)
    {
        _usernameIF.text = result.userName;
        _emailIF.text = result.email;
        _firstNameIF.text = result.firstName;
        _lastNameIF.text = result.lastName;
        _phoneNumberIF.text = result.phoneNumber;
    }
    private void ClearData()
    {
        _usernameIF.text = "";
        _emailIF.text = "";
        _firstNameIF.text = "";
        _lastNameIF.text = "";
        _phoneNumberIF.text = "";
    }
    public void Save()
    {
        var userData = CollectData();
        if (userData is not null)
            OnSaveClicked?.Invoke(userData);
    }

    private UserAccountDataDto CollectData()
    {
        bool isValidated = true;
        if (string.IsNullOrWhiteSpace(_emailIF.text))
        {
            MessageController.Instance.AlertInputValidation(_emailIF);
            isValidated = false;
        }
        if (string.IsNullOrWhiteSpace(_usernameIF.text))
        {
            MessageController.Instance.AlertInputValidation(_usernameIF);
            isValidated = false;
        }

        if (!isValidated)
            return null;

        UserAccountDataDto userData = new UserAccountDataDto();
        userData.userName = _usernameIF.text;
        userData.email = _emailIF.text;
        userData.firstName = _firstNameIF.text;
        userData.lastName = _lastNameIF.text;
        userData.phoneNumber = _phoneNumberIF.text;
        return userData;
    }
}