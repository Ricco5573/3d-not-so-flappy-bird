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

    [Header("Player settings"), SerializeField]
    private float jumpStrength;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int health = 3;
    private Rigidbody rb;

    [Header("Player controlls")]
    [SerializeField]
    private InputAction move;
    [SerializeField]    
    private InputAction jump;

    [Header("Sounds"), SerializeField]
    private AudioSource engineSound;
    [SerializeField]
    private AudioSource playerSound;
    [SerializeField]
    private AudioClip hitSound;
    [SerializeField]
    private AudioClip jumpSound;

    private Camera cam;

    private bool started = false;
    private bool isAlive = true;
    [Header("things i was too lazy to assign in code"),SerializeField]
    private UIManager uiMan;
    private GameManager gameManager;

    private List<ParticleSystem> engineParticles;

    private void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        cam = Camera.main;
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
            HandleAudio();
        }
    }


    private void HandleAudio()
    {
        if(isAlive && started)
        {
            engineSound.pitch = gameManager.GetGameSpeed();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && isAlive)
        {
            playerSound.clip = hitSound;
            playerSound.volume = 0.75f;
            playerSound.Play();
            if (health == 1)
            {
                health = 0;
                isAlive = false;
                uiMan.PlayerDeath();
                gameManager.ResetGame();
                foreach (ParticleSystem particle in engineParticles)
                {
                    particle.Stop();
                }
                engineSound.Stop();
            }
            else
            {
                health -= 1;
                Destroy(collision.gameObject);
            }
        }
    }
    private void Movement()
    {
        Quaternion targetRotation = quaternion.identity;
        if (jump.triggered)
        {
            playerSound.clip = jumpSound;
            playerSound.volume = 0.25f;
            playerSound.Play();
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpStrength, 0));
            if (!started)
            {
                StartGame();
            }
        }
        if (started)
        {
            float moveDir = move.ReadValue<Vector2>().x;

            moveDir *= moveSpeed * gameManager.GetGameSpeed();
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
        engineSound.Play();
        rb.isKinematic = false;
    }
}
