using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingScript : MonoBehaviour, IHandInterface
{
    PlayerController player;
    GameObject marker;
    GameObject Target;
    bool bReady = false;
    public float grapSpeed = 200.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        if (marker)
        {
           // transform.position = marker.transform.position + new Vector3(0, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Target && bReady)
        {
            Debug.Log(Target.transform.position);
            GrapToPoint(Target.transform.position);
        }
       
    }
    public void InitVariables(PlayerController controller, float destroyTime, GameObject mark)
    {
        Debug.Log("patata");
        player = controller;
        //Destroy(gameObject, destroyTime);
        marker = mark;
    }
    private void OnDestroy()
    {
        player.EndPunch();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        GameObject ladder = other.gameObject;
        if (ladder)
        {
            if (ladder.CompareTag("Grap"))
            {
                Target = ladder;
                
                IActivable activable = ladder.GetComponent<IActivable>();
                if (activable != null)
                {
                   
                    activable.Activate();
                }
            }

        }

    }
    public void GrapToPoint(Vector3 position)
    {
        Debug.Log("Llegando");
        Vector3 targetDirection = (position - player.transform.position).normalized;
        if (((position - player.transform.position).magnitude) < 1.0f) { Destroy(gameObject); }
        player.transform.Translate(targetDirection * grapSpeed * Time.deltaTime, Space.World);
    }
    public void Ready()
    {
        bReady = true;
    }
}
