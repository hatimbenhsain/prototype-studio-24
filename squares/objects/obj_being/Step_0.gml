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
var canMove=true;

x+=spd*directions[dir][0]*dt;
y+=spd*directions[dir][1]*dt;

var person=id;

with(obj_wall){
	if(person==collision_line(node1.x,node1.y,node2.x,node2.y,person,false,false)){
		with(person){
			var currentDir=dir;
			var k=0;
			while(k<100 && currentDir==dir){
				dir=floor(random(4));	
			}
			canMove=false;
			x-=directions[currentDir][0];
			y-=directions[currentDir][1];
			var ghost=instance_create_depth(x,y,depth+1,obj_ghost);
			ghost.sprite_index=sprite_index;
		}
		collided=true;
		collisionObject=person.object_index;
		break;
	}
}

if(//x<2 || x>camera_get_view_width(view_camera[0]) || 
	y<2 || y>camera_get_view_height(view_camera[0])){
	x-=directions[dir][0];
	y-=directions[dir][1];
	dir=(dir+2)%4;
}

if(x<-5 || x>camera_get_view_width(view_camera[0])+5){
	instance_destroy(self);
}


//if(random(1)<0.01){
//	dir=floor(random(4));	
//}