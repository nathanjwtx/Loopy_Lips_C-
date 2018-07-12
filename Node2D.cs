using Godot;
using static Godot.GD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using File = Godot.File;

public class Story
{
    public List<string> inputs = new List<string>();
    public string story;
}

public class Node2D : Godot.Node2D
{
    public RichTextLabel ButtonLabel;
    public RichTextLabel StoryText;
    public LineEdit TextEnter;
    public List<string> PlayerWords = new List<string>();

    public List<string> Prompts = new List<string>();

    private string _story;
    
    public override void _Ready()
    {
        ButtonLabel = (RichTextLabel) GetNode("Background/EnterButton/ButtonLabel");
        TextEnter = (LineEdit) GetNode("Background/TextBox");
        StoryText = (RichTextLabel) GetNode("Background/StoryText");
        GetFromJSON("loopy_lips.JSON");
        ResetTextbox();
        ButtonLabel.Text = "Ok";
        StoryText.BbcodeEnabled = true;
        StoryText.BbcodeText = "Welcome to Loopy Lips. \nPlease follow the prompts for a word or phrase."
                             + "\nCan I have " + Prompts[PlayerWords.Count] + " , please?";
    }

    private void _on_TextBox_text_entered(String newText)
    {
        PlayerWords.Add(newText);
        ResetTextbox();
        CheckNumberOfWords();
    }

    private void _on_EnterButton_pressed()
    {
        if (IsStoryDone())
        {
            GetTree().ReloadCurrentScene();
        }
        else
        {
            var newText = TextEnter.Text;
            _on_TextBox_text_entered(newText);    
        }
    }

    #region HelperMethods
    
    public void GetFromJSON(string filename)
    {
        var file = new File();
        file.Open(filename, 1);
        var text = file.GetAsText();
        List<Story> data = JsonConvert.DeserializeObject<List<Story>>(
            text);
//        replace line below with Godot's own method for getting the JSON text from file
//        System.IO.File.ReadAllText(@"C:\Users\Nathan - User\OneDrive\Godot\Loopy Lips - C#\loopy_lips.json")
        Random rnd = new Random();
        var i = rnd.Next(0, data.Count);
        _story = data[i].story;
        Prompts = data[i].inputs;
        file.Close();
    }
    
    public void ResetTextbox()
    {
        TextEnter.Text = "";
    }
    
    private void PlayerPrompt()
    {
        StoryText.BbcodeText = "Can I have " + Prompts[PlayerWords.Count] + " , please?";
    }

    private bool IsStoryDone()
    {
        return PlayerWords.Count == Prompts.Count;
    }

    private void CheckNumberOfWords()
    {
        if (IsStoryDone())
        {
            TellStory();
        }
        else
        {
            PlayerPrompt();
        }
    }

    private void TellStory()
    {
        for (int i = 0; i < PlayerWords.Count; i++)
        {
            _story = _story.Replace("%s" + System.Convert.ToString(i), PlayerWords[i]);
        }
        StoryText.BbcodeText = _story;
        ButtonLabel.Text = "Doogan!";
        EndGame();
    }

    private void EndGame()
    {
        TextEnter.QueueFree();
    }
    
    #endregion

}
