using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float attackTime= 1.0f;
    public GameObject Punches;
    public GameObject Marker;
    MarkerScript markerScript;
    public float speed = 15;
    public float Combatspeed = 5;
    [Range(0.1f, 0.9f)]
    public float acceleration = 0.2f;
    public int maxHP;
    private Rigidbody body;
    bool bIsPunching = false;
    float punchingTime = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        markerScript = Marker.GetComponent<MarkerScript>();
        markerScript.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setVelocity(Vector2 velocity)
    {
        float currentSpeed = bIsPunching ? Combatspeed : speed;
        Vector3 direction = new Vector3(velocity.x, 0, velocity.y);
        if (direction.magnitude > 1.0f) direction = direction.normalized;
        body.velocity = Vector3.Lerp(body.velocity, direction * currentSpeed, acceleration );
        if (direction.magnitude > 0.2 && !bIsPunching) {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    public void UseHand()
    {
        markerScript.LaunchHand();
    }
    public void punch()
    {
        if (bIsPunching) return;
        bIsPunching = true;
        GameObject hand = Instantiate(Punches, Marker.transform.position + new Vector3(0, 1, 0), Marker.transform.rotation);
        PunchScript punch = hand.GetComponent<PunchScript>();
        if (punch)
        {
            punch.InitVariables(this, attackTime, Marker);
        }
    }
    public void EndPunch()
    {
        bIsPunching = false;
    }
    public void Dash()
    {

    }


   
}
