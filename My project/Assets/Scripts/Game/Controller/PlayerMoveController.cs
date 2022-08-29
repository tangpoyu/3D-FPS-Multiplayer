using Photon.Pun;
using UnityEngine;

// Controller
public class PlayerMoveController : MonoBehaviour
{
 
    [SerializeField]
    private GameObject cameraHolder, ui, decoratorUI;
    private Rigidbody rb;
    private PhotonView pv;
    [SerializeField]
    private float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    private float verticalLookRotation;
    private bool grounded, mouselocked;
    private Vector3 smoothMoveVelocity, moveAmount;



    public bool Grounded { get => grounded; set => grounded = value; }
    public bool Mouselocked { get => mouselocked; set => mouselocked = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        Mouselocked = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!pv.IsMine)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            Destroy(this);
            Destroy(cameraHolder);
            Destroy(ui);
            Destroy(decoratorUI);
            // Destroy(rb);
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (!pv.IsMine) return;
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Look();
        }
        Move();

    }

    private void FixedUpdate()
    {
        // if (!pv.IsMine) return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, move * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += -Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -70, 80);
        cameraHolder.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0, 0);
    }

    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Mouselocked = !Mouselocked;
        }

        Cursor.lockState = Mouselocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = Mouselocked ? false : true;
    }
}
