using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HierarchyGOModel : DisplayableEntityModel
{
    public override System.Type GetViewType() { return typeof(HierarchyGOView); }
    public override System.Type GetControllerType() { return typeof(HierarchyGOController); }

    public new HierarchyGOView view { get; set; }

    public override void BindEntityView(EntityView view)
    {
        base.BindEntityView(view);
        this.view = view as HierarchyGOView;
    }

    public event System.Action<string> OnGONameChanged = null;

    private string m_GOName = "";
    public string goName
    {
        get { return m_GOName; }
        set { m_GOName = value; OnGONameChanged?.Invoke(value); }
    }

    [System.NonSerialized]
    public DisplayableEntityModel attachedGO;
}
