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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 moveDir = movementValue.Get<Vector2>();
        moveDir *= moveSpeed;
        rb.AddForce(moveDir);
    }
    private void Movement()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0));
        }
        
    }
}
