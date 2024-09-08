using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [Header("Movement")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    Vector3 movement;
    bool isGrounded;

    [Header("Damage")]
    [SerializeField] float invincibilityTime = 1f;
    bool isInvincible;
    #region Actions
    public InputActionAsset actions;
    InputActionMap player;
    InputAction moveAction;
    InputAction jump;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        //player = actions.FindActionMap("Player"); //Testar isso depois para não repetir código
        moveAction = actions.FindActionMap("Player").FindAction("Move");
        actions.FindActionMap("Player").FindAction("Jump").performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        movement = new Vector3(moveVector.x, 0, moveVector.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            //GameManagement.instance.SetSafePosition(transform.position); nao parece necessario com OnCollisionExit
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange); // Se debuffs afetarem o peso isso deve ser alterado
        }
        isGrounded = false;
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }

    public void TakeDamage()
    {
        if (!isInvincible) 
        {
            StartCoroutine(InvincibilityOn(invincibilityTime));
            GameManagement.instance.ReduceHealth();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 7)
        {
            GameManagement.instance.SetSafePosition(collision.collider.gameObject.transform.position - Vector3.forward);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 7)
        {
            isGrounded = true;
        }
    }

    IEnumerator InvincibilityOn(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

}
