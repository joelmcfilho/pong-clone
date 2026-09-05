using Godot;
using System;
using System.Threading.Tasks;

public partial class HudSurvival : Control
{
	private Label _timeCounter;
	private Label _ballCounter;
	private Label _countdownLabel;
	private Label _textLabel;
	private Label _marker;
	private Label _headsupCountdown;
	private MarginContainer _endGameButtonBox;
	private MarginContainer _ballHeadsup;


	private GameManager _gm;
	private AudioManager _am;
	private GameMode _gameMode;

	private AudioStream _countSFX = GD.Load<AudioStream>("res://assets/sounds/SFX/countdown.wav");
	private AudioStream _goSFX = GD.Load<AudioStream>("res://assets/sounds/SFX/go.wav");
	public override void _Ready()
	{
		_timeCounter = GetNode<Label>("MarginContainerTimer/Tophud/TimeCounter");
		_ballCounter = GetNode<Label>("MarginContainerBalls/Tophud/BallCounter");
		_countdownLabel = GetNode<Label>("MarginContainerCountdown/CountdownLabel");
		_textLabel = GetNode<Label>("MarginContainerEndGame/VBoxContainer/TextLabel");
		_marker = GetNode<Label>("MarginContainerEndGame/VBoxContainer/Marker");
		_endGameButtonBox = GetNode<MarginContainer>("MarginContainerEndButtons");
		_ballHeadsup = GetNode<MarginContainer>("MarginContainerBallHeadsup");
		_headsupCountdown = GetNode<Label>("MarginContainerBallHeadsup/VBoxContainer/HeadsupCount");

		_gm = GetNode<GameManager>("/root/GameManager");
		_am = GetNode<AudioManager>("/root/AudioManager");
		_gameMode = new GameMode();

		_countdownLabel.Visible = false;
		_textLabel.Visible = false;
		_marker.Visible = false;
		_endGameButtonBox.Visible = false;
		_ballHeadsup.Visible = false;


	}


	public async Task Countdown()
	{
		_gm.isCountdownActive = true;
		_am.PlaySFX(_countSFX);
		ShowCountdownLabel("3");

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		_am.PlaySFX(_countSFX);
		ShowCountdownLabel("2");

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		_am.PlaySFX(_countSFX);
		ShowCountdownLabel("1");

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		_gm.isCountdownActive = false;

		_am.PlaySFX(_goSFX);
		ShowCountdownLabel("GO!");

		await ToSignal(GetTree().CreateTimer(0.6), 
		SceneTreeTimer.SignalName.Timeout);

		HideCountdownLabel();

	}

	public async Task BallHeadsupCountdown()
	{
		_ballHeadsup.Visible = true;

		_headsupCountdown.Text = "3";

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		_headsupCountdown.Text = "2";

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		_headsupCountdown.Text = "1";

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		_ballHeadsup.Visible = false;

	}

	public void ShowCountdownLabel(String text)
	{
		_countdownLabel.Text = text;
		_countdownLabel.Visible = true;
	}


	public void HideCountdownLabel()
	{
		_countdownLabel.Visible = false;
	}

	public void ShowSurvivalEndGameText(double time)
	{
		ToSignal(GetTree().CreateTimer(1.5),SceneTreeTimer.SignalName.Timeout);

		_textLabel.Visible = true;
		_marker.Visible = true;
		_endGameButtonBox.Visible = true;

		GetTree().Paused = true;
		_textLabel.Text = "Game Over!";
		_marker.Text = $"Your time is {_gm.RegisterTime(time)} seconds!";
	}

	public void UpdateSurvivalTimer(double time)
	{
		_timeCounter.Text = $"{time.ToString("F1")} sec";
	}

	public void UpdateSurvivalBallCounter(int ballcount)
	{
		_ballCounter.Text = $"{ballcount}";
	}

	public void RestartPressed()
	{
		double time = 0.0f;
		_timeCounter.Text = $"{time.ToString("F1")} sec";
		_gm.KillTimeCount();
		_gm.ClearBalls();

		_textLabel.Visible = false;
		_marker.Visible = false;
		_endGameButtonBox.Visible = false;

		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://survival.tscn");

	}

	public void QuitSurvivalPressed()
	{
		double time = 0.0f;
		_timeCounter.Text = $"{time.ToString("F1")} sec";

		_textLabel.Visible = false;
		_marker.Visible = false;
		_endGameButtonBox.Visible = false;

		_gm.KillTimeCount();
		_gm.ClearBalls();
		_gm.ClearSurvival();
		_gameMode = GameMode.None;
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://MainMenu.tscn");

	}
}
