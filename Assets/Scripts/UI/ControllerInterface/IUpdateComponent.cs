using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUpdateComponent<T>
{
    void SetTargetInputFields(List<InputField> targets);
    void UpdateComponent(IEquatable<T> data);
}
