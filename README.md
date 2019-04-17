# ArenaGame
-------------
Arena is a multi-player fighting game inspired by Super Smash Series and implemented with Unity3D game engine.

### Demo videos: ###

Map Skydome: https://youtu.be/2asCLoekFyE

Map Tavern:  https://youtu.be/1op365c7Qt8

## Getting Started ##

### Prerequisites ###
Download Unity version 2018.3.2 from 
```
https://unity3d.com/get-unity/download/archive
```
### Installing ###
- First clone the repository to local with 

	```
	git clone https://github.com/karthshen/ArenaGame
	```
- Load the project in Unity, and open `Start.unity` under `Assets/Scenes/`

### Running the tests ###
The tests are written under the Unity Test Runner framework. The test files can be found under `Assets/Scripts/tests`. Currently most of the test cases are not passing due to a recent upgrade of InControl

### Deployment ###
The game can be built with the Unity game builder. There is currently no live system available for live deployment.

### Built With ###
- Unity3D - a powerful game engine with C# support
- InControl - an open source custom game controller support for Unity. InControl is primarily used here to support multiple controller feature in this game.

## Game Instructions ##
A Dual Shock 4 controller mapping can be found under `Settings` page, as illustrated below:
![](https://i.imgur.com/s9m6nGO.png)

### Basic Control - Menu ###
- As a reference, the name of the button are referred by the InControl: Standardized Controls 
![](http://www.gallantgames.com/assets/InControl/Controller-ebf136616887bd7fe67bc086d8e672716ddd6e1c8194f39d7b4fb908b1d0b86d.png)
- **Action 2**: Confirm the menu selection
- **DPad and Left Stick**: moving between menu items
- **Action 1**: Back to previous menu
- **Trigger/Bumper**: Switching between maps in Map Selection Menu.

### Basic Control - In-Game ###
Here I will be using Warrior Actor to illustrate the basic combat control in-game.

- **Left Stick**: Character Movement 
- **Action 3 and 4**: Character Jump, double jump is supported
![](https://i.imgur.com/uYxRseO.gif)

- **Action 2** : Character Attacking
![](https://i.imgur.com/xUcGMvy.gif)

- **Action 1** : Character Neutral Ability
![](https://i.imgur.com/cQlhpy1.gif)
- **Action 1 + Left Stick Down/Up/Left/Right**: Character Directional Ability, includes a down ability, up ability, and horizontal abilities

	![Down Ability](https://i.imgur.com/Xd6a2ta.gif)
	![Up Ability](https://i.imgur.com/16WSCaB.gif)
	![Horizontal Ability](https://i.imgur.com/w7788lu.gif)

- **Right Trigger + Left Stick Direction**: Character throw his unique ability to the direction of the left stick. In the case of Warrior, warrior will shoot a claw hook to the direction he desires. If it hits another character, warrior will pull the character toward him; if it hits a wall, warrior will be pulled toward the wall.
![Trigger](https://i.imgur.com/Xl2MOYb.gif)

- **NOTE:** In the case of Mage, she will shoot a teleport bolt at the direction she desires, and then player can push **Right Bumper** to teleport to the location of the bolt.

### Authors ###
- Jiantao Shen - karth-shen@hotmail.com
- Bingyu Li - bingyuli0428@gmail.com

### License ###
Everything beside the imports are licensed under the GPL 3.0 License. see the [LICENSE.md](LICENSE.md) file for details

### Acknowledgments ###
- Nothing yet

## Imported Assets Credits ##
**Mesh**:
- Red Knight : https://assetstore.unity.com/packages/3d/characters/humanoids/red-knight-91016
- Castle Guard Archer : https://assetstore.unity.com/packages/3d/characters/humanoids/castle-guard-archer-86932
- Archbishop : https://assetstore.unity.com/packages/3d/characters/humanoids/archbishop-87005
- Rudy the Chicken : https://assetstore.unity.com/packages/3d/characters/animals/micro-rooster-rudy-smashy-craft-series-121331

**Particle**:

- Slash: https://realtimevfx.com/t/electro-energy-slash-sheet-animation/5161

**Sound**:

- Sword 2 : https://freesound.org/people/qubodup/sounds/59992/
- Arrow: https://freesound.org/people/SypherZent/sounds/420668/
- Arrow 2: https://freesound.org/people/Taira%20Komori/sounds/215020/
- Staff Whoosh: https://freesound.org/people/Nightflame/sounds/422494/
- Staff Whoosh 2: https://freesound.org/people/qubodup/sounds/60013/
- Thunder https://freesound.org/people/Robinhood76/sounds/316850/
- Fireball https://freesound.org/people/Robinhood76/sounds/316850/
- Tornado https://freesound.org/people/RogerBoyX69/sounds/338721/
- Trap https://freesound.org/people/TheBuilder15/sounds/434898/
- Hook back https://freesound.org/people/willc2_45220/sounds/75137/
- Hook https://freesound.org/people/Erdie/sounds/65734/
- Chicken https://freesound.org/people/Rudmer_Rotteveel/sounds/316920/
- Back UI https://freesound.org/people/NenadSimic/sounds/171697/
- 321 GO https://freesound.org/people/steel2008/sounds/231277/

