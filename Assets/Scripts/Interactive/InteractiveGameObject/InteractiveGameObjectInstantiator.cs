using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectInstantiator : Singleton<InteractiveGameObjectInstantiator>
{
    private static Dictionary<string, string> id2ModelType = new Dictionary<string, string>()
    {
        {"gameobject_triangle","TriangleModel"},
        {"gameobject_texture_quad","TextureQuadModel"},
    };

    public void InstantiateGameObject(CoursesModel model)
    {
        if (model.createModelsAssetID != null)
        {
            model.createModelsAssetID.ForEach((id) =>
            {
                Debug.Log("model is " + id);
                if (id2ModelType.ContainsKey(id))
                    GameResourceManager.Instance.CreateEntityController(id2ModelType[id], id);
            });
        }
    }
}
