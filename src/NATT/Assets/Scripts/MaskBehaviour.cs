using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class MaskBehaviour : MonoBehaviour
{
	private MaskState state;
	private bool isDragging;

	[SerializeField] private Image _image;
	[SerializeField] private Sprite _defaultSprite;
	[SerializeField] private List<SpriteState> _sprites;

	private void Start()
	{
		_image.sprite = _defaultSprite;
	}

	public void SetSpriteToEmotion(MaskState state)
	{
		_image.sprite = _sprites.First(x => x.state == state).sprite;
	}

	public void ResetSprite()
	{
		_image.sprite = _defaultSprite;
	}
}

[Serializable]
public class SpriteState
{
	public Sprite sprite;
	public MaskState state;
}