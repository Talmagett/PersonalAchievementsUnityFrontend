using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AchievementsController : MonoBehaviour
{
    [BoxGroup("Response")]
    [SerializeField] private string _responseURL;
    [Space]
    [BoxGroup("Response")]
    [SerializeField] private string _getByUsernameURL;
    [BoxGroup("Response")]
    [SerializeField] private string _getAllURL;

    [SerializeField] private Transform _achivementsContentParent;
    [SerializeField] private AchievementItemView _achievementItemView;
    [SerializeField] private SingleAchievementView _singleAchievementView;
    [Inject] private LoadingView _loading;
    [SerializeField]
    private int _startIndex;
    [SerializeField]
    private int _endIndex;
    public void GetAllAchievements()
    {
        GetAllAchievementsRequest($"{_responseURL}/{_getAllURL}?startIndex={_startIndex}&endIndex={_endIndex}");
    }
    public void GetAllAchievements(string userName)
    {
        GetAllAchievementsRequest($"{_responseURL}/{_getAllURL}?startIndex={_startIndex}&endIndex={_endIndex}&userId={userName}");
    }


    private async UniTask GetAllAchievementsRequest(string url)
    {
        _loading.SetVisible(true);
        var result = await APIService.SendRequest(url, APIService.RequestType.GET);

        _loading.SetVisible(false);

        if (result is null)
            return;

        if (result.Code == "200" || result.Code == "204")
        {
            while (_achivementsContentParent.childCount > 0)
            {
                DestroyImmediate(_achivementsContentParent.GetChild(0).gameObject);
            }

            List<AchievementDto> userList = JsonConvert.DeserializeObject<List<AchievementDto>>(result.Description);
            if (userList is not null)
                foreach (var achievement in userList)
                {
                    Instantiate(_achievementItemView, _achivementsContentParent).SetData(achievement, _singleAchievementView);
                }
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