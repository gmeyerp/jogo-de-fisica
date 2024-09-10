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
    Vector3 movement;
    bool isGrounded;
    bool isMovementEnabled;
    TurretBlock mount;

    [Header("Damage")]
    [SerializeField] float invincibilityTime = 1f;
    bool isInvincible;

    #region Actions
    public InputActionAsset inputActions;
    InputActionMap playerActions;
    InputAction moveAction;
    InputAction jumpAction;
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
            mount.Move(movement * speed);
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
    }

    void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
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
            GameManagement.instance.SetSafePosition(collision.collider.gameObject.transform.position - Vector3.forward);
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
        block.Mount(transform);
        mount = block;
        rb.isKinematic = true;
    }

    private void Dismount()
    {
        mount.Dismount();
        mount = null;
        rb.isKinematic = false;

        float dismountForce = jumpForce * 1.2f;
        Launch(/*mountVelocity + */(Vector3.up * dismountForce));
    }

    IEnumerator InvincibilityOn(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    public void ApplyDamage()
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
