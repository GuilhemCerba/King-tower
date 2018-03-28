using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//S : (11,8) 
//1 : (11,7)
//2 : (1,7)
//3 : (1,5)
//4 : (11,5)
//5 : (11,3)
//6 : (1,3)
//7 : (1,1)
//8 : (8,1)
//K : (8,0)


public class Monster : MonoBehaviour {

    Vector3 start = new Vector3(-17.6f, 8.5f,-10);

    int current_index = 0;
    Animator animator;

    const int stop = 0;
    const int runUp = 1;
    const int runRight = 2;
    const int runDown = 3;
    const int runLeft = 4;

    const float x_shift = -1.0f ;
    const float y_shift = -0.5f;

    //private float tileSize = LevelManager.Instance.TileSize;
    private float tileSize = 2;


    Point[] destinations = new Point[10] {new Point(11,8), new Point(11,7),new Point(1,7), new Point(1,5), new Point(11, 5), new Point(11, 3),
        new Point(1, 3), new Point(1, 1), new Point(8, 1),new Point(8,0) };
   

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float hitpoints = 100;

    private Vector3[] path = new Vector3[10];

    public bool IsActive { get; set; }

	public void Spawn()
    {
        tileSize = LevelManager.Instance.TileSize;
        
        IsActive = true;

        transform.position = LevelManager.Instance.EntrancePortal.transform.position;
        start = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        Debug.Log("In Monster start : " + start);
        Debug.Log("Initial position : " + transform.position);

        //start = transform.position; 
        SetupPath();
        
        //destinations[0] = LevelManager.Instance.EntrancePortal.transform.position + new Vector3(0, 2.0f, 0);
        //destinations[1] = destinations[0] + new Vector3(-20f, 0, 0);
        
        //destinations[2] = destinations[1] + new Vector3(0,4.0f, 0);
        //destinations[3] = destinations[2] + new Vector3(20f, 0, 0);
        //destinations[4] = destinations[3] + new Vector3(0, 4.0f, 0);
        //destinations[5] = destinations[4] + new Vector3(-20f, 0, 0);
        //destinations[6] = destinations[5] + new Vector3(0, 4f, 0);
        //destinations[7] = destinations[6] + new Vector3(14f, 0, 0);
        //destinations[8] = destinations[7] + new Vector3(0, 2f, 0);
        
    }

    void SetupPath()
    {
        for (int i = 0; i <destinations.Length; i++)
        {
            path[i] = (new Vector3(start.x + destinations[i].X * tileSize, start.y - destinations[i].Y * tileSize, 0));
            Debug.Log(i + " (" + path[i].x + "," + path[i].y + ")");

        }

    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        Debug.Log("Je vais à : " + current_index + "(" + path[current_index].x + "," + path[current_index].y + ")");
        transform.position = Vector3.MoveTowards(transform.position, path[current_index], speed * Time.deltaTime);



        if ((path[current_index] - transform.position).magnitude < 0.1f)
        //if (path[current_index] == transform.position)
        
        {
            //King reached
            if (current_index == destinations.Length - 1)
            {
                Destroy(gameObject);
                IsActive = false;
            }
            //Go to next checkpoint
            else
            {
                current_index++;
                animator.SetInteger("Direction", Update_Direction_animation());
            }
        }

    }

    public int Update_Direction_animation()
    {
        Vector3 orientation = path[current_index] - transform.position;
        if (orientation.x < 0 && abs(orientation.y) < 0.5) return runLeft;
        if (orientation.x > 0 && abs(orientation.y) < 0.5) return runRight;
        if (abs(orientation.x) < 0.5 && orientation.y < 0) return runDown;
        if (abs(orientation.x) < 0.5 && orientation.y > 0) return runUp;
        
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
