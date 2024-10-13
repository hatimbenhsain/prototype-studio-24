/// @description Insert description here
// You can write your code in this editor

if(x>obj_game.x){
	dir=2;	
}else{
	dir=0;	
}

if(position_meeting(mouse_x,mouse_y,id)){
	image_blend=c_white;
	active=true;
	if((cooldownTimer)>=cooldownTime){
		instance_destroy(obj_being);
		instance_destroy(obj_wall);
	}
	cooldownTimer=0;
}else{
	image_blend=c_grey;
	cooldownTimer+=delta_time/1000000;
	active=false;
}	
