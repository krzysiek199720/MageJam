using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave", order = 1)]
public class Wave : ScriptableObject
{
    public string prefabName;

    public List<WaveElement> enemies;

    public List<Vector3> spawnPoints;

    [HideInInspector]
    private List<WaveElement> validEnemies = null;

    public WaveElement GetEnemyToSpawn()
    {
        if (validEnemies == null)
        {
            validEnemies = new List<WaveElement>();
            foreach (WaveElement we in enemies)
            {
                if (we.spawned < we.count)
                    validEnemies.Add(we);
            }
        }
        WaveElement res = validEnemies[Random.Range(0, validEnemies.Count)];
        res.spawned++;
        if (res.spawned >= res.count)
            validEnemies.Remove(res);

         return res;
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
