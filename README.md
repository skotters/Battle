<h1 align ="center"> Battle for a pastry...<br> against lorem ipsum monsters</h1>

## Features included
* Implement a "master loop" console application
* Create a dictionary or list, populate it with several values, retrieve at least one value, and use in program
* Create an additional class which inherits one or more properties from its parent
* Read data from an external file and use that data in your application
* Connect to an external/3rd party API and read data into your app

## To Run Program

### On Windows
1) Clone repo
2) Run solution in visual studio or, 
3) Navigate to 2nd Battle folder in repo with `cd Battle` and `dotnet run --project Battle.csproj` with command line or Powershell.

### On Mac
1) Clone repo
2) Run solution in visual studio or, 
3) Navigate to 2nd Battle folder in repo with `cd Battle` and `dotnet run --project Battle.csproj` with terminal.

## Additional note for running program
Whether the program is being run directly in Visual Studio, VS Code, Mac terminal, or Powershell, the viewable console area/window will need to be resized to allow at least 27 rows to view all material. 

<h1></h1>

It is possible a monster may randomly be assigned the 
name "Error" which is an actual word that pulls from 
the placeholder API and is not an actual error in 
the monster name.

________________________________________________________
## Summary
This Battle project is a game. For those who may be
unfamiliar with the style, it borrows some elements
from common role-playing-games (RPGs).

The concept is simple - you take a turn, perform an 
action, and then the enemy takes a turn and attacks you.
That process repeats over and over until either you
or the opponent wins the fight. 

**HP** is short for "hit points" and is a number that 
represents the amount of health you have. When it hits
zero, you're dead. Same thing goes for the enemy.

**MP** is short for "magic points" (or mana points) and is
a number that represents the "currency" required to
spend on casting spells. Casting a fireball against
the enemy, for example, costs 6MP.

### Battle Abilities
* Attack - standard attack that does 5 to 8 pts of dmg.
* Strong attack - attack that does 10 to 18 pts of dmg.
    * has 50% chance to miss.
* Magic - open the magic menu
    * Fireball - uses 6MP and does 15 to 20 dmg.
    * Arcane Missiles - uses 10MP and attempts (3) magic magic attacks each between 12 to 18 pts of dmg. Each missile has a 50% of success. Potential max dmg is 54!
    * Heal - uses 8MP and heal yourself for 20HP.
* Item - open the item menu (these are purchased in the store)
    * Health Potion - heals the player for 25HP.
    * Magic Potion - restores 25MP to the player.
    * Antidote - removes poison status from the player.
    * Sword - passive item that adds +2 dmg to non-magic attacks.
    * Armor - reduces all incoming dmg by 2.
* Run Away
    * You have a 15% chance to bail out of the fight and
    head straight to the bakery. Each failed attempt will 
    result in the enemy simply taking their turn and attacking you.
    
### Status effects
There are three states the player can be in:
* Normal
* Poisoned - you take 5 dmg every turn until fight is over or until an antidote is used
* Confused - you have a 50% chance to hit yourself instead of the enemy when attacking!

### V monsters
Monsters who have a name starting with "V" are extra powerful and start with 20 additional HP!




