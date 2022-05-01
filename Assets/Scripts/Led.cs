using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour
{
    public LogicGateBase logicGate;
    Renderer _renderer;

    [ColorUsage(true, true)] public Color ledColorOff;
    [ColorUsage(true, true)] public Color ledColorOn;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isOn = logicGate.UseGate();

        if(isOn)
            _renderer.material.SetColor("_EmissionColor", ledColorOn);
        else
            _renderer.material.SetColor("_EmissionColor", ledColorOff);
    }
}
