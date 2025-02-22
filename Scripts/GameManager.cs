using Godot;
using System;

public class GameManager  {

	#region Singleton
	private static GameManager instance;
	public static GameManager Instance {
		get {
			instance ??= new GameManager();
			
			return instance;
		}
	}

	private GameManager() { /* MT */ }
	#endregion

	public int CurrentLevel { get; set; } = 0;
	public const int MAX_LEVELS = 3;

}
