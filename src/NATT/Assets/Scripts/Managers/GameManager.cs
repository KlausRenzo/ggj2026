using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PlayerController player;
	public MaskManager maskManager;
	public CrowdManager crowdManager;

	public Camera gameCamera;
	public Camera maskCamera;
	[SerializeField] private GameState _state;
	public List<BlockDefinition> blocks;

	public UiElement captionGroup;

	public GameState State
	{
		get => _state;
		set => _state = value;
	}

	private void Awake()
	{
		ServiceManager.RegisterService(this);
	}

	private void Start()
	{
		player.Initialize(this);
		maskManager.Initialize(this);
		crowdManager.Initialize(this);
	}

	public BlockDefinition activeBlock;
	public BlockPhaseDefinition activePhase;
	private int activeBlockIndex = -1;
	private int _activePhaseIndex;

	public void GoToNextBlock()
	{
		activeBlockIndex++;

		activeBlock = blocks[activeBlockIndex];
		_activePhaseIndex = 0;
		activePhase = activeBlock.phases[0];
	}

	public void RestartBlock()
	{
		_activePhaseIndex = 0;
		activePhase = activeBlock.phases[0];
	}

	public bool NextPhase()
	{
		_activePhaseIndex++;
		return _activePhaseIndex == activeBlock.phases.Count;
	}

	public int bonusScore;
	public int isolationScore;
	public int burnout;

	[SerializeField] private AudioSource _fxSource;
	[SerializeField] private AudioSource _musicSource;

	public void PlaySound(AudioClip clip)
	{
		_fxSource.PlayOneShot(clip);
	}

	public void PlayMusic(AudioClip clip)
	{
		_musicSource.clip = clip;
		_musicSource.Play();
	}
}