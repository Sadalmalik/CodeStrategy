using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name}.OnTriggerEnter: {other.gameObject.name}");
    }
    
    void OnTriggerExit(Collider other)
    {
        Debug.Log($"{gameObject.name}.OnTriggerExit: {other.gameObject.name}");
    }
}
