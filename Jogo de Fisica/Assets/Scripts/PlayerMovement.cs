using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    Vector3 lastSafePosition;
    Vector3 movement;
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
        GameManagement.instance.SetSafePosition(transform.position);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange); // Se debuffs afetarem o peso isso deve ser alterado
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
        GameManagement.instance.ReduceHealth();
    }
}
