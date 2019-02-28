using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI aliveText;
    [SerializeField] private TextMeshProUGUI deadText;
    [SerializeField] private TextMeshProUGUI timeText;

    private GameObject[] persons;
    private float currentTime = 0f;
    private int alive = 0;
    private int dead = 0;

    // Start is called before the first frame update
    void Start()
    {
        persons = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SetTime();
        UpdatePeople();
    }


    private void SetTime()
    {
        currentTime += Time.deltaTime;
        string minutes = Mathf.Floor(currentTime / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");
        StringBuilder sb = new StringBuilder();
        sb.Append("TIME    ").Append(minutes).Append(":").Append(seconds);

        timeText.text = sb.ToString();
    }


    private void UpdatePeople()
    {
        alive = 0;
        dead = 0;
        foreach (var item in persons)
        {
            if(item.gameObject.GetComponent<Person>().health <= 0)
            {
                dead++;
            }

            if (item.gameObject.GetComponent<Person>().health > 0)
            {
                alive++;
            }
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("ALIVE    ").Append(alive);
        aliveText.text = sb.ToString();

        sb.Clear();
        sb.Append("DEAD    ").Append(dead);
        deadText.text = sb.ToString();
    }
}
