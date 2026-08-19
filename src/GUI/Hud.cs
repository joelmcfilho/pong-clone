using System;
using System.Threading.Tasks;
using Godot;

public partial class Hud : Control
{
	private Label _playerScore;
	private Label _aiScore;
	private Label _countdownLabel;
	private Label _textLabel;
	private Label _player2Label;
	private GameManager _gm;

	public override void _Ready()
	{
		_playerScore = GetNode<Label>("MarginContainer/Tophud/Score/Player1Score");
		_aiScore = GetNode<Label>("MarginContainer/Tophud/Score/Player2Score");
		_countdownLabel = GetNode<Label>("MarginContainer2/CountdownLabel");
		_textLabel = GetNode<Label>("MarginContainer3/TextLabel");
		_player2Label = GetNode<Label>("MarginContainer/Tophud/Player2");
		_gm = GetNode<GameManager>("/root/GameManager");

		_countdownLabel.Visible = false;
		_textLabel.Visible = false;

		if(_gm.gameModeSelect == GameMode.Single)
		{
			_player2Label.Text = "CPU";

		}
		else if(_gm.gameModeSelect == GameMode.Multi)
		{
			_player2Label.Text = "Player 2";
		}
	}


	public void UpdateScoreHUD(int playerScore, int aiScore)
	{
		_playerScore.Text = playerScore.ToString();
		_aiScore.Text = aiScore.ToString();
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

	public async Task ShowPointSign()
	{
		_textLabel.AddThemeFontSizeOverride("font_size",128);
		_textLabel.Text = "POINT!";
		_textLabel.Visible = true;

		await ToSignal(GetTree().CreateTimer(3),
						SceneTreeTimer.SignalName.Timeout);

		_textLabel.Visible = false;
	}

	public async Task ShowWinner(Side side)
	{
		_textLabel.AddThemeFontSizeOverride("font_size",96);
		if(side == Side.Player)
		{
			_textLabel.Text = "The Player 1 Wins!";
		}
		else if(side == Side.Player2)
		{
			_textLabel.Text = "The Player 2 Wins!";
		}
		else if(side == Side.AI)
		{
			_textLabel.Text = "The CPU Wins!";
		}

		_textLabel.Visible = true;

		await ToSignal(GetTree().CreateTimer(5),
						SceneTreeTimer.SignalName.Timeout);

		_textLabel.Visible = false;
		
	}

}
