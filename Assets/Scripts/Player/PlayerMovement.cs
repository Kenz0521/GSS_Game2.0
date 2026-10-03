using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;

    private PlayerControls controls;
    private Vector2 moveInput;

    int isWalkingHash;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

        controls = new PlayerControls();

        isWalkingHash = Animator.StringToHash("IsWalking");

        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
    }


    void OnEnable()
    {
        controls.Player.Enable();
    }


    void OnDisable()
    {
        controls.Player.Disable();
    }


    void OnDestroy()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
    }


    void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    void FixedUpdate()
    {
        Move();
        Turning();
        Animating();
    }


    void Move()
    {
        movement.Set(moveInput.x, 0f, moveInput.y);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }


    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }


    void Animating()
    {
        bool walking = moveInput.x != 0f || moveInput.y != 0f;

        anim.SetBool(isWalkingHash, walking);
    }
}
