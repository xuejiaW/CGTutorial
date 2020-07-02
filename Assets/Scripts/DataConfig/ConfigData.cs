using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigData
{
    public List<InteractiveIndicatorModel> indicatorsList = new List<InteractiveIndicatorModel>();
    public List<InteractiveGameObjectModel> gameobjectList = new List<InteractiveGameObjectModel>();
    public List<ComponentModel> componentsList = new List<ComponentModel>();

    private Dictionary<string, EntityModel> assetID2EntityDataDict = new Dictionary<string, EntityModel>();

    public void Init()
    {
        foreach (EntityModel indicator in indicatorsList) RegisterConfigData(indicator);
        foreach (EntityModel go in gameobjectList) RegisterConfigData(go);
        foreach (EntityModel component in componentsList) RegisterConfigData(component);
    }

    public EntityModel GetEntityModel(string assetID)
    {
        return assetID2EntityDataDict.TryGetValue(assetID, out var result) ? result : null;
    }

    private void RegisterConfigData(EntityModel entity)
    {
        Debug.Assert(!string.IsNullOrEmpty(entity.assetID), "ERROR: AssetID is empty");
        Debug.Assert(!assetID2EntityDataDict.ContainsKey(entity.assetID), "ERROR: AssetID conflict");
        assetID2EntityDataDict.Add(entity.assetID, entity);
    }
}
