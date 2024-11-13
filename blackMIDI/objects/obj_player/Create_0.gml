/// @description Insert description here
// You can write your code in this editor
walking=false;



walkSpeed=32;

tileWidth=16;

button_left=vk_left;
button_right=vk_right;
button_up=vk_down;
button_down=vk_up;

enum DIRECTIONS{
	LEFT,
	RIGHT,
	UP,
	DOWN
}

dir=DIRECTIONS.DOWN;

x=floor(x/16)*16+8
y=floor(y/16)*16+12

tx=x;
ty=y;