﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityEngineExtension
{
    public static T GetComponent_AutoAdd<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
            component = gameObject.AddComponent<T>();
        return component;
    }

    public static float GetMaxAbsAxis(this Vector3 vector)
    {
        int maxIndex = 0;// 0 -> x, 1 -> y, 2 -> z
        float max = Mathf.Abs(vector.x);

        if (vector.y > max)
        {
            max = Mathf.Abs(vector.y);
            maxIndex = 1;
        }

        if (vector.z > max)
        {
            max = vector.z;
            maxIndex = 2;
        }

        return vector[maxIndex];
    }

    public static float GetAxisSum(this Vector3 vector)
    {
        return vector[0] + vector[1] + vector[2];
    }

    public static Vector3 GetInverse(this Vector3 vector)
    {
        float x = vector.x, y = vector.y, z = vector.z;
        return new Vector3(1.0f / x, 1.0f / y, 1.0f / z);
    }

    public static Vector3 Clamp(this ref Vector3 vector, float min, float max)
    {
        for (int i = 0; i != 3; ++i)
            vector[i] = Mathf.Clamp(vector[i], min, max);

        return vector;
    }
}
