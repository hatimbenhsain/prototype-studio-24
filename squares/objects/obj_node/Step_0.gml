/// @description Insert description here
// You can write your code in this editor
var inst=instance_place(x,y,obj_follower);
if(!selected && inst!=noone){
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
			var wallExists=false;
			var n1=self;
			var n2=maker.nodeSelected;
			with(obj_wall){
				if((node1==n1 && node2==n2) || (node1==n2 && node2==n1)){
					wallExists=true
					instance_destroy(self);
					show_debug_message("wall exists");
					break;
				}
			}
			if(!wallExists){
				var wall=instance_create_depth(x,y,depth+1,obj_wall);
				wall.node1=n1;
				wall.node2=n2;
				wall.col=getColor(n1.x,n1.y,n2.x,n2.y);
				show_debug_message("making wall");
			}
			//if(!array_contains(maker.nodeSelected.links,self) &&
			//!array_contains(links,maker.nodeSelected)){
			//	array_push(maker.nodeSelected.links,self);
			//	array_push(obj_game.walls,
			//	{x1:x,y1:y,x2:maker.nodeSelected.x,y2:maker.nodeSelected.y});
			//}else{
			//	for(var i=0;i<array_length(maker.nodeSelected.links);i++){
			//		if(maker.nodeSelected.links[i]==id){
			//			array_delete(maker.nodeSelected.links,i,1);
			//			break;
			//		}
			//	}
			//	for(var i=0;i<array_length(links);i++){
			//		if(links[i]==maker.nodeSelected){
			//			array_delete(links,i,1);
			//			break;
			//		}
			//	}
			//	for(var i=0;i<array_length(obj_game.walls);i++){
			//		var wall=obj_game.walls[i];
			//		if((wall.x1==x && wall.y1==y 
			//		&& wall.x2==maker.nodeSelected.x && wall.y2==maker.nodeSelected.y) ||
			//		(wall.x2==x && wall.y2==y && wall.x1==maker.nodeSelected.x 
			//		&& wall.y1==maker.nodeSelected.y)){
			//			array_delete(obj_game.walls,i,1);
			//			break;
			//		}
			//	}
			//}
		}
		selected=true;
		maker.nodeSelected=self;
	}
}