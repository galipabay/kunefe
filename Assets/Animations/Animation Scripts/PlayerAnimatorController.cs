using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public InputSystem_Actions controls; // Reference to the input actions
    Vector2 moveInput; // Store the movement input
    Vector3 moveDirection; // Store the movement direction

    private Animator animator; // Reference to the Animator component

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
        controls = new InputSystem_Actions(); // Initialize the input actions

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Bind the movement input action

        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero; // Reset the input when the action is canceled

    }

    private void Start() => animator = GetComponent<Animator>(); // Ensure the Animator component is assigned

    private void OnEnable() => controls.Enable(); // Enable the input actions

    private void OnDisable() => controls.Disable(); // Disable the input actions

    private void Update()
    {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized; // Calculate the movement direction based on the input
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World); // Move the player based on the input and speed

        animator.SetFloat("Speed", moveDirection.magnitude);// Set the animator parameter for speed based on the movement direction magnitude

        //Movedirection mmagnitude degerini console'a yazdýrma (isteðe baðlý)
        Debug.Log("Move Direction Magnitude: " + moveDirection.magnitude);
    }
}
