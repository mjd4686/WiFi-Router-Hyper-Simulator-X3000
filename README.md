# WiFi-Router-Hyper-Simulator-X3000
CS590G Final Project


# Responsibilities for each group member
## Jerry Fu
Initial game vision and UI direction, Scoring calculations and implementation, leaderboard setup, rendering and hookups, scoreboard display, inputting and hookups (scoreboard using playerprefs) to save across the session for the user

## Mark Dow
Repository setup (including GitHub plugins for collaboration), FPS controller setup and initial raycast and initial clock setup, level design, level creation (both level 1 and level 2 including asset placement, router placement and material design), initial main menu hookups and creation

## Jacob Calkins
Art design and direction, game mechanics implementation (Health system, raycast shooting, particle effects, HUD design, all sprite design, 3D modelling of the routers, new game timer, difficulty levels), sound design (all music and sound effect compositions are original, composed, recorded, edited) new main menu hookups (created settings, levels, info pages) and main menu design, game music controls, special abilities design, post processing graphics effects on the player camera

# Notes
## Pausing
Intentionally omitted due to the short, fast paced levels. It allowed the user to take a breath and perform noticeably better rather than being drowned in the chaos of the sound design, music and heavy post-processing effects (blur, shallow depth of field, film grain to enhance the difficulty and add to the beauty).


## Main Theme
The main song was written to be inspired by black metal and death metal, albeit in 8 bit to fit the voxel/low poly theme

## Sound Effects
Sound effects were recorded by Jake using household items:
Gun cocking: rubbing a spoon on a colander
Lasers, jumping noises, cooldown and warp noises: Jake making weird noises with his mouth
Destruction sounds: hitting an old VCR with a 10 lb sledgehammer then dropping it out of a 2 story building

## Bugs we couldn’t squash in time
There is a Unity audio bug that prevents the music switcher from working, but theoretically you can add as many songs as you want and rotate through it
The same audio bug prevented the original game start/ending noises from using a coroutine to play back-to-back. It wasn’t deemed important enough to consider the lengthy workaround worth it.
Lens distortion effect could not be altered in script, but since it’s depreciated that will likely never be fixed. This was replaced with the static “warp speed” graphic on the Dash ability.
