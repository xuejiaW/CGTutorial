using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyGOView : EntityView
{
    private new HierarchyGOModel model = null;
    public Button hierarchyButton { get; private set; }
    public Text hierarchyGONameText { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        hierarchyButton = thisTransform.GetComponent<Button>();
        hierarchyGONameText = thisTransform.Find("Name").GetComponent<Text>();
    }

    public override void BindEntityModel(DisplayableEntityModel model)
    {
        base.BindEntityModel(model);
        this.model = model as HierarchyGOModel;
        this.model.OnGONameChanged += SetGoName;
    }

    private void SetGoName(string name)
    {
        hierarchyGONameText.text = name;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.model.OnGONameChanged -= SetGoName;
    }


}
