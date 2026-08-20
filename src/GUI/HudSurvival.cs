using Godot;
using System;
using System.Threading.Tasks;

public partial class HudSurvival : Control
{
	private Label _timeCounter;
	private Label _ballCounter;
	private Label _countdownLabel;
	private Label _textLabel;

	private GameManager _gm;
	public override void _Ready()
	{
		_timeCounter = GetNode<Label>("MarginContainer/Tophud/TimeCounter");
		_ballCounter = GetNode<Label>("MarginContainer/Tophud/BallCounter");
		_countdownLabel = GetNode<Label>("MarginContainer2/CountdownLabel");
		_textLabel = GetNode<Label>("MarginContainer3/TextLabel");

		_gm = GetNode<GameManager>("/root/GameManager");

		_countdownLabel.Visible = false;
		_textLabel.Visible = false;


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
}
