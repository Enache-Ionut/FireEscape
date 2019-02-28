using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectsController : MonoBehaviour
{
    [SerializeField] GameObject[] effects;
    List<Transform> spawnLocations;
    List<Transform> smokeLocations;
    List<Transform> fireLocations;

    public int updateFireRate = 5;
    public int updateEvolveRate = 10;
    public float evolveFireChance = 0.2f;
    public float transmissionChance = 0.1f;
    public float transmissionMaxDist = 5f;
    public int startFireTime = 3;

    private float startTime;
    private float elapsedTime;
    private bool fireStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get all spawn locations
        GameObject[] spawnLocationsGameObj = GameObject.FindGameObjectsWithTag("SpawnLocation");
        foreach (var spawnLocation in spawnLocationsGameObj)
        {
            spawnLocations.Add(spawnLocation.transform);
        }

        startTime = Time.time;
    }

    void SpawnFirstFire()
    {
        System.Random rnd = new System.Random();
        int rndIndex = rnd.Next(0, spawnLocations.Count);
        Instantiate(effects[1], spawnLocations[rndIndex]);
        smokeLocations.Add(spawnLocations[rndIndex]);
        fireStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;

        if (elapsedTime > 1)
        {
            if (fireStarted == false &&
                (int)Math.Round(elapsedTime) % startFireTime == 0)
            {
                SpawnFirstFire();
            }
            if(fireStarted == true)
            {
                UpdateFires((int)Math.Round(elapsedTime));
            }
        }
    }

    void UpdateFires(int elapsedTime)
    {

    }
}
