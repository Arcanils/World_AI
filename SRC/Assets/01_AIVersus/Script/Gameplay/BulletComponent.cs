using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour {

	public float DurationBeforeDeath;
	public Vector2 Speed;

	private Vector3 _direction;

	public void Awake()
	{
		Invoke("SelfDestroy", DurationBeforeDeath);
	}

	public void InitDirection(Vector3 Direction)
	{
		_direction.x = Speed.x * Direction.x * Time.fixedDeltaTime;
		_direction.y = Speed.y * Direction.y * Time.fixedDeltaTime;

		Debug.Log(_direction);
	}

	void FixedUpdate ()
	{
		transform.position += _direction;
	}

	public void SelfDestroy()
	{
		Destroy(gameObject);
	}
}
