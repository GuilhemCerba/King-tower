using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {


	public TowerBtn ClickedBtn{
		get;
		set;
	}

		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleEscape ();
		
	}
	public void PickTower(TowerBtn towerBtn){
		this.ClickedBtn = towerBtn;
		Hover.Instance.Activate (towerBtn.Sprite);
	}

	public void BuyTower(){
		Hover.Instance.Deactivate ();

	}

	private void HandleEscape(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Hover.Instance.Deactivate ();

		}
	}
}
