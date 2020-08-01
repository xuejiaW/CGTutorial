using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUIManager : MonobehaviorSingleton<ComponentUIManager>
{
    private TransformUIModel transformModel = null;
    private Transform componentGroup = null;
    protected override void Init()
    {
        base.Init();
        componentGroup = transform.Find("ComponentGroup");
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnSelectedGoUpdated;

        transformModel = GameResourceManager.Instance.CreateEntityController<TransformUIModel>("component_transform").
                            model as TransformUIModel;
        transformModel.view.transform.SetParent(componentGroup, false);

        transformModel.active = false;
    }
    private void OnSelectedGoUpdated(DisplayableEntityModel oldGbj, DisplayableEntityModel newGbj)
    {
        transformModel.active = newGbj != null;
    }
}
