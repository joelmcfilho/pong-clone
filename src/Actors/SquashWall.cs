using Godot;
using System;

public partial class SquashWall : Area2D
{
	private AudioManager _am;

	public override void _Ready()
	{
		_am = GetNode<AudioManager>("/root/AudioManager");

		BodyEntered += BallsToTheWall;

	}

	public void BallsToTheWall(Node2D body)
	{
		if(body is BallBehavior)
		{
			_am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/impactwall.wav"));
		}
		
	}
}
