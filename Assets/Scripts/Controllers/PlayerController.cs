using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Haxor;

public class PlayerController : MonoBehaviour {

    public List<Command> Commands;
    private string MyCommandNow;
    private OTAnimatingSprite OTComponent;
    private Vector2 initialPosition;
    private float initialRotation;

    void Awake()
    {
        Commands = new List<Command>();
        OTComponent = this.GetComponent<OTAnimatingSprite>();
        initialPosition = OTComponent.position;
        initialRotation = OTComponent.rotation;
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
        rigidbody.velocity = Vector3.zero;
        OTComponent.position = initialPosition;
        OTComponent.rotation = initialRotation;
    }

    public void Play()
    {
        StartCoroutine(executeCommands());
    }

    private IEnumerator executeCommands()
    {
        rigidbody.velocity = Vector3.zero;
        OTComponent.position = initialPosition;
        OTComponent.rotation = initialRotation;
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
                    Debug.Log("s = " + rigidbody.velocity.y);
                    if (Mathf.Abs(rigidbody.velocity.y) < 1f)
                    {
                        MyCommandNow = "MoveSlower";
                        OTComponent.Play("fallRight");
                        rigidbody.velocity = new Vector3(0f, 7f, 0f);
                    }
                    yield return new WaitForSeconds(0.6f);
                    OTComponent.Stop();
                    break;
            }
        }
        Commands.Clear();
        MyCommandNow = null;
    }

    public static PlayerController Find()
    {
        return GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
    }
}
