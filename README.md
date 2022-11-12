# Project _NAME_

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Madeline Boussa
-   Section: 05

## Simulation Design

The player will help manage a small garden simulation! In this top-down perspective game, the player will help place flowers to be pollinated by bees and humming birds. Help the ecosystem out by planting flowers in ideal spots where critters flock, but be wary of weeds and invasive plants which will drive the critters away, and damage the ecosystem.

### Controls

-   _List all of the actions the player can have in your simulation_
    -   _Include how to preform each action ( keyboard, mouse, UI Input )_
    -   _Include what impact an action has in the simulation ( if it could be unclear )_
    
    -Player can move around the screen (keyboard - WASD)
    
    <br>
    -Player can place flowers at their current position (mouse - left click)
      <br>  -by placing flowers, player can dictate where bees and hummingbirds flock to, as the critters will seek the placed flowers

## Bee (& Hummingbird)

Bee agent will wander around the screen and will seek flowers when in a placed flower's proximity. The bee's goal is to collide with found flowers and "pollinate" them (flowers disappear upon being pollinated), rewarding the player with points.

### Aimless Wander

**Objective:** _A brief explanation of this state's objective._

Bees and hummingbirds will wander until they "see" a flower or target to pollinate. During the wander state, the critters will just attempt to stay in bounds and wander the screen in a believable way.

#### Steering Behaviors

- _List all behaviors used by this state_
   - _If behavior has input data list it here_
   - _eg, Flee - nearest Agent2_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   - _eg, When this agent gets within range of Agent2_
   - _eg, When this agent has reached target of State2_
   
### Seek Flower

**Objective:** _A brief explanation of this state's objective._

When a flower is found (critter falls within a certain area around the flower object), bees & hummingbirds will seek that flower.

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_

## _Agent 2 Name_

_A brief explanation of this agent._

### _State 1 Name_

**Objective:** _A brief explanation of this state's objective._

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   
### _State 2 Name_

**Objective:** _A brief explanation of this state's objective._

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_

## Make it Your Own

- _List out what you added to your game to make it different for you_
- _If you will add more agents or states make sure to list here and add it to the documention above_
- _If you will add your own assets make sure to list it here and add it to the Sources section

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

