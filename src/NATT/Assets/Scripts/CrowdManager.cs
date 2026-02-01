using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
	private GameManager _gameManager;
	[SerializeField] private List<NpcBehaviour> _npcs;
	private NpcBehaviour _activeNpc;

	[SerializeField] private Transform cameraFront;

	public void Initialize(GameManager gameManager)
	{
		_gameManager = gameManager;
	}

	public TweenerCore<Vector3, Vector3, VectorOptions> SendNewNpc()
	{
		return _npcs.First().transform.DOMove(cameraFront.position, 0.5f).SetEase(Ease.OutQuint).Play();
	}
}