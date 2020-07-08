using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUIManager : MonobehaviorSingleton<ComponentUIManager>
{
    private TransformUIModel transformModel = null;
    protected override void Init()
    {
        base.Init();
        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnSelectedGoUpdated;

        transformModel = GameResourceManager.Instance.CreateEntityController<TransformUIModel>("component_transform").
                            model as TransformUIModel;
        transformModel.view.transform.SetParent(transform, false);

        transformModel.active = false;
    }
    private void OnSelectedGoUpdated(DisplayableEntityModel oldGbj, DisplayableEntityModel newGbj)
    {
        transformModel.active = newGbj != null;
    }
}
