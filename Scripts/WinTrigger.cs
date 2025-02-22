using Godot;
using System;

public partial class WinTrigger : Area2D {

	public override void _Ready() {
		base._Ready();

		this.BodyEntered += this.OnBodyEnter;
	}

	private void OnBodyEnter(Node2D body) {
		
		if (body is Character character && character.Label == "PLAYER") {
			GameManager.Instance.CurrentLevel++;
			if (GameManager.Instance.CurrentLevel > GameManager.MAX_LEVELS) {
				CallDeferred("MoveToWinScene");
			} else {
				CallDeferred("MoveToNextLevel");
			}
		}

	}

	private void MoveToNextLevel() {
		GetTree().ReloadCurrentScene();
	}

	private void MoveToWinScene() {
		GetTree().ChangeSceneToFile("res://Scenes/WinScene.tscn");
	}
}
