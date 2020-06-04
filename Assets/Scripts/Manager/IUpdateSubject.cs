using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateSubjector
{
    void RegisterObserver(IUpdateObserver observer);
    void UnregisterObserver(IUpdateObserver observer);
}