using System;

namespace simulator
{
	/// <summary>
	/// Class UserInstruction - instructions in string form
	/// </summary>
	
	public class UserInstruction
	{
		// instruction elements
		public String salt,micro,mux,dest,sursa,c,operatie,adresaA,adresaB,adresaD,numar;	

		private String[] operatieStrings = { "ADD", "SUBR", "SUBS", "OR", "AND", "NOTRS", "EXOR", "EXNOR" };
		private String[] sursaStrings = { "AQ", "AB", "ZQ", "ZB", "ZA", "DA", "DQ", "DZ" };
		private String[] destStrings = { "QREG", "NOP", "RAMA", "RAMF", "RAMQD", "RAMD", "RAMQU", "RAMU" };
		private String[] muxStrings = { "ZERO", "ROT", "ROTD", "SHD" };
		private String[] microStrings = { "JRNZF", "JR", "CONT", "JD", "JSRNZF", "JSR"
											, "RS", "JSTV", "TCPOZF", "PUCONT", "POCONT", "TCPOC", "JRZF", "JRF3", "JROVR", "JRC"};

		//============================ EMPTY INSTRUCTION CONSTRUCTOR ==========================

		public UserInstruction(String str)
		{
			salt=" ";
			micro=" ";
			mux=" ";
			dest=" ";
			sursa=" ";
			c=" ";
			operatie=" ";
			adresaA=" ";
			adresaB=" ";
			adresaD=" ";
			numar=new String(str.ToCharArray());
		}



		//============================ AMD ISNTRUCTION CONSTRUCTOR ====================

		public UserInstruction(AMD_instruction instr, String str)
		{
			int nr=instr.MUX0+instr.MUX1*2;
			salt=instr.R.ToString();
			micro=microStrings[instr.P];	
			mux=muxStrings[nr];
			dest=destStrings[instr.I86];
			sursa=sursaStrings[instr.I20];
			c=instr.Cn.ToString();
			operatie=operatieStrings[instr.I53];
			adresaA=instr.Aadr.ToString();
			adresaB=instr.Badr.ToString();
			adresaD=instr.Data.ToString();
			numar=new String(str.ToCharArray());
		}	
	}
}