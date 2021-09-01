using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EMarkerState
{
    Void,
    PushLadder,
    Grap,

}
public class MarkerScript : MonoBehaviour
{
    EMarkerState state;
    PlayerController player;
    Vector3 targetedElementPos;
    // Start is called before the first frame update
    void Start()
    {
        state = EMarkerState.Void; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            state = EMarkerState.PushLadder;
            targetedElementPos = other.transform.position;
        }
        if (other.CompareTag("Grap"))
        {
            state = EMarkerState.Grap;
            targetedElementPos = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            state = EMarkerState.Void;
            
        }
        if (other.CompareTag("Grap"))
        {
            state = EMarkerState.Void;
        }
    }
    public void SetPlayer(PlayerController p)
    {
        player = p;
    }

    public void LaunchHand()
    {
        switch (state)
        {
            case EMarkerState.Void:
                player.Punch();
                break;
            case EMarkerState.PushLadder:
                player.PullLader(targetedElementPos);
                break;
            case EMarkerState.Grap:
                player.GrapToPoint(targetedElementPos);
                break;
            default:
                break;
        }
    }
}
