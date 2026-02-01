using DG.Tweening;
using UnityEngine;

public class UiElement : MonoBehaviour
{
	[SerializeField] private Vector3 _startPosition;
	[SerializeField] private Vector3 _endPosition;

	[ContextMenu("Register Open")]
	public void RegisterOpen()
	{
		_startPosition = this.transform.position;
	}

	[ContextMenu( "Register Close")]
	public void RegisterClose()
	{
		_endPosition = this.transform.position;
	}

	public void Open()
	{
		this.transform.DOMove(_endPosition, 1);
	}

	public void Close()
	{
		this.transform.DOMove(_startPosition, 1);
	}
}