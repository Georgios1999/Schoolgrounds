using UnityEngine;
using Unity.Netcode;

public class movement : NetworkBehaviour
{
    private CharacterController controller;
    Vector3 velocity = Vector3.zero;
    Transform camera;
    Animator animator;

    [SerializeField]
    public float defaultSpeed = 0.08f;
    public float sensitivity = 10;
    public float jumpForce = 5;
    public float stickForce = 2;
    public float gravityMultiplier = 1;
    public float sprintSpeed;

    [Header("Animation Related")]
    [Range(0, 1)] public float steadyingBalance = 0.7f;
    public float animationSpeed = 1;
    public bool steady = true;

    [HideInInspector] public float stamina;
    float speed;

    float yaw;
    float pitch;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (IsOwner)
        {
            GetComponentInChildren<Camera>().enabled = true;
            camera = GetComponentInChildren<Camera>().transform;
        }
    }

    private void Update()
    {
        if (!IsOwner) return;
        animator.SetBool("steady", steady);
        animator.SetLayerWeight(0, 1 - steadyingBalance);
        animator.SetLayerWeight(1, steadyingBalance);
        animationSpeed = speed;
        animator.speed = animationSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 || vertical > 0) { animator.SetBool("running", true); }
        else { animator.SetBool("running", false); }

        if (horizontal != 0 || vertical != 0 || velocity.y != 0)
        {
            controller.Move((transform.forward * vertical * speed) + (transform.right * horizontal * speed) + velocity * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            speed = sprintSpeed;
            float staminaReduction = horizontal + vertical * Time.deltaTime;
            stamina -= staminaReduction;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = defaultSpeed;
            stamina += Time.deltaTime;
        }

        if (!controller.isGrounded)
        {
            velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y = -stickForce;
            if (Input.GetButton("Jump"))
            {
                velocity.y = jumpForce;
                animator.SetBool("jumping", true);
            } else { animator.SetBool("jumping", false); }
        }

        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
        pitch += Input.GetAxisRaw("Mouse Y") * -sensitivity;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, yaw, 0);
        camera.localRotation = Quaternion.Euler(Mathf.Clamp(pitch, -70, 45), 0, 0);
    }
}
