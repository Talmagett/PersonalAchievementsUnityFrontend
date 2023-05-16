using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOutView : MonoBehaviour
{
    public event Action OnSignOutClicked;
    public void SignOut()
    {
        OnSignOutClicked?.Invoke();
    }
}