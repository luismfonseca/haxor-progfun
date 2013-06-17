using UnityEngine;
using System.Collections;
using Haxor;

public class LineSegmentNext : MonoBehaviour {

    public GameObject next;

    void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public void AnimateNextLineSegment()
    {
        if (next != null)
        {
            next.animation.Play();
        }
        else
        {
            var gameController = GameController.Find();
            var level = gameController.Game.CurrentLevel;
            float progress = Game.EvaluateProgress(level.Lines, gameController.linesPanel.PlayerRunResultLines);
            if (progress >= 1)
            {
                StoryController.Find().Advance();
            }
        }
    }
}
