using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkelletonScript : MonoBehaviour
{
    public int maxHP;
    int hp;
    Rigidbody body;
    NavMeshAgent agent;
    public float impactForce = 4.0f ;
    GameObject PlayerOne;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        hp = maxHP;
        agent = GetComponent<NavMeshAgent>();
        PlayerOne = GameObject.FindGameObjectsWithTag("Player")[0];
       
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = PlayerOne.transform.position;
    }
    public void GetHit(int damage, Vector3 direction)
    {
        hp = hp - damage;
        
        Vector3 target = (transform.position - direction).normalized * impactForce;
        target.y = 0;
        //Debug.Log(impactForce);
        body.AddForce(target , ForceMode.Impulse);
        if (hp >= 0)
        {
            Die();
        }

    }
    public void Die()
    {
        Destroy(gameObject, 0.5f);
    }
}
