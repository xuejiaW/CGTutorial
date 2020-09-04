using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveGameObjectCollection : Singleton<InteractiveGameObjectCollection>
{
    private List<DisplayableEntityModel> interactiveGo = null;
    public DisplayableEntityModel holdingInteractiveGo { get; private set; }

    //Parameters: <oldHoldingInteractiveGO,NewHoldingInteractiveGo>
    public event System.Action<DisplayableEntityModel, DisplayableEntityModel> OnHoldingInteractiveGOUpdated = null;
    public event System.Action<DisplayableEntityModel> OnInteractiveGoCreated = null;

    protected override void InitProcess()
    {
        base.InitProcess();
        interactiveGo = new List<DisplayableEntityModel>();
        holdingInteractiveGo = null;
    }

    public void AddInteractiveGo(DisplayableEntityModel model)
    {
        interactiveGo.Add(model);
        OnInteractiveGoCreated?.Invoke(model);
        Debug.Log("interactiveGO name is " + interactiveGo.Count);
    }

    public void SelectGameObject(DisplayableEntityModel GO)
    {
        if (holdingInteractiveGo == GO)
            return;
        DisplayableEntityModel old = holdingInteractiveGo;
        holdingInteractiveGo = GO;
        OnHoldingInteractiveGOUpdated?.Invoke(old, holdingInteractiveGo);
    }

    public void SelectGameObject(GameObject GO)
    {
        Debug.Log("On Select GO");
        SelectGameObject(GO?.GetComponent<InteractiveGameObjectView>().model);
    }

    public void InstantiateGameObject(CoursesModel model)
    {
        if (model.createModelsAssetID != null)
        {
            model.createModelsAssetID.ForEach((id) =>
            {
                Type modelType = ReflectionManager.Instance.GetAssetType(id, "gameobject_", "Model") ?? typeof(InteractiveGameObjectModel);
                Type viewType = ReflectionManager.Instance.GetAssetType(id, "gameobject_", "View") ?? typeof(InteractiveGameObjectView);
                Type controllerType = ReflectionManager.Instance.GetAssetType(id, "gameobject_", "Controller") ?? typeof(InteractiveGameObjectController);
                GameResourceManager.Instance.CreateEntityController(modelType, viewType, controllerType, id);
            });
        }
    }
}
