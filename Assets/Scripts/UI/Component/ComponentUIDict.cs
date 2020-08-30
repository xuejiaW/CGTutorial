using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentUIDict
{
    public static Dictionary<string, System.Type> id2ModelDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIModel) },
                    {"component_color",typeof(ColorComponentModel)},
                    {"component_position",typeof(PositionComponentModel)},
                    {"component_rotation",typeof(RotationComponentModel)},
                    {"component_scale",typeof(ScaleComponentModel)},
                    {"component_orientation",typeof(OrientationComponentModel)},
                    {"component_texcoord",typeof(TexcoordComponentModel)},
                    {"component_camera",typeof(CameraComponentModel)},
                    {"component_camera_transform",typeof(CameraTransformUIModel)}};

    public static Dictionary<string, System.Type> id2ViewDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIView) },
                    {"component_color",typeof(ColorComponentView)},
                    {"component_position",typeof(PositionComponentView)},
                    {"component_rotation",typeof(RotationComponentView)},
                    {"component_scale",typeof(ScaleComponentView)},
                    {"component_orientation",typeof(OrientationComponentView)},
                    {"component_texcoord",typeof(TexcoordComponentView)},
                    {"component_camera",typeof(CameraComponentView)},
                    {"component_camera_transform",typeof(CameraTransformUIView)}};
    public static Dictionary<string, System.Type> id2ControllerDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIController) },
                    { "component_color", typeof(ColorComponentController) },
                    { "component_position", typeof(PositionComponentController) },
                    { "component_rotation",typeof(RotationComponentController)},
                    { "component_scale",typeof(ScaleComponentController)},
                    { "component_orientation",typeof(OrientationComponentController)},
                    { "component_texcoord", typeof(TexcoordComponentController) },
                    { "component_camera", typeof(CameraComponentController) },
                    { "component_camera_transform",typeof(CameraTransformUIController)}};
}
