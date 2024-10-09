/// @description Insert description here
// You can write your code in this editor
var inst=instance_place(x,y,obj_follower);
if(inst!=noone){
	var otherNode=noone;
	with(obj_node){
		if(maker==inst){
			otherNode=self;
			break;
		}
	}
	if(otherNode==noone || point_distance(x,y,otherNode.x,otherNode.y)<maxD){
		if(otherNode!=noone){
			with(otherNode){
				selected=false;
				maker=noone;	
			}
		}
		maker=inst;
		if(maker.nodeSelected!=noone){
			if(!array_contains(maker.nodeSelected.links,self) &&
			!array_contains(links,maker.nodeSelected)){
				array_push(maker.nodeSelected.links,self);
			}
		}
		selected=true;
		maker.nodeSelected=self;
	}
}