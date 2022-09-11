using UnityEngine;

public class UCSSU_Controller : MonoBehaviour
{
    public enum UC_State
    {
        Wait,

    }

    [Header("Object Setting")]
    [SerializeField]
    GameObject playerModel;
    [SerializeField]
    GameObject camObj;


    [Header( "Status Setting" )]
    [SerializeField]
    float rotateSpeed = 600.0f;
    [SerializeField]
    float runSpeed = 6.0f;
    public bool turnCharcterByCam = true;

    CharacterController controller;
    Animator animator;

    Vector3 direction;
    Vector3 forward;
    Vector3 camForward;
    Vector3 camRight;
    float curSpeed;
     
    float horizontal;
    float vertical;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = playerModel.GetComponent<Animator>();
    }

    void Update()
    {
        PlayerInput();
        SetDirection();
        if (turnCharcterByCam)
        {
            RotateToDirection();
        }

        MoveCharacter();
        UpdateAnimation();
    }

    void SetDirection()
    {
        camForward = Vector3.ProjectOnPlane(camObj.transform.forward, Vector3.up);
        camRight = camObj.transform.right;
        direction = (horizontal * camRight + vertical * camForward).normalized;
    }

    void RotateToDirection()
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            forward = Vector3.Slerp(playerModel.transform.forward, direction,
                rotateSpeed * Time.deltaTime / Vector3.Angle(playerModel.transform.forward, direction));
            playerModel.transform.LookAt(playerModel.transform.position + forward);
        }
    }

    void MoveCharacter()
    {
        //curSpeed = Mathf.Lerp()
        controller.Move(runSpeed * Time.deltaTime * direction );
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxis( "Horizontal" );
        vertical = Input.GetAxis( "Vertical" );
    }

    void UpdateAnimation()
    {
        if(animator != null)
        {
            if(controller != null)
            {
                animator.SetFloat("Speed", controller.velocity.magnitude);
            }
        }
    }

    private void OnGUI()
    {
        //GUI.Box(new Rect(0, 0, 150, 30), runSpeed.ToString());
    }

}


