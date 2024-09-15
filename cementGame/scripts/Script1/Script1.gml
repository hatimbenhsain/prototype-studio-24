// Script assets have changed for v2.3.0 see
// https://help.yoyogames.com/hc/en-us/articles/360005277377 for more information
function Normalize(v){
	var len=Magnitude(v);
	var vn
	if(len==0){
		vn=[0,0];	
	}else{
		vn=[v[0]/len,v[1]/len];
	} 
	return vn;
}

function Magnitude(v){
	return sqrt(sqr(v[0])+sqr(v[1]));
}	