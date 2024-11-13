/// @description Insert description here
// You can write your code in this editor
if !surface_exists(surf)
{
    surf = surface_create(240,160);
}

surface_copy(surf, -player.x+w/2, -player.y+h/2, application_surface);