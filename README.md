# Space Sim with Dependency Inversion


https://user-images.githubusercontent.com/82777171/127158500-a50c5fb4-cec7-43f0-b617-37cb4d0d0a69.mp4



This project uses the same logic  as [this one](https://github.com/ForestSquirrelDev/GenericSpaceSim), but this time i've made an attempt to make ship-related code follow Dependency inversion principle.

Now, instead of all core ship systems being MonoBehaviours and having to find their dependencies on their own, we only have one MonoBehaviour that represents player ship. Other core systems are non-MonoBehaviour C# classes and their dependencies are getting filled using the constructors.
