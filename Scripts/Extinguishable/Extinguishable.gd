class_name Extinguishable
extends Node

func extinguish() -> void:
    queue_free()
    on_extinguish()

func on_extinguish() -> void:
    pass
