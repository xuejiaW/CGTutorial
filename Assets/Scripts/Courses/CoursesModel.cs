using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoursesModel : EntityModel
{
    public float indicatorSize = 1.0f;
    public float indicatorSensitive = 1.0f;
    public string createUIAssertID = null;
    public bool allowCameraFlythrough = true;
    public bool cameraOrthographic = true;
    public bool allowTranslate = true;
    public bool allowRotate = true;
    public bool allowScale = true;
    public string vertexShaderPath = "";
    public string fragmentShaderPath = "";
    public string mainCPPPath = "";
}
