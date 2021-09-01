using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullScript : MonoBehaviour, IHandInterface
{
    PlayerController player;
    GameObject marker;
    bool bReady = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (marker)
        {
           // transform.position = marker.transform.position + new Vector3(0, 1, 0);
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
    public void Pull()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        Debug.Log("Collides");
        GameObject ladder = other.gameObject;
        if (ladder)
        {
             if (ladder.CompareTag("Ladder"))
            {
                Debug.Log("HasTag");
                IActivable activable = ladder.GetComponent<IActivable>();
                if (activable != null)
                {
                    Debug.Log("Casted");
                    activable.Activate();
                }
            }

        }

    }
    public void Ready()
    {
        bReady = true;
    }
}
