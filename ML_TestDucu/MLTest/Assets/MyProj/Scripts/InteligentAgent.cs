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

  void Start()
  {
    controller = GetComponent<CharacterController>();
  }
  public override void InitializeAgent()
  {
    base.InitializeAgent();

    objRenderer = GetComponent<Renderer>();
    objMaterial = objRenderer.material;
  }

  public override void AgentReset()
  {
    System.Random r = new System.Random();
    transform.position = new Vector3(3, 0.6f, r.Next(-4, 4));
    Obstacle.position = new Vector3(r.Next(-2, 0), 0.5f, r.Next(-2, 2));
    Obstacle.localScale = new Vector3(0.5f, 1, r.Next(3, 6));
  }
  public override void CollectObservations()
  {
    // Target and Agent positions
    AddVectorObs(this.transform.position);
    AddVectorObs(Goal.position);
  }

  public override void AgentAction(float[] vectorAction, string textAction)
  {
    // Actions, size = 2
    Vector3 controlSignal = Vector3.zero;
    controlSignal.x = vectorAction[0];
    controlSignal.z = vectorAction[1];
    controller.Move(controlSignal * Time.deltaTime * speed);

    float distanceToTarget = Mathf.Abs(this.transform.position.x -
                                              Goal.position.x);

    if(distanceToTarget < 1)
    {
      this.GoalAchieved();
    }
    else
    {
      if (Vector3.Distance(this.transform.position, Obstacle.position) < 2)
        AddReward(-0.02f);

      if (Vector3.Distance(this.transform.position, Wall1.position) < 2)
        AddReward(-0.02f);

      if (Vector3.Distance(this.transform.position, Wall1.position) < 2)
        AddReward(-0.02f);

      if (Vector3.Distance(this.transform.position, Wall1.position) < 2)
        AddReward(-0.02f);

      AddReward(-0.01f);
    }
  }

  public void GoalAchieved()
  {
    System.Random r = new System.Random();
    AddReward(5.0f);
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
