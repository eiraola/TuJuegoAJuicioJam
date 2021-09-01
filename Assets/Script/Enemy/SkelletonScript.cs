using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkelletonScript : MonoBehaviour
{
    public int maxHP = 4;
    int hp = 4;
    Rigidbody body;
    NavMeshAgent agent;
    public float impactForce = 4.0f ;
    GameObject PlayerOne;
    public GameObject EnemyModel;
    Animator animationController;
    public GameObject enemyBody;
    bool bIsDead = false;
    bool isActive = false;
    public GameObject ActivatorElement;
    IActivable activatorScript;
    



    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        hp = maxHP;
        agent = GetComponent<NavMeshAgent>();
        PlayerOne = GameObject.FindGameObjectsWithTag("Player")[0];
        animationController = EnemyModel.GetComponent<Animator>();
        if (ActivatorElement)
        {
            activatorScript = ActivatorElement.GetComponent<IActivable>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerOne.transform.position) < 20.0f) { isActive = true; }
        if(agent.enabled && isActive)
            agent.destination = PlayerOne.transform.position;
        
        animationController.SetFloat("Velocity", agent.velocity.magnitude/3.5f);
    }
    public void GetHit(int damage, Vector3 direction)
    {
        Debug.Log(hp);
        if (hp <= 0) { return; }
            hp = hp - damage;
        
        Vector3 target = (transform.position - direction).normalized * impactForce;
        target.y = 0;
        //Debug.Log(impactForce);
        body.AddForce(target , ForceMode.Impulse);
        if (hp <= 0)
        {
            Die();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (bIsDead) return;
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.GetDamaged(transform.position);
        }
    }
    public void Die()
    {
        agent.enabled = false;
        Animator anim = enemyBody.GetComponent<Animator>();
        anim.applyRootMotion=true;
        anim.SetBool("Die", true);
        if(activatorScript!= null)
        {
            activatorScript.Activate();
        }
    }
}
