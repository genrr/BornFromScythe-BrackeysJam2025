using UnityEngine;

public class GameArea : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit()
    {
        States.gameRunning = false;
    }
}
