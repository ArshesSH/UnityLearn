using UnityEngine;

public class UCSSU_Controller : MonoBehaviour
{
    public enum WeaponState
    {
        GreatSword,
        Bow,
        Rifle,
    }

    [Header("Character Object Setting")]
    [SerializeField]
    GameObject controllerObj;
    [SerializeField]
    GameObject playerModel;
    [SerializeField]
    GameObject camObj;
    [SerializeField]
    Transform mainSpine;
    [SerializeField]
    Transform headPosition;
    Quaternion mainSpineInitRot;
    [SerializeField]
    Transform bowAim;

    [Header("Weapon Object Setting")]
    [SerializeField]
    GameObject[] weapons;
    GameObject curWeapon;
    [SerializeField]
    GameObject arrowObj;
    [SerializeField]
    GameObject bulletObj;
    [SerializeField]
    Transform arrowSpawnPos;
    [SerializeField]
    Transform bulletSpawnPos;

    [Header("Status Setting")]
    [SerializeField]
    float rotateSpeed = 600.0f;
    [SerializeField]
    float runSpeed = 6.0f;
    public bool turnCharcterByCam = true;
    [SerializeField]
    float maxAimRotateAngle = 50.0f;

    // Components
    CharacterController controller;
    Animator animator;

    // Move Variables
    Vector3 direction;
    Vector3 forward;
    Vector3 camForward;
    Vector3 camProjToPlane;
    Vector3 camRight;
    float horizontal;
    float vertical;

    // Character Status
    WeaponState weaponState = WeaponState.GreatSword;
    bool isArmed = false;
    bool isAiming = false;
    bool isFire = false;
    bool isCrouching = false;


    void Start()
    {
        controller = controllerObj.GetComponent<CharacterController>();
        animator = playerModel.GetComponent<Animator>();
        mainSpineInitRot = mainSpine.localRotation;
        curWeapon = weapons[0];
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

    private void LateUpdate()
    {
        AimToCamera();
    }

    void SetDirection()
    {
        camForward = camObj.transform.forward;
        camProjToPlane = Vector3.ProjectOnPlane(camForward, Vector3.up);
        camRight = camObj.transform.right;
        direction = (horizontal * camRight + vertical * camProjToPlane).normalized;
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
        controller.Move(runSpeed * Time.deltaTime * direction);
    }

    void PlayerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        UpdateWeaponState();

        if (Input.GetKeyDown(KeyCode.R))
        {
            isArmed = !isArmed;
            animator.SetBool("IsArmed", isArmed);
        }
        if(Input.GetButtonDown("Crouch"))
        {
            isCrouching = !isCrouching;
            animator.SetBool("IsCrouching", isCrouching);
        }
    }

    void AimToCamera()
    {
        switch (weaponState)
        {
            case WeaponState.GreatSword:
            {
            }
            break;
            case WeaponState.Bow:
            {
                if (isArmed)
                {
                    mainSpine.LookAt(mainSpine.position + camRight);
                    float curAngle = mainSpine.localRotation.eulerAngles.y;
                    if (curAngle >= maxAimRotateAngle && curAngle <= 180.0f)
                    {
                        playerModel.transform.LookAt(playerModel.transform.position + camProjToPlane);
                    }
                    else if (curAngle <= 360.0f - maxAimRotateAngle && curAngle >= 180.0f)  
                    {
                        playerModel.transform.LookAt(playerModel.transform.position + camProjToPlane);
                    }
                }
                else
                {
                    if (mainSpine.localRotation != mainSpineInitRot)
                    {
                        mainSpine.localRotation = Quaternion.RotateTowards(mainSpine.localRotation, mainSpineInitRot, 10.0f);
                    }
                }
            }
            break;
            case WeaponState.Rifle:
            {
                if (isArmed)
                {
                    mainSpine.LookAt(mainSpine.position + camRight);
                    float curAngle = mainSpine.localRotation.eulerAngles.y;
                    if (curAngle >= maxAimRotateAngle && curAngle <= 180.0f)
                    {
                        playerModel.transform.LookAt(playerModel.transform.position + camProjToPlane);
                    }
                    else if (curAngle <= 360.0f - maxAimRotateAngle && curAngle >= 180.0f)
                    {
                        playerModel.transform.LookAt(playerModel.transform.position + camProjToPlane);
                    }
                }
            }
            break;
        }
    }

    void UpdateAttackAnimation()
    {
        switch (weaponState)
        {
            case WeaponState.GreatSword:
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    animator.SetTrigger("Attack");
                }
            }
            break;
            case WeaponState.Bow:
            {
                isAiming = Input.GetButton("Fire1");
                animator.SetBool("IsDraw", isAiming);
            }
            break;
            case WeaponState.Rifle:
            {
                isFire = Input.GetButton("Fire1");
                animator.SetBool("IsFire", isFire);
            }
            break;
        }
    }

    void UpdateWeaponState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponState = WeaponState.GreatSword;
            animator.SetInteger("WeaponState", (int)weaponState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponState = WeaponState.Bow;
            animator.SetInteger("WeaponState", (int)weaponState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponState = WeaponState.Rifle;
            animator.SetInteger("WeaponState", (int)weaponState);
        }
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            if (controller != null)
            {
                animator.SetFloat("Speed", controller.velocity.magnitude);
            }
        }

        UpdateAttackAnimation();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isAiming)
        {
            animator.SetLookAtWeight(1.0f);
            animator.SetLookAtPosition(bowAim.position);
        }
    }

    void onFireRifle()
    {
        GameObject obj = null;
        if (bulletObj != null && bulletSpawnPos != null)
        {
            obj = Instantiate(bulletObj, bulletSpawnPos.position, bulletSpawnPos.rotation);
        }
    }

    void onFireArrow()
    {
        GameObject obj = null;
        if(arrowObj != null)
        {
            obj = Instantiate(arrowObj, arrowSpawnPos.position, arrowSpawnPos.rotation);
        }
    }

    void onWeaponCheck()
    {
        if(curWeapon.activeInHierarchy)
        {
            curWeapon.SetActive(false);
            curWeapon = weapons[(int)weaponState];
        }
        else
        {
            curWeapon = weapons[(int)weaponState];
            curWeapon.SetActive(true);
        }
    }


    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 150, 30), weaponState.ToString());
        GUI.Box(new Rect(0, 30, 150, 30), mainSpine.localRotation.eulerAngles.y.ToString());
        GUI.Box(new Rect(0, 60, 150, 30), "IsAiming=" + isAiming.ToString());
    }

}


