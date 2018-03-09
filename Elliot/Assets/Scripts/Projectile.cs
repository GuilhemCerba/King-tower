using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    private Monster target;
    private Tower parent;

	// Use this for initialization
	void Start () {
		
	}

    public void Initialize(Tower parent)
    {
        this.parent = parent;
        this.target = parent.Target;
    }
	
	// Update is called once per frame
	void Update () {

        MoveToTarget();
	}

    private void MoveToTarget()
    {
        Debug.Log("target is null : " + (target == null) );
        Debug.Log("target is active : " + (target.IsActive));
        if (target != null && target.IsActive)
        {
            Debug.Log("MoveToTarget");
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);
        }
    }
}
