using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentUIDict
{
    public static Dictionary<string, System.Type> id2ModelDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIModel) },
                    {"component_clear_color",typeof(ClearColorUIModel)},
                    {"component_vertex_position",typeof(VertexPosUIModel)}
                    };

    public static Dictionary<string, System.Type> id2ViewDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIView) },
                    {"component_clear_color",typeof(ClearColorUIView)},
                    {"component_vertex_position",typeof(VertexPosUIView)}
                    };
    public static Dictionary<string, System.Type> id2ControllerDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIController) },
                    { "component_clear_color", typeof(ClearColorUIController) },
                    { "component_vertex_position", typeof(VertexPosUIController) },
                    };
}
