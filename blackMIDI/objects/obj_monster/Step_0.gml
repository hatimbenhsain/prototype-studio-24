/// @description Insert description here
// You can write your code in this editor
if(game!=-1 && game.combatMode){
	if(attacking){
		var prevAttackTimer=attackTimer;
		attackTimer+=delta_time/1000000;
		if(attackTimer>=preAttackTime && prevAttackTimer<preAttackTime){
			game.player.hp-=atk;
		}
		if(attackTimer>=preAttackTime+postAttackTime && prevAttackTimer<preAttackTime+postAttackTime){
			attacking=false;
			attackTimer=0;
			game.inMenu=true;
		}
	}
}