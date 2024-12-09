timer+=delta_time/1000000


if(timer>=stepLength && !sorted){
	switch(sortType){
		case "bubble":
			deactivateNodes();
			if(sortIndex1==0){
				if(swapped==false){
					sorted=true
				}
				swapped=false;
			}
			sortIndex1=bubbleSort(sortIndex1);
			if(sortIndex2!=-1){
				sortIndex2=bubbleSort(sortIndex2);
			}
			if(sortIndex3!=-1){
				sortIndex3=bubbleSort(sortIndex3);
			}
			if(sortIndex4!=-1){
				sortIndex4=bubbleSort(sortIndex4);
			}
			
			if(sortIndex2==-1 && sortIndex1>array_length(arrayA)/4){
				sortIndex2=0
			}
			if(sortIndex3==-1 && sortIndex1>array_length(arrayA)*2/4){
				sortIndex3=0
			}
			if(sortIndex4==-1 && sortIndex1>array_length(arrayA)*3/4){
				sortIndex4=0
			}
			
			break;
		case "bogo":
			deactivateNodes();
			sortIndex1=bogoSort(sortIndex1);
			break;
		case "selection":
			deactivateNodes();
			sortIndex1=selectionSort(sortIndex1);
			break;
	}
	if(stepLength>0) timer=timer%stepLength;
}


if(sorted){
	if(sortedTimer==0){
		instance_destroy(obj_node);
		array_delete(arrayA,0,array_length(arrayA));
	}
	sortedTimer+=delta_time/1000000;
	if(sortedTimer>=3){
		room_goto(room);	
	}
}

if(keyboard_check_pressed(vk_left)){
	stepLength-=0.1	
}
if(keyboard_check_pressed(vk_right)){
	stepLength+=0.1	
}

stepLength=clamp(stepLength,0,10)