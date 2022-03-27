using System;

namespace simulator
{
	/// <summary>
	/// Class for the Control Unit AMD2909 functionality.
	/// </summary>
	
	public class UC_AMD2909
	{

		public int R;						// jump address
		public int P;						// next instruction selector
		public AMD_instruction generalInstr=new AMD_instruction(); 				//instruction
	
	
		public UE_instruction ueInstr=null;//the instruction part that will be sent to the ALU
		public MS_instruction msInstr=null;
	
		public int nextAdr;
	
		// register D in the microsequencer 		(for external addresses) 
		// register D in the ALU 	(for external data)
		// D field (DATE) in the instruction (sets both previous registers)
	
	
		public UE_AMD2901 unitExec=null;
		public MicroSecventiator microSecv=null;

	
		//RESET constructor
		public UC_AMD2909()
		{
			ueInstr=new UE_instruction();
			msInstr=new MS_instruction();
			unitExec=new UE_AMD2901();
			microSecv=new MicroSecventiator();
		} 



		//copy constructor
		public UC_AMD2909(UC_AMD2909 oldUC )
		{
			ueInstr=new UE_instruction();
			msInstr=new MS_instruction();
			generalInstr=new AMD_instruction(oldUC.generalInstr);
			unitExec=new UE_AMD2901(oldUC.unitExec);
			microSecv=new MicroSecventiator(oldUC.microSecv);
		}	
					

		//	for testing
		public void Print(String s)
		{
			System.Console.WriteLine(s);
		}



		//=======================SETS MICROSEQUENCER ACTIVITY============================	
	
		private void ApelMicroSecventiator(int p30)
		{ 
			
			//	obtaines the next instruciton address
			//	emulates the auxiliary circuits in the CU (outside the microsequencer)
			
		
			msInstr.OR=0;
			msInstr.notZER0=1;

			switch (p30&15)		// selects the microinstruction
			{
				case 0: 
					//Print(" JRNZF ");
					if (unitExec.F !=0)
					{
						//(JMP R) (R-> X)
						microSecv.D=R&15;
						msInstr.S1=1; msInstr.S0=1;
						msInstr.notFE=1;	//stack inactive;
					}
					else
					{
						//continue - next instruction
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack inactive
					}
					break;
				case  1:
					//Print("JR "+(R&15));
					//jump to R
					microSecv.D=R&15;
					msInstr.S1=1; msInstr.S0=1;
					msInstr.notFE=1;	//stack inactive
					break;
				case 2:
					//Print(" CONT ");
					//next microinstruction
					msInstr.S1=0; msInstr.S0=0;
					msInstr.notFE=1;	//stack inactive
					break;
				case 3:
					//Print(" JD ");
					//jump to the address given in the data field
					microSecv.D=generalInstr.Data&15;	//loads reg D with the D data
					msInstr.S1=1; msInstr.S0=1;
					msInstr.notFE=1;	//stack inactive
					break;
				case 4:
					//Print(" JSRNZF ");
					if (unitExec.F !=0)
					{
						//subroutine at R (CALL R) (R-> RR)
						microSecv.RR=R&15;
						msInstr.S1=0; msInstr.S0=1;
						msInstr.notFE=0;	//stack active 
						msInstr.PUP=1;	//push current address
					}
					else
					{
						//continue
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack inactive
					}
					break;	
				case 5:	
					//Print(" JSR ");
					//subroutine at R (CALL R) (R-> RR)
					microSecv.RR=R&15;
					msInstr.S1=0; msInstr.S0=1;
					msInstr.notFE=0;	//stack active 
					msInstr.PUP=1;	//push current address
					break;
				case 6:	
					//Print(" RS ");
					//subroutine at R (CALL R) (R-> RR)
					msInstr.S1=1; msInstr.S0=0;
					msInstr.notFE=0;	//stack active  
					msInstr.PUP=0;	//pop next address
					break;
				case 7:
					//Print(" JSTV ");
					//jumps to address in stack top 
					msInstr.S1=1; msInstr.S0=0;	//sets te MUX to select the address given in stack
					msInstr.notFE=1;		//stack inactive (using register topSTV)
					break;
				case 8:
					//Print( "TCPOZF ");
					//pop if F=0, else continue
					if (unitExec.F==0)
					{
						//POP
						msInstr.S1=1;
						msInstr.S0=0;
						msInstr.notFE=0;	//stack active
						msInstr.PUP=0;	//POP address
					}
					else
					{
						//continue
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack e inactive
					}
					break;
				case 9:
					//Print(" PUCONT ");
					msInstr.S1=0;msInstr.S0=0;
					msInstr.notFE=0;	//stack active
					msInstr.PUP=1;	//PUSH address
					break;	
				case 10:
					//Print(" POCONT ");
					msInstr.S1=0;msInstr.S0=0;
					msInstr.notFE=0;	//stack active
					msInstr.PUP=0;	//PUSH address
					break;
				case 11:
					//Print(" TCPOC ");
					//pop if carryOut=0, else continua
					if (unitExec.carryOut==1)
					{
						//POP
						msInstr.S1=1;
						msInstr.S0=0;
						msInstr.notFE=0;	//stack active
						msInstr.PUP=0;	//POP address
					}
					else
					{
						//continue
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack inactive
					}
					break;
				case 12: 
					//Print(" JRZF ");
					if (unitExec.F ==0)
					{
						//jump to R (JMP R) (R-> X)
						microSecv.D=R&15;
						msInstr.S1=1; msInstr.S0=1;
						msInstr.notFE=1;	//stack inactive
					}
					else
					{
						//continue
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack inactive
					}
					break;
				case 13: 
					//Print(" JRF3 ");
					if ((unitExec.sign&1) ==1)
					{
						//jump to R (JMP R) (R-> X)
						microSecv.D=R&15;
						msInstr.S1=1; msInstr.S0=1;
						msInstr.notFE=1;	//stack inactive
					}
					else
					{
						//continue
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack inactive
					}
					break;	
				case 14: 
					//Print(" JROVR ");
					if (unitExec.overFlow==1)
					{
						//jump to R (JMP R) (R-> X)
						microSecv.D=R&15;
						msInstr.S1=1; msInstr.S0=1;
						msInstr.notFE=1;	//stack inactive
					}
					else
					{
						//continue
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack inactive
					}
					break;
				case 15: 
					//Print(" JRC ");
					if (unitExec.carryOut==1)
					{
						//jump to R (JMP R) (R-> X)
						microSecv.D=R&15;
						msInstr.S1=1; msInstr.S0=1;
						msInstr.notFE=1;	//stack inactive
					}
					else
					{
						//continue
						msInstr.S1=0; msInstr.S0=0;
						msInstr.notFE=1;	//stack inactive
					}
					break;
			}
			
			//the instruction and the registers for the microsequencer have been set
			
			nextAdr=microSecv.Execute(msInstr);
	
		}

		
		
		//=======================SETS CU INSTRUCTION========================================
		public void SetInstruction(AMD_instruction instr)
		{
			generalInstr.R=instr.R;
			generalInstr.P=instr.P;
			generalInstr.MUX1=instr.MUX1;
			generalInstr.MUX0=instr.MUX0;
			generalInstr.Cn=instr.Cn;
			generalInstr.I86=instr.I86;
			generalInstr.I53=instr.I53;
			generalInstr.I20=instr.I20;
			generalInstr.Aadr=instr.Aadr;
			generalInstr.Badr=instr.Badr;
			generalInstr.Data=instr.Data;
		}
	


		//=======================GETS INSTRUCTION FOR ALU======================================
		public int Executa()
		{
			
			P=generalInstr.P;
			R=generalInstr.R;
			ApelMicroSecventiator(P);
		
			ueInstr.Aadr=generalInstr.Aadr;
			ueInstr.Badr=generalInstr.Badr;
			ueInstr.Cn=generalInstr.Cn;
			ueInstr.Data=generalInstr.Data;
			ueInstr.I20=generalInstr.I20;
			ueInstr.I53=generalInstr.I53;
			ueInstr.I86=generalInstr.I86;
			ueInstr.MUX0=generalInstr.MUX0;
			ueInstr.MUX1=generalInstr.MUX1;
			unitExec.Execute(ueInstr);
			return nextAdr;		
		}		
	}


	//ALU instruction structure 
	public class UE_instruction
	{
		public int MUX1, MUX0;			//the 2 MUXes
		public int Cn;					//carry 
		public int I86,I53,I20;			//command vector 
		public int Aadr,Badr;			//4-bit A and B address
		public int Data;				//4-bit D data
	}
}
