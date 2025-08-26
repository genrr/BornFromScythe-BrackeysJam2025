using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputSystem_Actions inputClass;
    public GameObject mainMenu;

    GameObject player;


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

    public void OnESC()
    {
        Debug.Log("ESC");
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
    }

    void OnAttack()
    {
        //GetComponent<EntityData>().SetPlayerMove("Attack");
        GetComponent<MeleeSystem>().ExecuteMove(player.GetComponent<EntityData>().moves[0]);
    }

    void OnBlock()
    {
        //GetComponent<EntityData>().SetPlayerMove("Attack");
        GetComponent<MeleeSystem>().ExecuteMove(player.GetComponent<EntityData>().moves[3]);
    }

    void OnDash()
    {
        GetComponent<MeleeSystem>().ExecuteMove(player.GetComponent<EntityData>().moves[2]);
    }

    void OnDeflect()
    {
        GetComponent<MeleeSystem>().ExecuteMove(player.GetComponent<EntityData>().moves[1]);
    }

    void OnHarvest()
    {
        GetComponent<MeleeSystem>().ExecuteMove(player.GetComponent<EntityData>().moves[4]);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputClass.Player.ESC.triggered)
        {
            Debug.Log("esc");
        }
/*         foreach (Move m in player.GetComponent<EntityData>().moves)
                {
                    m.inputString
                } */
    }
}
