using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D body;
    private Animator animator;
    private Collider2D bodyCollider;
    private Collider2D feetCollider;
    private LayerMask groundLayer;
    private LayerMask ladderLayer;
    private LayerMask enemiesLayer;
    private LayerMask hazardsLayer;

    [SerializeField] private float runSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Vector2 deathKick = new Vector2(5, 25);
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform bow;
    public bool isAlive = true;

    private float defaultGravity;
    private float standardAnimationSpeed;
    private float pauseAnimationSpeed = 0;

    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsClimbing = Animator.StringToHash("isClimbing");
    private static readonly int Dying = Animator.StringToHash("Dying");

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

        groundLayer = LayerMask.GetMask("Ground");
        ladderLayer = LayerMask.GetMask("Ladders");
        enemiesLayer = LayerMask.GetMask("Enemies");
        enemiesLayer = LayerMask.GetMask("Hazards");

        defaultGravity = body.gravityScale;
        standardAnimationSpeed = animator.speed;
    }

    void Update()
    {
        CheckDeath();

        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        PauseAnimationWhenNotMovingOnLadder();


    }

    // Called by Input system
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // When Jump Input button is pressed, called by Input system
    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!feetCollider.IsTouchingLayers(groundLayer)) return;

        if (value.isPressed) body.velocity += new Vector2(0f, jumpForce);

    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;

        if (value.isPressed) Instantiate(arrow, bow.position, transform.rotation);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, body.velocity.y);
        body.velocity = playerVelocity;

        bool playerIsRunning = Math.Abs(moveInput.x) > Mathf.Epsilon;
        animator.SetBool(IsRunning, playerIsRunning);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(body.velocity.x), 1f);
        }

    }

    void ClimbLadder()
    {
        if (!isAlive) return;

        bool playerIsClimbing = bodyCollider.IsTouchingLayers(ladderLayer);
        animator.SetBool(IsClimbing, playerIsClimbing);

        if (playerIsClimbing)
        {
            Vector2 playerVelocity = new Vector2(body.velocity.x, moveInput.y * climbSpeed);
            body.velocity = playerVelocity;
            body.gravityScale = 0;
        }
        else
        {
            body.gravityScale = defaultGravity;
        }

    }

    void PauseAnimationWhenNotMovingOnLadder()
    {
        animator.speed = standardAnimationSpeed;
        bool playerIsClimbing = bodyCollider.IsTouchingLayers(ladderLayer);
        if (!playerIsClimbing) return;

        bool playerIsNotIdle =
            Mathf.Abs(body.velocity.y) > Mathf.Epsilon ||
            Mathf.Abs(body.velocity.x) > Mathf.Epsilon;

        if (playerIsNotIdle) return;
        animator.speed = pauseAnimationSpeed;
    }

    void CheckDeath()
    {
        if (!isAlive) return;

        if (bodyCollider.IsTouchingLayers(enemiesLayer) ||
            bodyCollider.IsTouchingLayers(hazardsLayer))
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger(Dying);
        body.velocity = deathKick;

        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSecondsRealtime(2);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    public void DisableControls()
    {
        isAlive = false;
    }

/*
[SerializeField] private float maxExtraJumps;
private float extraJumps;

if (value.isPressed && extraJumps > 0)
    {
        body.velocity += new Vector2(0f, jumpForce);
        extraJumps--;
        Debug.Log("Remaining jumps:" + extraJumps);
    }
 
void RefreshJumps()
{
    bool playerIsOnTheGround = collider.IsTouchingLayers(groundLayer);

    if (playerIsOnTheGround)
    {
        extraJumps = maxExtraJumps;
    }
}
*/
}
