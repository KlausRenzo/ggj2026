using System;
using UnityEngine;
using Data;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Block_definition", menuName = "NATT/Block Definition", order = 0)]
public class BlockDefinition : ScriptableObject
{
	public int blockNumber;
	public List<BlockPhaseDefinition> phases;
}

[CreateAssetMenu(fileName = "Robo_definition", menuName = "NATT/Robo Definition", order = 0)]
public class NpcDefinition : ScriptableObject
{
	public int id;
}

[Serializable]
public class BlockPhaseDefinition
{
	public string question;
	public MaskState requiredState;

	public int bonus;
	public int malus;
}

