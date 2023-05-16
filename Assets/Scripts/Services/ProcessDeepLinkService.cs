using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcessDeepLinkService : MonoBehaviour
{
    public static ProcessDeepLinkService Instance { get; private set; }
    //[SerializeField] private Login _login;
    private string deeplinkURL;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                // Cold start and Application.absoluteURL not null so process Deep Link.
                onDeepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
            else deeplinkURL = "[none]";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void onDeepLinkActivated(string url)
    {
        deeplinkURL = url;
        string processingUrl = url.Split("?"[0])[1];
        DataProcess(processingUrl);
    }

    private void DataProcess(string data)
    {
        string[] processingUrl = data.Split('=');
        string typeOfProcess = processingUrl[0];
        string dataOfProcess = processingUrl[1];
        switch (typeOfProcess)
        {
            case "PasswordReset":
                //_login.StartPasswordReset(dataOfProcess);
                break;
        }
        print(typeOfProcess);
    }
}