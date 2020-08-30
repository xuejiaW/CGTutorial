﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexcoordComponentController : ComponentController
{
    public override UpdateModelPropertyBase GetModelUpdater()
    {
        return new TexcoordModelUpdater();
    }
}
