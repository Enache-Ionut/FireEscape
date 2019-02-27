using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class InteligentAgent : Agent
{
    public Transform Goal;

    public float speed;
    private CharacterController controller;
    Material objMaterial;
    Renderer objRenderer;
    public float zPoz;
    private RayPerception rayPer;

    private Vector3 originalPosition;
    private float punishment = -0.01f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPosition = new Vector3(3, 0.6f, 0);
    }

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        rayPer = GetComponent<RayPerception>();
        objRenderer = GetComponent<Renderer>();
        objMaterial = objRenderer.material;
    }

    public void ResetAgent()
    {
        punishment = -0.01f;
        System.Random r = new System.Random();
        this.transform.position = originalPosition;
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

        AddReward(punishment*5);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            AddReward(punishment * 5);
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
        AddReward(100.0f);
        Done();
        ResetAgent();
        StartCoroutine(GoalScoredSwapGroundMaterial(0.5f));
    }

    IEnumerator GoalScoredSwapGroundMaterial(float time)
    {
        objRenderer.material.SetColor("_Color", Color.green);
        yield return new WaitForSeconds(time); // Wait for 2 sec
        objRenderer.material.SetColor("_Color", Color.red);
    }
}
