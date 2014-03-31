using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour {

    public float Speed;
    public bool IsBusy { get; private set; }
    public bool Retreating { get; private set; }

    private Transform _body;
    private Transform _end;
    private BoxCollider2D _collider;
    private float _length;

	// Use this for initialization
    public void Start()
    {
        _body = transform.FindChild("Body");
        _end = transform.FindChild("End");
        _collider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	public void Update () {

        if(Retreating)
            _length -= Speed;
        else
            _length += Speed;

        if (_length < 0)
        {
            Debug.Log("lenght < 0");
            Deactivate();
            return;
        }

        // set the tongue sprite at the correct position
        _body.localScale = new Vector2(1, _length);
        _body.localPosition = new Vector2(0, _length / 2 / 100);
        _collider.size = new Vector2(1, (_length + 46) / 100);
        _collider.center = _body.localPosition + new Vector3(0, 30.0f / 100);

        SetEndOnBody();
	}

    public void Fire()
    {
        Active();
    }

    public void Retreat()
    {
        Retreating = true;
    }

    private void Active()
    {
        Debug.Log("Activate");
        Retreating = false;
        gameObject.SetActive(true);
        IsBusy = true;
        _length = 0;
    }

    private void Deactivate()
    {
        Debug.Log("Deactivate");
        Retreating = false;
        gameObject.SetActive(false);
        IsBusy = false;
        _length = 0;
    }

    private void SetEndOnBody()
    {
        _end.localPosition = new Vector2(0, (_length + 46 / 2 ) / 100);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bush")
        {
            other.gameObject.GetComponent<Bush>().Eat();
        }
    }
}
