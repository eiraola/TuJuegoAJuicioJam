using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Kill()
    {
        Destroy(transform.parent.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
