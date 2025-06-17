class_name WinScreen
extends Node

@export var level_path: String = ""

func next_level() -> void:
	if level_path: 
		levelManager.load_level(level_path)

func restart() -> void:
	levelManager.reset_current_scene()
