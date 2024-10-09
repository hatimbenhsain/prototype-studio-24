/// @description Insert description here
// You can write your code in this editor

var dt=delta_time/1000000

var prevX=x;
var prevY=y;

//if(abs(x-currentTargetX)>=1 || abs(y-currentTargetY)>=1){
//	x=x+spd*sign(currentTargetX-x)*dt;
//	y=y+spd*sign(currentTargetY-y)*dt;
	
//	if(sign(currentTargetX-x)!=sign(currentTargetX-prevX)){
//		x=currentTargetX;
//	}
//	if(sign(currentTargetY-y)!=sign(currentTargetY-prevY)){
//		y=currentTargetY;
//	}
//}else{
//	currentTarget+=1;
//	if(currentTarget<array_length(path)){
//		currentTargetX=path[currentTarget][0];
//		currentTargetY=path[currentTarget][1];	
//	}
//}

x+=spd*directions[dir][0]*dt;
y+=spd*directions[dir][1]*dt;

if(random(1)<0.01){
	dir=floor(random(5));	
}