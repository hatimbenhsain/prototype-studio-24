/// @description Insert description here
// You can write your code in this editor

// Inherit the parent event
event_inherited();

var dt=delta_time/1000000

if(!prevFlicked && flicked){
	obj_game.npcsFlicked+=1;	
}

if(!triggered){
	if(flicked && flickedTimer>0){
		if(obj_game.npcsFlicked==3){
			if(sprite_index!=spr_npc_flickedAngry){
				sprite_index=spr_npc_flickedAngry;
				image_index=0;
			}else if(image_index>=10){
				depth=-10000;	
				if(image_index>=13){
					triggered=true;
					obj_game.showingDialogue=true;
					flickedTimer=0;
				}
			}
		}else if((flickedTimer%shakeTime)>((flickedTimer+dt)%shakeTime)){
			var flickIntensity=3
			if(flickedTimer<=0.5){
				flickIntensity=1;
			}
			offsetx=random(flickIntensity*2)-flickIntensity;
			offsety=random(flickIntensity*2)-flickIntensity;
		}
	}
}else{
	flicked=false;
	sprite_index=spr_npc_flickedAngry_1;	
	depth=-100000;
	var cx=camera_get_view_x(view_camera[0])
	var cy=camera_get_view_y(view_camera[0]);
	x=cx+186;
	y=cy+284+yJump;
	image_speed=1;
	if(instance_exists(obj_lighter) && obj_lighter.lit){
		sprite_index=spr_npc_flickedAngry_2;
		var s=1;
		var d=point_distance(cx+137,cy+326,obj_lighter.xx,obj_lighter.yy);
		s=1+clamp(1-(d-100)/100,0,1);
		image_speed=s;
	}
	if(obj_game.burning){
		yJump-=dt*1200;
		if(yJump<-400)instance_destroy(self);
	}
}



prevFlicked=flicked;
