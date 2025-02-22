using Godot;
using System;

public partial class EnemyMovement : Node {

	[Export] private Character characterBody;

	public override void _Process(double delta) {
		base._Process(delta);
		characterBody.Velocity = new Vector2(-characterBody.MovementSpeed, characterBody.Velocity.Y);
	}

}
