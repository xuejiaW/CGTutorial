using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentUIDict
{
    public static Dictionary<string, System.Type> id2ModelDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIModel) },
                    {"component_color",typeof(ColorComponentModel)},
                    {"component_vertex_position",typeof(VertexPosUIModel)},
                    {"component_vertex_texcoord",typeof(VertexTexcoordUIModel)},
                    {"component_camera",typeof(CameraUIModel)},
                    {"component_camera_transform",typeof(CameraTransformUIModel)}};

    public static Dictionary<string, System.Type> id2ViewDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIView) },
                    {"component_color",typeof(ColorComponentView)},
                    {"component_vertex_position",typeof(VertexPosUIView)},
                    {"component_vertex_texcoord",typeof(VertexTexcoordUIView)},
                    {"component_camera",typeof(CameraUIView)},
                    {"component_camera_transform",typeof(CameraTransformUIView)}};
    public static Dictionary<string, System.Type> id2ControllerDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIController) },
                    { "component_color", typeof(ColorComponentController) },
                    { "component_vertex_position", typeof(VertexPosUIController) },
                    { "component_vertex_texcoord", typeof(VertexTexcoordUIController) },
                    { "component_camera", typeof(CameraUIController) },
                    {"component_camera_transform",typeof(CameraTransformUIController)}};
}
