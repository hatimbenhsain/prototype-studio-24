/// @description Insert description here
// You can write your code in this editor

// Inherit the parent event

var dt=delta_time/1000000

if((dir>=1 && v[0]<dir*maxSpeed) || (dir<=-1 && v[0]>dir*maxSpeed)){
	v[0]+=dir*dt*acceleration;
}

event_inherited();

