using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public GameObject powerupPrefab;
    public Vector3[] spawnPoints;

    [HideInInspector]
    public List<Powerup> powerups;
    private List<int> raritySums;
    private int raritySum;

    [HideInInspector]
    public Dictionary<PowerupType, float> activePowerups = new Dictionary<PowerupType, float>();
    private List<PowerupType> keys = new List<PowerupType>();

    private PowerupActions powerupActions;

    private void HandleGlobalPowerups()
    {
        bool changed = false;

        if (activePowerups.Count > 0)
        {
            foreach (PowerupType type in keys)
            {
                if (activePowerups[type] > 0)
                {
                    activePowerups[type] -= Time.deltaTime;
                }
                else
                {
                    changed = true;

                    Powerup powerup = powerups.Find(pu => pu.type == type);

                    activePowerups.Remove(type);
                    powerup.End();
                }
            }
        }

        if (changed)
        {
            keys = new List<PowerupType>(activePowerups.Keys);
        }
    }

    // Adds a global Powerup to the activePowerups list.
    public void ActivatePowerup(Powerup powerup)
    {
        // means this is immediate powerup
        if (powerup.duration < 0f)
        {
            powerup.Start();
            powerup.End();
            return;
        }
        
        if (!activePowerups.ContainsKey(powerup.type))
        {
            powerup.Start();
            activePowerups.Add(powerup.type, powerup.duration);
        }
        else
        {
            activePowerups[powerup.type] += powerup.duration;
        }

        keys = new List<PowerupType>(activePowerups.Keys);
    }

    public void ClearActivePowerups()
    {
        foreach (KeyValuePair<PowerupType, float> Powerup in activePowerups)
        {
            powerups.Find(pu => pu.type == Powerup.Key).End();
        }
        activePowerups.Clear();
    }

    void Update()
    {
        HandleGlobalPowerups();
    }

    public GameObject SpawnPowerup(Powerup powerup, Vector3 position)
    {
        if (powerup == null)
            return null;
        if (position == null)
            position = Vector3.zero;

        GameObject powerupGameObject = Instantiate(powerupPrefab);

        PowerupBehaviour powerupBehaviour = powerupGameObject.GetComponent<PowerupBehaviour>();

        powerupBehaviour.controller = this;

        powerupBehaviour.SetPowerup(powerup);

        powerupGameObject.transform.position = position;

        return powerupGameObject;
    }

    public GameObject SpawnRandomPowerup()
    {
        return SpawnPowerup(getRandomPowerup(), GetRandomSpawnPosition());
    }

    public Vector3 GetRandomSpawnPosition()
    {
        if(spawnPoints.Length > 0)
            return spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
        return Vector3.zero;
    }

    public Powerup getRandomPowerup()
    {
        Powerup res = null;

        int index = 0;
        int randomRarity = Random.Range(0, raritySum);
        foreach(int sum in raritySums)
        {
            if(randomRarity <= sum)
            {
                res = powerups[index];
                return res;
            }
            ++index;
        }

        Debug.LogError("Could not find Powerup");
        return res;
    }

    private void Start()
    {
        powerupActions = new PowerupActions();
        raritySums = new List<int>();

        CreatePowerUps();
        CalculateRarities();
        SpawnRandomPowerup();
    }

    private void CalculateRarities()
    {
        int localSum = 0;
        foreach(Powerup powerup in powerups)
        {
            localSum += powerup.rarity;
            raritySums.Add(localSum);
        }
        raritySum = localSum;
    }

    private void CreatePowerUps()
    {
        // PLEASE READ!!
        // Reczne uzupelnianie
        // Może być wiecej niż jedno na typ, ale
        // MUSI ROBIC TO SAMO !!
        // MOZE SIE ROZNIC TYLKO RARITY I DURATION !!
        // nie zabezpieczam tego dobrze bo nie ma czasu

        // mapUnveil
        Powerup mapUnveil = new Powerup();
        mapUnveil.type = PowerupType.mapUnveil;
        mapUnveil.duration = 5f;
        mapUnveil.rarity = 1;
        mapUnveil.startAction = new UnityEngine.Events.UnityEvent();
        mapUnveil.endAction = new UnityEngine.Events.UnityEvent();
        mapUnveil.startAction.AddListener(powerupActions.DisableFog);
        mapUnveil.endAction.AddListener(powerupActions.EnableFog);
        this.powerups.Add(mapUnveil);

        //// doubleDamage
        //Powerup doubleDamage = new Powerup();
        //doubleDamage.type = PowerupType.doubleDamage;
        //doubleDamage.duration = 15f;
        //doubleDamage.rarity = 1;
        ////doubleDamage.startAction.AddListener();
        ////doubleDamage.endAction.AddListener();
        //this.powerups.Add(doubleDamage);

        //// unlimitedAmmo
        //Powerup unlimitedAmmo = new Powerup();
        //unlimitedAmmo.type = PowerupType.unlimitedAmmo;
        //unlimitedAmmo.duration = 15f;
        //unlimitedAmmo.rarity = 1;
        ////unlimitedAmmo.startAction.AddListener();
        ////unlimitedAmmo.endAction.AddListener();
        //this.powerups.Add(unlimitedAmmo);

        //// visionRange
        //Powerup visionRange = new Powerup();
        //visionRange.type = PowerupType.visionRange;
        //visionRange.duration = 5f;
        //visionRange.rarity = 1;
        ////visionRange.startAction.AddListener();
        ////visionRange.endAction.AddListener();
        //this.powerups.Add(visionRange);

        //// healing
        //Powerup healing = new Powerup();
        //healing.type = PowerupType.healing;
        //healing.duration = -1f;
        //healing.rarity = 1;
        ////healing.startAction.AddListener();
        ////healing.endAction.AddListener();
        //this.powerups.Add(healing);

        //// endurance
        //Powerup endurance = new Powerup();
        //endurance.type = PowerupType.endurance;
        //endurance.duration = 5f;
        //endurance.rarity = 1;
        ////endurance.startAction.AddListener();
        ////endurance.endAction.AddListener();
        //this.powerups.Add(endurance);
    }
}