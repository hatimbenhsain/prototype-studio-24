/// @description Insert description here
// You can write your code in this editor
targetMaker=obj_follower_mouse;

targetX=66;
targetY=64;

path=[[targetX,y],[targetX,targetY]];

currentTarget=0;
currentTargetX=path[currentTarget][0];
currentTargetY=path[currentTarget][1];

spd=10;

directions=[[1,0],[0,1],[-1,0],[0,-1]]
dir=0;

tone=random_range(-0.05,0.05);