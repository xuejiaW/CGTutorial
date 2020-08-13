using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentUIDict
{
    public static Dictionary<string, System.Type> id2ModelDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIModel) },
                    {"component_clear_color",typeof(ClearColorUIModel)} };

    public static Dictionary<string, System.Type> id2ViewDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIView) },
                    {"component_clear_color",typeof(ClearColorUIView)} };
    public static Dictionary<string, System.Type> id2ControllerDict = new Dictionary<string, System.Type>() {
                    { "component_transform", typeof(TransformUIController) },
                    { "component_clear_color", typeof(ClearColorUIController) }, };
}
