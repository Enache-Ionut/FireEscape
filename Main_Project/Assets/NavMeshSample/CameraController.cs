using UnityEngine;

public class CameraController : MonoBehaviour
{
  private const int cameraSpeed = 5;
  [SerializeField] private Camera[] cameras;

  public PlayerController playerController;

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      ChangeCamera();
    }

    MoveCamera();
  }


  private void ChangeCamera()
  {
    for (int i = 0; i < cameras.Length; i++)
    {
      if (cameras[i].enabled == true)
      {
        if (i == cameras.Length - 1)
        {
          Change(i, 0);
          break;
        }
        Change(i, i + 1);
        break;
      }
    }
  }


  private void Change(int currentCamera, int nextCamera)
  {
      cameras[nextCamera].enabled = true;
      cameras[currentCamera].enabled = false;
      playerController.cam = cameras[nextCamera];
  }


  private void MoveCamera()
  {
    if ("MainCamera" == playerController.cam.tag)
    {
      return;
    }

    if ("CameraTop" == playerController.cam.tag)
    {
      if (Input.GetKey(KeyCode.RightArrow))
      {
        playerController.cam.transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
        playerController.cam.transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, 0, 0));
      }
      if (Input.GetKey(KeyCode.DownArrow))
      {
        playerController.cam.transform.Translate(new Vector3(0, -cameraSpeed * Time.deltaTime, 0));
      }
      if (Input.GetKey(KeyCode.UpArrow))
      {
        playerController.cam.transform.Translate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
      }
    }
    else if ("Camera45" == playerController.cam.tag)
    {
      if (Input.GetKey(KeyCode.RightArrow))
      {
        playerController.cam.transform.localPosition += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
        playerController.cam.transform.localPosition += new Vector3(-cameraSpeed * Time.deltaTime, 0, 0);
      }
      if (Input.GetKey(KeyCode.DownArrow))
      {
        playerController.cam.transform.localPosition += new Vector3(0, 0, -cameraSpeed * Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.UpArrow))
      {
        playerController.cam.transform.localPosition += new Vector3(0, 0, cameraSpeed * Time.deltaTime);
      }
    }

  }
}
