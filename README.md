# unity-damage-popups
Damage Popups for Unity 3D.

## Key Features
* Support for spawn position variance
* Support for random colors
* Popups move, fade, and scale up and down

## Setup
1. Ensure a static reference to the DamagePopup prefab is set within DamagePopup.cs

## Methods
*Creates and returns a DamagePopup.*
```cs
public static DamagePopup Create(Vector3 position, float damageAmount, params Color32[] colors)
```

## Dependencies
* TextMeshPro
* AssetManager (If you don't want to make your own static reference) https://github.com/KingSlme/unity-managers
