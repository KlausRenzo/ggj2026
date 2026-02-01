using UnityEngine;

[CreateAssetMenu(fileName = "Robo_definition", menuName = "NATT/Robo Definition", order = 0)]
public class NpcDefinition : ScriptableObject
{
	public string name;

	private string[] names = new[] {"Crumbio", "Teef", "Voronoi", "Maxwell", "Captain Joker", "De Mart", "Luuloo", "Spreets", "Heef", "Graziella"};

	public void Reset()
	{
		name = names[Random.Range(0, names.Length)];
	}
}