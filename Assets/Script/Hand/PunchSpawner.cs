using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSpawner : MonoBehaviour
{
    public GameObject punchPrefab;
    public Transform maxX, minX, maxZ, minZ;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnHand");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetRandomPointInRange()
    {
        //Vector3 target = new Vector3(Random.Range(minX.position.x, maxX.position.x),this.transform.position.y + Random.Range(-0.25f, 0.25f), Random.Range(minZ.position.z, maxZ.position.x));
         Vector3 target =  new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.25f, 0.25f), Random.Range(-0.45f, 0.45f));
        if (punchPrefab)
        {
            GameObject x = Instantiate(punchPrefab,gameObject.transform.position,gameObject.transform.rotation);
            x.transform.SetParent(gameObject.transform);
            x.transform.localPosition = target;
            Destroy(x,0.25f);
        }
    }
    IEnumerator SpawnHand()
    {
        for (; ; )
        {
            GetRandomPointInRange();
            yield return new WaitForSeconds(.01f);
        }
    }
}
