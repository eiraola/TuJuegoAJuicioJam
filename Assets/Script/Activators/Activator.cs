using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour, IActivable
{
    public Mesh mesh;
    public Mesh mesh2;
    public IActivable iactivable;
    public GameObject activable;
    bool bIsActivated = false;
    Renderer MyMaterial;
    public  bool bHasTImer = false;
    public float timerTime = 10.0f;
    public void Activate()
    {
        if (!bIsActivated && iactivable != null)
        {
            Debug.Log("Me activo");
            iactivable.Activate();
            bIsActivated = true;
            mesh = GetComponent<MeshFilter>().sharedMesh;
            mesh2 = Instantiate(mesh2);
            GetComponent<MeshFilter>().sharedMesh = mesh2;
            MyMaterial.material.SetColor("_Color", Color.red);
            if (bHasTImer)
            {
                StartCoroutine("DeactivationTimer");
            }
        }

    }

    public bool IsActivated()
    {
        return bIsActivated;
    }

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
       


        MyMaterial = GetComponent<Renderer>();
        iactivable = activable.GetComponent<IActivable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Deactivate()
    {
        StopCoroutine("DeactivationTimer");
        bIsActivated = false;
        GetComponent<MeshFilter>().sharedMesh = mesh;
        iactivable.Deactivate();

    }
    IEnumerator DeactivationTimer()
    {
        for (;;)
        {
            yield return new WaitForSeconds(timerTime);
            Deactivate();
        }
    }
}
