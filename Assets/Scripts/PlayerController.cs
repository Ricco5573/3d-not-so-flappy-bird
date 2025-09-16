using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [Header("Tilt Settings"), SerializeField]
    private float leanAmount = 15f;      // How much to tilt based on movement
    private float leanSpeed = 5f;        // How quickly the tilt smooths into place

    [Header("Player Movement"), SerializeField]
    private float jumpStrength, moveSpeed;
    private Rigidbody rb;
    private int score = 0;
    private bool started = false;
    private bool isAlive = true;
    [Header("things i was too lazy to assign in code"),SerializeField]
    private UIManager uiMan;
    private GameManager gameManager;

    private List<ParticleSystem> engineParticles;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        engineParticles = new List<ParticleSystem>();
        engineParticles.AddRange(gameObject.GetComponentsInChildren<ParticleSystem>());
        foreach (ParticleSystem particle in engineParticles)
        {
            particle.Stop();
        }
    }
    void Update()
    {
        if (isAlive)
        {
            Movement();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle" && isAlive)
        {
            isAlive = false;
            uiMan.PlayerDeath();
            gameManager.ResetGame();
            foreach (ParticleSystem particle in engineParticles)
            {
                particle.Stop();
            }
        }
    }
    /* void OnMove(InputValue movementValue)
     {
         Vector2 moveDir = movementValue.Get<Vector2>();

         moveDir *= moveSpeed;
         Vector3 movement = new Vector3(moveDir.x, 0, 0);
         this.gameObject.transform.position += movement;
     }*/
    private void Movement()
    {
        Quaternion targetRotation = quaternion.identity;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpStrength, 0));
            if (!started)
            {
                StartGame();
            }
        }
        if (started)
        {
            float moveDir = Input.GetAxis("Horizontal");

            moveDir *= moveSpeed;
            Vector3 movement = new Vector3(moveDir, 0, 0);
            this.gameObject.transform.position += movement;
            float lean = movement.x * leanAmount;
            targetRotation = quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, lean);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * leanSpeed);
        }
    }


    private void StartGame()
    {
        gameManager.StartGame();
        started = true;
        foreach (ParticleSystem particle in engineParticles)
        {
            particle.Play();
        }
        rb.isKinematic = false;
    }
    private void HandleParticles()
    {
        if (!started)
        {
            foreach(ParticleSystem particle in engineParticles)
            {
                particle.playbackSpeed = gameManager.GetGameSpeed(); //i get its deprecated, but the suggested other solution doesnt work.
            }
        }
    }
}
