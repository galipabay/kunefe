using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Vector2 moveInput;
    private Vector3 moveDirection;
    private bool isGrounded = true;

    private Rigidbody rb;
    private InputSystem_Actions controls;
    private PlayerAnimatorController animatorController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animatorController = GetComponent<PlayerAnimatorController>();

        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();

        controls.Player.Attack.performed += ctx => animatorController.TriggerAttack();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);

        animatorController.SetSpeed(moveDirection.magnitude);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animatorController.TriggerJump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
