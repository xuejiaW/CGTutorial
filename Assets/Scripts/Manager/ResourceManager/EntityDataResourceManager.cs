using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameResourceManager : Singleton<GameResourceManager>
{
    private ConfigData configData;

    private void InitConfigEntityData()
    {
        Config data = Resources.Load<Config>("ConfigData");
        configData = data.configData;
        configData.Init();
    }

    public EntityController CreateEntityController(string modelTypeName, string assetID)
    {
        Type modelType = Type.GetType(modelTypeName);
        DisplayableEntityModel model = Activator.CreateInstance(modelType) as DisplayableEntityModel;
        model.assetID = assetID;
        return CreateEntityController(model);
    }

    public EntityController CreateEntityController<T>(string assetID) where T : DisplayableEntityModel, new()
    {
        T model = new T() { assetID = assetID };
        return CreateEntityController(model);
    }

    public EntityController CreateEntityController(Type modelType, Type viewType, Type controllerType, string assetID)
    {
        DisplayableEntityModel model = Activator.CreateInstance(modelType) as DisplayableEntityModel;
        model.assetID = assetID;
        LoadConfigData(model);
        GameObject gObj = Instantiate(model.prefabPath);
        EntityView view = gObj.AddComponent(viewType) as EntityView;
        DisplayableEntityController controller = Activator.CreateInstance(controllerType) as DisplayableEntityController;
        CombineMVC(model, view, controller);

        return controller;
    }


    private EntityController CreateEntityController(DisplayableEntityModel model)
    {
        LoadConfigData(model);

        GameObject gObj = Instantiate(model.prefabPath);

        EntityView view = gObj.AddComponent(model.GetViewType()) as EntityView;
        DisplayableEntityController controller = Activator.CreateInstance(model.GetControllerType()) as DisplayableEntityController;
        CombineMVC(model, view, controller);

        return controller;
    }

    public void CombineMVC(DisplayableEntityModel model, EntityView view, DisplayableEntityController controller)
    {
        view.BindEntityModel(model);
        model.BindEntityView(view);

        controller.BindEntityModel(model);
        model.BindEntityController(controller);

        view.BindEntityController(controller);

        controller.Init();
    }

    public void LoadConfigData(EntityModel entity)
    {
        EntityModel model = configData.GetEntityModel(entity.assetID);
        Debug.Assert(model != null, "ERROR: Asset data is empty" + entity.assetID);

        FieldInfo[] fieldInfos = model.GetType().GetFields();
        foreach (FieldInfo info in fieldInfos)
            info.SetValue(entity, info.GetValue(model));
    }
}
