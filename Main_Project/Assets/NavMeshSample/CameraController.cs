using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const int cameraSpeed = 20;
    [SerializeField] private Camera[] cameras;

    private Camera currentPlayerCamera;


  private void Start()
  {
    currentPlayerCamera = cameras[0];
  }


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
        currentPlayerCamera = cameras[nextCamera];
    }


    private void MoveCamera()
    {
        if ("MainCamera" == currentPlayerCamera.tag)
        {
            return;
        }

        if ("CameraTop" == currentPlayerCamera.tag)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentPlayerCamera.transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentPlayerCamera.transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                currentPlayerCamera.transform.Translate(new Vector3(0, -cameraSpeed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                currentPlayerCamera.transform.Translate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
            }
        }
        else if ("Camera45" == currentPlayerCamera.tag)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentPlayerCamera.transform.localPosition += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentPlayerCamera.transform.localPosition += new Vector3(-cameraSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                currentPlayerCamera.transform.localPosition += new Vector3(0, 0, -cameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                currentPlayerCamera.transform.localPosition += new Vector3(0, 0, cameraSpeed * Time.deltaTime);
            }
        }

    }
}
