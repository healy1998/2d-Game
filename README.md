# 2d-Game
Final Year Project
Instructions for playing the game:
Click the left mouse button to fire and use the wasd or arrow keys to move and spacebar to jump.
The game should start on a game over screen or just on the level itself.
If it starts on the game over screen that is intentional for the sake of taking values from it and adding it to the leader board.
Click main menu and then play if you start on the game over screen.

There were a few complications in certain parts of the game where the player couldn't perform an action or a texture wouldn't update correctly:
First the player unfortunately can't jump on platforms. This is because the player character knows it can jump on any surface with a tag called "ground".
If I give the platforms this tag the player can still jump on the platforms but if they touch the platforms while ascending from a previous jump they will jump again which is a bit disorienting for the player.
It's for that reason I opted to not allow the player to jump on platforms.
The second issue was with the health graphic. When the player loses enough health to lose a life the health bar remains empty until the player gets hit again.
After the player takes damage the health bar decreases as normal.
The last thing is if the character isn't moving or jumping immediately when you start the game you will have to restart unity. 
This is a common issue in unity and doesn't have a resolution outside of restarting.

The game doesn't technically end, it's more about overcoming the challenges that are presented to you and gaining as many points as possible.
Open the chest at the end to restart the level with your current number of points to gain more and rise through the leaderboard.
Enjoy!!!
