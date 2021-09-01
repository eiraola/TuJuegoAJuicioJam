using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerModel;
    Animator animationController;
    public float attackTime= 1.0f;
    public GameObject Punches;
    public GameObject Puller;
    public GameObject Grapper;
    public GameObject Marker;
    MarkerScript markerScript;
    public float speed = 15;
    public float Combatspeed = 5;
    [Range(0.1f, 0.9f)]
    public float acceleration = 0.2f;
    public int maxHP;
    private Rigidbody body;
    bool bIsPunching = false;
    bool bIsGrabbing = false;
    float punchingTime = 0.7f;
    bool recentlyDamaged = false;
    public Camera cam;
    Vector3 direction;
    bool bAimToTarget;
    public GameObject ConstraintItem;
    public GameObject handController;
    TwoBoneIKConstraint constraint;
    public Transform mainHandPos;
    public Transform initHandPos;
    public Transform endHandPos;
    PlayerDamageState damageState;
    Vector3 ResurrectionPosition;
    enum PlayerDamageState
    {
        JustDamaged,
        RecentlyDamaged,
        Free, 
        Invul

    }
    // Start is called before the first frame update
    void Start()
    {
        ResurrectionPosition = transform.position;
        constraint = ConstraintItem.GetComponent<TwoBoneIKConstraint>();
        damageState = PlayerDamageState.Free;
        animationController = PlayerModel.GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        markerScript = Marker.GetComponent<MarkerScript>();
        markerScript.SetPlayer(this);
        //StartCoroutine(Reposition());
    }

    // Update is called once per frame
    void Update()
    {
        setAnimation();
        //RotateToMouse();
       
    }
    public void setVelocity(Vector2 velocity)
    {
        
        if (bIsGrabbing) { body.velocity = new Vector3(0, 0, 0); setAnimation(); return; }


        float currentSpeed = bIsPunching ? Combatspeed : speed;
        direction = new Vector3(velocity.x, 0 , velocity.y);
        if (direction.magnitude > 1.0f) direction = direction.normalized;
        //body.velocity = direction * currentSpeed;//Vector3.Lerp(body.velocity, direction * currentSpeed, acceleration );
        if (direction.magnitude > 0.2 && !bIsPunching) {
            //direction = new Vector3(direction.x, 0, direction.z);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        setAnimation();
        direction = direction * currentSpeed;
    }
    public void UseHand()
    {
        animationController.SetBool("Hitting", true);
        markerScript.LaunchHand();
    }
    public void Punch()
    {
        if (bIsPunching || bIsGrabbing) return;
        bIsPunching = true;
        StartCoroutine("OraOraOra");
        constraint.weight = 1.0f;
        //RotateToMouse();
        GameObject hand = Instantiate(Punches, Marker.transform.position + new Vector3(0, 1, 0), Marker.transform.rotation);
        PunchScript punch = hand.GetComponent<PunchScript>();
        if (punch)
        {
            punch.InitVariables(this, attackTime, Marker);
        }
    }
    public void PullLader(Vector3 pos)
    {
        if (bIsPunching || bIsGrabbing) return;

        body.velocity = Vector3.zero;
        bIsGrabbing = true;
        direction = Vector3.zero;
        //RotateToMouse();
        GameObject hand = Instantiate(Puller, pos + new Vector3(0, 1, 0), Marker.transform.rotation);
        PullScript puller = hand.GetComponent<PullScript>();
        if (puller)
        {
            puller.InitVariables(this, attackTime, Marker);
        }
    }

    public void GrapToPoint(Vector3 pos)
    {
        if (bIsPunching || bIsGrabbing) return;
        bIsGrabbing = true;
        //body.velocity = new Vector3(0, 0, 0); setAnimation();
        direction = Vector3.zero;
       // RotateToMouse();
        GameObject hand = Instantiate(Grapper, pos + new Vector3(0, 1, 0), Marker.transform.rotation);
        GraplingScript grapper = hand.GetComponent<GraplingScript>();
        if (grapper)
        {
            grapper.InitVariables(this, attackTime, Marker);
        }

    }
    public void EndPunch()
    {
        StopCoroutine("OraOraOra");
        constraint.weight = 0.0f;
        handController.transform.position = mainHandPos.position;
        animationController.SetBool("Hitting", false);
        bIsPunching = false;
        bIsGrabbing = false;
    }
    public void Dash()
    {

    }
    public void setAnimation()
    {
        if (animationController)
        {
            float vel = body.velocity.magnitude / speed;
            animationController.SetFloat("Velocity", vel);
           

        }
    }
    private void FixedUpdate()
    {
        if (damageState == PlayerDamageState.Free) { 
             body.velocity = new Vector3(direction.x, body.velocity.y, direction.z);
        } else
        {
            if (body.velocity.magnitude < 0.1 && damageState != PlayerDamageState.JustDamaged)
            {
                damageState = PlayerDamageState.Free;
            }
        }
    }
    public void GetDamaged(Vector3 hitLocation)
    {   if (damageState == PlayerDamageState.JustDamaged || damageState == PlayerDamageState.RecentlyDamaged) return;
        damageState = PlayerDamageState.JustDamaged;
        body.AddForce((transform.position-hitLocation).normalized*2000);
        Debug.Log("OutCh!");
        StartCoroutine("Recover");
    }
    public void RecoverControl()
    {
       // StopCoroutine("Recover");
        recentlyDamaged = false;
    }
    public void StartingToRecover()
    {
        damageState = PlayerDamageState.RecentlyDamaged;
        StopCoroutine("Recover");

    }
    IEnumerator Recover()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(0.5f);
            StartingToRecover();
        }
    }
    IEnumerator OraOraOra()
    {
        bool init = false;
        for (; ; )
        {
            
            if (init) { handController.transform.position = initHandPos.position; }
            else{ handController.transform.position = endHandPos.position; Debug.Log("patata"); }
            //Vector3.Lerp(initHandPos.position,endHandPos.position, Mathf.Sin(Time.time));
            init = !init;
            yield return new WaitForSeconds(0.1f);      
        }
    }

    public void RotateToMouse()
    {
        if (!bAimToTarget) return;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycast;
        if (Physics.Raycast(ray, out raycast))
        {
            Vector3 k = raycast.point - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(k.x, 0, k.z)), 1.0f);
            //transform.rotation = Quaternion.LookRotation(new Vector3(k.x, 0, k.z));
           
        }
    }
    public void SetResurrectionPosition(Vector3 pos)
    {
        ResurrectionPosition = pos;
    }

    public void Die()
    {
        transform.position = ResurrectionPosition;
    }




}
