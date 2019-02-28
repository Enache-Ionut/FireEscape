using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] GameObject[] effects;
    Transform[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spawnLocationsGameObj = GameObject.FindGameObjectsWithTag("SpawnLocation");
        spawnLocations = new Transform[spawnLocationsGameObj.Length];
        int count = 0;
        foreach (var spawnLocation in spawnLocationsGameObj)
        {
            spawnLocations[count] = spawnLocation.transform;
            count++;
        }

        SpawnRandomFires();
    }

    void SpawnRandomFires()
    {
        System.Random rnd = new System.Random();
        int rndnumber = rnd.Next(8, spawnLocations.Length);
        for (int i = 0; i < rndnumber; i++)
        {
            int rndindex = rnd.Next(0, spawnLocations.Length);
            Instantiate(effects[rnd.Next(0, 2)], spawnLocations[rndindex]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
