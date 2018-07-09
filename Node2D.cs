using Godot;
using static Godot.GD;
using System;
using System.Collections.Generic;

public class Node2D : Godot.Node2D
{
    public RichTextLabel ButtonLabel;
    public RichTextLabel StoryText;
    public LineEdit TextEnter;
    public List<string> PlayerWords = new List<string>();
    public readonly List<string> prompts = new List<string>
    {
        "a name",
        "a thing",
        "a feeling",
        "another feeling",
        "some things"
    };

    private string _story;
    
    public override void _Ready()
    {
        ButtonLabel = (RichTextLabel) GetNode("Background/EnterButton/ButtonLabel");
        TextEnter = (LineEdit) GetNode("Background/TextBox");
        StoryText = (RichTextLabel) GetNode("Background/StoryText");
        ResetTextbox();
        ButtonLabel.Text = "Ok";
        StoryText.BbcodeEnabled = true;
        StoryText.BbcodeText = "Welcome to Loopy Lips. \nPlease follow the prompts for a word or phrase."
                             + "\nCan I have " + prompts[PlayerWords.Count] + " , please?";
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

    public void ResetTextbox()
    {
        TextEnter.Text = "";
    }
    
    private void PlayerPrompt()
    {
        StoryText.BbcodeText = "Can I have " + prompts[PlayerWords.Count] + " , please?";
    }

    private bool IsStoryDone()
    {
        return PlayerWords.Count == prompts.Count;
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
        _story = $"Once upon a time there was {PlayerWords[0]} and he ate a {PlayerWords[1]} and felt" +
                $" very {PlayerWords[2]}. It was a {PlayerWords[3]} day for all good {PlayerWords[4]}";
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






