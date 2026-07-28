using Godot;

public partial class Hud : CanvasLayer
{
	private Label _playerScore;
	private Label _aiScore;

	public override void _Ready()
	{
		_playerScore = GetNode<Label>("MarginContainer/CenterContainer/VBoxContainer/Score/PlayerScore");
		_aiScore = GetNode<Label>("MarginContainer/CenterContainer/VBoxContainer/Score/AIScore");
	}

	public void UpdateScoreHUD(int playerScore, int aiScore)
	{
		_playerScore.Text = playerScore.ToString();
		_aiScore.Text = aiScore.ToString();
	}
}
