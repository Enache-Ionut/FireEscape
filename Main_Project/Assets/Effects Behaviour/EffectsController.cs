using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectsController : MonoBehaviour
{
    /*
     * fireRate ,evolveRate, evolveTimeMin and startFireTime is measured in seconds 
     * evolveChance and transmissionChance is measured in % expressed in [0, 1]
     * transmissionMaxDist is measured in game units
     */

    public int updateFireRate = 5;
    public int updateEvolveRate = 10;
    public int evolveTimeMin = 10;
    public int startFireTime = 3;

    public float evolveFireChance = 0.2f;
    public float transmissionChance = 0.1f;
    public float transmissionMaxDist = 5f;

    private bool updateFireTried = false;
    private bool updateEvolveTried = false;

    private float startTime;
    private float elapsedTime;
    private bool fireStarted = false;

    private int SMOKE = 1;
    private int FIRE = 0;


    [SerializeField] GameObject[] effects;
    List<Transform> spawnLocations;
    List<GameObject> smokes;
    List<GameObject> fires;
    Dictionary<GameObject, int> smokeStartTime;

    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize map
        smokeStartTime = new Dictionary<GameObject, int>();

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
        smokes.Add(smoke);
        smokeStartTime.Add(smoke, (int)Math.Round(Time.time));
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
        /*
        * updateEvolveTried is used to ensure that only 
        * once a second evolve is tried
        */ 
        if(elapsedTime % updateEvolveRate == 0 && updateEvolveTried == false)
        {
            updateEvolveTried = true;

            foreach(var smoke in smokes)
            {
                int smokeStart = 0;
                if(smokeStartTime.TryGetValue(smoke, out smokeStart))
                {
                    var smokeAge = (int)Math.Round(Time.time) - smokeStart;
                    if (smokeAge >= evolveTimeMin)
                    {
                        TryEvolveSmoke(smoke);
                    }
                }
            }
        }
        else
        {
            updateEvolveTried = false;
        }

        /*
        * updateFireTried is used to ensure that only 
        * once a second evolve is tried
        */
        if (elapsedTime % updateFireRate == 0 && updateFireTried == false)
        {
            updateFireTried = true;

            foreach(var fire in fires)
            {
                TryTransmitFire(fire);
            }
        }
        else
        {
            updateFireTried = true;
        }
    }

    void TryEvolveSmoke(GameObject smoke)
    {

    }

    void TryTransmitFire(GameObject Fire)
    {

    }

    void EvolveSmokeToFire(GameObject smoke)
    {
        // Add fire
        GameObject fire = Instantiate(effects[FIRE], smoke.transform);
        fires.Add(fire);

        // Remove smoke
        smokes.Remove(smoke);
        Destroy(smoke);
    }

    void TransmitFire(Transform nextSmokePosition)
    {
        // Add smoke
        GameObject smoke = Instantiate(effects[SMOKE], nextSmokePosition);
        smokes.Add(smoke);
    }
}
