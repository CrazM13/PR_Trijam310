using Godot;
using System;

public partial class PlayerController : Node {

	[Signal] public delegate void TransformBatEventHandler();
	[Signal] public delegate void TransformMistEventHandler();

	[Export] private Character characterBody;

	public bool IsActive { get; set; } = true;
	public bool CanMoveFreely { get; set; } = false;

	private uint activeCollisionMask;
	private uint inactiveCollisionMask = 2;
	private uint activeCollisionLayer;
	private uint inactiveCollisionLayer = 0;

	public override void _Ready() {
		base._Ready();

		characterBody.Collide += this.OnCollide;
		activeCollisionMask = characterBody.CollisionMask;
		activeCollisionLayer = characterBody.CollisionLayer;

	}

	private void OnCollide(KinematicCollision2D collision) {
		if (IsActive && collision.GetCollider() is Character) {
			IsActive = false;
			characterBody.Velocity += ((collision.GetNormal() + characterBody.UpDirection) * 0.5f) * characterBody.JumpForce;

			GetTree().CreateTimer(2f).Timeout += () => {
				GetTree().ReloadCurrentScene();
			};
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (IsActive) {
			if (CanMoveFreely) {
				FreeMovement();
			} else {
				PlatformerMovement();
			}
		}
	}

	private void PlatformerMovement() {
		Vector2 input = new Vector2(Input.GetAxis("ui_left", "ui_right"), Input.GetAxis("ui_down", "ui_up"));

		characterBody.Velocity = new Vector2(input.X * characterBody.MovementSpeed, characterBody.Velocity.Y);

		if (input.Y > 0) {
			if (characterBody.IsOnFloor()) {
				AttemptTurnIntoBat();
			}
		} else if (input.Y < 0) {
			AttemptTurnIntoMist();
		}
	}

	private void FreeMovement() {
		Vector2 input = new Vector2(Input.GetAxis("ui_left", "ui_right"), -Input.GetAxis("ui_down", "ui_up"));

		characterBody.Velocity = new Vector2(input.X * characterBody.MovementSpeed, input.Y * characterBody.MovementSpeed);
	}

	public void Untransform() {
		CanMoveFreely = false;
		characterBody.ApplyGravity = true;
		characterBody.CollisionMask = activeCollisionMask;
		characterBody.CollisionLayer = activeCollisionLayer;
		IsActive = true;
	}

	public void AttemptTurnIntoBat() {
		if (!IsActive || CanMoveFreely) return;

		characterBody.Velocity += characterBody.UpDirection * characterBody.JumpForce;
		CanMoveFreely = true;
		characterBody.ApplyGravity = false;
		EmitSignal(SignalName.TransformBat);
	}
	
	public void AttemptTurnIntoMist() {
		if (!IsActive || CanMoveFreely) return;

		IsActive = false;
		characterBody.ApplyGravity = false;
		characterBody.CollisionMask = inactiveCollisionMask;
		characterBody.CollisionLayer = inactiveCollisionLayer;
		EmitSignal(SignalName.TransformMist);
	}

}
