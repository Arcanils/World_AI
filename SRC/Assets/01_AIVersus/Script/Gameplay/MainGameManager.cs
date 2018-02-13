using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour {

	public enum EGameMode
	{
		TwoPlayers,
		OnePlayerOneAI,
		TwoAI,
	}

	public PawnComponent PawnPrefab;
	public GameObject PlayerPrefab;
	public AIController AIPrefab;

	public Transform TopDummy;
	public Transform BotDummy;

	public static MainGameManager Instance { get; private set; }

	private delegate void LaunchFct();
	private List<GameObject> _listObjectsToDestroy;
	private LaunchFct[] _launchGameFcts;
	private EGameMode _currentMode;

	private void Awake()
	{
		_listObjectsToDestroy = new List<GameObject>();
		_launchGameFcts = new LaunchFct[3];
		_launchGameFcts[(int)EGameMode.TwoPlayers] = LaunchModeTwoPlayer;
		_launchGameFcts[(int)EGameMode.OnePlayerOneAI] = LaunchModeOnePlayerOneAI;
		_launchGameFcts[(int)EGameMode.TwoAI] = LaunchModeTwoAI;
		Instance = this;
	}

	public void LaunchGame(EGameMode Mode)
	{
		_currentMode = Mode;
		_launchGameFcts[(int)_currentMode]();
	}
	public void ResetGameplay()
	{
		DestroyContents();
		_launchGameFcts[(int)_currentMode]();
	}

	private void LaunchModeTwoPlayer()
	{

	}

	private void LaunchModeOnePlayerOneAI()
	{

	}

	private void LaunchModeTwoAI()
	{

	}

	private void DestroyContents()
	{
		for (int i = _listObjectsToDestroy.Count - 1; i >= 0; --i)
		{
			if (_listObjectsToDestroy != null)
				Destroy(_listObjectsToDestroy[i]);
		}
	}

}
