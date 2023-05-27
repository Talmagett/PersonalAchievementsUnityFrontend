using UnityEngine;

public class CrudView : MonoBehaviour
{
    [SerializeField] private GameObject _achievementButton;
    [SerializeField] private GameObject _tagButton;
    [SerializeField] private GameObject _iconButton;
    public void OnShow()
    {
        _achievementButton.SetActive(AuthController.UserRole == AuthController.Role.Admin);
        _tagButton.SetActive(AuthController.UserRole == AuthController.Role.Admin);
        _iconButton.SetActive(AuthController.UserRole == AuthController.Role.Admin);
    }
}