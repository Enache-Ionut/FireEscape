using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectsController : MonoBehaviour
{
    [SerializeField] GameObject[] effects;
    List<Transform> spawnLocations;
    List<GameObject> smokeLocations;
    List<GameObject> fireLocations;

    public int updateFireRate = 5;
    public int updateEvolveRate = 10;
    public float evolveFireChance = 0.2f;
    public float transmissionChance = 0.1f;
    public float transmissionMaxDist = 5f;
    public int startFireTime = 3;

    private float startTime;
    private float elapsedTime;
    private bool fireStarted = false;

    private int SMOKE = 1;
    private int FIRE = 0;

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
        GameObject smoke = Instantiate(effects[SMOKE], spawnLocations[rndIndex]);
        smokeLocations.Add(smoke);
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

    void EvolveSmokeToFire(GameObject smoke)
    {
        // Add fire
        GameObject fire = Instantiate(effects[FIRE], smoke.transform);
        fireLocations.Add(fire);

        // Remove smoke
        smokeLocations.Remove(smoke);
        Destroy(smoke);
    }

    void TransmitFire(Transform nextSmokePosition)
    {
        // Add smoke
        GameObject smoke = Instantiate(effects[SMOKE], nextSmokePosition);
        smokeLocations.Add(smoke);
    }
}
