using UnityEngine;
using System.Collections;

public class GameAreaController : MonoBehaviour
{
    private GameController gameController;
    
    void Awake()
    {
        gameController = GameController.Find();
    }


}
