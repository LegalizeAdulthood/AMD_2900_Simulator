using System;

namespace simulator
{
	/// <summary>
	/// Microsequencer functionality Class.
	/// </summary>
	
	public class MicroSecventiator
	{
		//	internal elements
		public int[] STV=new int[4];	//stack = 4 4-bit words 
		public int topSTV;				//register containing stack top value
		public int IS;					//stack pointer
		public int CMP;					//microprogram counter
	

		//	circuit inputs
		public int RR;					//jump address given in the instruction
		public int D;					//external address (4-bit)from the data field of the instruction
		public int carryIn;				//carry-in for the internal microprogram counter incrementation

		public int notFE;					// stack active /inactive (reversed logic 0==true)
		public int PUP;					// PUSH/_POP bit
	
		public int S0,S1;				// MUX selection bits 
		public int OR;					// rezult (MUX | OR )& ZERO
		public int notZERO;				
	
	
		//	microsequencer outputs
	
		public int Y;
		public int carryOut;


		//	reset constructor
		public MicroSecventiator()
		{
			for (int i=0;i<4;i++)
			{
				STV[i]=0;
			}
			topSTV=0;
			IS=3;					
			CMP=0;					
			RR=0;	
			D=0;	
			carryIn=0;
			notFE=0;		
			PUP=0;		
			S0=0;S1=0;	
			OR=0;		
			notZERO=0;
			Y=0;
			carryOut=0;	
		}


		//	copy constructor
		public MicroSecventiator(MicroSecventiator oldMS)
		{
			for (int i=0;i<4;i++)
			{
				STV[i]=oldMS.STV[i];
			}
			topSTV=oldMS.topSTV;
			IS=oldMS.IS;					
			CMP=oldMS.CMP;					
			RR=oldMS.RR;	
			D=oldMS.D;	
			carryIn=oldMS.carryIn;
			notFE=oldMS.notFE;		
			PUP=oldMS.PUP;		
			S0=oldMS.S0;
			S1=oldMS.S1;	
			OR=oldMS.OR;		
			notZERO=oldMS.notZERO;
			Y=oldMS.Y;
			carryOut=oldMS.carryOut;	
		}
		
	
		//	testing purposes only
		public void Print(String s)
		{
			System.Console.WriteLine(s);
		}





		//----------------------------------- STACK CONTROL -----------------------------------	

		//if the stack is not POPed the method returns the top of the stack (TOPSTV)
		
		//if stack POPed => returns the top value and decrements the stack top pointer	
		private int Stiva()
		{
			if ((notFE & 1)==1)					// stack active
			{													
				return topSTV;					
			}

			//stack goes from location 0 to 3
			//IS counter containes the top pointer
			//a POP before any PUSH will return an indefinit result as will a number of pops larger
			//than the number of pushes or pushing more than 4 values on the stack

			if ((PUP & 1)==1)					//PUSH CMP on stack
			{
				topSTV=CMP&15;
				IS=(IS+1)&3;
				STV[IS]=CMP&15;
				//Print("\t stv PUSH "+topSTV+" ");
				return topSTV;
			}
			else
			{
				int aux=STV[IS];
				IS=(IS-1)&3;
				topSTV=STV[IS];					//IS stack t
				//Print("\t stv POP "+aux+" ");
				return aux;
			}
		}



		//----------------------------------- MUX SOURCE SELECTION -----------------------------------	
		//	chooses between CMP, RR, STV and D
		private int MUX()//returns MUX output
		{				
			S0=S0&1;
			S1=S1&1;
			int S10=(S1<<1)|S0;
			//Print("\n\t Contor STV "+IS);
			//Print("\n\t Varful stivei "+topSTV);
			
			int returnSTV=Stiva()&15;
			
			switch (S10) 
			{
				case 0 :
					//Console.WriteLine("\t sel CMP  "+CMP+"  ");
					return CMP&15;
				case 1 :
					//Console.WriteLine("\t sel RR "+RR+"  ");  
					return RR&15; 
				case 2 :
					//Console.WriteLine("\t sel STV  "+STV+"  ");
					return returnSTV;
				default:
					//Console.WriteLine("\t sel  D   "+D+"  ");
					return D&15; 
						    	 
			}
		}



		//----------------------------------- OUTPUT CONTROL ---------------------------------------				
		private void Iesire()
		{			
			notZERO=notZERO&1;
			if (notZERO==0)
			{
				Y=0;
				return;
			}
			OR=OR&15;					//4-bit OR
			Y=OR|MUX();					//OR between MUX output and OR[4]
			//Print("\t out Y= "+Y);
			return;
		}



		//----------------------------------- ISNTRUCTION EXECUTION -------------------------------				
		public int Execute(MS_instruction instr)
		{	
			//	receives an instruction vector
			//	the microprogram counter is increased first ( if we PUSH the stack, 
			//	the next instruction will be saved)
			//Print(" microsecventiator :\n");

			CMP=(Y+1+carryIn)&15;			// 4-bit CMP
			carryOut=((Y+1+carryIn)>>4)&1;	
				
			S1=instr.S1&1;		// 1 bit each
			S0=instr.S0&1;
			notFE=instr.notFE&1;
			PUP=instr.PUP&1;
			OR=instr.OR&15;		//4-bit
			notZERO=instr.notZER0&1;

			//we consider that the R, D , carryIn registers are set properly		
			//execution order :

			/*	1.	STACK METHOD -> PUSH in CMP or POP in popSTV
	
				2.	OUTPUT METHOD 
					2.1	call MUX method -> then we compute the Y output

				3. Y=output -> returns the incremented CMP + carryIn (the carryOut is obtained as well)	
			*/			
	
			Iesire();
			//Console.WriteLine("\n\t Instructiunea urmatoare "+Y);
			return Y; 	//next instruction		
		}		}


	//the microsequencer instruction structure

	public class MS_instruction
	{
		public int S1,S0;		//	MUX selection bits
		public int notFE;			//	stack active bit (reversed logic 0=true)
		public int PUP;			//	PUSH/ _POP bit
		public int OR;			//	4-bit OR
		public int notZER0;		//	forces the address to 0 if active (0=true)
	}
}
