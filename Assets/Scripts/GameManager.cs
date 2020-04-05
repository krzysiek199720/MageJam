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



    public Dictionary<string, int> enemiesKilled = new Dictionary<string, int>();
    public Dictionary<string, int> pointsPerEnemy = new Dictionary<string, int>();

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
    }

}
