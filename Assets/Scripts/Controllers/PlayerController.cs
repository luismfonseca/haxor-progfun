using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Haxor;

/// <summary>
/// Controller of the stickman, it controls its movements
/// and animations.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public List<Command> Commands;
    private string MyCommandNow;
    private OTAnimatingSprite OTComponent;
    private Vector2 initialPosition;
    private GameController gameController;

    void Awake()
    {
        gameController = GameController.Find();
        Commands = new List<Command>();
        OTComponent = this.GetComponent<OTAnimatingSprite>();
        initialPosition = OTComponent.position;
    }

	void Start ()
    {
	
	}

    void Update()
    {
        if (MyCommandNow == null)
        {
            return;
        }
        switch (MyCommandNow)
        {
            case "Move":
                OTComponent.position += new Vector2(Time.deltaTime * 8f, 0f);
                break;
            case "MoveSlower":
                OTComponent.position += new Vector2(Time.deltaTime * 5f, 0f);
                break;
        }
	}

    public void Stop()
    {
        StopCoroutine("executeCommands");
        Commands.Clear();
        MyCommandNow = null;
        ResetCharacter();
    }

    public void Play()
    {
        StartCoroutine(executeCommands());
    }

    private IEnumerator executeCommands()
    {
        ResetCharacter();
        yield return new WaitForSeconds(0.4f);
        foreach (var command in Commands)
        {
            MyCommandNow = command.Name;
            switch (command.Name)
            {
                case "Move":
                    OTComponent.Play("runRight");
                    yield return new WaitForSeconds(0.1f);
                    OTComponent.Stop();
                    break;
                case "Jump":
                    if (Mathf.Abs(rigidbody.velocity.y) < 1.5f)
                    {
                        MyCommandNow = "MoveSlower";
                        OTComponent.Play("fallRight");
                        rigidbody.angularVelocity = Vector3.zero;
                        rigidbody.velocity = new Vector3(0f, 9f, 0f);
                    }
                    yield return new WaitForSeconds(0.6f);
                    OTComponent.Stop();
                    break;
            }
        }
        Commands.Clear();
        MyCommandNow = null;

        // If on last linesegment, advance level
        if (GameObject.FindGameObjectsWithTag(Tag.LineSegment).Last().transform.position.x < transform.position.x)
        {
            StoryController.Find().Advance();
        }
    }

    private void ResetCharacter()
    {
        // Reset the velocity
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        // "Pause" the physics
        rigidbody.isKinematic = true;

        // Do positioning
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;

        // Re-enable the physics
        rigidbody.isKinematic = false;
        rigidbody.WakeUp();
    }

    public static PlayerController Find()
    {
        return GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
    }
}
