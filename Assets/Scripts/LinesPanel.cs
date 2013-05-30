using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinesPanel : MonoBehaviour {
	
	public List<CommandsLevel1> Commands;
	public GameObject prefab;
	
	Color currentColor = Color.black;
	int drawOffset = 0;
	int commandOffset = 0;
	Color generatedCurrentColor = Color.black;
	int generatedCommandOffset = 0;
	int generatedDrawOffset = 0;
	
	void Start () {
		
		Commands = new List<CommandsLevel1>();
		for (int i = 0; i < 100; ++i) {
			Commands.Add(GetRandomEnum<CommandsLevel1>());
		}
	}
	
	void Update () {
		// User line
		while (ControlPanel.Commands.Count - commandOffset > 0) {
			switch(ControlPanel.Commands[commandOffset++]) {
			case CommandsLevel1.Go:
				var lineSegment = Instantiate(prefab, new Vector3(-16.85f + drawOffset, 1, -2), Quaternion.identity) as GameObject;
				lineSegment.renderer.material.color = currentColor;
				drawOffset++;
				break;
			case CommandsLevel1.Skip:
				drawOffset++;
				break;
			case CommandsLevel1.Red:
				currentColor = Color.red;
				break;
			case CommandsLevel1.Black:
				currentColor = Color.black;
				break;
			case CommandsLevel1.Green:
				currentColor = Color.green;
				break;
			}
		}
		
		// Correct line
		while(this.Commands.Count - generatedCommandOffset > 0 && generatedDrawOffset < 13) {
			switch(Commands[generatedCommandOffset++]) {
			case CommandsLevel1.Go:
				var lineSegment = Instantiate(prefab, new Vector3(-16.85f + generatedDrawOffset, -1, -2), Quaternion.identity) as GameObject;
				lineSegment.renderer.material.color = generatedCurrentColor;
				generatedDrawOffset++;
				break;
			case CommandsLevel1.Skip:
				generatedDrawOffset++;
				break;
			case CommandsLevel1.Red:
				generatedCurrentColor = Color.red;
				break;
			case CommandsLevel1.Black:
				generatedCurrentColor = Color.black;
				break;
			case CommandsLevel1.Green:
				generatedCurrentColor = Color.green;
				break;
			}
			
		}
	}
	
	static T GetRandomEnum<T>()
	{
		System.Array array = System.Enum.GetValues(typeof(T));
		return (T)array.GetValue(UnityEngine.Random.Range(0, array.Length));
	}
}
