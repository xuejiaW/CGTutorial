﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGameObjectInstantiator : Singleton<InteractiveGameObjectInstantiator>
{
    private static Dictionary<string, string> id2ModelType = new Dictionary<string, string>()
    {
        {"gameobject_triangle","TriangleModel"},
        {"gameobject_texture_quad","TextureQuadModel"},
        {"gameobject_transformation_quad","TransformationQuadModel"},
        {"gameobject_cube","InteractiveGameObjectModel"},
        {"gameobject_world_camera","CameraModel"},
        {"gameobject_clear_color","ClearColorModel"}
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
