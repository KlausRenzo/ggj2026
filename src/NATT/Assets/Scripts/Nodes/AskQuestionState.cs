using DG.Tweening;
using TMPro;
using UnityEngine;

public class AskQuestionState : StateMachineBehaviour
{
	private GameManager _gameManager;

	[SerializeField] public AudioClip[] clips;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		_gameManager = ServiceManager.GetService<GameManager>();
		
		var clip = Random.Range(0, clips.Length);
		_gameManager.PlaySound(clips[clip]);

		_gameManager.captionGroup.Open();
		_gameManager.captionGroup.GetComponentInChildren<TextMeshProUGUI>().text = _gameManager.activePhase.question;

		_gameManager.maskManager.Enable()
					.OnComplete(() => animator.SetTrigger("Next"));
	}
}