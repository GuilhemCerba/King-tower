using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour {
   
    int current_index = 0;
    Animator animator;

    const int stop = 0;
    const int runUp = 1;
    const int runRight = 2;
    const int runDown = 3;
    const int runLeft = 4;

    const float x_shift = -1.0f ;
    const float y_shift = -0.5f;

    //+ y_shift

    /*Vector3[] destinations = { new Vector3(7.88363f + x_shift, -7.595014f + y_shift, 0f) , new Vector3(-12.11637f + x_shift, -7.595014f + y_shift, 0f)
    ,new Vector3(-12.11637f +x_shift , -3.595014f+ y_shift, 0f) , new Vector3(7.88363f +x_shift , -3.595014f + y_shift, 0f)
    ,new Vector3(7.88363f +x_shift , 0.4049864f + y_shift, 0f),new Vector3(-12.11637f + x_shift, 0.4049864f + y_shift, 0f)
    ,new Vector3(-12.11637f + x_shift, 4.404986f + y_shift, 0f) , new Vector3(1.88363f + x_shift , 4.404986f + y_shift, 0f)
    ,new Vector3(1.88363f + x_shift ,6.404986f + y_shift, 0f)};*/

    Vector3[] destinations = new Vector3[9];

float speed = 10;

	public void Spawn()
    {

        transform.position = LevelManager.Instance.EntrancePortal.transform.position;
        destinations[0] = LevelManager.Instance.EntrancePortal.transform.position + new Vector3(0, 2.0f, 0);
        destinations[1] = destinations[0] + new Vector3(-20f, 0, 0);
        destinations[2] = destinations[1] + new Vector3(0,4.0f, 0);
        destinations[3] = destinations[2] + new Vector3(20f, 0, 0);
        destinations[4] = destinations[3] + new Vector3(0, 4.0f, 0);
        destinations[5] = destinations[4] + new Vector3(-20f, 0, 0);
        destinations[6] = destinations[5] + new Vector3(0, 4f, 0);
        destinations[7] = destinations[6] + new Vector3(14f, 0, 0);
        destinations[8] = destinations[7] + new Vector3(0, 2f, 0);
        
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        

        transform.Translate((destinations[current_index] - transform.position).normalized * speed * Time.deltaTime);

        if ((destinations[current_index] - transform.position).magnitude < 0.1f)
        
        {
            if (current_index == destinations.Length - 1) Destroy(gameObject);
            else
            {
                current_index++;
                animator.SetInteger("Direction", Update_Direction_animation());
            }
        }

    }

    public int Update_Direction_animation()
    {
        Vector3 path = destinations[current_index] - transform.position;
        if (path.x < 0 && abs(path.y) < 0.5) return runLeft;
        if (path.x > 0 && abs(path.y) < 0.5) return runRight;
        if (abs(path.x) < 0.5 && path.y < 0) return runDown;
        if (abs(path.x) < 0.5 && path.y > 0) return runUp;
        
        return stop;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    float abs (float x)
    {
        if (x < 0) x = -x;
        return x;
    }
}
