/// @description Insert description here
// You can write your code in this editor

// Inherit the parent event
if(prevFlicked && !flicked){
	game_end();	
}

event_inherited();

if(flicked){
	layer_background_visible(layer_background_get_id("background_black"),false);	
}

prevFlicked=flicked;