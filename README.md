# Koh’s Wrath

**Koh’s Wrath** ist ein atmosphärisches First-Person Survival-Horror-Spiel im PSX-Stil, das im Rahmen des Moduls *Computer Games* an der Hochschule RheinMain entwickelt wurde.

Das Spiel kombiniert:

- Escape-Room-Mechaniken  
- Psychologischen Horror  
- Ressourcen- & Stressmanagement  
- Multiple Endings  

Der Spieler übernimmt die Rolle eines Kammerjägers, der in einem düsteren Haus eingeschlossen wird – verfolgt von einem übernatürlichen Wesen namens **Koh**.

---

## 🎮 Core Features

### 🧠 Sanity-System (Zentrale Spielmechanik)

- Sanity sinkt kontinuierlich über Zeit  
- Zusätzlicher Verlust durch Gegner & Fehler  
- Bestimmte Events triggern erst bei niedriger Sanity  
- Game Over bei 0 Sanity  

Das System erzeugt konstanten psychologischen Druck und beeinflusst aktiv das Gameplay.

---

### 🕯 Die Lampe als zentrales Tool

- Einzige Lichtquelle im Haus  
- Kann bestimmte Gegner vertreiben  
- Überhitzungsmechanik bei zu langer Nutzung  
- Quick-Time-Event beim Erlöschen  

Die Lampe ist gleichzeitig Überlebenswerkzeug und Risiko.

---

### 🧩 Escape-Room Gameplay

- Lineare, aufeinander aufbauende Rätsel  
- 3 Minigames zur Code-Generierung  
- Versteckte Hinweise & Environmental Storytelling  
- Progression durch logische Verknüpfung von Items  

---

### 👁 Dynamische Bedrohungen

- Zufällig spawnende Tausendfüßler  
- Sanity-abhängiger „Black Ghost“  
- Eskalierende Bedrohung bei niedrigem mentalen Zustand  
- Finaler Antagonist: Koh  

---

### 🎭 Multiple Endings

- Gutes Ende (richtiger Code)  
- Schlechtes Ende (falscher Code)  
- Tod durch Sanity-Verlust  

Die Enden basieren auf Spielerentscheidungen und mentalem Zustand.

---

## 🛠 Tech Stack

- **Engine:** Unity  
- **Version Control:** Perforce  
- **Projektmanagement:** GitLab (Ticket-System)  
- **3D-Modelling:** Blender  
- **Audio:** Externe Assets + eigene Bearbeitung  
- **Assets:** Unity Asset Store, itch.io, Sketchfab  

---

## 🏗 Architektur & Game Design Highlights

- Modular aufgebautes Sanity-System  
- Event-gesteuerte Spawn-Logik  
- State-abhängige Event-Trigger  
- QTE-System bei Lampenausfall  
- Iteratives Playtesting & Balancing  
- Klare Rollenverteilung im Team  
- Wöchentliche Sprint-Meetings  

---

## 🎨 Eigene Assets

Selbst erstellt wurden u.a.:

- Kleine Tausendfüßler (3D Model + Animation)  
- Laterne (Gameplay-relevant)  
- Stromkasten (Minigame)  
- UI-Elemente (Sanity-Meter)  
- Start- & End-Screens  
- Intro-Sequenz  
- FPS-Arm-Animationen  

---

## 🧪 Playtesting

- Interne iterative Tests  
- Externe Tests beim Open Games Day  
- Fokus auf:
  - Difficulty Balancing  
  - Atmosphärische Wirkung  
  - Audio-Immersion  
  - Spielfluss  

---

## 🧑‍💻 Team & Rollen

**Lead Engineers**  
- Ayman Laghlali  
- Yassin Bolahrir  

**Lead Artist**  
- Marvin Wernli  

**Lead Designer**  
- Marvin Wernli  
- Yassin Bolahrir  

**Writing & Story**  
- Kai Ozki  

Alle Teammitglieder arbeiteten aktiv an Implementierung, Testing und Iteration.

---

## 🎯 Game Design Fokus

Das Ziel war es, Horror nicht primär über Jump Scares zu erzeugen, sondern durch:

- Kontrollverlust  
- Psychologischen Druck  
- Ressourcenknappheit  
- Dunkelheit & Sounddesign  
- Sanity-basierte Eskalation  

Die PSX-Ästhetik verstärkt das Unbehagen durch:

- Niedrige Polygonanzahl  
- Pixelige Texturen  
- Reduzierte Sichtweite  
- Dithering-Effekte  

---

## 📦 Installation

1. Repository klonen  
2. Projekt in Unity öffnen  
3. Empfohlene Unity-Version verwenden (ggf. hier Version eintragen)  
4. Build starten  

Oder:  
- Executable aus dem `/Build`-Ordner herunterladen  

---

## 📚 Kontext

Dieses Projekt wurde im Wintersemester 2024/25 an der Hochschule RheinMain im Modul **Computer Games** entwickelt.

Ziel war es:

- Ein vollständiges Spiel von Konzept bis Testphase umzusetzen  
- Game Design theoretisch fundiert praktisch anzuwenden  
- Teamkoordination unter realistischen Bedingungen zu trainieren  

---

## 🚀 Why This Project Matters

Koh’s Wrath demonstriert:

- Gameplay-Systemdesign  
- State-Driven Architecture  
- Event-basierte Spielmechaniken  
- Player Pressure Systems  
- Horror-Atmosphären-Design  
- Teamarbeit & Versionskontrolle  
- Iteratives Development & Testing  

---

## 📄 License

Dieses Projekt wurde im universitären Kontext entwickelt.  
Weitere Nutzung oder Distribution nur nach Absprache mit dem Team.
