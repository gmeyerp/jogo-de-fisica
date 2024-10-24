using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float maxTurretSpeed = 10f;
    [SerializeField] float turretForce = 2f;
    Vector3 movement;
    public bool isGrounded { get; private set; }
    bool isMovementEnabled;
    TurretBlock mount;

    [Header("Damage")]
    [SerializeField] float invincibilityTime = 1f;
    bool isInvincible;

    [Header("Coins")]
    [SerializeField] CoinStack coinStack;

    [Header("FX")]
    [SerializeField] ParticleSystem jumpVFX;


    #region Actions
    public InputActionAsset inputActions;
    InputActionMap playerActions;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction pauseAction;
    #endregion

    void Awake()
    {
        if (instance == null)
        { instance = this; }
        else
        { Destroy(this.gameObject); }

        playerActions = inputActions.FindActionMap("Player");

        moveAction = playerActions.FindAction("Move");
        playerActions.FindAction("Jump").performed += OnJump;
        playerActions.FindAction("Pause").performed += OnPause;

        EnableMovement();
    }

    void Update()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        movement = new Vector3(movementInput.x, 0, movementInput.y);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!isMovementEnabled) return;
        if (movement == Vector3.zero) return;

        if (mount == null)
        {
            Move(movement * speed);
        }
        else
        {
            mount.Move(movement * turretForce, maxTurretSpeed);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!isMovementEnabled) return;
        if (!isGrounded) return;


        if (mount == null)
        {
            Jump();
        }
        else
        {
            Dismount();
        }
        jumpVFX.Play();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        GameManagement.instance.PauseGame();
    }

    void OnEnable()
    {
        playerActions.Enable();
    }
    void OnDisable()
    {
        playerActions.FindAction("Jump").performed -= OnJump;
        playerActions.FindAction("Pause").performed -= OnPause;
        playerActions.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 7)
        {
            isGrounded = true;
        }

        if (collision.collider.gameObject.TryGetComponent(out TurretBlock turret))
        {
            Mount(turret);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 7)
        {
                GameManagement.instance.SetSafePosition(collision.collider.gameObject.transform);
        }
    }

    private void Move(Vector3 force)
    {
        rb.MovePosition(transform.position + force * Time.fixedDeltaTime);
    }

    private void Launch(Vector3 force)
    {
        //GameManagement.instance.SetSafePosition(transform.position); nao parece necessario com OnCollisionExit
        rb.AddForce(force, ForceMode.Impulse);

        if (force.y > 0)
        { isGrounded = false; }
    }
    private void Jump() => Launch(Vector3.up * jumpForce);

    private void Mount(TurretBlock block)
    {
        mount = block;
        
        rb.isKinematic = true;
        ChangeBuyStatus(true);
        block.Mount(transform);

        coinStack.Collect();
    }

    private void Dismount()
    {
        mount.Dismount();
        rb.isKinematic = false;
        ChangeBuyStatus(false);

        float dismountForce = jumpForce * 1.2f;
        Launch(/*mount.Velocity + */(Vector3.up * dismountForce));

        mount = null;
    }

    public void UpWind(float windForce)
    {
        rb.AddForce(transform.up * windForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    void ChangeBuyStatus(bool status) => GameCanvas.instance.UpgradeButtonStatus(status, mount.turret);

    IEnumerator InvincibilityOn(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    public void TakeDamage()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityOn(invincibilityTime));
            GameManagement.instance.ReduceHealth();
        }
    }

    public void EnableMovement() => isMovementEnabled = true;
    public void DisableMovement() => isMovementEnabled = false;
}
