using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float jumpStrength, moveSpeed;
    private Rigidbody rb;
    private int score = 0;
    private bool isAlive = true;
    [SerializeField]
    private UIManager uiMan;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpStrength, 0));
        }
        float moveDir = Input.GetAxis("Horizontal");

        moveDir *= moveSpeed;
        Vector3 movement = new Vector3(moveDir, 0, 0);
        this.gameObject.transform.position += movement;
    }
}
