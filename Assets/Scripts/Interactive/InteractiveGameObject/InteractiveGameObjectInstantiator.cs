using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectInstantiator : Singleton<InteractiveGameObjectInstantiator>
{
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
