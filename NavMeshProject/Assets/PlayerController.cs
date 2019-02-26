using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

  //public Camera cam;
  public NavMeshAgent agent;

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //  Debug.Log("Update!!\n");
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        NavMeshHit navmeshHit;

        int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");

        if (NavMesh.SamplePosition(hit.point, out navmeshHit, 1.0f, walkableMask))
        {
          agent.SetDestination(navmeshHit.position);
        }

        // move the agent
        //agent.SetDestination(hit.point);

      }
    }
  }
}
