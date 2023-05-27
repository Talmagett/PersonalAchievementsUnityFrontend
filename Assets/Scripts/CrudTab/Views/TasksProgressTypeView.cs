using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TasksProgressTypeView : MonoBehaviour
{
    [SerializeField] private TaskProgressView _taskProgressViewPrefab;
    public IEnumerable<TaskProgressView.TaskProgressDto> GetAllTasks()
    {
        var tasksViews = GetComponentsInChildren<TaskProgressView>();
        var tasks = tasksViews.Select(t => t.GetTask()).Where(t => !string.IsNullOrEmpty(t.TaskText));
        return tasks;
    }
    public void SetTasks(IEnumerable<TaskProgressView.TaskProgressDto> tasks)
    {
        while (transform.childCount > 1)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        if (tasks is not null)
            foreach (var item in tasks)
            {
                var task = Instantiate(_taskProgressViewPrefab, transform);
                task.transform.SetSiblingIndex(transform.childCount - 2);
                task.SetData(item);
                task.AddListenerToDeleteBtn(() =>
                {
                    UpdateLayout();
                    Destroy(task.gameObject); 
                });
            }
        UpdateLayout();
    }
    public void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
    }
    public void AddTask()
    {
        var task = Instantiate(_taskProgressViewPrefab, transform);
        task.transform.SetSiblingIndex(transform.childCount - 2);
        task.AddListenerToDeleteBtn(() => Destroy(task.gameObject));
        UpdateLayout();
    }
}