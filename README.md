# Puzzlemania

Oklahoma State University | ECEN 4273 | Project 1

Team members:
		Jacobo Rosillo
		Krish Ravi

---
		
## To Play:

Welcome to PuzzleMania SPOOKY edition. The current version of PuzzleMania includes 4 puzzle games. Instructions to play 
each are included through a "help" button within each game screen. Briefly summarised:

### Sudoku

##### Rules

Deduce which number goes in the square by following these rules:
1.	Only the numbers 1-9 can be used
2.	Numbers cannot be repeated within the same row, column, or 3x3 grid (a.k.a ‘house’)

##### How To
1.	Start by selecting the type of entry you would like to make
	a.	Number (denoted by the # symbol)
	b.	Note (denoted by the Pencil symbol)
2.	Choose the number you would like to enter into the puzzle
3.	Click on the appropriate cell to place the number into the puzzle
	a.	If the number you are trying to enter conflicts with numbers already in the row, column, or house, they will briefly flash red to signify the confliction
4.	Completely fill out the puzzle to Win!

### Set

##### Rules

Identify three cards from the 12 available that form a SET. 
Each card has 4 properties	
1.	Number
	a.	One
	b.	Two
	c.	Three

2.	Filling
	a.	Empty
	b.	Striped
	c.	Full

3.	Color
	a.	Green
	b.	Red
	c.	Purple

4.	Shape
	a.	Capsule
	b.	Diamond
	c.	Squiggle


##### How To

1.	Use the mouse to select three cards that you think form a SET
	a.	If the three cards flash green, the cards formed a SET
	b.	If the three cards flash red, the cards did not form a SET
2.	Get as many SETs as you can!

### CrossSum

##### Rules

Cross Sum is a mathematical puzzle where you must enter each of the numbers 1-9 in a grid to satisfy 6 different equations simultaneously. The operations in each equation are performed Left-to-Right or Top-to-Bottom. They do not follow the normal order of operator precedence.
1.	Only the numbers 1-9 can be used
2.	Each number can only be used once

##### How To

1.	Start by selecting the type of entry you would like to make
	a.	Number (denoted by the # symbol)
	b.	Note (denoted by the Pencil symbol)
2.	Choose the number you would like to enter into the puzzle
3.	Click on the appropriate cell to place the number into the puzzle
4.	If you fill out a row or column, the result cell will change color to either green or red.
	a.	Green signifies that the equation does equal the result, but that does not mean the correct numbers or correct order of numbers was used
	b.	Red signifies that the equation does not equal the result
5.	Completely fill out the puzzle with all the equations equaling the results to Win!

### Wordle

##### Rules

You must use a limited amount of guesses to deduce the randomly chosen English word.
1.	Your guesses must be words that are in the game’s dictionary, therefore you will not be penalized for words that are not possible.
2.	Only letters are allowed

##### How To

This game accepts both mouse and keyboard input.
1.	Input a word by clicking on the appropriate letter button or by using the keyboard
	a.	To erase a letter, click the backspace button or use the Backspace key on the keyboard
	b.	To erase a specific letter, click on that letter first or use the left/right arrow keys to change the selected letter
	c.	You can also overwrite a letter without erasing it first
2.	Submit the word by clicking on “Submit” or by hitting the Enter key on the keyboard
	a.	If your word is valid the color of the letters will change
	b.	If your word is invalid, then nothing will happen
3.	Below is the meaning for each color (default colors assumed):
	a.	Green: The letter is in the word and in the correct spot
	b.	Yellow: The letter is in the word but is Not in the correct spot	
	c.	Dark Gray: The letter is Not in the word
4.	Guess the word before running out of attempts to Win!


### Known Issues

###### In the Set game, the Time Attack tracks 1 less set than you have found, we've yet to troubleshoot this.
---

## To Compile

Open project folder `PuzzleMania` in Unity Hub

Create `PuzzleMania/Builds` folder

Hit "Build and Run" and point to `PuzzleMania/Builds` folder
