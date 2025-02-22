using Godot;
using System;

public partial class TimerProgressBar : TextureProgressBar {

	[Export] private Timer timer;

	public override void _Process(double delta) {
		base._Process(delta);

		if (timer.IsStopped()) {
			this.Value = 0;
		} else {
			this.Value = timer.TimeLeft / timer.WaitTime;
		}

	}

}
