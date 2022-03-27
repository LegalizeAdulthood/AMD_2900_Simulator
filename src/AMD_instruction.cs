using System;

namespace simulator
{
	/// <summary>
	/// Class for instruction reprezentation in binary formC
	/// </summary>

	
	public class AMD_instruction
	{
		//	variables are public for direct access
		
		//for CU

		public int R;					//jump address
		public int P;					//next address selector
	
		//for ALU -> will be retransmisted by the CU to the ALU

		public int MUX1, MUX0;				//the two MUXes
		public int Cn;						//carry
		public int I86,I53,I20;				//command vector
		public int Aadr,Badr;				//A and B addresses 4-bit
		public int Data;					//D data 4-bit


		//==============================Copy Constructor===================================
		public AMD_instruction(AMD_instruction instr)
		{
			R=instr.R;
			P=instr.P;
			MUX1=instr.MUX1;
			MUX0=instr.MUX0;
			Cn=instr.Cn;
			I86=instr.I86;
			I53=instr.I53;
			I20=instr.I20;
			Aadr=instr.Aadr;
			Badr=instr.Badr;
			Data=instr.Data;
		}



		//==============================Empty Instruction Constructor========================
		public AMD_instruction()
		{
			R=0;
			P=0;
			MUX1=0;
			MUX0=0;
			Cn=0;
			I86=0;
			I53=0;
			I20=0;
			Aadr=0;
			Badr=0;
			Data=0;
		} 


	}
}
