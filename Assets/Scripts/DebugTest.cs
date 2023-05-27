using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    [SerializeField] private GameObject _authPanel;
    [SerializeField] private bool _useAuth;
    private void Awake()
    {
        _authPanel.SetActive(_useAuth);
    }
}