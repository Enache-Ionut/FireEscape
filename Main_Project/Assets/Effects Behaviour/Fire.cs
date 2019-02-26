﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private float damageTimer = 1f;
    bool doingDamage = false;

    private void OnTriggerEnter(Collider collider)
    {
        Person person = collider.GetComponent<Person>();
        doingDamage = true;

        if (person != null)
        {
            StartCoroutine (DoDamage(damage, person));
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        doingDamage = false;
    }

    IEnumerator DoDamage(int damage, Person person)
    {
        while (doingDamage)
        {
            person.health -= damage;
            yield return new WaitForSeconds(damageTimer);
        }
    }
}