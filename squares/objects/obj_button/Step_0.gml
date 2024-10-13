/// @description Insert description here
// You can write your code in this editor

if(x>obj_game.x){
	dir=2;	
}else{
	dir=0;	
}

if(position_meeting(mouse_x,mouse_y,id)){
	if(cooldownTimer>=cooldownTime){
		active=!active;
	}
	cooldownTimer=0;
}else{
	cooldownTimer+=delta_time/1000000;
}	

if(active){
	image_blend=c_white;
}else{
	image_blend=c_grey;
}