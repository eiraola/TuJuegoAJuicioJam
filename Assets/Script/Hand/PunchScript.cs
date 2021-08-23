using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    PlayerController player;
    GameObject marker;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (marker)
        {
           
            transform.position = marker.transform.position + new Vector3(0, 1, 0);
        }
    }
    public void InitVariables(PlayerController controller, float destroyTime, GameObject mark)
    {
        player = controller;
        Destroy(gameObject, destroyTime);
        marker = mark;
    }
    private void OnDestroy()
    {
        player.EndPunch();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        Debug.Log("Collides");
        GameObject enemy = other.gameObject;
        if (enemy){
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("HasTag");
                SkelletonScript Enemy = enemy.GetComponent<SkelletonScript>();
                if (Enemy)
                {
                    Enemy.GetHit(2, this.transform.position);
                }
            }
            else if (enemy.CompareTag("Activable"))
            {
                Debug.Log("HasTag");
                IActivable activable = enemy.GetComponent<IActivable>();
                if (activable != null)
                {
                    Debug.Log("Casted");
                    activable.Activate();
                }
            }
           
        }
       
    }
}
