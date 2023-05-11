using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTypeView : TabUI
{
    public int SelectedItem { get; private set; }
    private void OnEnable()
    {
        SelectedItem = 0;
        SelectTab(SelectedItem);
    }
    public override void SelectTab(int id)
    {
        base.SelectTab(id);
        SelectedItem = id;
    }
}
