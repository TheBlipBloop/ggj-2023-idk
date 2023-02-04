using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookWormTest : MonoBehaviour
{
    public BookWorm bookWorm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Root>() != null)
        {
            bookWorm.activateWorm("I'm Worm dabadee dabada");
        }
    }
}
