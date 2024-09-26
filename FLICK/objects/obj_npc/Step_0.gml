/// @description Insert description here
// You can write your code in this editor

// Inherit the parent event
event_inherited();

var dt=delta_time/1000000

if(flicked && flickedTimer>0){
	if((flickedTimer%shakeTime)>((flickedTimer+dt)%shakeTime)){
		var flickIntensity=3
		if(flickedTimer<=0.5){
			flickIntensity=1;
		}
		offsetx=random(flickIntensity*2)-flickIntensity;
		offsety=random(flickIntensity*2)-flickIntensity;
	}
	flickedTimer-=dt;
}


