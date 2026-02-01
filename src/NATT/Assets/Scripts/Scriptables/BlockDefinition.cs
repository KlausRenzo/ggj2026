using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Block_definition", menuName = "NATT/Block Definition", order = 0)]
public class BlockDefinition : ScriptableObject
{
	public int blockNumber;
	public List<BlockPhaseDefinition> phases;
}