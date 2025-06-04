class_name ExitDoor
extends Area2D

@export var level_path: String

func _ready() -> void:
    body_entered.connect(self.next_level)

func next_level(body: Node2D) -> void:
    if body.is_in_group("player"):
        levelManager.next_level(level_path)
