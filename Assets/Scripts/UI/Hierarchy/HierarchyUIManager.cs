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
        InteractiveGameObjectCollection.Instance.OnInteractiveGoCreated += OnNewInteractiveGOCreated;
    }

    private void OnNewInteractiveGOCreated(DisplayableEntityModel model)
    {
        InteractiveGameObjectModel targetModel = model as InteractiveGameObjectModel;

        HierarchyGOController hierarchyGO = GameResourceManager.Instance.CreateEntityController<HierarchyGOModel>("hierarchy_go") as HierarchyGOController;
        HierarchyGOModel hierarchyModel = hierarchyGO.model;
        hierarchyCtlList.Add(hierarchyGO);


        targetModel.hierarchyGO = hierarchyGO.model;
        hierarchyModel.attachedGO = model;
        hierarchyModel.goName = targetModel.name;

        Transform parent = (targetModel.parent as InteractiveGameObjectModel)?.hierarchyGO?.view.transform.Find("Children") ?? hierarchyGOGroup;
        hierarchyModel.view.transform.SetParent(parent, false);

    }

    private void OnDestroy()
    {
        InteractiveGameObjectCollection.Instance.OnInteractiveGoCreated -= OnNewInteractiveGOCreated;
    }
}
