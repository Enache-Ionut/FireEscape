using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class InteligentAcademy : Academy
{
    public Transform Obstacle;

    public override void AcademyReset()
    {
        base.AcademyReset();
        System.Random r = new System.Random();
        //Obstacle.transform.position = new Vector3(-3, 0.5f, r.Next(0, 2));
    }
}
