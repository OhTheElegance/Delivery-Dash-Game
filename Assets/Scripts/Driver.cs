using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{

    [SerializeField] float MoveSpeed;
    [SerializeField] float SteerSpeed;
    [HideInInspector] public float SprintMultiplier = 1.0f;
    Rigidbody2D rb;

    [SerializeField] ParticleSystem Smoke;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void StartSmoke()
    {
        if (!Smoke.isPlaying)
        {
            Smoke.Play();
        }
    }

    void StopSmoke()
    {
        if (Smoke.isPlaying)
        {

            Smoke.Stop();
        }
    }


    void Update()
    {
        float Move = 0f;
        float Steer = 0f;
        bool isMoving = false;

        if (Keyboard.current.wKey.isPressed)
        {
            isMoving = true;
            Move = -1f;
        }

        if (Keyboard.current.aKey.isPressed)
        {

            isMoving = true;
            Steer = 1f;
        }

        if (Keyboard.current.sKey.isPressed)
        {

            isMoving = true;
            Move = 1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {

            isMoving = true;
            Steer = -1f;
        }


        if (isMoving)
        {
            StartSmoke();
        }
        else
        {
            StopSmoke();
        }


        float MoveAmount = Move * MoveSpeed * SprintMultiplier * Time.deltaTime;
        float SteerAmount = Steer * SteerSpeed * SprintMultiplier * Time.deltaTime;

        rb.linearVelocity = transform.up * MoveAmount;
        if (Move != 0)
        {
            rb.rotation += SteerAmount;
        }
    }

    }
