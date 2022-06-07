using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
        }
    }
    
}
