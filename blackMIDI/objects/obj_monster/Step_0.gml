/// @description Insert description here
// You can write your code in this editor
hp=clamp(hp,0,maxHP);

if(game!=-1 && game.combatMode){
	offsetx=0;
	if(attacking){
		var prevAttackTimer=attackTimer;
		attackTimer+=delta_time/1000000;
		if(attackTimer<=preAttackTime){
			offsetx=-4*(attackTimer/preAttackTime);
			offsetx=clamp(-4*(((attackTimer))/(0.1)),-4,0);
		}else{
			offsetx=clamp(-4*((0.1-(attackTimer-preAttackTime))/(0.1)),-4,0);
		}
		if(keyboard_check_pressed(game.player.button_interact) && attackTimer>delta_time/1000000
		&& attackTimer>=preAttackTime/2 && attackTimer<preAttackTime){
			halvedAttack=true;
		}
		if(attackTimer>=preAttackTime && prevAttackTimer<preAttackTime){
			var k=1;
			if(halvedAttack) k=.5;
			game.player.hp-=atk*k;
			audio_play_sound(snd_hurt1,1,false,0.1);
		}
		if(attackTimer>=preAttackTime+postAttackTime && prevAttackTimer<preAttackTime+postAttackTime){
			attacking=false;
			attackTimer=0;
			game.inMenu=true;
			offsetx=-4*((attackTimer-preAttackTime)/postAttackTime);
			halvedAttack=false;
		}
	}
}

image_index=base*3+(image_index%3);

if(hp<=0){
	image_index=base*3;	
}

screenHP=lerp(screenHP,hp,15*delta_time/1000000);
if(screenHP<=0.5){
	screenHP=0;	
}