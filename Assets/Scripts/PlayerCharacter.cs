using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private Collider2D playerGroundCollider;
    [SerializeField]
    private Collider2D playerWallCollider;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D groundDectTrigger;
    [SerializeField]
    private Collider2D wallDectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;
    [SerializeField]
    private ContactFilter2D wallContactFilter;

    private float horizontalInput;
    private bool isOnWall;
    private bool isOnGround;
    private Collider2D[] groundHitDectionResults = new Collider2D[16];
    private Collider2D[] wallHitDectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;

    // Update is called once per frame
    void Update()
    {
        UpdateIsOnWall();
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandeJumpInput();
    }

    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    private void UpdateIsOnWall()
    {
        isOnWall = wallDectTrigger.OverlapCollider(wallContactFilter, wallHitDectionResults) > 0;
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDectTrigger.OverlapCollider(groundContactFilter, groundHitDectionResults) > 0;
        // Debug.Log("IsOnGround?: " + isOnGround);
    }
    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandeJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown("Jump") && isOnWall)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();
    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;
    }

    public void Respawn()
    {
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
        }
    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
}