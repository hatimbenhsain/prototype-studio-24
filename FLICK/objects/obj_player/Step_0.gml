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
	if(image_index>=2 && image_index-dt<2){
		with(obj_flickable){
			if(distance_to_object(obj_player)<8 && flickedTimer<=0){
				if((!flicked && (object_index!=obj_npc || !triggered)) || (object_index==obj_npc && !triggered)){
					if(array_length(sounds)>=1){
						var n=floor(random(array_length(sounds)));
						audio_play_sound(sounds[n],gain,false);
					}	
				}
				if(!flicked && object_index!=obj_npc) image_xscale=obj_player.image_xscale;
				if(object_index==obj_button) flicked=!flicked
				else flicked=true;
				if(object_index==obj_npc && !triggered){
					flickedTimer=3;
					sprite_index=spr_npc_beingFlicked;
				}
				if(object_index!=obj_npc || !triggered){
					image_index=0;
				}
				
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

if(abs(vx)+abs(vy)>=maxSpeed*1.5){
	var msx=max(maxSpeed*1.5-abs(vy),maxSpeed*1.5/2)
	var msy=max(maxSpeed*1.5-abs(vx),maxSpeed*1.5/2)
	vx=clamp(vx,-msx,msx);
	vy=clamp(vy,-msy,msy);
}

if(!obj_game.showingDialogue){
	if(abs(vx)>0.5){
		x+=vx;
	}
	if(abs(vy)>0.5){
		y+=vy;
	}
}

if(keyboard_check_pressed(ord("X")) && sprite_index!=spr_player_flick){
	sprite_index=spr_player_flick;
	image_index=0;
}

//camera
var cx=camera_get_view_x(view_camera[0]);
var cy=camera_get_view_y(view_camera[0]);
var tcx=x-camera_get_view_width(view_camera[0])/2;
var tcy=y-camera_get_view_height(view_camera[0])/2;
cx=lerp(cx,tcx,cameraSpeed*dt);
cy=lerp(cy,tcy,cameraSpeed*dt);

camera_set_view_pos(view_camera[0],cx,cy);

if(point_distance(x,y,prevLocationX,prevLocationY)>=setDistance && encounterIndex<array_length(encounters)){
	var dir=degtorad(point_direction(0,0,-vx,-vy)+random(45)-22.5);
	show_debug_message(dir);
	var xx=x-cos(dir)*createDistance;
	var yy=y+sin(dir)*createDistance;
	var canCreate=true;
	var md=minDistance
	with(obj_flickable){
		if(point_distance(xx,yy,x,y)<md){
			canCreate=false
			break;
		}
	}
	if(canCreate){
		array_push(creations,instance_create_depth(xx,yy,depth,encounters[encounterIndex]));
		prevLocationX=xx;
		prevLocationY=yy;
		setDistance=minDistance+random(distanceVar);
		encounterIndex++;
	}
}
if(encounterIndex>=array_length(encounters)){
	generateEncounters();	
	
}