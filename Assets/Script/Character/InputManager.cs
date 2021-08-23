using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovement();
        GetPunchAction();

    }
    void GetMovement()
    {
        float x = 0, y = 0;
        
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.05)
        {
            x = Input.GetAxis("Horizontal");
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.05)
        {
            y = Input.GetAxis("Vertical");
        }
        player.setVelocity(new Vector2(x,y));
    }
    void GetPunchAction()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            player.UseHand();
        }
    }
}

