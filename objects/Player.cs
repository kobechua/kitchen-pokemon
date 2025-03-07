using Godot;
using System;

public partial class Player : CharacterBody3D
{
	
	private Vector3 velocity = Vector3.Zero;
	private float tileSize = 0.32f;
	private bool moving = false;
	private Vector3 dir = Vector3.Zero;
	private String spriteDir = "moveDown";
	private AnimatedSprite3D _animatedSprite;
	
	public override void _Ready() {
		_animatedSprite = GetNode<AnimatedSprite3D>("Sprite");
	}	
	
	public override void _PhysicsProcess (double delta) {
		base._PhysicsProcess(delta);
		
		if (moving) return;
		
		Vector3 moveDirection = Vector3.Zero;
		
		if (Input.IsActionPressed("ui_down")) {
			dir.Z = 1;
			dir.X = 0;
			spriteDir = "moveDown";
			move();
		}
		else if (Input.IsActionPressed("ui_up")) {
			dir.Z = -1;
			dir.X = 0;
			spriteDir = "moveUp";
			move();
		}
		else if (Input.IsActionPressed("ui_right")) {
			dir.X = 1;
			dir.Z = 0;
			spriteDir = "moveRight";
			move();
		}
		else if (Input.IsActionPressed("ui_left")) {
			dir.X = -1;
			dir.Z = 0;
			spriteDir = "moveLeft";
			move();
		}

		MoveAndSlide();
	}

	private void move() {
		if (dir != Vector3.Zero) {
			if (moving == false) {
				moving = true;
				_animatedSprite.Play(spriteDir);
				Tween tween = GetTree().CreateTween();
				tween.TweenProperty(this, "position", Position + dir * tileSize, 0.75);
				tween.TweenCallback(Callable.From(() => {
					moving = false;
					_animatedSprite.Stop();
				}
				));
			}
		}
	}
}
