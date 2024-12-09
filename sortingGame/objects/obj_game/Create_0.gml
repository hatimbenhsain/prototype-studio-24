randomize()

sortTypes=["selection","bubble","bogo"]

sortType=sortTypes[floor(random(array_length(sortTypes)))]

sorted=false;

dw=display_get_width();
dh=display_get_height();

w=464
h=1000

rw=1920	//reference width
rh=1080	//reference height

resolution=min(dw/rw,dh/rh)

var k=ceil(rw/w)
var ww=rw/k
var ys=[0,(rh-h)/3,(rh-h)*2/3,(rh-h),(rh-h)*2/3,(rh-h)/3]

window_set_size(w*resolution,h*resolution);
if(current_time<5000){
	ini_open("settings.ini")
	var xIndex=ini_read_real("game","xIndex",0)
	var yIndex=ini_read_real("game","yIndex",0)
	//xx=(xx+w)%(rw-w)
	//if(xx<w) xx=0
	//else if(xx>rw-w) xx=rw-w-1
	xIndex+=1
	xIndex=xIndex%k
	yIndex+=1
	yIndex=yIndex%array_length(ys)
	var xx=clamp(ww*xIndex,0,rw-w)
	var yy=ys[yIndex]
	ini_write_real("game","xIndex",xIndex)
	ini_write_real("game","yIndex",yIndex)
	ini_close()
	window_set_position(xx*resolution,yy*resolution);
}

arrayA=[]
arrayB=[]

arrayW=440
arrayH=800

arrayAX=12
arrayAY=12

arrayBX=w/2+arrayAX
arrayBY=arrayAY

arraySize=256

nodeSpace=8
nodeW=32
nodeH=32

function getArrayACoordinates(i){
	var coordinates=getArrayCoordinate(i,nodeW,nodeH,nodeSpace,arrayW,arrayH)
	return[coordinates[0]+arrayAX,coordinates[1]+arrayAY]
}

function getArrayCoordinate(i,nw,nh,ns,aw,ah){
	var xx=floor((i%(aw/(nw+ns))))*(nw+ns)
	var yy=floor(i*(nw+ns)/aw)*(nh+ns)
	return[xx,yy]
}

for(var i=0;i<arraySize;i++){
	node=instance_create_depth(x,y,depth,obj_node)
	array_push(arrayA,node);
	var coordinates=getArrayACoordinates(i)
	node.x=coordinates[0];
	node.y=coordinates[1];
	node.value=floor(random(512))

	//node=instance_create_depth(x,y,depth,obj_node)
	//node.x=xx+arrayBX;
	//node.y=yy+arrayBY;
	//node.value=floor(random(512))
}

sortIndex1=0
sortIndex2=-1
sortIndex3=-1
sortIndex4=-1

function swap(index1,index2){
	node1=arrayA[index1]
	node2=arrayA[index2]
	arrayA[index1]=node2
	arrayA[index2]=node1
	var coordinates1=getArrayACoordinates(index1)
	var coordinates2=getArrayACoordinates(index2)
	node1.x=coordinates2[0]
	node1.y=coordinates2[1]
	node2.x=coordinates1[0]
	node2.y=coordinates1[1]
	swapped=true
}

var sls=[0,0.1,0.2]

stepLength=sls[floor(random(array_length(sls)))]
timer=0
stepNumber=1

activeNodes=[]

function activateNode(node){
	array_push(activeNodes,node)
	node.active=true
}

function deactivateNodes(){
	for(var i=array_length(activeNodes)-1;i>=0;i--){
		activeNodes[i].active=false;
	}
}

swapped=true;

function bubbleSort(index){
	for(var k=0;k<stepNumber;k++){
		if(index<array_length(arrayA)-1){
			activateNode(arrayA[index]);
			if(arrayA[index].value>arrayA[index+1].value){
				swap(index,index+1)	
				activateNode(arrayA[index]);
			}
			index+=1;
			index=index%(array_length(arrayA)-1);
		}
	}	
	return index;
}

function bogoSort(index){
	if(index<array_length(arrayA)-1){
		if(arrayA[index].value>arrayA[index+1].value){
			arrayA=array_shuffle(arrayA);
			for(var i=0;i<array_length(arrayA);i++){
				var coordinates=getArrayACoordinates(i);
				arrayA[i].x=coordinates[0]
				arrayA[i].y=coordinates[1]
			}
			index=0;
		}else{
			index+=1	
		}
		activateNode(arrayA[index]);
	}else{
		sorted=true;	
	}
	return index;
}

function selectionSort(index){
	if(index<array_length(arrayA)-1){
		var minIndex=index;
		var minNumber=arrayA[index];
		for(var i=index;i<array_length(arrayA);i++){
			if(arrayA[i].value<arrayA[minIndex].value){
				minNumber=arrayA[i];
				minIndex=i;
			}
		}
		if(minIndex!=index){
			swap(minIndex,index);
		}
		activateNode(arrayA[minIndex]);
		activateNode(arrayA[index]);
		return index+1;
	}else{
		sorted=true;	
	}
}

sortedTimer=0;