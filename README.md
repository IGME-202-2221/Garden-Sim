# Project Garden Sim

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Madeline Boussa
-   Section: 05

## Simulation Design

The player will help manage a small garden simulation! In this top-down perspective game, the player will help place flowers to be pollinated by bees and butterflies. Help the ecosystem out by planting flowers in ideal spots where critters flock, but be wary of beetles, an invasive species which will drive the critters away, and damage the ecosystem.

### Controls  
   -Player can move around the screen (keyboard - WASD)
    
   <br>
   -Player can place flowers at their current position, with cooldown (mouse - left click)
    <br>  -by placing flowers, player can dictate where bees and butterflies flock to, as the critters will seek the placed flowers

## Bee (& Butterfly)

Bee agents will wander around the screen and will seek flowers when in a placed flower's proximity. The bee's goal is to collide with found flowers and "pollinate" them (flowers disappear upon being pollinated), rewarding the player with points. An extra bee is also spawned each time a flower is successfully polinated.

### Aimless Wander

**Objective:**

Bees and butterflies will wander until they "see" a flower or target to pollinate. During the wander state, the critters will just attempt to stay in bounds, separate from one another, and wander the screen in a believable way. There will be no color tint to the sprite during this phase.

#### Steering Behaviors

-Wander()
<br>-StayinBounds - worldSize
<br>-Separate - other agents


- Obstacles - _List all obstacle types this state avoids_


- Seperation - other bees, other butterflies
   
#### State Transistions

- when agent is outside of range of a flower (doesn't "see" any flowers nearby), transition to wander state
   
### Seek Flower

**Objective:** 

When a flower is found (critter falls within a certain area around the flower object), bees & butterflies will seek that flower. The bee will also change to a slight color tint or have a particle effect when actively seeking a flower.

#### Steering Behaviors

-Seek() - flower's position
<br>-StayinBounds - worldSize
<br>-Separate - other agents


- Obstacles - _List all obstacle types this state avoids_


- Seperation - other bees, other butterflies
   
#### State Transistions

- when an agent comes within a range of a flower, transition to seeking state

## Beetles

Hostile beetles will slowly creep toward the player and aim to damage the ecosystem. If a bee comes in contact with a beetle the bee will die, and beetles will also destroy flowers upon collision. Beetles will move slower than bees. Thus the player's goal is to balance the environment by placing flowers in areas bees will flock while also leading the beetles away from the bees.

### Idle

**Objective:** 
When the beetle is first spawned in, it will remain still for a few seconds in order to give the player a headstart/time to plan accordingly. During this state, the beetle will count down and will begin to move when it gets to zero. Beetle will have a red color tint during its stationary phase.

#### Steering Behaviors

- no steering behaviors necessary (remaining still), however, beetle will rotate in direction of player
- Obstacles - no obstacle avoidance necessary during this state
- Seperation - no separation necessary, but beetles will not spawn overlapping one another
   
#### State Transistions

- start at this state upon spawn
   
### Seek Player

**Objective:** 
Beetles will slowly move towards the player's current location with the intention of harming the bees and placed flowers if they so happen to get in the beetle's way. Beetle will have no color tint during this state, as opposed to the red of the idle state. Beetles will die once they collide with a bee/flower.

#### Steering Behaviors

-Seek() - player's position
<br>-StayinBounds - worldSize
<br>-Separate - other beetle agents


- Obstacles - _List all obstacle types this state avoids_


- Seperation - other beetles
   
#### State Transistions

- Once the idle countdown has finished, beetles will transition to the seeking state

## Sources

-   all sprites were created by myself using Adobe Illustrator
-   background image was created by myself using Adobe Illustrator

## Make it Your Own

- original sprites and background created using Adobe Suite
- player "vehicle" (fairy) that takes user input to move around the garden
- player particle effect trail that displays whenever the player moves, created using Unity's built in particle system
- additional agent type called the Butterfly, which has the same functionality as the bee agents just with a quicker speed and more jerky feeling movement. The butterfly is a rarer spawn, and grants the player more points when flowers a pollinated with the butterfly. The butterfly has the same steering behaviors and state functionality as the bee.

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

