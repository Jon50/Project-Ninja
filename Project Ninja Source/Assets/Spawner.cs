using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        Instantiate(obj, transform.position - Vector3.left * 3, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Instantiate(obj, transform.position - Vector3.right * 3, Quaternion.identity);
    }
}
