using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    private int currency;

    [SerializeField]
    private Text currencyTxt;

	public TowerBtn ClickedBtn{
		get;
		set;
	}

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            currency = value;
            currencyTxt.text = value.ToString() + " <color=yellow>gold</color>";
        }
    }

    
    public ObjectPool Pool { get; set; }


    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }


    // Use this for initialization
    void Start () {
        Currency = 50000;
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleEscape ();
		
	}
	public void PickTower(TowerBtn towerBtn){

        if(Currency >= towerBtn.Price)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);

        }
		
	}

	public void BuyTower(){
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
        }

    }

	private void HandleEscape(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Hover.Instance.Deactivate ();

		}
	}

    public void StartWave()
    {
        StartCoroutine(SpawnWave());


    }

    private IEnumerator SpawnWave()
    {
        int monsterIndex = 0;
        string type = string.Empty;
        type = "Goblin";
        Monster monster = Pool.GetObject(type).GetComponent<Monster>();
        monster.Spawn();
        //monster.Move();

        yield return new WaitForSeconds(2.5f);
    }
}
