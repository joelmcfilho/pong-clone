using Godot;
using System;

public partial class SquashWall : StaticBody2D
{
	private AudioManager _am;

	public override void _Ready()
	{
		_am = GetNode<AudioManager>("/root/AudioManager");
	}

	public void BallsToTheWall(Node2D ball)
	{
		_am.PlaySFX(GD.Load<AudioStream>("res://assets/sounds/SFX/impactwall.wav"));
	}
}
