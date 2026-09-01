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
	private MarginContainer _endGameButtonBox;

	private GameManager _gm;
	private GameMode _gameMode;
	public override void _Ready()
	{
		_timeCounter = GetNode<Label>("MarginContainerTimer/Tophud/TimeCounter");
		_ballCounter = GetNode<Label>("MarginContainerBalls/Tophud/BallCounter");
		_countdownLabel = GetNode<Label>("MarginContainerCountdown/CountdownLabel");
		_textLabel = GetNode<Label>("MarginContainerEndGame/VBoxContainer/TextLabel");
		_marker = GetNode<Label>("MarginContainerEndGame/VBoxContainer/Marker");
		_endGameButtonBox = GetNode<MarginContainer>("MarginContainerEndButtons");

		_gm = GetNode<GameManager>("/root/GameManager");
		_gameMode = new GameMode();

		_countdownLabel.Visible = false;
		_textLabel.Visible = false;
		_marker.Visible = false;
		_endGameButtonBox.Visible = false;


	}


	public async Task Countdown()
	{
		_gm.isCountdownActive = true;
		ShowCountdownLabel("3");

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		ShowCountdownLabel("2");

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		ShowCountdownLabel("1");

		await ToSignal(GetTree().CreateTimer(1), 
		SceneTreeTimer.SignalName.Timeout);

		_gm.isCountdownActive = false;

		ShowCountdownLabel("GO!");

		await ToSignal(GetTree().CreateTimer(0.6), 
		SceneTreeTimer.SignalName.Timeout);

		HideCountdownLabel();

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

		//ZERAR CONTADOR DE BOLA AQUI
		//ELIMINAR TODAS AS INSTÂNCIAS DE BOLA E DEIXAR SÓ UMA

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

		//ZERAR CONTADOR DE BOLA AQUI
		//ELIMINAR TODAS AS INSTÂNCIAS DE BOLA E DEIXAR SÓ UMA

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
