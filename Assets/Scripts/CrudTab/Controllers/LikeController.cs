using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LikeController : MonoBehaviour
{
    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;
    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _addLikeURL;
    [BoxGroup("Response")]
    [SerializeField] private string _removeLikeURL;
    [BoxGroup("Response")]
    [SerializeField] private string _getLikesURL;
    [Space]
    [SerializeField] private SingleAchievementView _singleAchievementView;

    [Inject] private LoadingView _loading;
    private void OnEnable()
    {
        _singleAchievementView.OnLikeClick += LikeClickHandler;
        _singleAchievementView.OnAchievementShow += AchievementShowHandler;
    }
    private void OnDisable()
    {
        _singleAchievementView.OnLikeClick -= LikeClickHandler;
        _singleAchievementView.OnAchievementShow -= AchievementShowHandler;
    }

    private void LikeClickHandler(LikeDto likeDto)
    {
        //TODO: 
        PostLikeRequest($"{_responseURL}/{(likeDto.isLiked ? _addLikeURL : _removeLikeURL)}");
    }
    private void AchievementShowHandler(int id)
    {
        GetLikesRequest($"{_responseURL}/{_getLikesURL}/{id}");
    }
    private async UniTask GetLikesRequest(string url)
    {
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.GET);

        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200" || result.Code == "204")
        {
            LikeDto likeResponse = JsonConvert.DeserializeObject<LikeDto>(result.Description);
            _singleAchievementView.SetLikes(likeResponse);
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
    private async UniTask PostLikeRequest(string url)
    {
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.POST);

        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200" || result.Code == "204")
        {
            //liked or unliked?
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
    [System.Serializable]
    public class LikeDto
    {
        public int? achievementId;
        public bool isLiked;
        public int? likesCount;
    }
}