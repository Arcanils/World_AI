using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnComponent : MonoBehaviour {

	[Header("Character Setting")]
	[SerializeField]
	private GameObject PrefabToShoot;
	[SerializeField]
	private float FireRate;
	[SerializeField]
	private int NAmmos;
	[SerializeField]
	private Vector2 Speed;


	public float NAmmoLeft { get; private set; }

	private FSM.FSM _reloadFSM;
	private FSM.FSM _moveFSM;
	private FSM.FSM _shootFSM;
	private FSM.LocalSpace _space;

	private void Awake()
	{
		_space = new FSM.LocalSpace();
		FSM.FSM_Gameplay.CreateFSMGameplay(out _reloadFSM, out _moveFSM, out _shootFSM);
		FSM.FSM.LinksFSMToThisSpace(_space, _reloadFSM, _moveFSM, _shootFSM);
		FSM.FSM.InitFSMs(this, _reloadFSM, _moveFSM, _shootFSM);
	}

	private void Start()
	{
		NAmmoLeft = NAmmos;
	}

	public void Update()
	{
		_reloadFSM.Tick(Time.deltaTime);
		_shootFSM.Tick(Time.deltaTime);
	}

	public void FixedUpdate()
	{
		_moveFSM.Tick(Time.fixedDeltaTime);
		Debug.LogError("M:" + _moveFSM.CurrentState.GetType() + "|R:" + _reloadFSM.CurrentState.GetType() + "|S:" + _shootFSM.CurrentState.GetType());
	}

	public void ShootInput(bool InputDown)
	{
		_space.BoolVars["InputShoot"] = InputDown;
	}

	public void MoveInput(Vector2 Dir)
	{
		_space.FloatVars["InputMoveX"] = Dir.x;
	}

	public void ReloadInput(bool InputDown)
	{
		_space.BoolVars["InputReload"] = InputDown;
	}

	public void ShieldInput(bool InputDown)
	{
		_space.BoolVars["InputShield"] = InputDown;
	}



	public void Move(Vector3 Position)
	{
		transform.position += Position * Speed.x * Time.fixedDeltaTime;
	}

	public void Shoot(Vector3 Direction)
	{
		--NAmmoLeft;

		GameObject instance = GameObject.Instantiate(PrefabToShoot, transform.position, Quaternion.identity);
		var scriptBullet = instance.GetComponent<BulletComponent>();

		if (scriptBullet != null)
			scriptBullet.InitDirection(Direction);
	}

	public void ReloadAmmo()
	{
		NAmmoLeft = NAmmos;
	}

	public void Shield()
	{

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//MainGameManager.Instance.
	}
}
