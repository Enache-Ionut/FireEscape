﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    GameObject gameObject = other.gameObject;
    gameObject.GetComponent<Person>().free = true;
    gameObject.SetActive(false);
  }
}
