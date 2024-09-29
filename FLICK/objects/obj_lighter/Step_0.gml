/// @description Insert description here
// You can write your code in this editor
dt=delta_time/1000000;

if(vx!=0) vx-=sign(vx)*decceleration*dt;
if(vy!=0) vy-=sign(vy)*decceleration*dt;

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

if(!obj_game.showingDialogue){
	vy+=acceleration*dt;	
	if(y>=512+50){
		instance_destroy(self);
	}
}

if(keyboard_check_pressed(ord("X"))){
	attempts+=1;
	if(attempts<=10){
		sprite_index=spr_lighter_tryingToLight;	
	}else{
		if(random(1)<0.25){
			sprite_index=spr_lighter_lit;
			lit=true;
		}else{
			sprite_index=spr_lighter_tryingToLight;	
		}
	}
}
if(!keyboard_check(ord("X"))){
	sprite_index=spr_lighter;
	lit=false;
}

if(!obj_game.burning && lit && place_meeting(xx,yy,obj_game)){
	burningTime+=dt;
	if(burningTime>=2){
		obj_game.burning=true;	
		if(obj_game.tempText!="WAUUUUUGHHHHHHHHHHHH"){
			obj_game.charIndex=0;
		}
		obj_game.tempText="WAUUUUUGHHHHHHHHHHHH"
	}
}else if(!obj_game.burning){
	if(lit){
		obj_game.tempText="Hey. Hey! Get that away from me!!! That's dangerous!"
	}else{
		if(obj_game.tempText!=""){
			obj_game.charIndex=0;	
		}
		obj_game.tempText="";
	}
	burningTime-=dt;	
}
burningTime=clamp(burningTime,0,5);

vx=clamp(vx,-maxSpeed,maxSpeed);
vy=clamp(vy,-maxSpeed,maxSpeed);

if(abs(vx)+abs(vy)>=maxSpeed*1.5){
	var msx=max(maxSpeed*1.5-abs(vy),maxSpeed*1.5/2)
	var msy=max(maxSpeed*1.5-abs(vx),maxSpeed*1.5/2)
	vx=clamp(vx,-msx,msx);
	vy=clamp(vy,-msy,msy);
}


if(abs(vx)>0.5){
	x+=vx*dt;
}
if(abs(vy)>0.5){
	y+=vy*dt;
}

x=clamp(x,200,374+32)
y=clamp(y,-50,512+50)