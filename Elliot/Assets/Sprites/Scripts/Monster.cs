using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public void Spawn()
    {

        transform.position = LevelManager.Instance.EntrancePortal.transform.position;
    }
}
