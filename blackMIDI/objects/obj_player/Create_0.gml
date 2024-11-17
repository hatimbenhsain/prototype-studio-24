/// @description Insert description here
// You can write your code in this editor
walking=false;



walkSpeed=32;

tileWidth=16;

button_left=vk_left;
button_right=vk_right;
button_up=vk_down;
button_down=vk_up;
button_interact=ord("X");

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

game=-1;

encounterRate=0.5;

hp=999;
maxHP=hp;
atk=4;

attacking=false;
attackTimer=0;
preAttackTime=1;
postAttackTime=1;

activated=false;

doubleAttack=false;