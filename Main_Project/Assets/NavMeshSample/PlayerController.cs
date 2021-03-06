﻿using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
  public NavMeshAgent agent;

  public ThirdPersonCharacter character;

  private int waitCounter = 0;

  private GameObject[] exits;
  private Transform closestGameObjectTransform;
  private bool timer = false;
  private float startTime = 0f;

  private void Start()
  {
    agent.updateRotation = false;
    exits = GameObject.FindGameObjectsWithTag("Exit");

    Transform[] exitsTrasnforms = exits.Select(exit => exit.GetComponent<Transform>()).ToArray();
    closestGameObjectTransform = GetClosestExit(exitsTrasnforms);
    startTime = Time.time;

  }

  private Transform GetClosestExit(Transform[] enemies)
  {
    Transform tMin = null;
    float minDist = Mathf.Infinity;
    Vector3 currentPos = transform.position;
    foreach (Transform t in enemies)
    {
      float dist = Vector3.Distance(t.position, currentPos);
      if (dist < minDist)
      {
        tMin = t;
        minDist = dist;
      }
    }
    return tMin;
  }


  // Update is called once per frame
  private void Update()
  {
    if (Time.time - startTime > 5f)
    {
      agent.SetDestination(closestGameObjectTransform.position);


      if (agent.remainingDistance > agent.stoppingDistance)
      {
        character.Move(agent.desiredVelocity, false, false);
      }
      else
      {
        character.Move(Vector3.zero, false, false);
      }
    }
  }

  private void DisableNaveMashObstacle(string objectTag)
  {
    GameObject[] naveMeshObstacles = GameObject.FindGameObjectsWithTag(objectTag);
    for (int i = 0; i < naveMeshObstacles.Length; i++)
    {
      naveMeshObstacles[i].GetComponent<NavMeshObstacle>().carving = false;
      //naveMeshObstacles[i].GetComponent<BoxCollider>().enabled = false;
      naveMeshObstacles[i].GetComponent<MeshRenderer>().enabled = false;
    }
  }
}
