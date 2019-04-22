using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShakeCamera");
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator ShakeCamera()
    {
        Vector3 pos = transform.position;

        for (float i = 0f; i < 2f; i += .05f)
        {
            pos += new Vector3(Random.Range(-.05f, .05f) * i, Random.Range(-.05f, .05f) * i,0f);
            transform.position = pos;
            yield return new WaitForSeconds(.05f);
        }
    }

}
