using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class InteligentAgent : Agent
{
  public Transform Goal;
  public Transform Obstacle;
  public Transform Wall1;
  public Transform Wall2;
  public Transform Wall3;

  public float speed;
  private CharacterController controller;
  Material objMaterial;
  Renderer objRenderer;
  public float zPoz;
  private RayPerception rayPer;

  private float punishment = -0.01f;

  void Start()
  {
    controller = GetComponent<CharacterController>();
  }
  public override void InitializeAgent()
  {
    base.InitializeAgent();
        rayPer = GetComponent<RayPerception>();
        objRenderer = GetComponent<Renderer>();
    objMaterial = objRenderer.material;
  }

  public override void AgentReset()
  {
    punishment = -0.01f;
    System.Random r = new System.Random();
    transform.position = new Vector3(3, 0.6f, zPoz);
  }

  public override void CollectObservations()
  {
     float rayDistance = 50f;
     float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
     string[] detectableObjects = { "Obstacle", "Goal", "Agent"};
     AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
     AddVectorObs(this.transform.position);
    }

  public override void AgentAction(float[] vectorAction, string textAction)
  {
    // Actions, size = 2
    Vector3 controlSignal = Vector3.zero;
    controlSignal.x = vectorAction[0];
    controlSignal.z = vectorAction[1];
    controller.Move(controlSignal * Time.deltaTime * speed);

        AddReward(punishment);

  }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            AddReward(punishment * 50);
        }
        if (hit.gameObject.tag == "Agent")
        {
            AddReward(punishment * 5);
        }
        if (hit.gameObject.tag == "Goal")
        {
            GoalAchieved();
        }
    }

    public void GoalAchieved()
  {
    System.Random r = new System.Random();
    AddReward(100.0f);
    transform.position = new Vector3(3, 0.6f, r.Next(-4, 4));
    Done();

    StartCoroutine(GoalScoredSwapGroundMaterial(0.5f));
  }

  IEnumerator GoalScoredSwapGroundMaterial(float time)
  {
    objRenderer.material.SetColor("_Color", Color.green);
    yield return new WaitForSeconds(time); // Wait for 2 sec
    objRenderer.material.SetColor("_Color", Color.red);
  }
}
