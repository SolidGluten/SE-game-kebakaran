class_name ExitDoor
extends Interactable

@export var level_path: String
@export var uiManager: UImanager

func interact() -> void:
	if level_path:
		uiManager.enable_win_screen(level_path)
	else:
		printerr("Level path is not set.")
