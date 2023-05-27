using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private bool _usePort;
    [SerializeField] private string _localHostURL;
    [SerializeField] private string _localHostPortURL;
    [Space]
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private AchievementsController _achievementsController;
    [SerializeField] private CrudController _crudController;
    [SerializeField] private ProfileController _profileController;
    private void Awake()
    {
        APIService.HostPortAddress = _usePort ? _localHostPortURL : _localHostURL;
    }
    public void ShowCrud()
    {
        _crudController.ShowCrud();
        _mainMenuView.SelectTab(2);
    }
    public void ShowCrud(AchievementDto achievementDto)
    {
        _crudController.ShowCrud(achievementDto);
        _mainMenuView.SelectTab(2);
    }
    public void ShowAchievements()
    {
        _achievementsController.GetAllAchievements();
        _mainMenuView.SelectTab(3);
    }
    public void ShowAchievements(string id)
    {
        _achievementsController.GetAllAchievements(id);
        _mainMenuView.SelectTab(3);
    }
    public void ShowProfile()
    {
        _profileController.Show();
        _mainMenuView.SelectTab(4);
    }
}