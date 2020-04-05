using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveElement
{
    [SerializeField]
    public string name;

    [SerializeField]
    public int count;

    [HideInInspector]
    public int spawned = 0;

    [SerializeField]
    public int pointsPerKill;

    [SerializeField]
    public GameObject prefab;
    
}
