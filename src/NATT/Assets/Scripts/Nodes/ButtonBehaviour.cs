using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
	public void OnClick() => SceneManager.LoadScene(2);
	public void OnBlender() => SceneManager.LoadScene(1);
}