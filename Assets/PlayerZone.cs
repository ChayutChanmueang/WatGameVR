using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    [SerializeField] LuckyDraw DrawSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something is in Zone");
        if (other.tag=="Player")
        {
            DrawSystem.PlayerIsInZone = true;
            Debug.Log("Player is in Zone");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DrawSystem.PlayerIsInZone = false;
            Debug.Log("Player is not in Zone");
        }
    }
}
