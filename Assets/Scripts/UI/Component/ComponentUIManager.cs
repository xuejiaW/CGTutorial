using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentUIManager : MonobehaviorSingleton<ComponentUIManager>
{
    // TODO: create component according to the course
    // private ClearColorUIModel clearColorModel = null;
    private Transform componentGroup = null;
    private List<ComponentModel> componentList = null;

    protected override void Init()
    {
        base.Init();
        componentGroup = transform.Find("ComponentGroup");
        componentList = new List<ComponentModel>();

        InteractiveGameObjectCollection.Instance.OnHoldingInteractiveGOUpdated += OnSelectedGoUpdated;

        // clearColorModel = GameResourceManager.Instance.CreateEntityController<ClearColorModel>("component_clear_color").
        //                    model as ClearColorModel;
        // clearColorModel.view.transform.SetParent(componentGroup, false);

    }


    public ComponentModel CreateComponent(string componentID, InteractiveGameObjectModel targetGO)
    {
        Debug.Log("enter create component " + componentID);
        ComponentModel component = GameResourceManager.Instance.CreateEntityController(ComponentUIDict.id2ModelDict[componentID].ToString(), componentID).
                                    model as ComponentModel;
        component.targetGameObject = targetGO;
        componentList.Add(component);
        component.view.transform.SetParent(componentGroup, false);
        component.active = false;
        return component;
    }


    private void OnSelectedGoUpdated(DisplayableEntityModel oldGbj, DisplayableEntityModel newGbj)
    {
        componentList.ForEach(component => component.active = (newGbj != null && newGbj == component.targetGameObject));
    }
}
