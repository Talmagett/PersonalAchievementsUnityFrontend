using UnityEngine;

public class CrudController : MonoBehaviour
{
    [SerializeField] private CrudView _crudView;
    [SerializeField] private CrudAchievementView _crudAchievementView;
    public void ShowCrud()
    {
        _crudView.OnShow();
        _crudAchievementView.SetData(null);
    }
    public void ShowCrud(AchievementDto achievementDto)
    {
        _crudView.OnShow();
        _crudAchievementView.SetData(achievementDto);
    }
}