/// @description Insert description here
// You can write your code in this editor

var prevWalking=walking;

if(activated){
	if(game!=-1 && !game.combatMode){
		if(!walking){
			if(keyboard_check(button_left)){
				dir=DIRECTIONS.LEFT;
				walking=true;
				tx=x-tileWidth;
				ty=y;
				sprite_index=spr_player_walking_left;
			}else if(keyboard_check(button_down)){
				dir=DIRECTIONS.DOWN;
				walking=true;
				tx=x;
				ty=y-tileWidth;
				sprite_index=spr_player_walking_up;
			}else if(keyboard_check(button_right)){
				dir=DIRECTIONS.RIGHT;
				walking=true;
				tx=x+tileWidth;
				ty=y;
				sprite_index=spr_player_walking_right;
			}else if(keyboard_check(button_up)){
				dir=DIRECTIONS.UP;
				walking=true;
				tx=x;
				ty=y+tileWidth;
				sprite_index=spr_player_walking_down;
			}
			
			if(walking && !prevWalking){
				var xx=tx;
				var yy=ty;
				var canWalk=true;
				with(obj_player){
					if(abs(xx-x)<=5 && abs(yy-y)<=5){
						canWalk=false;	
						break;
					}
				}
				if(!canWalk){
					walking=false;	
				}
			}
		}

		if(walking){
			switch(dir){
				case DIRECTIONS.LEFT:
					x=x-walkSpeed*delta_time/1000000;
					if(x<=tx){
						x=tx;	
					}
					break;
				case DIRECTIONS.RIGHT:
					x=x+walkSpeed*delta_time/1000000;
					if(x>=tx){
						x=tx;	
					}
					break;
				case DIRECTIONS.DOWN:
					y=y-walkSpeed*delta_time/1000000;
					if(y<=ty){
						y=ty;	
					}
					break;
				case DIRECTIONS.UP:
					y=y+walkSpeed*delta_time/1000000;
					if(y>=ty){
						y=ty;	
					}
					break;
			}
			if(abs(x-tx)<=1 && abs(y-ty)<=1){
				x=tx;
				y=ty;
				walking=false;
				if(random(1)<=encounterRate){
					game.combatMode=true;	
				}
			}
		}else{
			image_index=1;	
		}
	}else{
		image_index=1;		
	}

	if(game!=-1 && game.combatMode){
		if(attacking){
			if(attackTimer==0 && game.battlePlayer!=-1){
				game.battlePlayer.sprite_index=spr_player_battle_attack;
				game.battlePlayer.image_index=0;
			}
			var prevAttackTimer=attackTimer;
			attackTimer+=delta_time/1000000;
			
			if(keyboard_check_pressed(button_interact) &&
			game.battlePlayer!=-1 && game.battlePlayer.sprite_index==spr_player_battle_attack 
			&& game.battlePlayer.image_index>=4 
			&& game.battlePlayer.image_index<=6){
				doubleAttack=true;
			}
			if(attackTimer>=preAttackTime && prevAttackTimer<preAttackTime){
				game.monster.hp-=atk;
				if(doubleAttack){
					game.monster.hp-=atk;
				}
				audio_play_sound(snd_hurt2,1,false,0.1);
				game.monster.hp=clamp(game.monster.hp,0,game.monster.maxHP);
				game.battlePlayer.sprite_index=spr_player_battle_unattack;
				game.battlePlayer.image_index=0;
			}
			if(attackTimer>=preAttackTime+postAttackTime){
				attacking=false;
				attackTimer=0;
				if(game.monster!=-1 && game.monster.hp>0) game.monster.attacking=true;
			}
		}
		if(game.battlePlayer!=-1 && game.battlePlayer.sprite_index==spr_player_battle_unattack && game.battlePlayer.image_index>=4){
			game.battlePlayer.sprite_index=spr_player_battle;
			game.battlePlayer.image_index=0;	
		}
	}
}else if(game!=-1){
	if(keyboard_check_pressed(button_down)	|| keyboard_check_pressed(button_left) || 
	keyboard_check_pressed(button_up) || keyboard_check_pressed(button_right)){
		activated=true;
	}
}

if(hp<=0){
	instance_destroy(game.monster);
	instance_destroy(game);
	instance_destroy(self);
}