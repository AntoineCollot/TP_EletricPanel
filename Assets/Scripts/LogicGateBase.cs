using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicGateBase : MonoBehaviour
{
    public ConnectionPin[] connectionPins;

    public abstract bool UseGate();
}
