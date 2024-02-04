# PlanetClicker
This is a cookie clicker clone game, made to show my ability to create web requests and implement firebase realtime database in Unity

The premise of the game is that you have to click on the planet and buy upgrades to destroy it 

[:camera: **See Screenshots**](#screenshots)

[:100: **Best code practices**](#best-code-practices-in-this-project)

## About the game
Genre: clicker

Unity version: 2022.3.18f1 (LTS)

Accessibility: The google-services.json and Firebase SDK files have been intentionally removed from this project due to security concerns. As a result, functionalities related to saving/loading data from my Firebase database within Unity Editor are currently disabled. However, the code related to saving and loading data uisng firebase realtime database is still available. Once this game is published on Google Play, you will be able to interact with my instance of the Firebase database.

## Screenshots
<div style="display:flex;">
  <img src="https://github.com/YankeeZuluDev/PlanetClicker/assets/129124150/c7c4a5c6-6f16-4f3f-8e7f-e76522bbe9bf" alt="screenshot_1">
  <img src="https://github.com/YankeeZuluDev/PlanetClicker/assets/129124150/462588ee-68b1-4559-92de-d87e436bf76b" alt="screenshot_2">
  <img src="https://github.com/YankeeZuluDev/PlanetClicker/assets/129124150/9d593692-1295-4186-be75-dbbb6ac12c45" alt="screenshot_3">
</div>

## Best code practices in this project

### Use of MVC design pattern

The [score system in this project](https://github.com/YankeeZuluDev/PlanetClicker/tree/main/Assets/Scripts/ScoreMVC) follows the Model-View-Controller (MVC) design pattern, promoting a clear and organized structure for scalable development.

<img src="https://github.com/YankeeZuluDev/PlanetClicker/assets/129124150/42602fda-344f-4f40-908f-f7cd4de444b7" alt="screenshot_4" width="720" height="494">

### Firebase realtime database integration

[Utilizing the Firebase realtime database](https://github.com/YankeeZuluDev/PlanetClicker/tree/main/Assets/Scripts/GameDataIO) provides a seamless and reliable solution for data storage. This integration enables efficient data management and real-time updates.

<img src="https://github.com/YankeeZuluDev/PlanetClicker/assets/129124150/9e784942-9f8e-4788-a997-cf99a901fd31" alt="screenshot_5" width="632" height="349">

### Use of UnityWebRequest class for Image Loading

The project [uses Unity's UnityWebRequest class](https://github.com/YankeeZuluDev/PlanetClicker/blob/main/Assets/Scripts/Other/URLImageLoader.cs) for loading images directly from the Internet.

### Dependency Injection

[The implementation of Zenject dependency injection framework](https://github.com/YankeeZuluDev/PlanetClicker/blob/main/Assets/Scripts/Installers/GameInstaller.cs) enhances the project's flexibility and maintainability by promoting loosely coupled components. This practice facilitates extensibility, and overall code cohesion.
