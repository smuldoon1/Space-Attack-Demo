using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatEngine : MonoBehaviour
{
    public bool cheatsEnabled { get; protected set; } = false;
    InputField commandBox;

    void Start()
    {
        commandBox = GetComponentInChildren<InputField>();
        commandBox.onEndEdit.AddListener(delegate { CheckCommand(commandBox); });
        commandBox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            Debug.Log("Commands toggled");
            if (commandBox.gameObject.activeSelf)
            {
                Game.Pause(true);
                commandBox.gameObject.SetActive(false);
            }
            else
            {
                Game.Pause(false);
                commandBox.gameObject.SetActive(true);
            }
        }
    }

    void CheckCommand(InputField command)
    {
        string[] parsedCommand = command.text.ToLower().Split(' ');
        switch (parsedCommand[0])
        {
            case "hitmewiththoselaserbeams":
                Debug.Log("Equipped laser turret");
                cheatsEnabled = true;
                break;
            case "eatyourspinach":
                Debug.Log("Player health increased by 10000");
                cheatsEnabled = true;
                break;
            case "doubletaprootbeer":
                Debug.Log("Player fire rate increased");
                cheatsEnabled = true;
                break;
            case "welcometothematrix":
                Debug.Log("Time slowed down");
                cheatsEnabled = true;
                break;
            case "nofloating":
                Debug.Log("Improved precision of player movement");
                cheatsEnabled = true;
                break;
            case "stickmeinthecockpit":
                Debug.Log("Enabled FPS camera");
                cheatsEnabled = true;
                break;
            case "whyistherumgone":
                Debug.Log("Changed world theme");
                cheatsEnabled = true;
                break;
            case "bankrupt":
                Debug.Log("Reset player score");
                break;
            case "openborders":
                Debug.Log("Removed forcefields");
                cheatsEnabled = true;
                break;
            case "kill":
                Debug.Log("Destroyed the player");
                break;
            case "spawn":
                if (parsedCommand.Length != 2)
                {
                    Debug.Log("Syntax error: Use command like: \"spawn [type]\"");
                    break;
                }
                switch (parsedCommand[1])
                {
                    case "small":
                        Debug.Log("Spawned a small enemy");
                        cheatsEnabled = true;
                        break;
                    default:
                        Debug.Log("Attempted spawn target not recognised");
                        break;
                }
                break;
            case "powerup":
                if (parsedCommand.Length < 2 || parsedCommand.Length > 4)
                {
                    Debug.Log("Syntax error: Use command like: \"powerup [name] [duration(optional)] [strength(optional)]\"");
                    break;
                }
                switch (parsedCommand[1])
                {
                    case "freeze":
                        Debug.Log("Enabled freeze powerup");
                        cheatsEnabled = true;
                        break;
                    case "quick_fire":
                        Debug.Log("Enabled quick_fire powerup");
                        cheatsEnabled = true;
                        break;
                    case "score_multiplier":
                        Debug.Log("Enabled score_multiplier powerup");
                        cheatsEnabled = true;
                        break;
                    case "repair":
                        Debug.Log("Enabled repair powerup");
                        cheatsEnabled = true;
                        break;
                    case "plasma_shield":
                        Debug.Log("Enabled plasma_shield powerup");
                        cheatsEnabled = true;
                        break;
                    case "":
                        Debug.Log("Enabled score_multiplier powerup");
                        cheatsEnabled = true;
                        break;
                    default:
                        Debug.Log("Powerup not recognised");
                        break;
                }
                break;
            default:
                Debug.Log("Command not recognised");
                break;
        }
        command.text = "";
    }
}
