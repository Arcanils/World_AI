using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float Speed;
	private PawnComponent _pawn;

	public void Update()
	{
		Vector3 DirectionMove = new Vector3(Input.GetAxis("horizontal"), 0f);

		if (DirectionMove == Vector3.zero)
		{
			if (Input.GetButtonDown("Fire"))
				_pawn.Shoot(Vector3.up);
			else if (Input.GetButton("Reload"))
			{
				//_pawn.StartReload();
			}
		}
		else
		{
			_pawn.Move(DirectionMove * Speed * Time.deltaTime);
		}

	}
}
