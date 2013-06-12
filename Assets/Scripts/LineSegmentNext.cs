using UnityEngine;
using System.Collections;

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
    }
}
