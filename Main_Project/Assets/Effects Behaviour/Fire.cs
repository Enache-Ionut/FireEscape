using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Effect
{
    [SerializeField] private float increaseSizeTimer = 1f;
    private float maxScale = 10f;

    private void Start()
    {
        StartCoroutine(ScaleFire());
    }

    IEnumerator ScaleFire()
    {
        while (transform.transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(1f, 1f, 1f);
            yield return new WaitForSeconds(increaseSizeTimer);
        }
    }
}
