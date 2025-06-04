class_name HealthPack
extends Pickable

func on_pickup() -> void:
    playerHealth.heal(1)
    queue_free()