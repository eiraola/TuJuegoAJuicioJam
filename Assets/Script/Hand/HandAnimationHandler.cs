using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationHandler : MonoBehaviour
{
    IHandInterface parent;
    void Start()
    {
        parent = gameObject.GetComponentInParent<IHandInterface>();
    }

    // Update is called once per frame
   public void Ready()
    {
        parent.Ready();
    }
}
