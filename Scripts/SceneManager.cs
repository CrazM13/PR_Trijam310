using Godot;
using System;

[GlobalClass]
public partial class SceneManager : Node {

	[Export(PropertyHint.File, "*.tscn")] private string transitionPath = "res://Scenes/Transition.tscn";

	public override void _Ready() {
		base._Ready();
	}

	public void LoadScene(string scenePath) {
		TransitionEffect transition = ResourceLoader.Load<PackedScene>(transitionPath).Instantiate<TransitionEffect>();

		transition.TransitionInComplete += () => FullLoadScene(scenePath);
		transition.TransitionInComplete += transition.PlayTransitionOut;

		GetTree().Root.AddChild(transition);

		transition.PlayTransitionIn();
	}

	private void FullLoadScene(string scenePath) {
		GetTree().ChangeSceneToPacked(ResourceLoader.Load<PackedScene>(scenePath));

	}

	public void Quit() {
		GetTree().Quit();
	}
}
