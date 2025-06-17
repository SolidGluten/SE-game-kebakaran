class_name PlayerAnimation
extends AnimatedSprite2D

enum playerState {
	IDLE,
	WALK,
	JUMP,
	ROLL
}

@export var current_state: playerState = playerState.IDLE
@onready var on_fire_anim: AnimatedSprite2D = $"../Fire"

func _physics_process(_delta: float) -> void:

	if playerHealth.is_on_fire and not playerHealth.is_rolling:
		on_fire_anim.visible = true
	else:
		on_fire_anim.visible = false

	if playerHealth.is_rolling:
		set_state(playerState.ROLL)
		duck_roll_anim()
		return

	match current_state:
		playerState.IDLE:
			play_idle_anim()
		playerState.WALK:
			play_walk_anim()
		playerState.JUMP:
			play_jump_anim()
			
func set_state(new_state: playerState) -> void:
	if current_state != new_state:
		current_state = new_state

func duck_roll_anim():
	play("duck_roll")

func play_walk_anim():
	var item = playerInventory.get_current_item()
	match item:
		playerInventory.ItemTypes.FIRE_AXE:
			play("walk_axe")
		playerInventory.ItemTypes.WET_CLOTH:
			play("walk_cloth")
		playerInventory.ItemTypes.FIRE_EXTINGUISHER:
			play("walk_exting")
		_:
			play("walk")

func play_idle_anim():
	var item = playerInventory.get_current_item()
	match item:
		playerInventory.ItemTypes.FIRE_AXE:
			play("idle_axe")
		playerInventory.ItemTypes.WET_CLOTH:
			play("idle_cloth")
		playerInventory.ItemTypes.FIRE_EXTINGUISHER:
			play("idle_exting")
		_:
			play("idle")

func play_jump_anim():
	var item = playerInventory.get_current_item()
	match item:
		playerInventory.ItemTypes.FIRE_AXE:
			play("jump_axe")
		playerInventory.ItemTypes.WET_CLOTH:
			play("jump_cloth")
		playerInventory.ItemTypes.FIRE_EXTINGUISHER:
			play("jump_exting")
		_:
			play("jump")
