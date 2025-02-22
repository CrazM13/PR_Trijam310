using Godot;
using System;

public partial class ResetButton : BaseButton {

	public override void _Ready() {
		base._Ready();

		this.Pressed += () => {
			GameManager.Instance.CurrentLevel = 0;
		};

	}

}
