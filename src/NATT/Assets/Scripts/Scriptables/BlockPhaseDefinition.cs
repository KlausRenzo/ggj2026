using System;
using Data;
using UnityEngine;

[Serializable]
public class BlockPhaseDefinition
{
	public NpcDefinition npc;
	public string question;
	[Range(1, 3)] public int distance = 1;
	public MaskState requiredState;

	public int bonus;
	public int malus;
}