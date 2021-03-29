# Project Notes.

* (domain/entity) -> (use cases/application layer) -> persentation layer or data access.
* Domain/entity - holds all the domain logic and bussiness rules.
* Application layer - logic of how the domain logic will be used. 
   In our case that would be ,
   1. Create game. 
   2. Game contains boards
   3. we can do commands on the board.
  1. We are doing update commands on the board.
* Presentation layer.
  1. We use MVC to provide the browser with the view.
* We use dependecy inversion to cross different boundries.

* A class that is less stable should depend on more stable ones.
  * Stable in this context is a class that will frequently change.
* The domain logic is the most stable since it should not change to much.
* We need add the database interface to application core.

## Domain logic.
--

### Entities.
* Game 
* Board
* Lights

### Value objects.
* Lights on and off value.

## Application Logic (Should not depend on Data Access layer).
---

* Puzzle (Save games using in memeory DB)
  1. Create a new game.
  2. Add new Board.
  3. Delete Board.

* Board
  1. Create intial board. (inside of game)
  2. PressedLight(). (Toggles all four adjacent ligts)
  3. IsComplete. (all ligts are off)
  4. GiveUp()

* Lights
  1. Toggle()
  2. IsOn()

* LightValue (make static class)
  1. on = true
  2. off = false

