/// @description Insert description here
// You can write your code in this editor
if(!walking){
	if(keyboard_check(button_left)){
		dir=directions.LEFT;
		walking=true;
		tx=x-tileWidth;
		ty=y;
	}else if(keyboard_check(button_down)){
		dir=directions.DOWN;
		walking=true;
		tx=x;
		ty=y-tileWidth;
	}else if(keyboard_check(button_right)){
		dir=directions.RIGHT;
		walking=true;
		tx=x+tileWidth;
		ty=y;
	}else if(keyboard_check(button_up)){
		dir=directions.UP;
		walking=true;
		tx=x;
		ty=y+tileWidth;
	}
}

if(walking){
	switch(dir){
		case directions.LEFT:
			x=x-walkSpeed*delta_time/1000000;
			break;
		case directions.RIGHT:
			x=x+walkSpeed*delta_time/1000000;
			break;
		case directions.DOWN:
			y=y-walkSpeed*delta_time/1000000;
			break;
		case directions.UP:
			y=y+walkSpeed*delta_time/1000000;
			break;
	}
	if(abs(x-tx)<=1 && abs(y-ty)<=1){
		x=tx;
		y=ty;
		walking=false;
	}
}