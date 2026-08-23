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

	private GameManager _gm;
	public override void _Ready()
	{
		_timeCounter = GetNode<Label>("MarginContainerTimer/Tophud/TimeCounter");
		_ballCounter = GetNode<Label>("MarginContainerBalls/Tophud/BallCounter");
		_countdownLabel = GetNode<Label>("MarginContainer2/CountdownLabel");
		_textLabel = GetNode<Label>("MarginContainer3/TextLabel");
		_marker = GetNode<Label>("MarginContainer3/Marker");

		_gm = GetNode<GameManager>("/root/GameManager");

		_countdownLabel.Visible = false;
		_textLabel.Visible = false;
		_marker.Visible = false;


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

	public void ShowSurvivalEndGameText()
	{
		ToSignal(GetTree().CreateTimer(1.5),SceneTreeTimer.SignalName.Timeout);

		_textLabel.Visible = true;
		_marker.Visible = true;

		GetTree().Paused = true;
		_textLabel.Text = "Game Over!";
		_marker.Text = $"Your time is {null} seconds!";
	}

	public void UpdateSurvivalTimer(double time)
	{
		_timeCounter.Text = $"{time.ToString("F1")} sec";
	}
}
