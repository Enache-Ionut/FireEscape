using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
  public Camera cam;
  public NavMeshAgent agent;

  public Transform spawneePosition;
  public GameObject obstacole;

  public ThirdPersonCharacter character;


  void Start()
  {
    agent.updateRotation = false;
  }


  // Update is called once per frame
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        agent.SetDestination(hit.point);
        Instantiate(obstacole, spawneePosition.position, spawneePosition.rotation);

        // DisableNaveMashObstacle("Obstacle");
      }
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
