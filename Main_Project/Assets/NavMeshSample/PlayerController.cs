using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
  public Camera cam;
  public NavMeshAgent agent;

  public Transform spawneePosition;
  public ThirdPersonCharacter character;

  private int waitCounter = 0;

  private GameObject[] exits;
  private Transform closestGameObjectTransform;

  private void Start()
  {
    agent.updateRotation = false;
    exits = GameObject.FindGameObjectsWithTag("Exit");
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

    if (waitCounter < 100)
    {
      ++waitCounter;
      return;
    }


    Transform[] exitsTrasnforms = exits.Select(exit => exit.GetComponent<Transform>()).ToArray();
    closestGameObjectTransform = GetClosestExit(exitsTrasnforms);


    Ray ray = cam.ScreenPointToRay(closestGameObjectTransform.localScale);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit))
    {
      agent.SetDestination(hit.point);
      //Instantiate(obstacole, spawneePosition.position, spawneePosition.rotation);
      // DisableNaveMashObstacle("Obstacle");
    }

    if (agent.remainingDistance > agent.stoppingDistance)
    {
      character.Move(agent.desiredVelocity, false, false);
    }
    else
    {
      character.Move(Vector3.zero, false, false);
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
