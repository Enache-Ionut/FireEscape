using UnityEngine;

public class CameraController : MonoBehaviour
{
  private const int cameraSpeed = 5;
  [SerializeField] private Camera[] cameras;

  // Start is called before the first frame update
  private void Start()
  {
    cameras[0].enabled = true;
    cameras[1].enabled = false;
    cameras[2].enabled = false;

  }

  // Update is called once per frame
  private void Update()
  {
    var currentCamera = GetCurrentCamera();

    if (Input.GetKeyDown(KeyCode.C))
      currentCamera = ChangeCamera();

    MoveCamera(currentCamera);
  }


  private Camera GetCurrentCamera()
  {
    foreach (var cam in cameras)
      if (cam.enabled == true)
        return cam;

    return Camera.main;
  }


  private Camera ChangeCamera()
  {
    for (int i = 0; i < cameras.Length; ++i)
    {
      if (cameras[i].enabled == true)
      {
        if (i == 0)
          return CameraTopActivate();

        if (i == 1)
          return Camera45Activate();

        if (i == 2)
          return CameraMainActivate();
      }
    }

    return Camera.main;
  }

  private void MoveCamera(Camera camera)
  {
    if ("MainCamera" == camera.tag)
      return;

    else if ("CameraTop" == camera.tag)
    {
      if (Input.GetKey(KeyCode.RightArrow))
      {
        camera.transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
        camera.transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, 0, 0));
      }
      if (Input.GetKey(KeyCode.DownArrow))
      {
        camera.transform.Translate(new Vector3(0, -cameraSpeed * Time.deltaTime, 0));
      }
      if (Input.GetKey(KeyCode.UpArrow))
      {
        camera.transform.Translate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
      }
    }
    else if ("Camera45" == camera.tag)
    {
      if (Input.GetKey(KeyCode.RightArrow))
      {
        camera.transform.localPosition += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
        camera.transform.localPosition += new Vector3(-cameraSpeed * Time.deltaTime, 0, 0);
      }
      if (Input.GetKey(KeyCode.DownArrow))
      {
        camera.transform.localPosition += new Vector3(0, 0, -cameraSpeed * Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.UpArrow))
      {
        camera.transform.localPosition += new Vector3(0, 0, cameraSpeed * Time.deltaTime);
      }
    }

  }


  private Camera CameraMainActivate()
  {
    cameras[0].enabled = true;
    cameras[1].enabled = false;
    cameras[2].enabled = false;

    return cameras[0];
  }


  private Camera CameraTopActivate()
  {
    cameras[0].enabled = false;
    cameras[1].enabled = true;
    cameras[2].enabled = false;

    return cameras[1];
  }


  private Camera Camera45Activate()
  {
    cameras[0].enabled = false;
    cameras[1].enabled = false;
    cameras[2].enabled = true;

    return cameras[2];
  }


}
