using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour, IActivable
{
    public IActivable iactivable;
    public GameObject activable;
    bool bIsActivated = false;
    Renderer MyMaterial;
    public void Activate()
    {
        if (!bIsActivated && iactivable != null)
        {
            Debug.Log("Me activo");
            iactivable.Activate();
            bIsActivated = true;
            MyMaterial.material.SetColor("_Color", Color.red);
        }
    }

    public bool IsActivated()
    {
        return bIsActivated;
    }

    // Start is called before the first frame update
    void Start()
    {
        MyMaterial = GetComponent<Renderer>();
        iactivable = activable.GetComponent<IActivable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
