using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 position;
    public Vector3 velocity;
    private Vector3 startPos;
    private float timeSinceLastCheck = 0.0f;
    private float randomChance = 9.0f;
    private float rand;
    void Start()
    {
        position = transform.position;
        startPos = position;
        Invoke("Delete", 5);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastCheck += Time.deltaTime;
        position = position + velocity*Time.deltaTime;
        transform.localPosition = position;
        if(position.x>90)
        {
            //Debug.Log(timeSinceLastCheck);
            if (timeSinceLastCheck > randomChance)
            {
                timeSinceLastCheck = 0.0f;
                //Debug.Log("check");
                //Debug.Log(randomChance);
                rand = Random.Range(0.0f, 10.0f);

                if (rand > randomChance)
                {
                    position = startPos;
                    randomChance = 9;
                }
                else
                {
                    randomChance -= 1;
                }
            }
        }
    }

    private void Delete()
    {
        Destroy(this.gameObject);
    }
}
