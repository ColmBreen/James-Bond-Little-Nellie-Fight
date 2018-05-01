using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {

    public ParticleSystem explosion;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Attacking")
        {
            Debug.Log("Destroy");
            explosion.gameObject.SetActive(true);
            GameObject enemy = GameObject.FindWithTag("Attacking");
            enemy.GetComponent<Rigidbody>().useGravity = true;
            Invoke("Quit", 1);
            
            
        }
    }

    private void Quit()
    {
        Application.Quit();
    }

}
