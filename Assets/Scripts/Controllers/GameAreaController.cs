using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Game area Controller is where the camera to the lines (left panel) is controlled 
/// </summary>
public class GameAreaController : MonoBehaviour
{
    private GameController gameController;
    private GameObject gameAreaCamera;

    private readonly float minPositionX = 49f;
    
    void Awake()
    {
        gameController = GameController.Find();
        gameAreaCamera = GameObject.FindGameObjectWithTag(Tag.GameAreaCamera);
        gameAreaCamera.transform.Translate(new Vector3(minPositionX - gameAreaCamera.transform.position.x, 0.0f, 0.0f));
    }

    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical") / -7f;
        gameAreaCamera.camera.orthographicSize = Mathf.Max(5f, Mathf.Min(10f, gameAreaCamera.camera.orthographicSize + zAxisValue));
        if (xAxisValue < 0.0f)
        {
            if (gameAreaCamera.transform.position.x <= minPositionX)
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
