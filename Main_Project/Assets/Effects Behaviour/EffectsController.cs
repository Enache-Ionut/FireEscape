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

    public int updateFireRate;
    public int updateEvolveRate;
    public int evolveTimeMin;
    public int transmitTimeMin;
    public int startFireTime;

    public float evolveFireChance;
    public float transmissionChance;
    public float transmissionMaxDist;

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
    List<Vector3> ocupiedLocation;
    Dictionary<GameObject, int> objectStartTime;

    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        spawnLocations = new List<Transform>();
        smokes = new List<GameObject>();
        fires = new List<GameObject>();
        ocupiedLocation = new List<Vector3>();
        objectStartTime = new Dictionary<GameObject, int>();

        // Get all spawn locations
        GameObject[] spawnLocationsGameObj = GameObject.FindGameObjectsWithTag("SpawnLocation");
        foreach (var spawnLocation in spawnLocationsGameObj)
        {
            spawnLocations.Add(spawnLocation.transform);
        }

        startTime = Time.time;

        updateFireRate = Math.Max(2, updateFireRate);
        updateEvolveRate = Math.Max(2, updateEvolveRate);
    }

    void SpawnFirstFire()
    {
        System.Random rnd = new System.Random();
        int rndIndex = rnd.Next(0, spawnLocations.Count);

        GameObject smoke = Instantiate(effects[SMOKE], spawnLocations[rndIndex]);
        smokes.Add(smoke);
        objectStartTime.Add(smoke, (int)Math.Round(Time.time));

        Vector3 poz = new Vector3(0, 0, 0);
        poz = smoke.transform.position;
        ocupiedLocation.Add(poz);

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
        if (elapsedTime % updateEvolveRate == 0 && updateEvolveTried == false)
        {
            updateEvolveTried = true;
            CheckRequirementsEvolve();
        }
        if (elapsedTime % updateEvolveRate != 0)
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
            CheckRequirementsTransmit();
        }
        if (elapsedTime % updateFireRate != 0)
        {
            updateFireTried = false;
        }
       
    }

    void CheckRequirementsEvolve()
    {
        List<GameObject> availableSmokes = new List<GameObject>();
        foreach (var smoke in smokes)
        {
            int smokeStart = 0;
            if (objectStartTime.TryGetValue(smoke, out smokeStart))
            {
                var smokeAge = (int)Math.Round(Time.time) - smokeStart;
                if (smokeAge >= evolveTimeMin)
                {
                    availableSmokes.Add(smoke);
                }
            }
        }

        TryEvolveSmoke(availableSmokes);
    }

    void CheckRequirementsTransmit()
    {
        foreach (var fire in fires)
        {
            int fireStart = 0;
            if (objectStartTime.TryGetValue(fire, out fireStart))
            {
                var fireAge = (int)Math.Round(Time.time) - fireStart;
                if (fireAge >= transmitTimeMin)
                {
                    List<Transform> freeLocationsInRange = new List<Transform>();
                    foreach (var location in spawnLocations)
                    {
                        if (Vector3.Distance(location.transform.position, fire.transform.position) < transmissionMaxDist)
                        {
                            if (ocupiedLocation.Contains(location.position) == false)
                            {
                                freeLocationsInRange.Add(location);
                                break;
                            }
                        }
                    }

                    TryTransmitFire(fire, freeLocationsInRange);
                }
            }
            
        }
    }

    void TryEvolveSmoke(List<GameObject> smokesToEvolve)
    {
        int percent = (int)Math.Round(evolveFireChance * 100);
        System.Random rnd = new System.Random();

        for(int i = smokesToEvolve.Count-1; i >= 0; i--)
        {
            var smoke = smokesToEvolve[i];
            int chance = rnd.Next(0, 100);
            if (chance < percent)
            {
                EvolveSmokeToFire(smoke);
            }
        }
        
    }

    void TryTransmitFire(GameObject fire, List<Transform> locationsInRange)
    {
        int percent = (int)Math.Round(evolveFireChance * 100);
        System.Random rnd = new System.Random();
        foreach (var location in locationsInRange)
        {
            int chance = rnd.Next(0, 100);
            if (chance < percent)
            {
                TransmitFire(location);
            }
        }
    }

    void EvolveSmokeToFire(GameObject smoke)
    {
        // Add fire
        GameObject fire = Instantiate(effects[FIRE]);
        fire.transform.position = smoke.transform.position;
        objectStartTime.Add(fire, (int)Math.Round(Time.time));
        fires.Add(fire);

        // Remove smoke
        smokes.Remove(smoke);
        Destroy(smoke);
    }

    void TransmitFire(Transform nextSmokePosition)
    {
        // Add smoke
        GameObject smoke = Instantiate(effects[SMOKE]);
        smoke.transform.position = nextSmokePosition.position;

        objectStartTime.Add(smoke, (int)Math.Round(Time.time));
        Vector3 poz = new Vector3(0, 0, 0);
        poz = smoke.transform.position;
        ocupiedLocation.Add(poz);
        smokes.Add(smoke);
    }
}
