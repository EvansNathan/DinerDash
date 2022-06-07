using System.Collections;
using System.Collections.Generic;
using Menus_UI;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public Transform goal;
    private NavMeshAgent agent;
    public Transform exit;
    private bool col=false;
    public GameObject self;
    private float lifeTime;
    public bool active = false;
    public GameObject music;
    bool musicDeath = false;
    private bool alive = true;
    private bool hasStolen = false;
    
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        music = GameObject.FindGameObjectWithTag("music");
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        goal = temp[0].transform;
        temp = GameObject.FindGameObjectsWithTag("exit");
        exit = temp[0].transform;
        lifeTime = 30;
        MusicMgr.Instance.musicSource.pitch += .1f;
        GameState.Instance.karenAttacks += 1;
    }

    void Update()
    {
        if (active)
        {
            lifeTime -= Time.deltaTime;
            if (!col)
            {
                agent.destination = goal.position;
            }
            if (lifeTime <= 0)
            {
                agent.destination = exit.position;
                if (!musicDeath)
                {
                    MusicMgr.Instance.musicSource.pitch -= .1f;
                    musicDeath = true;
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<playerController>().dashing == true)
        {
            Debug.Log("kill");
            active = false;
            
            gameObject.layer = 3;

            SetLayerRecursively(gameObject, 3);
            if (!musicDeath)
            {
                MusicMgr.Instance.musicSource.pitch -= .1f;
                musicDeath = true;
            }
            Destroy(GetComponent<NavMeshAgent>());

            GetComponent<Rigidbody>().mass = 1;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().AddTorque(0f, 100f, -150);
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(Random.Range(-5f, 5f), 100f, -150);
            
            

        }
        else if (collision.gameObject.CompareTag("Player") && active)
        {
            if (!hasStolen)
            {
                hasStolen = true;
                
                var subtracted = Random.Range(2, 5);
                UIMgr.Instance.StartStolenAnim(subtracted);
            }
            
            agent.destination = exit.position;
            GetComponent<NavMeshAgent>().speed = 5;
            //Destroy(GetComponent<BoxCollider>());
            col = true;
            if (!musicDeath)
            {
                MusicMgr.Instance.musicSource.pitch -= .1f;
                musicDeath = true;
            }
        }
        
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }


}
