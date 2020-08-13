using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectInstantiator : Singleton<InteractiveGameObjectInstantiator>
{
    private static Dictionary<string, string> id2ModelType = new Dictionary<string, string>()
    {
        {"gameobject_triangle","TriangleModel"}
    };

    public void InstantiateGameObject(CoursesModel model)
    {
        if (model.createModelsAssetID != null)
        {
            model.createModelsAssetID.ForEach((id) =>
            {
                if (id2ModelType.ContainsKey(id))
                    GameResourceManager.Instance.CreateEntityController(id2ModelType[id], id);
            });
        }
    }
}
