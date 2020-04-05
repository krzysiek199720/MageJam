using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave", order = 1)]
public class Wave : ScriptableObject
{
    public List<WaveElement> enemies;

    public List<Vector3> spawnPoints;

    public List<WaveElement> GetEnemiesToSpawn(float addTime)
    {
        List<WaveElement> res = new List<WaveElement>();
        bool isWaveOver = true;
        foreach(WaveElement we in enemies)
        {
            if (we.count <= we.spawned)
                continue;
            isWaveOver = false;
            we.timeSinceSpawn += addTime;
            if (we.timeSinceSpawn < we.spawnInterval)
                continue;
            
            res.Add(we);
            we.spawned++;
            we.timeSinceSpawn = 0f;
        }

        if (isWaveOver)
            return null;

         return res;
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    public void ResetWave()
    {
        foreach(WaveElement we in enemies)
        {
            we.spawned = 0;
            we.timeSinceSpawn = 0f;
            we.timeSinceSpawn = we.spawnInterval + 1f;
        }
    }
}
