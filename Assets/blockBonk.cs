using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockBonk : MonoBehaviour
{
    public GameObject coinPref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log("collided with box");
        if (collision.gameObject.tag == "Player")
        {
            GameObject coin;

            coin = Instantiate(coinPref, transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random .Range (-1f,1f), 30f));
        }
    }
}
