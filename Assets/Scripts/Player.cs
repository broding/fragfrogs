using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    enum Direction { LEFT, RIGHT, UP, DOWN }

	public float MoveSpeed;

    private Vector3 _acceleration;
    private Vector3 _velocity;

    private bool _moving;
    private Vector3 _target;
	private Vector3 _previousPosition;
    private float _time;
    private Vector3 _startPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_moving)
        {
            _time += Time.deltaTime;
            transform.localPosition = Move(_time, MoveSpeed, _startPosition, _target - _startPosition);

            if (_time > MoveSpeed)
                _moving = false;

        }
        else // find target
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
                    setTarget(horizontal > 0 ? Direction.LEFT : Direction.RIGHT);
                else
                    setTarget(vertical > 0 ? Direction.UP : Direction.DOWN);

                _moving = true;
                _time = 0;
                _startPosition = transform.localPosition;
            }

            
        }

        transform.position += _velocity * Time.deltaTime;
	}

    private Vector3 Move(float t, float d, Vector3 b, Vector3 c)
    {
        t /= d / 2;
        if (t < 1) return c / 2 * t * t + b;
        t--;
        return -c / 2 * (t * (t - 2) - 1) + b;
    }

    private void setTarget(Direction direction)
    {
        Vector3 position = transform.localPosition;
		_previousPosition = position;

        switch (direction)
        {
            case Direction.UP:
                _target = position - new Vector3(0, -GameManager.TILE_SIZE);
                break;
            case Direction.DOWN:
                _target = position - new Vector3(0, GameManager.TILE_SIZE);
                break;
            case Direction.LEFT:
                _target = position - new Vector3(-GameManager.TILE_SIZE, 0);
                break;
            case Direction.RIGHT:
                _target = position - new Vector3(GameManager.TILE_SIZE, 0);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bush")
        {
			_target = _previousPosition;
			_startPosition = transform.localPosition;
			_time = MoveSpeed / 3;
            other.gameObject.GetComponent<Bush>().Eat();
        }

    }
}
