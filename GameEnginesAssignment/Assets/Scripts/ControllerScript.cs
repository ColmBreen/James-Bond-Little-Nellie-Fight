using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControllerScript : MonoBehaviour {

    public GameObject enemy;
    public Camera main,second;

	// Use this for initialization
	void Start ()
    {
        Invoke("CameraSwitch", 6);

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void CameraSwitch()
    {
        main.gameObject.SetActive(false);
        second.gameObject.SetActive(true);
        Invoke("SceneSwitch", 11);
    }

    void SceneSwitch()
    {
        SceneManager.LoadScene("Scene1");
    }
}
