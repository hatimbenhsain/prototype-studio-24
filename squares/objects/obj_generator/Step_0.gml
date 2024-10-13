/// @description Insert description here
// You can write your code in this editor

if(x>obj_game.x){
	dir=2;	
}else{
	dir=0;	
}

if(position_meeting(mouse_x,mouse_y,id)){
	image_blend=c_white;
	if((cooldownTimer)>=1){
		var o=obj_person;
		if(random(1)<=0.5){
			o=obj_bug;	
		}
		var inst=instance_create_depth(x,y,depth,o);
		inst.dir=dir;
	}
	cooldownTimer=0;
}else{
	image_blend=c_grey;
	cooldownTimer+=delta_time/1000000;
}	
