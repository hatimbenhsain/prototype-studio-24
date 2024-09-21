/// @description Insert description here
// You can write your code in this editor
var v=97+70*(obj_game.speedValue);
var color=make_color_rgb(v,v,v);

draw_set_color(color);
draw_rectangle(camera_get_view_x(view_camera[0]),camera_get_view_y(view_camera[0]),
camera_get_view_x(view_camera[0])+512,camera_get_view_y(view_camera[0])+512,false);
draw_set_color(c_white);
