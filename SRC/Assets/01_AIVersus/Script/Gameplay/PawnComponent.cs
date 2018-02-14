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

	public float NAmmoLeft { get; private set; }

	private FSM.FSM _reloadFSM;
	private FSM.FSM _moveFSM;
	private FSM.FSM _shootFSM;

	private void Awake()
	{
		FSM.FSM.LinksAndInit(this, _reloadFSM, _moveFSM, _shootFSM);
	}

	private void Start()
	{
		NAmmoLeft = NAmmos;
	}

	public void Move(Vector3 Position)
	{
		transform.position += Position;
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
