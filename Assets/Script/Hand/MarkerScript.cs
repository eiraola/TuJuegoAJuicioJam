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
        }
        if (other.CompareTag("Grap"))
        {
            state = EMarkerState.Grap;
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
                player.punch();
                break;
            case EMarkerState.PushLadder:
                break;
            case EMarkerState.Grap:
                break;
            default:
                break;
        }
    }
}
