using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuenteActivable : MonoBehaviour, IActivable
{
    public Vector3 targetPos;
    Vector3 direction;
    Vector3 initPos;
    public float speed = 2;
    bool bActivated = false;
    float movePer = 0.0f;
    public void Activate()
    {
        bActivated = true;
        //transform.position = transform.position + new Vector3(0, 10, 0);
    }

    public bool IsActivated()
    {
        return bActivated;
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = (targetPos - transform.position).normalized;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bActivated)
        {
            TranslateToPosition();
        }
    }
    
    public void TranslateToPosition()
    {
        float x =(transform.position - targetPos).magnitude;
        Debug.Log(Vector3.Lerp(initPos, targetPos, speed * Time.deltaTime));
;        if(targetPos != null)
        {
            movePer += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(initPos, targetPos, movePer );
            //transform.Translate(direction*);
        }
      
    }
    public void Deactivate() {
        bActivated = false;
        transform.position = initPos;
    }
}
