using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoursesModel : EntityModel
{
    public float indicatorSize = 1.0f;
    public float indicatorSensitive = 1.0f;
    public bool allowMultiInteractiveMethod = false;
    public bool allowCameraFlythrough = true;
    public bool cameraOrthographic = true;
    public string vertexShaderPath = "";
    public string fragmentShaderPath = "";
    public string mainCPPPath = "";
    public List<string> createModelsAssetID = null;
    public List<string> createComponentAssetID = null;
}
