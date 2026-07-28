using Godot;


public partial class GoalArea : Area2D
{
	private GameManager gameManager;

	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("../GameManager");
	}

	private void PlayerGoalBodyEntered(Node2D ball)
	{
		gameManager.GoalScore(Side.AI);
	}

	private void AIGoalBodyEntered(Node2D ball)
	{
		gameManager.GoalScore(Side.Player);
	}


}
