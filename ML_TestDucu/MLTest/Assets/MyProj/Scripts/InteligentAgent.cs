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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPosition = transform.position;
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
        controller.Move(originalPosition - transform.position);
    }

    public override void CollectObservations()
    {
        AddVectorObs(this.transform.position);
        AddVectorObs(Goal.position);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        int action = Mathf.FloorToInt(vectorAction[0]);

        // Goalies and Strikers have slightly different action spaces.
        if (action != 0)
        {
            switch (action)
            {
                case 1:
                    dirToGo = transform.forward * 1f;
                    break;
                case 2:
                    dirToGo = transform.forward * -1f;
                    break;
                case 3:
                    dirToGo = transform.right * -1f;
                    break;
                case 4:
                    dirToGo = transform.right * 1f;
                    break;
                
            }
            //transform.Rotate(rotateDir, 5f);
            controller.Move(dirToGo * Time.deltaTime * speed);
        }

        if (Vector3.Distance(Goal.position, this.transform.position) < 2.5)
        {
            this.GoalAchieved();
        }

        AddReward(-0.01f);

    }

    public void GoalAchieved()
    {
        System.Random r = new System.Random();
        AddReward(5.0f);
        ResetAgent();
        Done();

        StartCoroutine(GoalScoredSwapGroundMaterial(0.5f, Color.green));
    }

    IEnumerator GoalScoredSwapGroundMaterial(float time, Color color)
    {
        objRenderer.material.SetColor("_Color", color);
        yield return new WaitForSeconds(time); // Wait for 2 sec
        objRenderer.material.SetColor("_Color", Color.red);
    }
}
