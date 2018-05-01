using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Invoke("Destroy", 2);
    }

    private void Destroy()
    {
        Debug.Log("Destroy");
        GameObject enemy = GameObject.FindWithTag("Attacking");
        enemy.GetComponent<Rigidbody>().useGravity = true;
        Destroy(enemy, 3);
    }
}
