using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float Speed;
	private PawnComponent _pawn;

	public void Awake()
	{
		_pawn = GetComponent<PawnComponent>();
	}

	public void Update()
	{
		Vector3 DirectionMove = new Vector3(Input.GetAxis("P1_MoveX"), 0f);

		_pawn.MoveInput(DirectionMove);
		_pawn.ShootInput(Input.GetButton("P1_Fire"));
		_pawn.ShieldInput(Input.GetButton("P1_Shield"));
		_pawn.ReloadInput(Input.GetButton("P1_Reload"));
	}
}
