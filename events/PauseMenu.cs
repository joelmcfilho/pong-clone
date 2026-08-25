using Godot;
using System;

public partial class PauseMenu : Control
{
	private bool _paused = false;
	private GameManager _gm;
	private CenterContainer _mainMenu;
	private CenterContainer _modeMenu;
	private InitSurvival _initSurvival;
	private GameMode _gameMode;
	
	public override void _Ready()
	{
		_gm = GetNode<GameManager>("/root/GameManager");
		// if(_gameMode == GameMode.Survival)
		// {
			_initSurvival = GetParent<InitSurvival>();
		// }
		GD.Print($"PauseMenu: {GetPath()}");
    	GD.Print($"Parent: {GetParent().GetPath()}");
    	GD.Print($"InitSurvival: {_initSurvival}");
		

		Visible = false;
	}

	
	public override void _Process(double delta)
	{
		if(_gm.isCountdownActive == true) return;

		if(_initSurvival.survivalStart == false) return;

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
