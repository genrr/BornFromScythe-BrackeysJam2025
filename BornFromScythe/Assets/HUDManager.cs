using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    GameObject player;
    EntityData playerStats;
    public GameObject gameMsg;
    public GameObject infoMsg;
    public GameObject sigilBox;
    public GameObject scBox;
    public Slider hpBar;
    float hpTargetValue;
    public TextMeshProUGUI hpValue;
    public TextMeshProUGUI scValue;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<EntityData>();
        player.GetComponent<EntityData>().onHUDChangedCallback += UpdateHUD;
    }

    // Update is called once per frame
    void Update()
    {
        if (!States.MenuOpen)
        {
            //unpause
            Time.timeScale = 1;


            if (!Mathf.Approximately(hpBar.value, hpTargetValue))
            {
                float hp = playerStats.HP;
                float maxHP = playerStats.maxHP;
                hpBar.value += 2.0f * Time.deltaTime * (hpTargetValue - hpBar.value);
                hpValue.SetText((int)(hp * hpBar.value) + " / " + maxHP);
            }
        }
    }


    void UpdateHUD()
    {
        float hp = playerStats.HP;
        float maxHP = playerStats.maxHP;
        hpTargetValue = hp / maxHP;

        //sc update
    }

}
