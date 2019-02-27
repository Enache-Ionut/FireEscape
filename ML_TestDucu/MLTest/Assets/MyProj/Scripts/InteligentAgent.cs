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
    private float reward = 0.01f;

    private Vector3 lastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPosition = transform.position;
        lastPosition = originalPosition;
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
        System.Random r = new System.Random();
        this.transform.position = originalPosition;
        lastPosition = originalPosition;
    }

    public override void CollectObservations()
    {
        //float rayDistance = 50f;
        //float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f };
        //string[] detectableObjects = { "Obstacle", "Goal", "Agent"};
        //AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(this.transform.position);
        //AddVectorObs(Vector2.Distance(this.transform.position, Goal.position));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        controller.Move(controlSignal * Time.deltaTime * speed);

        var currDist = Vector3.Distance(transform.position, Goal.position);
        var lastDist = Vector3.Distance(lastPosition, Goal.position);
        lastPosition = transform.position;
        /*
        if (currDist < lastDist)
        {
            AddReward(reward);
        }
        */
        AddReward(punishment);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
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
        AddReward(1000 * reward);
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
