using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Character : CharacterBody2D {

	[Signal] public delegate void CollideEventHandler(KinematicCollision2D collision);
	[Signal] public delegate void CollideWithCharacterEventHandler(KinematicCollision2D collision);

	[Export] public string Label { get; set; } = "NONE";
	[Export] public float Drag { get; set; } = 0.2f;
	[Export] public float FallingPower { get; set; } = 2f;
	[Export] public float MovementSpeed { get; set; } = 1000f;
	[Export] public float JumpForce { get; set; } = 1000f;
	[Export] public bool ApplyGravity { get; set; } = true;

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		if (ApplyGravity) {
			if (IsOnFloor()) {
				this.Velocity += this.GetGravity() * (float) delta;
			} else {
				this.Velocity += this.GetGravity() * FallingPower * (float) delta;
			}
		}

		this.Velocity *= 1 - (Drag * (float) delta);

		MoveAndSlide();

		for (int i = 0; i < GetSlideCollisionCount(); i++) {
			KinematicCollision2D collision = GetSlideCollision(i);

			if (collision.GetCollider() is Character character) {
				character.AlertOfCollision(collision);
				EmitSignal(SignalName.CollideWithCharacter, collision);
			}

			EmitSignal(SignalName.Collide, collision);
		}
	}

	public void AlertOfCollision(KinematicCollision2D collision) {
		EmitSignal(SignalName.Collide, collision);
	}

}
