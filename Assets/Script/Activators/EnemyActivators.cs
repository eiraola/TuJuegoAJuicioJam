using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivators : MonoBehaviour, IActivable
{
    public int enemyQuantity = 2;
    IActivable activable;
    public GameObject ObjetoActivable;
    
    public void Activate()
    {
        enemyQuantity -= 1;
        if(enemyQuantity == 0)
        {
            if (activable!=null)
            {
                activable.Activate();
            }
        }
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public bool IsActivated()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        activable = ObjetoActivable.GetComponent<IActivable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
