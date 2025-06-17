extends Control

@onready var fire_axe_img: CanvasItem = $ToolContainer/FireAxeImage
@onready var wet_cloth_img: CanvasItem = $ToolContainer/WetClothImage
@onready var fire_extinguisher_img: CanvasItem = $ToolContainer/FireExtinguisherImage
@onready var roll_timer_slider: Slider = $RollTimerSlider/HSlider

var heart_full_res = "res://Sprites/heart/heart_full.png"
var heart_empty_res = "res://Sprites/heart/heart_empty.png"

var heart_list: Array = []
var current_health: int = 2

func _ready():
	playerInventory.connect("item_added", Callable(self, "enable_item_ui"))
	playerHealth.connect("health_changed", Callable(self, "sync_health_ui"))

	fire_axe_img.visible = false
	wet_cloth_img.visible = false
	fire_extinguisher_img.visible = false

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

func enable_item_ui(item):
	match item:
		playerInventory.ItemTypes.FIRE_AXE:
			fire_axe_img.visible = true
		playerInventory.ItemTypes.WET_CLOTH:
			wet_cloth_img.visible = true
		playerInventory.ItemTypes.FIRE_EXTINGUISHER:
			fire_extinguisher_img.visible = true

func change_slider_value(value):
	if roll_timer_slider:
		roll_timer_slider.value = value

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
