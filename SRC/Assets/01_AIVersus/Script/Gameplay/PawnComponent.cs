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
	private float ReloadDuration;


	public bool IsShooting { get; private set; }
	public bool IsReloading { get; private set; }
	public bool IsMoving { get; private set; }
	public bool IsShieldOn { get; private set; }

	public bool CanShoot { get; private set; }

	public float NAmmoLeft { get; private set; }

	private float _cooldownShoot;
	private Coroutine _routineReload;

	private void Start()
	{
		StartCoroutine(CooldownShoot());
		NAmmoLeft = NAmmos;
	}

	public void Move(Vector3 Position)
	{
		transform.position += Position;
	}

	public void Shoot(Vector3 Direction)
	{
		if (CanShoot)
		{
			_Shoot(Direction);
		}
	}

	private void _Shoot(Vector3 Direction)
	{
		_cooldownShoot = FireRate;
		CanShoot = false;
		--NAmmoLeft;

		GameObject instance = GameObject.Instantiate(PrefabToShoot, transform.position, Quaternion.identity);
		var scriptBullet = instance.GetComponent<BulletComponent>();

		if (scriptBullet != null)
			scriptBullet.InitDirection(Direction);
	}


	public void StartReload()
	{
		if (IsReloading)
			return;

		IsReloading = true;
		if (_routineReload != null)
		{
			Debug.LogError("RoutineReload not finished and restarted ?");
			StopCoroutine(_routineReload);
			_routineReload = null;
		}
		_routineReload = StartCoroutine(ReloadingEnum());
	}
	public void StopReload()
	{
		IsReloading = false;
		if (_routineReload != null)
		{
			StopCoroutine(_routineReload);
			_routineReload = null;
		}
	}

	public void Shield()
	{

	}

	private IEnumerator CooldownShoot()
	{
		var waitValue = new WaitForFixedUpdate();
		var deltaTime = Time.fixedDeltaTime;
		while (true)
		{
			if (_cooldownShoot > 0)
			{
				_cooldownShoot -= deltaTime;
			}
			else
			{
				CanShoot = true;
			}

			yield return waitValue;
		}
	}


	private IEnumerator ReloadingEnum()
	{
		IsReloading = true;
		yield return new WaitForSeconds(ReloadDuration);
		NAmmoLeft = NAmmos;
		IsReloading = false;
		_routineReload = null;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//MainGameManager.Instance.
	}
}
