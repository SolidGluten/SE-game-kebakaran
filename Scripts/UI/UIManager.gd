class_name UImanager
extends Control

@onready var death_screen: DeathScreen = $DeathScreen
@onready var win_screen: WinScreen = $WinScreen

@onready var fire_axe_img: CanvasItem = $ToolContainer/FireAxeImage
@onready var wet_cloth_img: CanvasItem = $ToolContainer/WetClothImage
@onready var fire_extinguisher_img: CanvasItem = $ToolContainer/FireExtinguisherImage
@onready var roll_timer_slider: Slider = $RollTimerSlider/HSlider

var heart_full_res = "res://Sprites/heart/heart_full.png"
var heart_empty_res = "res://Sprites/heart/heart_empty.png"

var heart_list: Array = []
var current_health: int = 2

func _ready():
	gameManager.set_game_state(GameManager.GameState.PLAYING)

	playerInventory.item_added.connect(self.enable_item_ui)
	playerInventory.item_resetted.connect(self.reset_item_ui)
	playerHealth.health_changed.connect(self.sync_health_ui)
	playerHealth.dead.connect(self.enable_death_screen)

	fire_axe_img.visible = false
	wet_cloth_img.visible = false
	fire_extinguisher_img.visible = false

	death_screen.visible = false
	win_screen.visible = false

	for child in $HealthContainer.get_children():
		if child is TextureRect:
			heart_list.append(child)

	sync_health_ui(playerHealth.current_health)

func _physics_process(_delta: float) -> void:
	if playerHealth.is_rolling:
		roll_timer_slider.visible = true
		var slider_value = (playerHealth.roll_timer/playerHealth.roll_duration) * 100
		change_slider_value(slider_value)
	else:
		roll_timer_slider.visible = false

func change_slider_value(value):
	if roll_timer_slider:
		roll_timer_slider.value = value

func enable_item_ui(item):
	match item:
		playerInventory.ItemTypes.FIRE_AXE:
			fire_axe_img.visible = true
		playerInventory.ItemTypes.WET_CLOTH:
			wet_cloth_img.visible = true
		playerInventory.ItemTypes.FIRE_EXTINGUISHER:
			fire_extinguisher_img.visible = true

func reset_item_ui():
	fire_axe_img.visible = false
	wet_cloth_img.visible = false
	fire_extinguisher_img.visible = false

func sync_health_ui(health):
	current_health = health

	for heart in heart_list:
		heart.texture = load(heart_empty_res)

	for i in range(current_health):
		if i >= heart_list.size():
			break
		heart_list[i].texture = load(heart_full_res)

func _exit_tree():
	if playerInventory:
		playerInventory.disconnect("item_added", Callable(self, "enable_item_ui"))
	if playerHealth:
		playerHealth.disconnect("health_changed", Callable(self, "sync_health_ui"))

func enable_death_screen() -> void:
	death_screen.visible = true
	gameManager.set_game_state(GameManager.GameState.DEAD)

func enable_win_screen(next_level_path: String) -> void:
	win_screen.visible = true
	win_screen.level_path = next_level_path
	gameManager.set_game_state(GameManager.GameState.WIN)
