using Godot;
using System;

public partial class BGMusic : AudioStreamPlayer {

	private static float currentPlayback = 0;

	public override void _Ready() {
		base._Ready();

		this.Play(currentPlayback);

	}

	public override void _Process(double delta) {
		base._Process(delta);

		currentPlayback = this.GetPlaybackPosition();

	}

}
