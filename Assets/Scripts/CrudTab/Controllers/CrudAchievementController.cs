using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using UnityEngine;
using Zenject;

#nullable enable
public class CrudAchievementController : MonoBehaviour
{
    #region Data
    [System.Serializable]
    public class CrudAchievement
    {
        public int? id;
        public string? name;
        public string? description;
        public string? creatorId;
        public string? ownerId;
        public DateTime? createdDateTime;
        public bool isGlobal = false;
        public int? globalId;
        public int lockedIconId;
        public int unlockedIconId;
        public bool isPrivate = false;
        public ProgressType progressType;
        public bool isUnlocked;
        public int? progressTarget;
        public int? progressCurrent;
        public string? tasks;
        public string? notes;
    }
    public enum ProgressType
    {
        Single, Multiple, Tasks, Infinite
    }
    #endregion
    [SerializeField] private string _responseURL;

    [SerializeField] private CrudAchievementView _crudAchievementView;
    private bool _isProcessing;
    [Inject] private LoadingView _loading;
    private void OnEnable()
    {
        _crudAchievementView.OnClick += OnClickHandler;
    }
    private void OnDisable()
    {
        _crudAchievementView.OnClick -= OnClickHandler;
    }
    private void OnClickHandler(CrudAchievement crudAchievement)
    {
        string data = JsonConvert.SerializeObject(crudAchievement, formatting: Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        if (_isProcessing)
            return;
        PostRequestSave(_responseURL, crudAchievement.id is null ? APIService.RequestType.POST : APIService.RequestType.PUT, data);
    }

    private async UniTask PostRequestSave(string url, APIService.RequestType requestType, string json)
    {
        Debug.Log(url + "|||" + requestType + "|||" + json);
        _isProcessing = true;
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, requestType, json);

        _isProcessing = false;
        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "201")
        {
            MessageController.Instance.CallMessage($"{(requestType == APIService.RequestType.POST ? "Created" : "Edited")}", "Achievement");
        }
        else if (result.Code == "401")
        {
            MessageController.Instance.CallMessage("Unauthorized", "Wrong username or password");
        }
        else
        {
            MessageController.Instance.CallMessage(result.Code, result.Description);
        }
    }
}