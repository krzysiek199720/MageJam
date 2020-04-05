using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    [SerializeField]
    public List<Wave> waves;

    public int waveNumber { get; private set; }
    public float timeBetweenWaves = 20f;
    private float timeSinceWaveEnd = 0f;

    private bool isWaveActive = false;

    public Dictionary<string, int> enemiesKilled = new Dictionary<string, int>();
    public Dictionary<string, int> pointsPerEnemy = new Dictionary<string, int>();
    public int pointsSum { get; private set; }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        waveNumber = 0;
        pointsSum = 0;
    }

    private static bool start = true; //debug

    private void Update()
    {
        if (start) //debug
        {
            StartNextWave(); //debug
            start = false;
            return;
        }

        if (isWaveActive)
        {
            List<WaveElement> enemiesToSpawn = waves[waveNumber].GetEnemiesToSpawn(Time.deltaTime);
            if (enemiesToSpawn == null)
                EndWave();
            else
            {
                if (enemiesToSpawn.Count > 0)
                    SpawnEnemies(enemiesToSpawn);
            }
        }
        else
        {
            timeSinceWaveEnd += Time.deltaTime;
            if (timeSinceWaveEnd >= timeBetweenWaves)
                StartNextWave();
        }
    }

    public void SpawnEnemies(List<WaveElement> enemies)
    {
        foreach(WaveElement we in enemies)
        {
            Vector3 position = waves[waveNumber].GetSpawnPoint();

            GameObject enemy = Instantiate(we.prefab);
            enemy.transform.position = position;

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.enemyName = we.name;
            enemyScript.pointsPerKill= we.pointsPerKill;
        }
    }

    public void StartNextWave()
    {
        waves[waveNumber].ResetWave();
        Debug.Log("Wave start;");
        if (isWaveActive)
            return;
        isWaveActive = true;
    }

    public void EndWave()
    {
        Debug.Log("Wave end;");
        if (!isWaveActive)
            return;
        isWaveActive = false;
        timeSinceWaveEnd = 0f;
        waveNumber++;
    }

    public void addKill(string name, int points)
    {
        if (enemiesKilled.ContainsKey(name))
            enemiesKilled[name] += 1;
        else
            enemiesKilled.Add(name, 1);

        if (pointsPerEnemy.ContainsKey(name))
            pointsPerEnemy[name] += points;
        else
            pointsPerEnemy.Add(name, points);

        pointsSum += points;
    }

}
