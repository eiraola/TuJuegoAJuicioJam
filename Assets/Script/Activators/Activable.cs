using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour, IActivable
{
    public void Activate()
    {
        transform.position = transform.position + new Vector3(0, 10, 0);
    }

    public bool IsActivated()
    {
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
