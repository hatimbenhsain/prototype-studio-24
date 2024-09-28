/// @description Insert description here
// You can write your code in this editor

event_inherited();

var dt=delta_time/1000000

if(flicked && flickedTimer>0){
	flickedTimer-=dt;
}

if(flicked && flickedTimer<=0 && flickedSprite!=-1){
	sprite_index=flickedSprite;	
}