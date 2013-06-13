using UnityEngine;
using System.Collections;
using System.Linq;

public class GameAreaController : MonoBehaviour
{
    private GameController gameController;
    private GameObject gameAreaCamera;
    
    void Awake()
    {
        gameController = GameController.Find();
        gameAreaCamera = GameObject.FindGameObjectWithTag(Tag.GameAreaCamera);
    }

    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical") / 7f;
        gameAreaCamera.camera.orthographicSize = Mathf.Max(5f, Mathf.Min(10f, gameAreaCamera.camera.orthographicSize + zAxisValue));
        if (xAxisValue < 0.0f)
        {
            if (gameAreaCamera.transform.position.x <= 50f)
            {
                xAxisValue = 0f;
            }
        }
        else
        {
            GameObject lastLineSegment = GameObject.FindGameObjectsWithTag(Tag.LineSegment).LastOrDefault();
            if (lastLineSegment != null && gameAreaCamera.transform.position.x >= lastLineSegment.transform.position.x)
            {
                xAxisValue = 0f;
            }
        }
        gameAreaCamera.transform.Translate(new Vector3(xAxisValue, 0.0f, 0.0f));
    }
}
