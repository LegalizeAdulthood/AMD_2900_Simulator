using System;

namespace simulator
{
	/// <summary>
	/// Clasa for the ALU AMD2901 functionality.
	/// </summary>

	public class UE_AMD2901
	{

		//internal circuit elements

		public int[] RAM16=new int[16]; 		//general 16*4 bit register
		public int Q;							//auxiliary register Q (4-bit)
		public int F;							//ALU data out	
	
	
		//circuit input
		public int D;							//data input
		public int Aadr,Badr;					//A and B address input
		public int carryIn;						//input carry
		public int I20,I53,I86;					//ALU instruction
		public int MUX0, MUX1; 					//shift setting bits	
	
		public int RAM3, RAM0, Q3, Q0;			//bits to be inserted when shifting occures (set by MUX0 & MUX1)
		public int RAM;							//register used to load the internal RAM 	
	
		//circuit output
		//flags
		public int overFlow;					
		public int 	zero; 						
		public int 	sign; 							
		public int 	carryOut;					
		public int 	nonp,nong;						//carry generate and propagate for the carry look ahead
		
		//data out 
		public int Y;				
	
		//------------------------------------------------------------------------------------
		//RESET constructor

		public UE_AMD2901()
		{
			for(int i=0;i<16;i++)
			{
				RAM16[i]=0;
			}
			Q=0;
			F=0;
			D=0;
			Aadr=0;
			Badr=0;
			carryIn=0;
			I20=0; I53=0; I86=0;
			MUX0=0; MUX1=0;
			RAM3=0; RAM0=0; Q3=0; Q0=0;
			RAM=0;
			overFlow=0;
			zero=0;
			sign=0;
			carryOut=0;
			nonp=0; nong=0;
			Y=0;
		}
		//------------------------------------------------------------------------------------			
		//copy constructor

		public UE_AMD2901(UE_AMD2901 oldUE)
		{
			for(int i=0;i<16;i++)
			{
				RAM16[i]=oldUE.RAM16[i];
			}
			Q=oldUE.Q;
			F=oldUE.F;
			D=oldUE.D;
			Aadr=oldUE.Aadr;
			Badr=oldUE.Badr;
			carryIn=oldUE.carryIn;
			I20=oldUE.I20   ;    I53=oldUE.I53; 	I86=oldUE.I86;
			MUX0=oldUE.MUX0 ;    MUX1=oldUE.MUX1;
			RAM3=oldUE.RAM3 ;    RAM0=oldUE.RAM0;   Q3=oldUE.Q3; 		Q0=oldUE.Q0;
			RAM=oldUE.RAM;
			overFlow=oldUE.overFlow;
			zero=oldUE.zero;
			sign=oldUE.sign;
			carryOut=oldUE.carryOut;
			nonp=oldUE.nonp; 		nong=oldUE.nong;
			Y=oldUE.Y;
		}
	

		//for testing
		private void Print(String s){ System.Console.WriteLine(s);}
		private void ShowBits(int value ,int no)
		{
			int bit;
			if (no <0 || no>15) no=15;
			for (int i=0;i<=no;i++)
			{
				bit=(value>>(no-i))&1;
				System.Console.WriteLine(bit+"  ");
			}
			System.Console.WriteLine("");
		}
	
		//------------------------------- ALU--------------------------------------------------- 	
	
		//sets flags based on the current instruction
		//returns the result

		private int UALFunction(int R, int S)
		{
			int aux=0;				//	result
			int cIn=carryIn&1;		//	carryIn - 1 bit
		
			R=R&15;					//	R and S must be 4-bit data
			S=S&15;

			switch (I53&7)			//	3-bit data
			{
				case 0:	
				{
					aux=R+S+cIn;		
					break;
				}
				//for substraction : 	    Cn=0 => A-B-1   Cn=1 => A-B <=> A-B- (Cn^1)
				case 1: 
				{
					aux=S-R -cIn;		//complementary code	
					break;	
				}
				case 2: 
				{
					aux=R-S -cIn;
					break ;
				}
				case 3:  
				{
					aux=(R|S)&15;	
					break ;					//no overflow
				}
				case 4:  
				{
					aux=(R&S)&15;	
					break ;					//no overflow
				}
				case 5:  
				{
					aux=((~R)&S)&15;	
					break ;					//no overflow
				}
				case 6:  
				{
					aux=(R^S)&15;	
					break ;					//no overflow
				}
				case 7:  
				{
					aux=(~(R^S))&15;	
					break ;					//no overflow
				}
			}
			
			//	Set CarryOut and OverFlow
			//	Cn+4 (carryOut) generated for aux between 16 and 31
			//  there is only one alu so the carryout is the same as the overflow
			
			if (aux > 15 && aux <32) 
			{
				carryOut=1;
				overFlow=1;
			}
			else
			{
				carryOut=0;
				overFlow=0;
			}
			

			aux=aux&15;
			
			//	Sets zero flag

			if (aux==0)
				zero=1;		
			else
				zero=0;

			//propagate and generate 
			
			nonp=1;
			nong=1;			//no p and g
			
			if (aux==15) 
			{
				nonp=0;		//propagate
			}
			if (aux>=15) 
			{
				nong=0;		//generate
			}
			//sign
			sign=aux&8;		//3rd bit

//			Print("\n\t\tual: R="+R+" S="+S+" cI="+carryIn+" | cO="+carryOut+ " oF="+
//				overFlow+" zo="+zero+"semn"+sign+"\n");
			return aux;
		}

	
		//------------------------------- ALU Source Selector ---------------------------------------- 	
		//calls the ALU with the corresponding R and S
		
		//returns the ALU result for the given input
		private int UALSourceSelector()
		{		
			int ual=0;

//			Print("\n\t");
			switch (I20&7)		//3-bit data
			{			
				
				case 0:	//Print("\tsrc AQ ");
					ual=UALFunction(RAM16[Aadr],Q);   			break;
				case 1: //Print("\tsrc AB "); 
					ual=UALFunction(RAM16[Aadr],RAM16[Badr]);	break;
				case 2: //Print("\tsrc ZQ "); 
					ual=UALFunction(0,Q);					   	break;
				case 3: //Print("\tsrc ZB "); 
					ual=UALFunction(0,RAM16[Badr]);			  	break;
				case 4: //Print("\tsrc ZA "); 
					ual=UALFunction(0,RAM16[Aadr]);				break;
				case 5: //Print("\tsrc DA "); 
					ual=UALFunction(D,RAM16[Aadr]);			   	break;
				case 6: //Print("\tsrc DQ "); 
					ual=UALFunction(D,Q);					   	break;
				case 7: //Print("\tsrc DZ "); 
					ual=UALFunction(D,0);					   	break;
			}
			//Print("\n\t\t Rezultat ual = "+ual+" \n");
			return ual;
		}



		//----------------- SET RAM0 RAM3 Q3 Q0 FOR SHIFT ---------------------------- 	
		private void SetInsertionDate()
		{
			MUX1=MUX1& 1;
			MUX0=MUX0&1 ;					//LSB only
			
			int MUX10=(MUX1<<1)|MUX0;
			int aux;						//for values interchange
			RAM3=RAM3&1;
			RAM0=RAM0&1;
			Q3=Q3&1;
			Q0=Q0&1;
			//Print("\n\t");
			switch (MUX10) 
			{
				case 0: 
					//Print("ins ZERO ");
					RAM3=0;	
					Q3=0;
					RAM0=0;	
					Q0=0;
					break;
				case 1: 
					//Print("ins ROTIRE ");
					//RAM3 RAM0 interchange
					aux=RAM3; RAM3=RAM0; RAM0=aux;
					//Q3 Q0 interchange
					aux=Q3; Q3=Q0; Q0=aux;
					break;
				case 2: 
					//Print("ins ROTIRE DUBLA ");
					//RAM3 Q0 interchange
					aux=RAM3; RAM3=Q0; Q0=aux;
					//RAM0 Q3 interchange
					aux=Q3;   Q3=RAM0; RAM0=aux;
					break;
				case 3: 
					//Print("ins DEPLASARE DUBLA ");
					RAM3=(F&8)>>3;		//RAM3=F3
					Q3=RAM0;			//Q3=RAM0	
					RAM0=Q3;			//RAM0=Q3
					Q0=0;
					break;
			}
		}



		//------------------------------- ALU Destination Selector ---------------------------------------- 	
	
		//	destination selector  -> sets UE_AMD 2901 class memebers
		//	calls SourceSelector for the operation result
		private void UALDestinationSelector()
		{
			F=UALSourceSelector();  //output for the ALU result
			RAM=F;					//regsiter used to load the RAM
			
			//SET RAM0 RAM3 Q0 Q3 VALUES BASED ON	
			//THE FUNCTION TABLE  -> IN0 and IN3 are (RAM0/Q0 and RAM3/Q3)
			
			RAM3 = (RAM&8)>>3;
			Q3 = (Q&8)>>3;
			RAM0 = RAM&1;
			Q0 = Q&1;
			//Print("\n\t");
			//Print("\n initial RAM "); ShowBits(RAM,5);
			//Print("\n initial Q   "); ShowBits(Q,5);
			//Print("\n RAM3  "+RAM3+" RAM0 "+RAM0+" Q3 "+Q3+ " Q0 \n"+Q0);
			SetInsertionDate();
			
			Badr=Badr&15;				//Badr =4-bit address
			
			//Print("\n\t");

			switch(I86&7)				//bits 6 7 and 8 set the destination
			{ 		
				case 0:		//	QREG
				{
					//Print(" dst QREG ");
					Q=F;
					Y=F;
					break;	
				}
				case 1:		//	NOP
				{
					//Print(" dst NOP ");
					Y=F;
					break;
				}
				case 2:		//	RAMA
				{
					//Print(" dst RAMA");
					//load F at Badr
					RAM16[Badr]=RAM;
					Aadr=Aadr&15;				//Aadr (4-bit)
					Y=RAM16[Aadr];
					break;
				}
				case 3:		//	RANF	
				{
					//Print(" dst RAMF ");
					RAM16[Badr]=RAM;
					Y=F;
					break;
				}
				case 4:		//	RAMQD
				{
					//Print(" dst RAMQD ");
					Y=F;
					RAM0=F&1;							//RAM0=F0
					RAM16[Badr]=(RAM>>1)|(RAM3<<3);		//B=RAM3 | F/2
					Q0=Q&1;								//Q0=Q0
					Q=(Q>>1)|(Q3<<3);					//Q=Q3 | Q/2
					break;	
				}
				case 5:		//	RAMD
				{
					//Print(" dst RAMD ");
					Y=F;
					RAM0=F&1;					//RAM0=F0
					RAM16[Badr]=(RAM>>1)|(RAM3<<3);		//B=RAM3 | F/2
					Q0=Q&1;
					break;
				}
				case 6:		//	RAMQU
				{
					//Print(" dst RAMQU ");
					Y=F;
					RAM3=(F&8)>>3;				//RAM3 = F3;
					RAM16[Badr]=(F<<1)|RAM0;	//B=2*F | RAM0
					Q3=(Q&8)>>3;				//Q3=Q3
					Q=(Q<<1)|Q0;				//Q=2*Q | Q0;
					break;
				}
				case 7:		//	RAMU
				{
					//Print(" dst RAMU ");
					Y=F;
					RAM3=(F&8)>>3;				//RAM3 = F3;
					RAM16[Badr]=(F<<1)|RAM0;	//B=2*F | RAM0
					Q3=Q&8;
					break;
				}
			}
			RAM=RAM16[Badr];
			//Print("\n final RAM "); 		ShowBits(RAM,5);
			//Print("\n final  Q   "); 		ShowBits(Q,5);
			//Print("\n Badr "+Badr+" are "); ShowBits(RAM16[Badr],5);
		}



		//------------------------------EXECUTES MICROINSTRUCTION----------------------------------
		public void Execute(UE_instruction instr)
		{
			//sets the members for the current instruction
			D=instr.Data&15;
			Aadr=instr.Aadr&15;
			Badr=instr.Badr&15;	
			carryIn=instr.Cn;	
			I86=instr.I86;
			I53=instr.I53;
			I20=instr.I20;
			MUX0=instr.MUX0&1;		
			MUX1=instr.MUX1&1;
			//Print("\n U Executie ");
		
			
			//execute instruction
			UALDestinationSelector();
			//UALDestinationSelector -calls UALSourceSelector which calls UALFunction
			//						 -calls SetInsertionDate 
			//						 -selects output and shifts						
		}			
	}
}
