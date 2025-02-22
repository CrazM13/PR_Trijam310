using Godot;
using System;

public partial class PlayerAnimator : AnimatedSprite2D {

	[Export] private Character character;

	private enum Forms {
		NORMAL,
		BAT,
		MIST
	}

	private Forms currentForm;

	public void TransformIntoBat() {
		currentForm = Forms.BAT;
	}

	public void TransformIntoMist() {
		currentForm = Forms.MIST;
	}

	public void TransformIntoVampire() {
		currentForm = Forms.NORMAL;
	}

	private void UpdateAnimation() {
		switch (currentForm) {
			case Forms.NORMAL:
				UpdateNormalAnimation();
				break;
			case Forms.BAT:
				UpdateBatAnimation();
				break;
			case Forms.MIST:
				UpdateMistAnimation();
				break;
		}
	}

	private void UpdateNormalAnimation() {
		AttemptPlay("vampire_running");
	}

	private void UpdateBatAnimation() {
		AttemptPlay("vampire_bat_flying");
	}

	private void UpdateMistAnimation() {
		AttemptPlay("vampire_mist");
	}

	private void AttemptPlay(string animation) {
		if (this.Animation != animation) {
			this.Play(animation);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		UpdateAnimation();

	}
}
