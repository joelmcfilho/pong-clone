using Godot;
using System;

public partial class PauseMenu : Control
{
	private bool _paused = false;
	private GameManager _gm;
	private CenterContainer _mainMenu;
	private CenterContainer _modeMenu;
	
	public override void _Ready()
	{
		_gm = GetNode<GameManager>("/root/GameManager");

		Visible = false;
	}

	
	public override void _Process(double delta)
	{
		if(_gm.isCountdownActive == true)
		{
			return;
		}
		if(Input.IsActionJustPressed("ui_cancel") && _paused == false)
		{
			ShowPause();
		}
		else if(Input.IsActionJustPressed("ui_cancel") && _paused == true)
		{
			_paused = false;
			GetTree().Paused = false;
			Visible = false;
		}
	}

	public void ShowPause()
    {
        _paused = true;
        GetTree().Paused = true;
        Visible = true;
    }

    public void ResumeButton()
    {
        _paused = false;
		GetTree().Paused = false;
		Visible = false;
    }

    public void QuitButton()
    {
		_paused = false;
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://MainMenu.tscn");
        
    }
}
