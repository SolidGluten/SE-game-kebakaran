class_name Pickable
extends Area2D

@export var type: playerInventory.ItemTypes

func _ready() -> void:
    body_entered.connect(self._on_body_entered)

func _on_body_entered(_node: Node2D) -> void:
    playerInventory.add_item(type)
    on_pickup()

func on_pickup() -> void:
    queue_free()