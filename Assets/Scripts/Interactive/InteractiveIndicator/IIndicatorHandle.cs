using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIndicatorHandle
{
    void SetIndicator(InteractiveIndicator indicator);
    void SetIndicatorAxis(string axis);
    void DragIndicatorAxis(Vector3 dragDeltaScreen);
}
