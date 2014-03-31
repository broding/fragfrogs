using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum PlayerState { TONGUE, STUNNED, IDLE }

	public float MoveSpeed;
    public float MaxSpeed;
    public Direction Direction { get; private set; }
    public PlayerState State { get; private set; }

    private Tongue _tongue;

    private float _stunnedForSeconds;

	// Use this for initialization
	void Start () {
        State = PlayerState.IDLE;
        _tongue = transform.FindChild("Tongue").GetComponent<Tongue>();
        _tongue.gameObject.SetActive(false);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_tongue.IsBusy)
            {
                _tongue.Retreat();
            }
            else
            {
                State = PlayerState.TONGUE;
                _tongue.Fire();
            }
        }

        if (State == PlayerState.IDLE)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rigidbody2D.velocity = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);

            if (horizontal != 0 || vertical != 0)
            {
                Direction = Mathf.Abs(horizontal) > Mathf.Abs(vertical) ? (horizontal > 0 ? Direction.LEFT : Direction.RIGHT) : (vertical > 0 ? Direction.UP : Direction.DOWN);
                _tongue.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Util.GetAngleFromDirection(Direction)));
            }
        }
        else if (State == PlayerState.STUNNED)
        {
            _stunnedForSeconds -= Time.fixedDeltaTime;

            if (_stunnedForSeconds < 0)
            {
                State = PlayerState.IDLE;
            }
        }
        else if (State == PlayerState.TONGUE)
        {
            Debug.Log("tongue");
            rigidbody2D.velocity = new Vector2(0, 0);

            if (!_tongue.IsBusy)
                State = PlayerState.IDLE;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bush")
        {
            rigidbody2D.AddForce((transform.position - other.transform.position).normalized * 120);
            Stun(0.11f);
            other.gameObject.GetComponent<Bush>().Eat();
        }
    }

    public void Stun(float seconds)
    {
        State = PlayerState.STUNNED;
        _stunnedForSeconds = seconds;
    }
}
