using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    InputSystem_Actions inputClass;
    [SerializeField] float turnSpeed = 90f;
    [SerializeField] float headUpperAngleLimit = 85f;
    [SerializeField] float headLowerAngleLimit = -80f;

    float yaw = 0f;

    
    float pitch = 0f;

    Quaternion bodyStartOrientation;
    Quaternion headStartOrientation;

    Transform head;
    Transform player;

    float offSetX;
    float offSetZ;
    float rotY;

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
        head = this.transform;
        player = GameObject.Find("Player").transform;
        bodyStartOrientation = transform.localRotation;
        headStartOrientation = head.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {

        if(States.MenuOpen)
        {
            return;
        }



        var horizontal = inputClass.Player.Look.ReadValue<Vector2>().x * Time.deltaTime * turnSpeed;
        var vertical = inputClass.Player.Look.ReadValue<Vector2>().y * Time.deltaTime * turnSpeed;

        yaw += horizontal;
        pitch += vertical;
        pitch = Mathf.Clamp(pitch, headLowerAngleLimit, headUpperAngleLimit);

        var bodyRotation = Quaternion.AngleAxis(-yaw, Vector3.up);
        var headRotation = Quaternion.AngleAxis(pitch, Vector3.right);
        //transform.localRotation = bodyRotation * bodyStartOrientation;
        head.localRotation = bodyRotation * headRotation * headStartOrientation;
        //head.RotateAround(player.position, Vector3.up, SignWithZero(Input.GetAxis("LookAroundX")));
        //Vector3 distVector = head.position - player.position;

        rotY = -bodyRotation.eulerAngles.y;

        offSetX = 2.5f * Mathf.Cos(((rotY - 90) * Mathf.PI) / 180.0f);
        offSetZ = 2.5f * Mathf.Sin(((rotY - 90) * Mathf.PI) / 180.0f);

        head.position = new Vector3(Mathf.MoveTowards(head.position.x, player.position.x + offSetX, 2), player.position.y + 1.0f, Mathf.MoveTowards(head.position.z, player.position.z + offSetZ, 2));
        //Mathf.MoveTowards(head.position.y, player.position.y, 2);

        /* if(Mathf.Abs((player.position - head.position).x) > 1)
        {
            head.Translate(Time.deltaTime * new Vector3(3*SignWithZero(head.position.x - player.position.x), 0, 0));
        }
        else if(Mathf.Abs((player.position - head.position).y) > 2)
        {
            //head.Translate(Time.deltaTime * new Vector3(0, SignWithZero(head.position.y - player.position.y), 0));
        }
        else if(Mathf.Abs((player.position - head.position).z) > 1)
        {
            head.Translate(Time.deltaTime * new Vector3(0,0, 3*SignWithZero(head.position.z - player.position.z)));
        } */

    }

    float SignWithZero(float f)
    {
        if(Mathf.Approximately(0,f))
        {
            return 0.0f;
        }
        else if(f < 0)
        {
            return -1.0f;
        }
        else
        {
            return 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
