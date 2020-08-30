using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComponentModel : DisplayableEntityModel
{
    public DisplayableEntityModel targetGameObject { get; set; }
}
