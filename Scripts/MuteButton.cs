using Godot;
using System;

public partial class MuteButton : BaseButton {

	[Export] private string bus = "Master";

	public override void _Ready() {
		base._Ready();

		this.Toggled += this.OnToggle;

	}

	private void OnToggle(bool toggledOn) {
		AudioServer.SetBusMute(AudioServer.GetBusIndex(bus), toggledOn);
	}
}
