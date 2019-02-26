using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Person : MonoBehaviour
{
    public int health = 100;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private double weight = 25.6;
    [SerializeField] private int age = 39;
    [SerializeField] private Emotions emotions;
    [SerializeField] private Intelligence intelligence;

    private enum Emotions { Neutral, Anger, Fear, Sad, Confident, Happy, Psychotic };
    private enum Intelligence { AI_Nav, Smart, Average, Dumb };
    private ReadOnlyDictionary<string, float> speeds = new ReadOnlyDictionary<string, float>(new Dictionary<string, float>
    {
        {"Slow", 2.0f},
        {"Medium", 4.0f},
        {"Fast", 7.0f},
        {"Stop", 0f}
    });
    private CharacterController controller;
    private Vector3 moveDirection = new Vector3(0, 5, 0);
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSpeedAndColorDependingOnHealth();
        MovePerson();
    }

    private void SetSpeedAndColorDependingOnHealth()
    {
        switch (health)
        {
            case int n when (n == 100):
                speed = speeds["Slow"];
                break;

            case int n when (n < 100 && n > 70):
                speed = speeds["Medium"];
                break;

            case int n when (n <= 70 && n > 0):
                SetColorWhenHealthChanges(Color.yellow);
                speed = speeds["Fast"];
                break;

            case int n when (n <= 0):
                SetColorWhenHealthChanges(Color.red);
                speed = speeds["Stop"];
                break;
        }
    }

    private void SetColorWhenHealthChanges(Color color)
    {
            renderer.material.SetColor("_Color", color);    
    }

    private void MovePerson()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void MoveForward()
    {

    }

    public void MoveUp()
    {

    }

    public void MoveDown()
    {

    }

    public void MoveLeft()
    {

    }

    public void MoveRight()
    {

    }

    public void Rotate()
    {

    }
}
