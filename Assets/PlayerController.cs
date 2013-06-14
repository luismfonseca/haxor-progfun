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
                OTComponent.position += new Vector2(Time.deltaTime * 10, 0f);
                break;
        }
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
        yield return new WaitForSeconds(0.1f);
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
                    MyCommandNow = "Move";
                    OTComponent.Play("fallRight");
                    rigidbody.velocity = new Vector3(0f, 2.5f, 0f);
                    yield return new WaitForSeconds(0.5f);
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
