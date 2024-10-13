/// @description Insert description here
// You can write your code in this editor
if(collided){
	if(collidedTimer==0){
		audio_play_sound(obj_game.wallNotes[progress],1,false);
		col=obj_game.wallColors[progress];
		progress++;
		if(progress>=array_length(obj_game.wallColors)){
			instance_destroy(self);
		}
	}else if(collidedTimer>=collidedTime){
		collided=false;
	}
	collidedTimer+=delta_time/1000000
}else{
	collidedTimer=0;	
}

prevCollided=collided;