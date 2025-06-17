class_name DeathScreen
extends Node

var menu_path: String = "res://Scenes/Menu/main_menu.tscn"

func restart_level() -> void:
    levelManager.reset_current_scene()

func back_to_menu() -> void:
    levelManager.load_level(menu_path)