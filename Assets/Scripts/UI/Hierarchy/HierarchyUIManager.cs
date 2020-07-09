using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyUIManager : MonobehaviorSingleton<HierarchyUIManager>
{
    private List<HierarchyGOController> hierarchyCtlList = null;
    private Transform hierarchyGOGroup = null;

    protected override void Init()
    {
        base.Init();

        hierarchyCtlList = new List<HierarchyGOController>();

        hierarchyGOGroup = transform.Find("InteractiveGOGroup");
        InteractiveGameObjectCollection.Instance.OnCreateInteractiveGo += OnNewInteractiveGOCreated;
    }

    private void OnNewInteractiveGOCreated(DisplayableEntityModel model)
    {
        HierarchyGOController hierarchyGO = GameResourceManager.Instance.CreateEntityController<HierarchyGOModel>("hierarchy_go") as HierarchyGOController;
        hierarchyCtlList.Add(hierarchyGO);
        hierarchyGO.model.attachedGO = model;
        hierarchyGO.model.goName = (model as InteractiveGameObjectModel).name;
        hierarchyGO.model.view.transform.SetParent(hierarchyGOGroup, false);
    }

    private void OnDestroy()
    {
        InteractiveGameObjectCollection.Instance.OnCreateInteractiveGo -= OnNewInteractiveGOCreated;
    }
}
