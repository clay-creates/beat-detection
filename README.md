# 🎵 Beat-Driven Marble Maze Game

## 📌 Overview
The **Beat-Driven Marble Maze** is a rhythm-based physics puzzle game where the environment **reacts to music** chosen by the player. Navigate your marble through dynamic obstacles, pitfalls, and changing walls while staying in sync with the beat!

---

## 🚀 Features
### 🎼 Music-Driven Gameplay
- Select a **background track** in the **Main Menu**.
- The environment **reacts dynamically** based on the music’s frequency bands.

### 🟠 Interactive Obstacles
- **Walls & Pitfalls** appear/disappear **to the beat**.
- **Pillars scale & rotate** dynamically based on frequency bands.
- **Player movement remains smooth & responsive** while rolling.

### 🎨 Customization
- Select a **marble skin (material)** in the **Main Menu**, which is applied in the Maze Scene.
- Choose from **multiple music tracks** to enhance the rhythm-based gameplay.

### 🕹️ Game Flow
1. 🎶 **Select your song & marble skin** in the Main Menu.
2. 🏁 **Navigate the maze while avoiding pitfalls & using beat-reactive mechanics**.
3. ⏳ **A timer tracks your run** and is displayed upon completing the maze.
4. 📜 **End Game UI allows replaying, returning to the main menu, or quitting**.

---

## 🔊 Audio Processing (`AudioPeer.cs`)
- **Breaks down audio** into **8 frequency bands**.
- Updates **game elements based on beat intensity**.
- **Triggers animations & environmental changes** using:
  - `_audioBandBuffer[]` → Used for **dynamic wall & pitfall movement**.
  - `_freqBand[]` → Controls **pillar scaling & rotations**.
  - `_Amplitude` → Controls **overall music intensity**.

---

## 🎨 Customization
### 🎼 Music Selection (`MusicManager.cs`)
- Songs are **stored in `PlayerPrefs`**.
- Selected **music plays in the Maze Scene** using `AudioPeer.cs`.

### 🟠 Marble Skins (`MarbleSkinManager.cs` & `MarbleSkinApplier.cs`)
- Skins are **stored in `PlayerPrefs`**.
- **The chosen skin is applied to the marble in the game.**

---

## Download this build here:
[https://drive.google.com/drive/folders/1N2Kx_viBP6Jwatod-CtT6b5c1tzVApRu?usp=drive_link]
