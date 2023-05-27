using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskProgressView : MonoBehaviour
{
    [SerializeField] private Toggle _isDoneToggle;
    [SerializeField] private TMP_InputField _taskIF;
    [SerializeField] private Button _deleteBtn;
    public void AddListenerToDeleteBtn(UnityAction unityAction) => _deleteBtn.onClick.AddListener(unityAction);
    public void SetData(TaskProgressDto task)
    {
        _isDoneToggle.isOn = task.IsDone;
        _taskIF.text = task.TaskText;
    }
    public TaskProgressDto GetTask()
    {
        return new TaskProgressDto() { IsDone = _isDoneToggle.isOn, TaskText = _taskIF.text };
    }
    [System.Serializable]
    public class TaskProgressDto
    {
        public bool IsDone;
        public string TaskText;
    }
}