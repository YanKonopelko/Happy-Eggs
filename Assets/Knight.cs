using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed;
    private bool facingRight = true;

    public float startWaitTime;
    private float waitTime;
    private int i = 0;

    public Transform[] moveSpots;
    
    void Start()
    {
        waitTime = startWaitTime;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[i].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                if (facingRight == true)
                {
                    facingRight = false;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    facingRight = true;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                i += 1;
                if (i == moveSpots.Length)
                {
                    i = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }

    }

}
