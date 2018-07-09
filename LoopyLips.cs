using Godot;
using static Godot.GD;
using System;
using System.Collections.Generic;

public class LoopyLips2 : Node2D
{
    // Member variables here, example:
    public List<string> PlayerWords;

    public readonly List<string> prompts = new List<string>
    {
        "a name",
        "a thing"
    };

    public override void _Ready()
    {
        Print("Nathan");
//        var textbox = (RichTextLabel) GetNode("StoryText");
//        textbox.Text = "Yo this is Nathan";
//        $Background / StoryText.bbcode_text =
//            ("Welcome to Loopy Lips. \nPlease follow the prompts for a word or phrase."
//             + "\nCan I have " + prompts[PlayerWords.Count] + " , please?");
    }
}
