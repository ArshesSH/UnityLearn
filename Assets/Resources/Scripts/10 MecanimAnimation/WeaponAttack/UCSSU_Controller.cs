using UnityEngine;

public class UCSSU_Controller : MonoBehaviour
{
    public enum WeaponState
    {
        GreatSword,
        Bow,
        Riffle,
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

    // Components
    CharacterController controller;
    Animator animator;

    // Move Variables
    Vector3 direction;
    Vector3 forward;
    Vector3 camForward;
    Vector3 camRight;
    float horizontal;
    float vertical;

    // Character Status
    WeaponState weaponState = WeaponState.GreatSword;
    bool isArmed = false;

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
        controller.Move(runSpeed * Time.deltaTime * direction );
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxis( "Horizontal" );
        vertical = Input.GetAxis( "Vertical" );

        UpdateWeaponState();

        if (Input.GetKeyDown(KeyCode.R))
        {
            isArmed = !isArmed;
        }
    }

    void UpdateWeaponState()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    weaponState = WeaponState.NoWeapon;
        //}
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponState = WeaponState.GreatSword;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponState = WeaponState.Bow;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponState = WeaponState.Riffle;
        }
    }

    void UpdateAnimation()
    {
        if(animator != null)
        {
            if(controller != null)
            {
                animator.SetFloat("Speed", controller.velocity.magnitude);
            }
            animator.SetBool("IsArmed", isArmed);
            animator.SetInteger("WeaponState", (int)weaponState);
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 150, 30), weaponState.ToString());
    }

}


