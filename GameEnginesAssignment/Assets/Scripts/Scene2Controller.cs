using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene2Controller : MonoBehaviour {

    public Camera main, enemy, second, fightCam;
    public ParticleSystem flame;
    private float dist;
    private bool switched = false;
    private bool fightSwitch = false;

    // Use this for initialization
    void Start () {
        main.gameObject.SetActive(true);
        second.gameObject.SetActive(false);
        Invoke("firstSwitch", 11);
    }
	
	// Update is called once per frame
	void Update () {
        
        
        dist = Vector3.Distance(GameObject.FindGameObjectWithTag("Leader").transform.position, GameObject.FindGameObjectWithTag("JamesBond").transform.position);
        if(dist <= 20.0f)
        {
            if (!switched)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                float distance = 0;
                int closest = 0;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (distance == 0)
                    {
                        distance = Vector3.Distance(enemies[i].gameObject.transform.position, GameObject.FindGameObjectWithTag("JamesBond").transform.position);
                        closest = i;
                    }
                    else if (distance > Vector3.Distance(enemies[i].gameObject.transform.position, GameObject.FindGameObjectWithTag("JamesBond").transform.position))
                    {
                        distance = Vector3.Distance(enemies[i].gameObject.transform.position, GameObject.FindGameObjectWithTag("JamesBond").transform.position);
                        closest = i;
                    }
                }
                GameObject.FindGameObjectWithTag("Leader").tag = "Attacking";
                enemies[closest].gameObject.tag = "Leader";
                GameObject.FindGameObjectWithTag("Leader").GetComponent<Boid>().maxSpeed = 15;
                switched = true;
            }
        }
        GameObject.FindGameObjectWithTag("Leader").GetComponent<OffsetPursue>().enabled = false;
        GameObject.FindGameObjectWithTag("Leader").GetComponent<Pursue>().enabled = true;
        if(GameObject.FindWithTag("Attacking") != null)
        {
            GameObject.FindWithTag("Attacking").GetComponent<Boid>().maxSpeed = 20;
            GameObject.FindWithTag("JamesBond").GetComponent<Boid>().maxSpeed = 20;
            fight();
        }
    }

    void firstSwitch()
    {
        if (!fightSwitch)
        {
            main.gameObject.SetActive(false);
            enemy.gameObject.SetActive(true);
            Invoke("secondSwitch", 7);
        }
    }

    void secondSwitch()
    {
        if (!fightSwitch)
        {
            enemy.gameObject.SetActive(false);
            second.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Leader").GetComponent<Boid>().maxSpeed = 18;
        }
        
    }

    void fight()
    {
        second.gameObject.SetActive(false);
        fightCam.gameObject.SetActive(true);
        flame.gameObject.SetActive(true);
        Invoke("SwitchScene", 5);
        fightSwitch = true;
    }

    void SwitchScene()
    {
        SceneManager.LoadScene("Scene2");
    }
}
