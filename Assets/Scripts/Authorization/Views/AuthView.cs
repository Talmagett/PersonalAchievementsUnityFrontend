using System;
using TMPro;
using UnityEngine;

public class AuthView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailIF;
    [SerializeField] private TMP_InputField _passwordIF;

    public event Action<AuthController.UserData> OnLogInClick;
    public event Action<AuthController.UserData> OnSignUpClick;
    public void Login()
    {
        var user = CollectData();
        if (user is not null)
            OnLogInClick?.Invoke(user);
    }
    public void SignUp()
    {
        var user = CollectData();
        if (user is not null)
            OnSignUpClick?.Invoke(user);
    }
    private AuthController.UserData CollectData()
    {
        bool isValidated = true;
        if (string.IsNullOrWhiteSpace(_emailIF.text))
        {
            MessageController.Instance.AlertInputValidation(_emailIF);
            isValidated = false;
        }

        if (string.IsNullOrWhiteSpace(_passwordIF.text))
        {
            MessageController.Instance.AlertInputValidation(_passwordIF);
            isValidated = false;
        }
        else if (_passwordIF.text.Length < 6)
        {
            MessageController.Instance.AlertInputValidation(_passwordIF);
            MessageController.Instance.CallMessage("Password", "Password is too short");
            isValidated = false;
        }

        if (!isValidated)
            return null;

        AuthController.UserData user = new AuthController.UserData();
        user.email = _emailIF.text;
        user.password = _passwordIF.text;
        return user;
    }
}