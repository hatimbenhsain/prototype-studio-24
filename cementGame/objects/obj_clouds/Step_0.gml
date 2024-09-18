/// @description Insert description here
// You can write your code in this editor
var dt=delta_time/1000000;

offsetx+=imageSpeed*dt;

offsetx=offsetx%sprite_width;

x=camera_get_view_x(view_camera[0])+offsetX;