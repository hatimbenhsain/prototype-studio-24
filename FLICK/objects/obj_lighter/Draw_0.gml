/// @description Insert description here
// You can write your code in this editor

var cx=camera_get_view_x(view_camera[0]);
var cy=camera_get_view_y(view_camera[0]);

xx=x+sin(current_time/1000)*6+cx;
yy=y+cos(current_time/1000)*6+cy;

draw_sprite(sprite_index,image_index,xx,yy);