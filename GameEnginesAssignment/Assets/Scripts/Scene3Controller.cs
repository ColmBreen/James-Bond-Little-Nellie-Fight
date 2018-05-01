using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Controller : MonoBehaviour {

    public Camera first, second, third, goingAbove, above, bombsFalling, explosionCam;
    public GameObject bombs = null;
    bool moveAbove = false;
    bool switched = false;
    bool bombCam = false;

	// Use this for initialization
	void Start () {
        Invoke("FirstSwitch", 5);
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(GameObject.FindWithTag("Leader").transform.position, GameObject.FindWithTag("JamesBond").transform.position) <= 20 && !moveAbove)
        {
            moveAbove = true;
            third.gameObject.SetActive(false);
            goingAbove.gameObject.SetActive(true);
            Invoke("AboveSwitch", 3);
            GameObject.FindWithTag("Leader").GetComponent<Boid>().maxSpeed = 10;
            GameObject.FindWithTag("JamesBond").GetComponent<Boid>().maxSpeed = 15;
            GameObject.FindWithTag("Leader").GetComponent<Pursue>().enabled = false;
            GameObject.FindWithTag("Leader").GetComponent<Seek>().enabled = true;
        }
        if (moveAbove)
        {
            GameObject.FindWithTag("JamesBond").GetComponent<Seek>().aboveEnemy = GameObject.FindWithTag("Leader").transform.position + new Vector3(0f, 50f, 50f);
        }
        if(Vector3.Distance(GameObject.FindWithTag("JamesBond").GetComponent<Seek>().aboveEnemy, GameObject.FindWithTag("JamesBond").transform.position) <= 23f && moveAbove && !switched)
        {
            GameObject.FindWithTag("JamesBond").GetComponent<Seek>().aboveEnemy = Vector3.zero;
            bombs.transform.position = GameObject.FindWithTag("JamesBond").transform.position - new Vector3(3f, 5f, 0f);
            bombs.SetActive(true);
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
            enemies[closest].gameObject.tag = "Leader";
            GameObject.FindGameObjectWithTag("Leader").tag = "Attacking";
            switched = true;
            above.gameObject.SetActive(false);
            bombsFalling.gameObject.SetActive(true);
        }
        if (switched && !bombCam)
        {
            bombCam = true;
            Invoke("explosionCamera", 2);
        }
    }

    void FirstSwitch()
    {
        Invoke("SecondSwitch", 8);
        first.gameObject.SetActive(false);
        second.gameObject.SetActive(true);
    }

    void SecondSwitch()
    {
        second.gameObject.SetActive(false);
        third.gameObject.SetActive(true);
        GameObject.FindWithTag("JamesBond").GetComponent<NoiseWander>().enabled = false;
        GameObject.FindWithTag("JamesBond").GetComponent<Seek>().enabled = true;
        GameObject.FindWithTag("Leader").GetComponent<Boid>().maxSpeed = 15;
    }

    void AboveSwitch()
    {
        goingAbove.gameObject.SetActive(false);
        above.gameObject.SetActive(true);
    }

    void explosionCamera()
    {
        bombsFalling.gameObject.SetActive(false);
        explosionCam.gameObject.SetActive(true);
    }

}
