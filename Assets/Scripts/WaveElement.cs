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

    [SerializeField]
    public float spawnInterval = 1f;

    [HideInInspector]
    public float timeSinceSpawn = 0f;

    [HideInInspector]
    public int spawned = 0;

    [SerializeField]
    public int pointsPerKill;

    [SerializeField]
    public GameObject prefab;
    
}
