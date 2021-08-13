using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    int GetMaxStack();

    int GetCurrentStackCount();

    void IncreaseStack(int value);
}
