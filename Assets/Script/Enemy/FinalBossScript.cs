using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyScript : MonoBehaviour
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
        if (ActivatorElement)
        {
            activatorScript = ActivatorElement.GetComponent<IActivable>();
        }
        body = GetComponent<Rigidbody>();
        hp = maxHP;
      
        PlayerOne = GameObject.FindGameObjectsWithTag("Player")[0];
      


    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerOne.transform.position) < 20.0f) { isActive = true; } else { isActive = false; }
        if(isActive)
        {
            body.velocity = transform.forward * 600 * Time.deltaTime;
            Vector3 k = PlayerOne.transform.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(k.x, 0, k.z)), 80.0f*Time.deltaTime);
        }
        
        
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
        if (activatorScript != null)
        {
            activatorScript.Activate();
        }
        Destroy(gameObject, 1.0f) ;
    }
}
