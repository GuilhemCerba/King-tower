using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>{
    //6.404986
    [SerializeField]
	private GameObject[] tilePrefabs;

	[SerializeField]
	private CameraMovement cameraMovement;

	[SerializeField]
	private Transform map;

	private Point entrancePortal, king;

	[SerializeField]
	private GameObject entrancePortalPrefab;

    
    public Portal EntrancePortal { get; set; }

    [SerializeField]
	private GameObject kingPrefab;

	public Dictionary<Point,TileScript> Tiles { get; set; }

	public float TileSize{
		get { return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x;}
	}

	void Start(){
		createLevel ();
        nextLevel();
	}

	void Update(){
	}
		

	private void createLevel(){

		Tiles = new Dictionary<Point,TileScript> ();
		string[] mapData = ReadLevelText("Level");


		int mapXSize = mapData[0].ToCharArray().Length;
		int mapYSize = mapData.Length;
		Vector3 maxTile = Vector3.zero;

		Vector3 start = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));

		for (int y = 0; y < mapYSize; y++) {
			char[] newTiles = mapData[y].ToCharArray();

			for (int x = 0; x < mapXSize; x++) {
				PlaceTile(newTiles[x].ToString(),x, y, start);
				
			}
		}
		maxTile = Tiles [new Point (mapXSize - 1, mapYSize - 1)].transform.transform.position;

		cameraMovement.SetLimits(new Vector3(maxTile.x+TileSize,maxTile.y-TileSize));
		spawnPortals ();
	}
	private void PlaceTile(string tileType, int x, int y, Vector3 start){
		int tileIndex = int.Parse(tileType);

		TileScript newTile = Instantiate (tilePrefabs[tileIndex]).GetComponent<TileScript>();

		newTile.Setup(new Point (x, y), new Vector3(start.x +TileSize * x,start.y - TileSize * y, 0),map);


	}

	private string[] ReadLevelText(string textDoc){
		TextAsset leveldata = Resources.Load (textDoc) as TextAsset;
		string tempData = leveldata.text.Replace (Environment.NewLine, string.Empty);

		return tempData.Split ('-');
	}

	private void spawnPortals(){
		entrancePortal = new Point (11, 8);
		GameObject temp = (GameObject) Instantiate (entrancePortalPrefab, Tiles [entrancePortal].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        EntrancePortal = temp.GetComponent<Portal>();
        EntrancePortal.name = "EntrancePortal";


		king = new Point (8, 0);
		Instantiate (kingPrefab, Tiles[king].GetComponent<TileScript>().WorldPosition, Quaternion.identity);

	}

    private void nextLevel()
    {
        StartCoroutine(levelCreator());
    }

    IEnumerator levelCreator()
    {
        yield return new WaitForSeconds(5);

        string[] mapData = ReadLevelText("Level1");


        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;
        Vector3 maxTile = Vector3.zero;

        Vector3 start = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y+9, start);

            }
        }
        maxTile = Tiles[new Point(mapXSize - 1, mapYSize - 1)].transform.transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, (maxTile.y - TileSize) - 17));
    }
}
