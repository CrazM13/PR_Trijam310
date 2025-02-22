using Godot;
using System;

public partial class LevelSpawner : Node {

	private const string LEVEL_PATH = "res://Scenes/Levels/Level{0}.tscn";

	public override void _Ready() {
		base._Ready();

		PackedScene level = ResourceLoader.Load<PackedScene>(string.Format(LEVEL_PATH, GameManager.Instance.CurrentLevel));
		AddChild(level.Instantiate());

	}

}
