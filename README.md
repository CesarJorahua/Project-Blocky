# Project-Blocky

A Unity-based grid puzzle game featuring block-matching mechanics with pseudo gravity physics. Players click on connected blocks of the same color to remove them, earning points while managing limited moves.

## 📋 Project Information

This project was developed as part of a Unity Game Developer technical assessment. For complete test specifications and requirements, please refer to the [Senior Unity Game Developer Test.pdf](./Senior%20Unity%20Game%20Developer%20Test.pdf) document.

## 🎮 Game Features

- **Grid-Based Puzzle Gameplay**: Interactive grid system with customizable dimensions
- **Block Matching Mechanics**: Click connected blocks of the same color to remove them
- **Gravity System**: Pseudo-gravity physics that makes blocks fall to fill empty spaces
- **Score & Move Tracking**: Score points based on blocks removed and manage limited moves
- **Auto-Refill**: Automatically generates new random blocks to fill empty grid positions
- **Input Locking**: Prevents player interactions during animations and turn resolution

## 🏗️ Technical Implementation

### Core Systems

#### GridManager
The main game controller responsible for:
- Grid initialization with random colored blocks
- Block positioning and world-space calculations
- Click event handling and input management
- Flood-fill algorithm for finding connected blocks
- Turn resolution (block removal, gravity, refill)
- Input locking during animations

#### ScoreAndMoveManager
Manages game state including:
- Score calculation and tracking
- Move counting and limits
- Game-over conditions

#### InputManager
Handles player inputs using Unity's new Input System:
- Block click detection
- Input action mapping
- Player interaction events

### Architecture Highlights

- **Component-Based Design**: Modular scripts following Unity best practices
- **New Input System**: Utilizes Unity's modern Input System for better cross-platform support
- **Singleton Pattern**: Used for managers to ensure single instances
- **Coroutine-Based Turn Resolution**: Smooth gameplay flow
- **HashSet Collections**: Efficient block collection for connected block detection

## 📁 Project Structure

```
Project-Blocky/
├── Assets/
│   ├── Scripts/
│   │   ├── GridManager.cs          # Main grid logic and game controller
│   │   ├── Block.cs                # Individual block component
│   │   ├── ScoreAndMoveManager.cs  # Score and move tracking
│   │   ├── InputManager.cs         # Input handling
│   │   ├── Constants.cs            # Game configuration constants
│   │   └── PlayerActions.cs        # Generated Input System actions
│   ├── Prefabs/                    # Block and UI prefabs
│   ├── Sprites/                    # Visual assets
│   ├── Scenes/                     # Game scenes
│   └── Settings/                   # URP and project settings
├── ProjectSettings/                # Unity project configuration
└── Packages/                       # Unity packages and dependencies
```

## 🛠️ Technologies Used

- **Unity Engine**
- **C#**
- **Universal Render Pipeline (URP)**
- **New Input System**
- **TextMesh Pro**

## 🚀 Getting Started

### Prerequisites
- Unity 6.0 LTS or later
- Visual Studio or any other C# IDE

### Setup
1. Clone the repository
2. Open the project folder in Unity Hub
3. Let Unity import all assets and packages
4. Open the main scene from `Assets/Scenes/`
5. Press Play to start the game

## 🎯 How to Play

1. Click on any block in the grid
2. All connected blocks of the same color will be removed
3. Blocks above will fall down to fill empty spaces
4. New blocks will spawn to fill the grid
5. Score points based on the number of blocks removed
6. Continue until you run out of moves

## 📝 Configuration

Game parameters can be modified in `Constants.cs`:
- Grid dimensions (rows/columns)
- Cell size and spacing
- Animation timings

## 📄 License

Please refer to project documentation for licensing information.

## 👤 Developer

Developed by Cesar Mory Jorahua as part of a Unity Game Developer technical assessment.

---