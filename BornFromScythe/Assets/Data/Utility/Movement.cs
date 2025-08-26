using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    InputSystem_Actions inputClass;
    GameObject playerModel;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float runMultiplier = 2f;
    [SerializeField] float jumpHeight = 1.3f;
    [SerializeField] float gravity = 7f;
    [Range(0,10), SerializeField] float airControl = 5f;

    Vector3 moveDirection = Vector3.zero;

    CharacterController controller;


    Quaternion bodyStartOrientation;

    //float yaw = 0f;
    //float prev = 0f;
    Animator animator;
    Camera playerCamera;

    protected void Awake()
    {
        inputClass = new();
    }

    protected void OnEnable()
    {
        inputClass.Player.Enable();
    }

    protected void OnDisable()
    {
        inputClass.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();    
        DontDestroyOnLoad(gameObject);
        //playerModel = GameObject.Find("SynTheStrainReaper009");
        //animator = playerModel.GetComponentInChildren<Animator>();
        playerCamera = GameObject.Find("MainCamera").GetComponentInChildren<Camera>();
        bodyStartOrientation = transform.localRotation;
    }

    void FixedUpdate()
    {


        if(States.MenuOpen)
        {
            return;
        }

        /*
        Movement

        */


        

        //Debug.Log("getaxis vertical * max(abs(input.x),abs(input.z) "+Mathf.Abs(Input.GetAxis("MoveVertical") * Mathf.Max(Mathf.Abs(input.x),Mathf.Abs(input.z))));
        //Debug.Log("getaxis vertical * max(abs(movedir.x),abs(movedir.z)) "+Mathf.Abs(Input.GetAxis("MoveVertical")*Mathf.Abs( Mathf.Max(Mathf.Abs(moveDirection.x),Mathf.Abs(moveDirection.z)))));
        //Debug.Log("max(abs(movedir.x),abs(movedir.z)) "+Mathf.Max(Mathf.Abs(moveDirection.x),Mathf.Abs(moveDirection.z)));

        //TODO: do not count sideways movement as Speed increase

        
        //update speed for Animator
        //animator.SetFloat("Speed",Mathf.Max(Mathf.Abs(moveDirection.x),Mathf.Abs(moveDirection.z)));

        

        


        Vector2 dir = new Vector2(inputClass.Player.Move.ReadValue<Vector2>().x,inputClass.Player.Move.ReadValue<Vector2>().y);

        

        if(Mathf.Abs(dir.x) > 0.01 || Mathf.Abs(dir.y) > 0.01)
        {
            dir = dir.normalized;

            //Debug.Log(dir);
            
            var inputAngle = axisIntoAngle(dir) + playerCamera.transform.eulerAngles.y;

            //Debug.Log(playerCamera.transform.eulerAngles.y+" "+inputAngle);

            Vector2 currentDir = new Vector2(transform.forward.x, transform.forward.z);

            currentDir = currentDir.normalized;

            var currentAngle = axisIntoAngle(currentDir);

            /*
            angle = 0, y = 1
            angle = 90, y = 0
            angle = 180, y = -1

            f(rotY) = r

            */

            float camRotY = playerCamera.transform.eulerAngles.y;
            float tX = Mathf.Cos(((camRotY - 90) * Mathf.PI) / 180.0f);
            float tY = Mathf.Sin(((camRotY + 90) * Mathf.PI) / 180.0f) - 1.0f;
        

            if(inputAngle != currentAngle)
            {
                //Debug.Log("input "+inputAngle+" current "+currentAngle);
                transform.localRotation = Quaternion.AngleAxis(inputAngle,Vector3.up);
            }
        }
        



        var input = transform.forward * (Mathf.Abs(inputClass.Player.Move.ReadValue<Vector2>().x) + Mathf.Abs(inputClass.Player.Move.ReadValue<Vector2>().y));

        input = input.normalized;

        input *= moveSpeed;

        //input = transform.TransformDirection(input);

        if(controller.isGrounded)
        {
            //Debug.Log("grounded");

            moveDirection = input;
            

 /*            //Jumping
            if(inputClass.Player.B2.triggered)
            {
                moveDirection.y = Mathf.Sqrt(2 * gravity * Mathf.Sqrt(jumpHeight));
            }
            else
            {
                moveDirection.y = 0f;
            }

            //Running
            if(inputClass.Player.B4.IsPressed())
            {
                moveDirection *= runMultiplier;
            } */
            
        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, new Vector3(moveDirection.x + inputClass.Player.Move.ReadValue<Vector2>().x,moveDirection.y,moveDirection.z + inputClass.Player.Move.ReadValue<Vector2>().y), airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;


        //yaw += dirAngle;

        //transform.Rotate(new Vector3(0,Time.deltaTime,0), dirAngle, Space.Self);

        //var horizontal = Input.GetAxis("MoveHorizontal") * Time.deltaTime * 100;

        //yaw += horizontal;

        //var bodyRotation = Quaternion.AngleAxis(yaw, Vector3.up);

        //transform.localRotation = bodyRotation * bodyStartOrientation;

        /* 

        if(Input.GetAxis("MoveVertical") < 0.001f)
        {
            yaw -= horizontal;
        }

        else
        {
            yaw += horizontal;
        }*/
        


        //head.transform.RotateAround(playerModel.transform.position, Vector3.left, 1);

        //Debug.Log(moveDirection);

        float air = Mathf.Abs(moveDirection.y) > 2.0f ? 1.0f : 0f;


        //animator.SetFloat("Speed",Mathf.Sqrt(Mathf.Abs(moveDirection.x)*Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z)*Mathf.Abs(moveDirection.z)));


        /* ((Mathf.Abs(Input.GetAxis("MoveHorizontal")) < 0.001 ? 0.0f : Mathf.Abs(Input.GetAxis("MoveHorizontal"))) + 
        (Mathf.Abs(Input.GetAxis("MoveVertical")) < 0.001 ? 0.0f : Mathf.Abs(Input.GetAxis("MoveVertical")))));
 */
        controller.Move(moveDirection * Time.deltaTime);


        //prev = horizontal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float axisIntoAngle(Vector2 v)
    {


        v.x = Mathf.Approximately(v.x, 0f) ? 0f : v.x;
        v.y = Mathf.Approximately(v.y, 0f) ? 0f : v.y;
        v.x = Mathf.Approximately(v.x, 1f) ? 1f : v.x;
        v.y = Mathf.Approximately(v.y, 1f) ? 1f : v.y;
        v.x = Mathf.Approximately(v.x, -1f) ? -1f : v.x;
        v.y = Mathf.Approximately(v.y, -1f) ? -1f : v.y;



        if(v.x == 1 && v.y == 0)
        {
            return 90f;
        }
        else if(v.x == -1 && v.y == 0)
        {
            return 270f;
        }
        else if(v.x == 0 && v.y == 1)
        {
            return 0f;
        }
        else if(v.x == 0 && v.y == -1)
        {
            return 180f;
        }
        else if(v.x > 0 && v.y > 0)
        {
            return - (Mathf.Acos(v.x) * 360/(2*Mathf.PI) - 90f);
        }
        else if(v.x < 0 && v.y > 0)
        {
            return - (Mathf.Acos(v.x) * 360/(2*Mathf.PI) - 90f);
        }
        else if(v.x < 0 && v.y < 0)
        {
            return - (- Mathf.Acos(v.x) * 360/(2*Mathf.PI) - 90f);
        }
        else
        {
            return - (- Mathf.Acos(v.x) * 360/(2*Mathf.PI) - 90f);
        }

        

    }

}
