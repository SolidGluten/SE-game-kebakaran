class_name GameManager
extends Node

enum GameState {
    MENU = 0,
    PLAYING = 2,
    DEAD = 4,
    WIN = 8
}

var current_state = GameState.MENU
var is_paused = false

func set_game_state(state: GameState) -> void:
    current_state = state
    match current_state:
        GameState.DEAD:
            Engine.time_scale = 0.0
        GameState.WIN:
            Engine.time_scale = 0.0
        GameState.PLAYING:
            Engine.time_scale = 1.0
        pass
    

