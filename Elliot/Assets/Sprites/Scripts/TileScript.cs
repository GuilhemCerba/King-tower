using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {
	public Point GridPosition {get;set;}

	public bool IsEmpty { get; private set; }
	private Color32 fullColour = new Color32(255,118,118,255);
	private Color32 emptyColour = new Color32(97,255,90,255);
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public Vector2 WorldPosition{
		get{
			return new Vector2 (transform.position.x + (GetComponent<SpriteRenderer> ().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer> ().bounds.size.y / 2));
		}
	}


	public void Setup(Point gridPos, Vector3 worldPos,Transform parent){
		IsEmpty = true;
		this.GridPosition = gridPos;
		transform.position = worldPos;
		transform.SetParent(parent);
		LevelManager.Instance.Tiles.Add(gridPos,this);

	}
	private void OnMouseOver(){

		if (!EventSystem.current.IsPointerOverGameObject () && GameManager.Instance.ClickedBtn != null) {
			
			if (IsEmpty) {
				ColourTile (emptyColour);
			} 
			if (!IsEmpty) {
				ColourTile (fullColour);
			}
		
			else if (Input.GetMouseButtonDown (0)) {
				PlaceTower ();

			}
		}
	}

	private void OnMouseExit(){
		ColourTile (Color.white);
	}

	private void PlaceTower(){

	
		GameObject tower = Instantiate (GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
		tower.transform.SetParent (transform);
		GameManager.Instance.BuyTower ();
		IsEmpty = false;
		ColourTile (Color.white);


	}

	private void ColourTile(Color newColour){
		spriteRenderer.color = newColour;
	}
}
