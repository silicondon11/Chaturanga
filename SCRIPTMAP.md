# Script Map for Chaturanga

The following is a map outlining the general structure of the scripts, separated by scenes they are active in, as well as scriptable objects and enumerations at the end.

## Scene: MainMenu
- **MenuScript.cs**  
  Handles the UI elements in the MainMenu scene. If the player clicks the play button, the ModesMenu scene is loaded; if the settings button is clicked, the Settings scene is loaded.
- **Scroll.cs**  
  Logic for starting and handling the looped horizontal scrolling of the background sprite. An instance of this script exists in all menu scenes with this background.

## Scene: ModesMenu
- **ModesScript.cs**  
  Handles the UI elements in the ModesMenu scene. If the player clicks the continue button, they continue their existing round. If the player clicks the campaign button, the LevelsMenu scene is loaded.
- **Scroll.cs**  
  Duplicate instance from MainMenu.

## Scene: LevelsMenu
- **LevelsScript.cs**  
  Handles the UI elements in the LevelsMenu scene. Each button is numbered and corresponds to a level, which, when pressed, loads that level’s scene.
- **Scroll.cs**  
  Duplicate instance from MainMenu.

## Scene: Level#
- **MousePosition.cs**  
  Tracks the position of the player’s mouse on the screen relative to the 3D space, projecting a blue glowing pointer onto the terrain under the mouse’s position.
- **PlayerTrack.cs**  
  Converts player “WASD” keyboard inputs to north, south, east, and west translations of the camera, as well as “Q” and “E” inputs to control the camera's y-axis rotation.
- **PausePanelScript.cs**  
  Handles the activation and deactivation of the pause panel after the pause button is pressed, as well as the logic for the UI elements within the pause panel.
- **InventoryUI.cs**  
  Handles the activation and deactivation of the inventory panel after the “I” key is pressed.
  - **Inventory.cs**  
    Handles the logic for the UI elements on the inventory panel by instantiating `ItemSlot` objects, which represent each unique personnel unit. These emblems can be dragged into the 3D space to place personnel on the map.
    - **ItemSlot.cs**  
      Manages the transition of dragging a 2D personnel emblem from the inventory UI to the 3D prefab piece placed on the map. This transition is handled by `UnitMover.cs`, and an `ItemObjectScript.cs` with the correct `ItemObject` reference is created on the prefab.
- **Clock.cs**  
  Handles the initiation and running of the clock, starting from a preset date and time unique to each level/battle.
- **CompassHandler.cs**  
  Handles compass logic, where a linear compass scrolls a looped sprite with directions.
- **SunDialRotate.cs**  
  Updates the configuration of the sundial based on the position of the sun and the player’s camera.
- **Wind.cs**  
  Simulates semi-random wind effects, influenced by the current weather. The wind’s direction and speed are displayed on the weather vane.
- **WeatherEvents.cs**  
  Simulates historically accurate weather effects for the time of the battle and updates the UI box indicating the weather type.
- **PreBlurb.cs**  
  Activates the pre-blurb UI at the start of each round. This exists in all LevelX scenes and is populated with unique text for each level.
- **DistanceManager.cs**  
  Manages all piece movement in the 3D space of the map and uses the scripts below to handle specific functionalities, such as showing the distance info when hovering over a destination point after clicking a unit.
  - **DistInfoDelayScript.cs**  
    Displays distance info on a 3D panel when the player hovers over the destination point after clicking the unit.
  - **HighlighterMover.cs**  
    Creates a temporary glowing trail that highlights the path from the unit’s starting point to the destination point under the player's mouse.
  - **UnitMover.cs**  
    Handles the movement of 3D unit prefabs deployed by the player.
    - **ItemObjectScript.cs**  
      Attached to every personnel 3D prefab when deployed, storing the associated `ItemObject` containing the unit’s information as a local reference.
    - **UnitClick.cs**  
      Instantiates the `UnitMenu` prefab above the unit’s 3D prefab, which contains buttons that trigger specific unit actions. Special cases for Engineer, Scout, or Messenger units include a fourth button for unique commands.
      - **CollectButton.cs**  
        Simulates the unit gathering resources like wood and ore, and creates or finds a `CampObject` at the unit's current position to track the resources.
      - **RestButton.cs**  
        Simulates the unit resting over a timed coroutine.
      - **SharpenButton.cs**  
        Simulates the unit sharpening their weapons and creates or finds a `CampObject` at the unit's current position to track the depleted resources.
      - **EngButton.cs** *(One Case)*  
        Used by engineer units to lay mines, entrench positions, or build bridges across natural barriers.
      - **MsgButton.cs** *(One Case)*  
        Used by messenger units to send a message to enemy command after the player enters a message subject.
      - **SctButton.cs** *(One Case)*  
        Used by scout units to execute scouting or espionage operations with additional button-triggered options.
      - **TimerFill.cs**  
        Instantiates a 3D cylinder above the unit prefab to represent an action timer for unit tasks, which gradually fills in as time passes.
  - **LineRendererUtility.cs** *(Static)*  
    Static functionality called in `DistanceManager` to get the starting positions of each `LineRenderer`/road in the level.

## Scene: Settings
- **SettingsMenu.cs**  
  Handles the UI elements in the Settings scene, which control music and SFX volume, as well as the application’s resolution (for MacOS).
- **Scroll.cs**  
  Duplicate instance from MainMenu.

## ScriptableObjects
- **CampObject.cs**  
  A scriptable object created when a unit collects or sharpens at a point, establishing a `CampObject` that tracks the owning army, position, and gathered resources (wood, ore, and food).
- **ItemObject.cs**  
  A scriptable object used to store personnel unit information.

## Other
- **Enumerations.cs**  
  Contains enumerations used throughout the project.
