using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyGOController : DisplayableEntityController
{
    public new HierarchyGOModel model;

    public override void BindEntityModel(EntityModel model)
    {
        base.BindEntityModel(model);
        this.model = model as HierarchyGOModel;
        this.model.view.hierarchyButton.onClick.AddListener(OnClickHierarchyGO);
    }

    public void OnClickHierarchyGO()
    {
        InteractiveGameObjectCollection.Instance.SelectGameObject(model.attachedGO);
    }
}
