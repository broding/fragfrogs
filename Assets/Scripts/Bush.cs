using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {

	public int RegenTime = 10;

	private CircleCollider2D _collider;
	private float _timer;
	private float _randomRegenTime;
	private int _state;

	private Sprite[] _sprites;

	// Use this for initialization
	void Start () {
        _collider = GetComponent<CircleCollider2D>();
		_randomRegenTime = Random.Range(0,1) * 1.5f + 8.0f; 
		_timer = 0; 
		_state = 3; 

		_sprites = new Sprite[4];
		_sprites[0] = Resources.Load("Textures/bush0", typeof(Sprite)) as Sprite;
		_sprites[1] = Resources.Load("Textures/bush1", typeof(Sprite)) as Sprite;
		_sprites[2] = Resources.Load("Textures/bush2", typeof(Sprite)) as Sprite;
		_sprites[3] = Resources.Load("Textures/bush3", typeof(Sprite)) as Sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if(_state < 3) 
			_timer += Time.deltaTime;

		if(_timer > _randomRegenTime-(_state*1.4) && _state < 3) 
		{ 
			_state++; 
			_timer = 0; 
		} 

		if(_state == 0)
			_collider.enabled = false;
		else
			_collider.enabled = true;
		
		GetComponent<SpriteRenderer>().sprite = _sprites[_state];
	}

    public void Eat()
    {
		_state = 0;
		_timer = 0;
    }
}
