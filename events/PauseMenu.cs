using Godot;
using System;

public partial class PauseMenu : Control
{
	private bool _paused = false;
	private AudioStream _selectSFX = GD.Load<AudioStream>("res://assets/sounds/SFX/menuselect.wav");

	private GameManager _gm;
	private AudioManager _am;


	private CenterContainer _mainMenu;
	private CenterContainer _modeMenu;
	private InitSurvival _initSurvival;
	private GameMode _gameMode;
	
	public override void _Ready()
	{
		_gm = GetNode<GameManager>("/root/GameManager");
		_am = GetNode<AudioManager>("/root/AudioManager");
		

		Visible = false;
		ProcessMode = ProcessModeEnum.Always;
	}

	
	public override void _Process(double delta)
	{
		if(_gm.isCountdownActive == true) return;

		if(_gm.survivalStartPublic == false && _gm.gameModeSelect == GameMode.Survival) return;


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
		_am.PlaySFX(_selectSFX);
        _paused = true;
        GetTree().Paused = true;
        Visible = true;
    }

    public void ResumeButton()
    {
		_am.PlaySFX(_selectSFX);
        _paused = false;
		GetTree().Paused = false;
		Visible = false;
    }

    public void QuitButton()
    {
		_am.PlaySFX(_selectSFX);
		_gameMode = GameMode.None;			
		_paused = false;
		GetTree().Paused = false;
		_gm.ClearSurvival();
		_gm.ClearBalls();
		_am.StopMusic();
		GetTree().ChangeSceneToFile("res://MainMenu.tscn");
		
		
        
    }
}
