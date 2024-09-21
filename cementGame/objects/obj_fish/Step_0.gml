/// @description Insert description here
// You can write your code in this editor

// Inherit the parent event

if(obj_player.currentState==STATES.SWIMMING){
	speedValue=obj_game.speedValue;
	maxImgSpeed=speedValue

	var dt=delta_time/1000000;

	if((dir>=1 && v[0]<dir*maxSpeed) || (dir<=-1 && v[0]>dir*maxSpeed)){
		v[0]+=dir*dt*acceleration*speedValue;
	}
}

event_inherited();
