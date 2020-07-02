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

    public EntityView CreateEntityView<T>(string assetID) where T : DisplayableEntityModel, new()
    {
        T model = new T() { assetID = assetID };
        LoadConfigData(model);

        GameObject gObj = Instantiate(model.prefabPath);
        EntityView view = gObj.AddComponent(model.GetViewType()) as EntityView;
        view.BindEntityModel(model);
        model.BindEntityView(view);

        return view;
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
