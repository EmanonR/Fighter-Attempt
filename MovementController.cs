using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class MovementController : NetworkBehaviour
{
  //Gonna add online later, still learning that, lots to learn here

    //These are classes to make them collapsable in Inspector
    public MovementClass Movement;
    //Removed, Since ground is always at 0, can just check players position for "Grounded check", collision is as usual;
    //public GroundedClass Grounded; 

    public Character selectedCharacter;   //Not implemented yet, should have info for values such as speed, damage, etc.

    public state StateMachine;
        
    //Changed to automatically getting camera.Main
    new private Transform cam;

    //Physics
    private bool isGrounded;
    private bool running;
    private RaycastHit groundedHit;
    private Rigidbody rb;
    //Movement
    private float currentSpeed;
    private float horizontalInput, verticalInput;
    private Vector2 inputDir
    private float targetAngle;
    private float turnSmoothVelocity;


    private enum state {
      standing,
      crouching,
      airborne,
      running,
    }

    void Start()
    {
      if (!isOnwer) return;
        rb = GetComponent<Rigidbody>();
        cam = camera.Main;
    }

    void Update()
    {
      if (!isOnwer) return;

        switch (StateMachine) {
            case StateMachine.standing:
                if (Input.inputDir.y > .1f)
                  {
                      HandleJump();
                  }
                break;

            case StateMachine.crouching:
                break;

            case StateMachine.airBorne:
                break;

            case StateMachine.running:
                break;
        }

          
        HandleSettings();
        UpdatePlayerRotation();
        UpdateGroundedStatus();
        HandleJump();
    }

    private void FixedUpdate()
    {      
      if (!isOnwer) return;

        if (movementDir.x != 0) HandleMovement();
    }

    void HandleSettings()
    {
        //Directional input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementDir = new Vector2(horizontalInput, verticalInput).normalized;

        //Set current speed
        currentSpeed = running ? Movement.runSpeed : Movement.walkSpeed;
    }

    void UpdateGroundedStatus()
    {
        if (rb.transform.position <= 0)
        {
          //Grounded
          isGrounded = true;
          rb.transform.position = new Vector3(rb.transform.position.x,0);
        }
    }

    void HandleJump()
    {
        
    }

    void HandleMovement()
    {
        if (movementDir.x != 0 && isGrounded)
        {
            
            rb.velocity = new Vector3(moveDir.x * currentSpeed, rb.velocity.y);
        }
        else if (movementDir.x != 0 && !isGrounded)
        {
            //In air
        }
    }

    void UpdatePlayerRotation()
    {
        //Flip player model
    }


    private void OnDrawGizmosSelected()
    {
        DrawGroundRayGizmos();
    }

    void DrawGroundRayGizmos()
    {
        //Position of start poses of rays, using both transform.position and height
        //Create a similar one at tip of ray
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, Grounded.groundRayHeight), Grounded.groundRayWidth);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, Grounded.groundRayHeight - Grounded.groundRayLength), Grounded.groundRayWidth);

        //same as before but constant size, usefull if somehow width is 0
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, Grounded.groundRayHeight), .02f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, Grounded.groundRayHeight - Grounded.groundRayLength), .02f);

        //Line representing ray
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0, Grounded.groundRayHeight), transform.position + new Vector3(0, Grounded.groundRayHeight - Grounded.groundRayLength));
    }


    [System.Serializable]
    public class MovementClass
    {
        public float walkSpeed = 5, runSpeed = 10;
        public float jumpPower = 7;
        
        //Turn speed (I think it was? gonna update that)
        public float turnSmoothTime = .1f;
    }
}
