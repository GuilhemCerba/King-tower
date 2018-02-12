using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    // -12.11637 , -7.595014
    Vector3 direction = new Vector3 (7.88363f +1.0f, -7.595014f - 1.0f, 0f);
    Vector3 direction_2 = new Vector3(-12.11637f + 1.0f, -7.595014f - 1.0f, 0f);
    Vector3 fixed_direction1;
    float speed = 1;

	public void Spawn()
    {

        transform.position = LevelManager.Instance.EntrancePortal.transform.position;
        fixed_direction1 = new Vector3(7.88363f + 1.0f, -7.595014f - 1.0f, 0f) - transform.position;
    }

    public void Move()
    {

        //transform.Translate(new Vector3(direction.x, direction.y + 2) - transform.position * speed * Time.deltaTime);

        transform.Translate((direction - transform.position).normalized * speed * Time.deltaTime);
        //transform.Translate = Time.deltaTime * speed * (direction - transform.position);

        
        if ((direction - transform.position).magnitude < 0.1f)
        {
            direction = direction_2;

        }
        //transform.Translate((fixed_direction1) * speed * Time.deltaTime);
        //if ((direction - transform.position).magnitude < 0.1f)
        //{
        //    fixed_direction1 = new Vector3(-12.11637f + 1.0f, -7.595014f - 1.0f, 0f) - transform.position;
        //    direction = direction_2;

        //}

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
