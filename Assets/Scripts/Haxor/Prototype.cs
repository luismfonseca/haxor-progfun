using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// the prototypes of the command buttons
/// </summary>
public class Prototype
{
    public const string CommandButton = "CommandButton";
    public const string RepeatButton = "RepeatButton";
    public const string SelectionBoxTiny = "Selection-Box-Tiny";

    public static string BestFit(string commandName)
    {
        switch (commandName)
        {
            case "Repeat":
                return RepeatButton;
            default:
                return CommandButton;
        }
    }
}
