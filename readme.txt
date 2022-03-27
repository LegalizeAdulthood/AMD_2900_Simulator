AMD 2901 ALU and AMD 2909 CU Simulator v 1.01

by

Vlad Ion and Sorin Toma (UPB, 2003)



Note: 	This program is written in C# and requires the .NET Framework. You can freely download it from the Microsoft site 
	(www.microsoft.com) in the download section.


Interface usage
---------------


The Editor Window:

	Table -		Shows the currently loaded program.
	
	Text Box -	You can enter program comments here. They are saved when you save your program listing.	

	Comboboxes -	Use them to create a microinstruction (the binary value of the instruction is written underneath
			the comboboxes); read the command reference for the AMD 2909 Control Unit for instruction format.
			You must additionally specify the 4-bit data value in the last combobox; it was integrated in the
			microinstruction to simplify program writing.
			
	Set - 		Adds te currently selected microinstruction to the end of the program unless you are editing a line
			(a previously written line can be edited by double clicking on the row header in front of the line)
			in which case pressing the "Set" button updates the line with the content of the currently selected
			microinstruction.

	Insert Line - 	Select a line header in the table and press "Insert Line" to insert a new line above the selected one;
			Create the microinstruction and press "Set". If you don't select a line the currently selected one is
			used.

	Delete Line -	Deletes the currenty selected row. Select the row to be deleted with the mouse by clicking in the header.

	Save -		Saves your program to a binary file with the "amd" extention.

	Load - 		Loads a previously saved ".amd" file. Corrupted files may lead to unexpected results (the program still
			interprets the file if the lenght is correct).

	About... -	Does what it says.
	
	Exit -		No surprises here either.

	TEST -		Starts the simulator.

There are keyboard shortcuts for all the buttons. Press the ALT key and the underlined letter on the button (Ex: Alt+x
for "Exit", Alt+L for "Load", Alt+S for "Save").


The Simulator Window:

	Step - 		Goes to the next line in the program.
	
	Back - 		Goes one line backwards in the program.

	Exit - 		Returns to the editor.

In the simulator window one can see the currenly simulated program listing  on the left and the two units on the right.
While steping through the program the current line is shown and the data and flags in the ALU an CU are updated
accordingly. There can be only one simulator window opened at any given time.
There are keyboard shortcuts for the buttons (Alt+S for "Step"; Alt+B for "Back"; Alt+X for "Exit").
	
