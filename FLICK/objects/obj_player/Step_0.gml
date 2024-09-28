/// @description Insert description here
// You can write your code in this editor
event_inherited();

var moving=false;

var dt=delta_time/1000000;

if(vx!=0) vx-=sign(vx)*decceleration*dt;
if(vy!=0) vy-=sign(vy)*decceleration*dt;

if(sprite_index!=spr_player_flick){
	if(keyboard_check(vk_left)){
		vx-=acceleration*dt;	
		moving=true;
		image_xscale=-1;
	}
	if(keyboard_check(vk_right)){
		vx+=acceleration*dt;	
		moving=true;
		image_xscale=1;
	}
	if(keyboard_check(vk_up)){
		vy-=acceleration*dt;	
		moving=true;
	}
	if(keyboard_check(vk_down)){
		vy+=acceleration*dt;	
		moving=true;
	}
}

if(sprite_index==spr_player_flick && image_index<=sprite_get_number(spr_player_flick)-1){
	if(image_index>=2){
		with(obj_flickable){
			if(distance_to_object(obj_player)<64 && flickedTimer<=0){
				flicked=true;
				if(object_index==obj_npc){
					flickedTimer=3;
					sprite_index=spr_npc_beingFlicked;
				}
				image_index=0;
				image_xscale=obj_player.image_xscale;
			}
		}	
	}
}else if(sprite_index==spr_player_flick){
	sprite_index=spr_player;
}else if(moving){
	sprite_index=spr_player_walk;	
}else{
	sprite_index=spr_player;	
}


vx=clamp(vx,-maxSpeed,maxSpeed);
vy=clamp(vy,-maxSpeed,maxSpeed);

if(abs(vx)>0.1){
	x+=vx;
}
if(abs(vy)>0.1){
	y+=vy;
}

if(keyboard_check_pressed(ord("X"))){
	sprite_index=spr_player_flick;
	image_index=0;
}