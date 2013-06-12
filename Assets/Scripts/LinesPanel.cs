using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Haxor;

public class LinesPanel : MonoBehaviour
{
    internal static readonly float OFFSET_X = 44.2f;

    public List<Command> Commands;

    public List<Line> LinesDrawn;

	public GameObject Prefab;

    public Color CurrentColor = Color.black;

    private GameController gameController;

    void Awake()
    {
        gameController = GameController.Find();
    }

	void Start()
    {
        for (int i = 0; i < gameController.Game.CurrentLevel.Lines.Count; i++)
        {
            var line = gameController.Game.CurrentLevel.Lines[i];
            if (line.Color.Color == LineColor.Transparent.Color)
            {
                continue;
            }

            var previousLineSegment = GameObject.FindGameObjectsWithTag(Tag.LineSegment).Last();
            var lineSegment = Instantiate(Prefab, new Vector3(OFFSET_X + i, 1, -2), Quaternion.identity) as GameObject;

            var lineBody = lineSegment.transform.GetChild(0);
            lineBody.renderer.material.color = line.Color.Color;
            if (i == 0)
            {
                lineBody.animation.Play();
            }
            else
            {
                previousLineSegment.transform.GetChild(0).GetComponent<LineSegmentNext>().next = lineBody.gameObject;
            }
        }
	}
	
	void Update()
    {
        
	}
	
	static T GetRandomEnum<T>()
	{
		System.Array array = System.Enum.GetValues(typeof(T));
		return (T)array.GetValue(UnityEngine.Random.Range(0, array.Length));
	}
}
