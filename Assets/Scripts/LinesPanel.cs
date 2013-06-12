using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Haxor;
using Assets.Scripts.Haxor;

public class LinesPanel : MonoBehaviour, IHandleCommand
{
    internal static readonly float OFFSET_X = 44.2f;

    public List<Line> PlayerRunResultLines;
    public LineColor PlayerRunCurrentLineColor;

	public GameObject Prefab;

    public Color CurrentColor = Color.black;

    private GameController gameController;

    void Awake()
    {
        gameController = GameController.Find();
    }

	void Start()
    {
        RenderLineDraw(gameController.Game.CurrentLevel.Lines);
	}

    public void Play(PlayerSolution playerSolution)
    {
        GameObject.FindGameObjectsWithTag(Tag.LineSegmentPlayer).ToList().ForEach(gameObject => Destroy(gameObject));

        PlayerRunResultLines = new List<Line>();
        PlayerRunCurrentLineColor = LineColor.Black;
        foreach (var command in playerSolution)
        {
            command.GetAction()(this);
        }
        RenderLineDraw(PlayerRunResultLines, true);
    }

    public void AddLine(Line line)
    {
        line.LineColor = PlayerRunCurrentLineColor;
        PlayerRunResultLines.Add(line);
    }

    public void ChangeCurrentColor(LineColor color)
    {
        PlayerRunCurrentLineColor = color;
    }

    public void SkipLine()
    {
        var line = new Line();
        line.LineColor = LineColor.Transparent;
        PlayerRunResultLines.Add(line);
    }

    public void RenderLineDraw(List<Line> lines, bool isPlayerLine = false)
    {
        string tag = isPlayerLine ? Tag.LineSegmentPlayer : Tag.LineSegment;
        Transform firstLineBody = null;
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (line.LineColor.Color == LineColor.Transparent.Color)
            {
                continue;
            }

            var previousLineSegment = GameObject.FindGameObjectsWithTag(tag).LastOrDefault();

            var lineSegment = Instantiate(Prefab, new Vector3(OFFSET_X + i, isPlayerLine ? 1 : -1, -2), Quaternion.identity) as GameObject;

            lineSegment.tag = tag;
            var lineBody = lineSegment.transform.GetChild(0);
            lineBody.renderer.material.color = line.LineColor.Color;
            if (firstLineBody == null)
            {
                firstLineBody = lineBody;
            }
            else
            {
                previousLineSegment.transform.GetChild(0).GetComponent<LineSegmentNext>().next = lineBody.gameObject;
            }
        }
        if (firstLineBody != null)
        {
            firstLineBody.animation.Play();
        }
    }
}
