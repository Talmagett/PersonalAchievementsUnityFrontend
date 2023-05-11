using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUpView : MonoBehaviour
{
    public event Action OnClick;

    public void TestFunction()
    {
        OnClick?.Invoke();
    }
}
