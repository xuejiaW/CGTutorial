using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUIManager : Singleton<ComponentUIManager>
{
    private Transform componentGroup = null;
    private List<ComponentModel> componentList = null;

    public override void Init()
    {
        base.Init();

        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnSelectedGoUpdated;
    }

    protected override void InitProcess()
    {
        componentGroup = GameObject.Find("ComponentGroup").transform;
        componentList = new List<ComponentModel>();
    }


    public ComponentModel CreateComponent(string componentID, InteractiveGameObjectModel targetGO, bool autoHide = true)
    {
        ComponentModel component = GameResourceManager.Instance.CreateEntityController(ComponentUIDict.id2ModelDict[componentID]
                                .ToString(), componentID).model as ComponentModel;
        component.targetGameObject = targetGO;
        componentList.Add(component);
        component.view.transform.SetParent(componentGroup, false);
        (component.controller as ComponentController).InitComponent();
        (component.view as ComponentView).InitComponent();
        component.active = !autoHide;
        return component;
    }


    private void OnSelectedGoUpdated(DisplayableEntityModel oldGbj, DisplayableEntityModel newGbj)
    {
        componentList.ForEach(component => component.active = (newGbj != null && newGbj == component.targetGameObject));
    }
}
