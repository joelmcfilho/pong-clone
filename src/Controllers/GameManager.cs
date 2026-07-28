using Godot;
using System;

public partial class GameManager : Node
{
	private int _playerScore = 0;
	private int _aiScore = 0;
	private BallBehavior _ball;
	private PlayerMovement _playerPaddle;
	private AIBehavior _aiPaddle;
	private Hud _hud;

	public override void _Ready()
	{
		_ball = GetNode<BallBehavior>("../Ball");
		_playerPaddle = GetNode<PlayerMovement>("../PlayerBar");
		_aiPaddle = GetNode<AIBehavior>("../AIBar");
		_hud = GetNode<Hud>("../HUD");

	}
	
	public void GoalScore(Side side)
	{
		if(side == Side.Player)
		{
			_playerScore ++;
			_hud.UpdateScoreHUD(_playerScore,_aiScore);
			GD.Print("Jogador pontuou!"); //Retirar na UI
			_ball.ResetBall();
		}
		if(side == Side.AI)
		{
			_aiScore ++;
			_hud.UpdateScoreHUD(_playerScore,_aiScore);
			GD.Print("CPU pontuou!"); //Retirar na UI
			_ball.ResetBall();
		}

		GD.Print($"SCORE: {_playerScore} x {_aiScore}");

		if(_playerScore == 3)
		{
			GD.Print($"SCORE FINAL: {_playerScore} x {_aiScore}");
			EndGame(Side.Player);
		}
		else if(_aiScore == 3)
		{
			GD.Print($"SCORE FINAL: {_playerScore} x {_aiScore}");
			EndGame(Side.AI);
		}	

	}

	private void EndGame(Side side)
	{
		String winner;
		_playerScore = 0;
		_aiScore = 0;
		_hud.UpdateScoreHUD(_playerScore,_aiScore);

		if(side == Side.Player)
		{
			winner = "Jogador"; //Retirar na UI
			GD.Print($"Jogo Finalizado. O vencedor é o {winner}!"); //Retirar na UI
		}
		else if(side == Side.AI)
		{
			winner = "CPU"; //Retirar na UI
			GD.Print($"Jogo Finalizado. O vencedor é o {winner}!"); //Retirar na UI
		}

		_ball.ResetBall();
		_playerPaddle.ResetPaddle(_playerPaddle._initialPosition);
		_aiPaddle.ResetPaddle(_aiPaddle._initialPosAI);

		
	}

	
}
