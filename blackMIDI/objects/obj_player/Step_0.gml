/// @description Insert description here
// You can write your code in this editor
if(!walking){
	if(keyboard_check(button_left)){
		dir=DIRECTIONS.LEFT;
		walking=true;
		tx=x-tileWidth;
		ty=y;
	}else if(keyboard_check(button_down)){
		dir=DIRECTIONS.DOWN;
		walking=true;
		tx=x;
		ty=y-tileWidth;
	}else if(keyboard_check(button_right)){
		dir=DIRECTIONS.RIGHT;
		walking=true;
		tx=x+tileWidth;
		ty=y;
	}else if(keyboard_check(button_up)){
		dir=DIRECTIONS.UP;
		walking=true;
		tx=x;
		ty=y+tileWidth;
	}
}

if(walking){
	switch(dir){
		case DIRECTIONS.LEFT:
			x=x-walkSpeed*delta_time/1000000;
			if(x<=tx){
				x=tx;	
			}
			break;
		case DIRECTIONS.RIGHT:
			x=x+walkSpeed*delta_time/1000000;
			if(x>=tx){
				x=tx;	
			}
			break;
		case DIRECTIONS.DOWN:
			y=y-walkSpeed*delta_time/1000000;
			if(y<=ty){
				y=ty;	
			}
			break;
		case DIRECTIONS.UP:
			y=y+walkSpeed*delta_time/1000000;
			if(y>=ty){
				y=ty;	
			}
			break;
	}
	if(abs(x-tx)<=1 && abs(y-ty)<=1){
		x=tx;
		y=ty;
		walking=false;
	}
}