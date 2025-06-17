extends Node2D

@export var ray_cast_length: float = 25.0

@onready var shape_cast: ShapeCast2D = $ShapeCast2D
var player_movement

func _ready():
	player_movement = get_parent()

func _process(_delta):
	var new_target_pos = Vector2(ray_cast_length * (1.0 if player_movement.is_facing_right else 0.0), 0)
	shape_cast.target_position = new_target_pos

	if Input.is_action_just_pressed("interact") and shape_cast.is_colliding():
		var collision_count = shape_cast.get_collision_count()
		for i in range(collision_count):
			var collider = shape_cast.get_collider(i)
			handle_collision(collider)

func handle_collision(collider):
	print("Collided with: ", collider.name)

	if collider is Interactable:
		collider.interact()

	match playerInventory.get_current_item():
		playerInventory.ItemTypes.FIRE_AXE:
			if collider is Breakable:
				collider.destroy()
			print("Using Fire Axe")
		playerInventory.ItemTypes.FIRE_EXTINGUISHER:
			if collider is Extinguishable:
				collider.extinguish()
			print("Using Fire Extinguisher")
		playerInventory.ItemTypes.WET_CLOTH:
			print("Using Wet Cloth")
		_:
			pass
