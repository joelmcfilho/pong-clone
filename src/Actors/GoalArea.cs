using Godot;


public partial class GoalArea : Area2D
{
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("/root/GameManager");
	}

	private async void PlayerGoalBodyEntered(Node2D ball)
	{
		await gameManager.GoalScore(Side.AI);
	}

	private async void AIGoalBodyEntered(Node2D ball)
	{
		await gameManager.GoalScore(Side.Player);
	}


}
