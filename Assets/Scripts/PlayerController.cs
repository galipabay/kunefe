using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private InputSystem_Actions controls;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        controls.Player.Move.performed += ctx =>
        {
            moveInput = ctx.ReadValue<Vector2>();
        };

        controls.Player.Move.canceled += ctx =>
        {
            moveInput = Vector2.zero;
        };
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        // Parmağın sürüklendiği yönde karakteri hareket ettir
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

    }
}
