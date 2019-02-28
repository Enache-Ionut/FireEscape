using System.Linq;
using UnityEngine;

public class SpawnPlayerController : MonoBehaviour
{
  private GameObject[] positions;
  public PlayerController player;

  // Start is called before the first frame update
  private void Start()
  {
    //var playersLocations = GameObject.FindGameObjectsWithTag("PlayerSpawnLocation").Select(obj => obj.transform);
    //foreach(var location in playersLocations)
    //{
    //  Instantiate(player, location.position, location.rotation);
    //}
  }

  // Update is called once per frame
  private void Update()
  {

  }
}
