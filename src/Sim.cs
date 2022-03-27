using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace simulator
{
	/// <summary>
	/// Clasa ferestrei simulatorului
	/// </summary>

	
	public class simulator : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

		Editor2 ed_ref;			// referinta la obiectul editor care a creat fereastra

		MyTableModel myModel;
		RamTableModel ramModel;
		ArrayList instructionData=new ArrayList();		//	Vector de AMD_instruction
		ArrayList userData=new ArrayList();				//	Vector de UserInstruction
		ArrayList ucData=new ArrayList();
		AMD_instruction instr;

		String[] operatieStrings = { "ADD", "SUBR", "SUBS", "OR", "AND", "NOTRS", "EXOR", "EXNOR" };
		String[] sursaStrings = { "AQ", "AB", "ZQ", "ZB", "ZA", "DA", "DQ", "DZ" };
		String[] destStrings = { "QREG", "NOP", "RAMA", "RAMF", "RAMQD", "RAMD", "RAMQU", "RAMU" };
 		String[] muxStrings = { "ZERO", "ROT", "ROTD", "SHD" };
		String[] microStrings = { "JRNZF", "JR", "CONT", "JD", "JSRNZF", "JSR"
									, "RS", "JSTV", "TCPOZF", "PUCONT", "POCONT", "TCPOC", "JRZF", "JRF3", "JROVR", "JRC"};
		int posProgram=0;
		UC_AMD2909 uc=new UC_AMD2909();

		#region visual components
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label label58;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.Label label68;
		private System.Windows.Forms.Label label70;
		private System.Windows.Forms.Label label72;
		private System.Windows.Forms.Label label74;
		private System.Windows.Forms.Label label76;
		private System.Windows.Forms.Label label78;
		private System.Windows.Forms.Label label80;
		private System.Windows.Forms.Label label82;
		private System.Windows.Forms.Label label83;
		private System.Windows.Forms.Label label84;
		private System.Windows.Forms.Label label87;
		private System.Windows.Forms.Label label89;
		private System.Windows.Forms.Label label91;
		private System.Windows.Forms.Label label93;
		private System.Windows.Forms.Label label95;
		private System.Windows.Forms.Label label97;
		private System.Windows.Forms.Label label99;
		private System.Windows.Forms.Label label101;
		private System.Windows.Forms.Label label103;
		private System.Windows.Forms.Label label105;
		private System.Windows.Forms.Label label107;
		private System.Windows.Forms.DataGrid L_instr;
		private System.Windows.Forms.Label ram3;
		private System.Windows.Forms.Label ram0;
		private System.Windows.Forms.Label ramshift;
		private System.Windows.Forms.Label q0;
		private System.Windows.Forms.Label q;
		private System.Windows.Forms.Label q3;
		private System.Windows.Forms.Label i20;
		private System.Windows.Forms.Label i53;
		private System.Windows.Forms.Label i86;
		private System.Windows.Forms.Label dadr;
		private System.Windows.Forms.Label badr;
		private System.Windows.Forms.Label aadr;
		private System.Windows.Forms.Label c;
		private System.Windows.Forms.Label mux1;
		private System.Windows.Forms.Label mux0;
		private System.Windows.Forms.Label f;
		private System.Windows.Forms.Label nonp;
		private System.Windows.Forms.Label nong;
		private System.Windows.Forms.Label y;
		private System.Windows.Forms.Label ovr;
		private System.Windows.Forms.Label cn4;
		private System.Windows.Forms.Label f3;
		private System.Windows.Forms.Label f0;
		private System.Windows.Forms.Label p;
		private System.Windows.Forms.Label r;
		private System.Windows.Forms.ListBox stiva;
		private System.Windows.Forms.Label varf;
		private System.Windows.Forms.Label cmp;
		private System.Windows.Forms.Label ist;
		private System.Windows.Forms.Label s0;
		private System.Windows.Forms.Label s1;
		private System.Windows.Forms.Label d;
		private System.Windows.Forms.Label cout;
		private System.Windows.Forms.Label cin;
		private System.Windows.Forms.Label pup;
		private System.Windows.Forms.Label nonfe;
		private System.Windows.Forms.Label next;
		private System.Windows.Forms.Label nonzero;
		private System.Windows.Forms.Label or;
		private System.Windows.Forms.Label muxx1;
		private System.Windows.Forms.Label micro;
		private System.Windows.Forms.Label salt;
		private System.Windows.Forms.Label dest;
		private System.Windows.Forms.Label muxx0;
		private System.Windows.Forms.Label cc;
		private System.Windows.Forms.Label sursa;
		private System.Windows.Forms.Label operatie;
		private System.Windows.Forms.Label adresaD;
		private System.Windows.Forms.Label adresaB;
		private System.Windows.Forms.Label adresaA;
		private System.Windows.Forms.DataGrid ramGrid;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn8;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn9;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn10;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn11;
		private System.Windows.Forms.ToolTip tt;

		#endregion

		ArrayList contor=new ArrayList();		
	
	
		//	Constructor implicit
		public simulator()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();			
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
				
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}



		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.L_instr = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle2 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn10 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn11 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label28 = new System.Windows.Forms.Label();
			this.label42 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.f3 = new System.Windows.Forms.Label();
			this.label76 = new System.Windows.Forms.Label();
			this.f0 = new System.Windows.Forms.Label();
			this.label74 = new System.Windows.Forms.Label();
			this.ovr = new System.Windows.Forms.Label();
			this.label72 = new System.Windows.Forms.Label();
			this.cn4 = new System.Windows.Forms.Label();
			this.label70 = new System.Windows.Forms.Label();
			this.nonp = new System.Windows.Forms.Label();
			this.label68 = new System.Windows.Forms.Label();
			this.nong = new System.Windows.Forms.Label();
			this.label66 = new System.Windows.Forms.Label();
			this.y = new System.Windows.Forms.Label();
			this.label64 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.badr = new System.Windows.Forms.Label();
			this.label62 = new System.Windows.Forms.Label();
			this.aadr = new System.Windows.Forms.Label();
			this.label60 = new System.Windows.Forms.Label();
			this.dadr = new System.Windows.Forms.Label();
			this.label58 = new System.Windows.Forms.Label();
			this.mux1 = new System.Windows.Forms.Label();
			this.label56 = new System.Windows.Forms.Label();
			this.mux0 = new System.Windows.Forms.Label();
			this.label54 = new System.Windows.Forms.Label();
			this.i20 = new System.Windows.Forms.Label();
			this.label50 = new System.Windows.Forms.Label();
			this.i53 = new System.Windows.Forms.Label();
			this.label48 = new System.Windows.Forms.Label();
			this.i86 = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.c = new System.Windows.Forms.Label();
			this.label52 = new System.Windows.Forms.Label();
			this.f = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.ramGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.q0 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.q = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.q3 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.ram0 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.ramshift = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.ram3 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.next = new System.Windows.Forms.Label();
			this.label82 = new System.Windows.Forms.Label();
			this.p = new System.Windows.Forms.Label();
			this.label80 = new System.Windows.Forms.Label();
			this.r = new System.Windows.Forms.Label();
			this.label78 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.nonzero = new System.Windows.Forms.Label();
			this.label105 = new System.Windows.Forms.Label();
			this.cout = new System.Windows.Forms.Label();
			this.label103 = new System.Windows.Forms.Label();
			this.cin = new System.Windows.Forms.Label();
			this.label101 = new System.Windows.Forms.Label();
			this.pup = new System.Windows.Forms.Label();
			this.label99 = new System.Windows.Forms.Label();
			this.nonfe = new System.Windows.Forms.Label();
			this.label97 = new System.Windows.Forms.Label();
			this.s0 = new System.Windows.Forms.Label();
			this.label95 = new System.Windows.Forms.Label();
			this.s1 = new System.Windows.Forms.Label();
			this.label93 = new System.Windows.Forms.Label();
			this.d = new System.Windows.Forms.Label();
			this.label91 = new System.Windows.Forms.Label();
			this.cmp = new System.Windows.Forms.Label();
			this.label89 = new System.Windows.Forms.Label();
			this.label84 = new System.Windows.Forms.Label();
			this.label83 = new System.Windows.Forms.Label();
			this.stiva = new System.Windows.Forms.ListBox();
			this.varf = new System.Windows.Forms.Label();
			this.ist = new System.Windows.Forms.Label();
			this.label87 = new System.Windows.Forms.Label();
			this.or = new System.Windows.Forms.Label();
			this.label107 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.adresaD = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.adresaB = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.adresaA = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.cc = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.sursa = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.muxx0 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.dest = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.muxx1 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.micro = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.salt = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.operatie = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.tt = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.L_instr)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ramGrid)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// L_instr
			// 
			this.L_instr.AllowNavigation = false;
			this.L_instr.AllowSorting = false;
			this.L_instr.BackgroundColor = System.Drawing.SystemColors.Window;
			this.L_instr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.L_instr.CaptionBackColor = System.Drawing.SystemColors.ControlDark;
			this.L_instr.CaptionVisible = false;
			this.L_instr.DataMember = "";
			this.L_instr.Enabled = false;
			this.L_instr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.L_instr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.L_instr.Location = new System.Drawing.Point(8, 32);
			this.L_instr.Name = "L_instr";
			this.L_instr.ReadOnly = true;
			this.L_instr.RowHeadersVisible = false;
			this.L_instr.Size = new System.Drawing.Size(432, 432);
			this.L_instr.TabIndex = 0;
			this.L_instr.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								this.dataGridTableStyle2});
			// 
			// dataGridTableStyle2
			// 
			this.dataGridTableStyle2.AllowSorting = false;
			this.dataGridTableStyle2.DataGrid = this.L_instr;
			this.dataGridTableStyle2.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3,
																												  this.dataGridTextBoxColumn4,
																												  this.dataGridTextBoxColumn5,
																												  this.dataGridTextBoxColumn6,
																												  this.dataGridTextBoxColumn7,
																												  this.dataGridTextBoxColumn8,
																												  this.dataGridTextBoxColumn9,
																												  this.dataGridTextBoxColumn10,
																												  this.dataGridTextBoxColumn11});
			this.dataGridTableStyle2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle2.MappingName = "";
			this.dataGridTableStyle2.PreferredColumnWidth = 40;
			this.dataGridTableStyle2.PreferredRowHeight = 12;
			this.dataGridTableStyle2.ReadOnly = true;
			this.dataGridTableStyle2.RowHeadersVisible = false;
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.HeaderText = "Nr";
			this.dataGridTextBoxColumn1.MappingName = "Nr";
			this.dataGridTextBoxColumn1.NullText = "";
			this.dataGridTextBoxColumn1.Width = 25;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "Salt";
			this.dataGridTextBoxColumn2.MappingName = "Salt";
			this.dataGridTextBoxColumn2.NullText = "";
			this.dataGridTextBoxColumn2.Width = 30;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "µInstr";
			this.dataGridTextBoxColumn3.MappingName = "µInstr";
			this.dataGridTextBoxColumn3.NullText = "";
			this.dataGridTextBoxColumn3.Width = 50;
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.HeaderText = "Mux";
			this.dataGridTextBoxColumn4.MappingName = "Mux";
			this.dataGridTextBoxColumn4.NullText = "";
			this.dataGridTextBoxColumn4.Width = 40;
			// 
			// dataGridTextBoxColumn5
			// 
			this.dataGridTextBoxColumn5.Format = "";
			this.dataGridTextBoxColumn5.FormatInfo = null;
			this.dataGridTextBoxColumn5.HeaderText = "Dest";
			this.dataGridTextBoxColumn5.MappingName = "Dest";
			this.dataGridTextBoxColumn5.NullText = "";
			this.dataGridTextBoxColumn5.Width = 50;
			// 
			// dataGridTextBoxColumn6
			// 
			this.dataGridTextBoxColumn6.Format = "";
			this.dataGridTextBoxColumn6.FormatInfo = null;
			this.dataGridTextBoxColumn6.HeaderText = "Sursa";
			this.dataGridTextBoxColumn6.MappingName = "Sursa";
			this.dataGridTextBoxColumn6.NullText = "";
			this.dataGridTextBoxColumn6.Width = 40;
			// 
			// dataGridTextBoxColumn7
			// 
			this.dataGridTextBoxColumn7.Format = "";
			this.dataGridTextBoxColumn7.FormatInfo = null;
			this.dataGridTextBoxColumn7.HeaderText = "C";
			this.dataGridTextBoxColumn7.MappingName = "C";
			this.dataGridTextBoxColumn7.NullText = "";
			this.dataGridTextBoxColumn7.Width = 25;
			// 
			// dataGridTextBoxColumn8
			// 
			this.dataGridTextBoxColumn8.Format = "";
			this.dataGridTextBoxColumn8.FormatInfo = null;
			this.dataGridTextBoxColumn8.HeaderText = "UAL";
			this.dataGridTextBoxColumn8.MappingName = "UAL";
			this.dataGridTextBoxColumn8.NullText = "";
			this.dataGridTextBoxColumn8.Width = 48;
			// 
			// dataGridTextBoxColumn9
			// 
			this.dataGridTextBoxColumn9.Format = "";
			this.dataGridTextBoxColumn9.FormatInfo = null;
			this.dataGridTextBoxColumn9.HeaderText = "AdrA";
			this.dataGridTextBoxColumn9.MappingName = "AdrA";
			this.dataGridTextBoxColumn9.NullText = "";
			this.dataGridTextBoxColumn9.Width = 40;
			// 
			// dataGridTextBoxColumn10
			// 
			this.dataGridTextBoxColumn10.Format = "";
			this.dataGridTextBoxColumn10.FormatInfo = null;
			this.dataGridTextBoxColumn10.HeaderText = "AdrB";
			this.dataGridTextBoxColumn10.MappingName = "AdrB";
			this.dataGridTextBoxColumn10.NullText = "";
			this.dataGridTextBoxColumn10.Width = 40;
			// 
			// dataGridTextBoxColumn11
			// 
			this.dataGridTextBoxColumn11.Format = "";
			this.dataGridTextBoxColumn11.FormatInfo = null;
			this.dataGridTextBoxColumn11.HeaderText = "Data";
			this.dataGridTextBoxColumn11.MappingName = "Data";
			this.dataGridTextBoxColumn11.NullText = "";
			this.dataGridTextBoxColumn11.Width = 40;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label28);
			this.panel1.Controls.Add(this.label42);
			this.panel1.Controls.Add(this.label41);
			this.panel1.Controls.Add(this.panel5);
			this.panel1.Controls.Add(this.panel4);
			this.panel1.Controls.Add(this.f);
			this.panel1.Controls.Add(this.label40);
			this.panel1.Controls.Add(this.ramGrid);
			this.panel1.Controls.Add(this.q0);
			this.panel1.Controls.Add(this.label38);
			this.panel1.Controls.Add(this.q);
			this.panel1.Controls.Add(this.label36);
			this.panel1.Controls.Add(this.q3);
			this.panel1.Controls.Add(this.label34);
			this.panel1.Controls.Add(this.ram0);
			this.panel1.Controls.Add(this.label32);
			this.panel1.Controls.Add(this.ramshift);
			this.panel1.Controls.Add(this.label30);
			this.panel1.Controls.Add(this.ram3);
			this.panel1.Controls.Add(this.label43);
			this.panel1.Location = new System.Drawing.Point(448, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(304, 432);
			this.panel1.TabIndex = 1;
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label28.Location = new System.Drawing.Point(24, 8);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(35, 16);
			this.label28.TabIndex = 3;
			this.label28.Text = "Ram3";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label42
			// 
			this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label42.Location = new System.Drawing.Point(184, 72);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(88, 16);
			this.label42.TabIndex = 21;
			this.label42.Text = "Input";
			this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label41
			// 
			this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label41.Location = new System.Drawing.Point(112, 320);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(88, 16);
			this.label41.TabIndex = 20;
			this.label41.Text = "Output";
			this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel5
			// 
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel5.Controls.Add(this.f3);
			this.panel5.Controls.Add(this.label76);
			this.panel5.Controls.Add(this.f0);
			this.panel5.Controls.Add(this.label74);
			this.panel5.Controls.Add(this.ovr);
			this.panel5.Controls.Add(this.label72);
			this.panel5.Controls.Add(this.cn4);
			this.panel5.Controls.Add(this.label70);
			this.panel5.Controls.Add(this.nonp);
			this.panel5.Controls.Add(this.label68);
			this.panel5.Controls.Add(this.nong);
			this.panel5.Controls.Add(this.label66);
			this.panel5.Controls.Add(this.y);
			this.panel5.Controls.Add(this.label64);
			this.panel5.Location = new System.Drawing.Point(8, 344);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(288, 80);
			this.panel5.TabIndex = 19;
			// 
			// f3
			// 
			this.f3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.f3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.f3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.f3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.f3.Location = new System.Drawing.Point(245, 48);
			this.f3.Name = "f3";
			this.f3.Size = new System.Drawing.Size(35, 16);
			this.f3.TabIndex = 45;
			this.f3.Text = "0";
			this.f3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label76
			// 
			this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label76.Location = new System.Drawing.Point(245, 24);
			this.label76.Name = "label76";
			this.label76.Size = new System.Drawing.Size(35, 16);
			this.label76.TabIndex = 44;
			this.label76.Text = "F3";
			this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// f0
			// 
			this.f0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.f0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.f0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.f0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.f0.Location = new System.Drawing.Point(205, 48);
			this.f0.Name = "f0";
			this.f0.Size = new System.Drawing.Size(35, 16);
			this.f0.TabIndex = 43;
			this.f0.Text = "0";
			this.f0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label74
			// 
			this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label74.Location = new System.Drawing.Point(205, 24);
			this.label74.Name = "label74";
			this.label74.Size = new System.Drawing.Size(35, 16);
			this.label74.TabIndex = 42;
			this.label74.Text = "F=0";
			this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ovr
			// 
			this.ovr.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ovr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ovr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ovr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ovr.Location = new System.Drawing.Point(165, 48);
			this.ovr.Name = "ovr";
			this.ovr.Size = new System.Drawing.Size(35, 16);
			this.ovr.TabIndex = 41;
			this.ovr.Text = "0";
			this.ovr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label72
			// 
			this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label72.Location = new System.Drawing.Point(165, 24);
			this.label72.Name = "label72";
			this.label72.Size = new System.Drawing.Size(35, 16);
			this.label72.TabIndex = 40;
			this.label72.Text = "OVR";
			this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cn4
			// 
			this.cn4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.cn4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cn4.Location = new System.Drawing.Point(125, 48);
			this.cn4.Name = "cn4";
			this.cn4.Size = new System.Drawing.Size(35, 16);
			this.cn4.TabIndex = 39;
			this.cn4.Text = "0";
			this.cn4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label70
			// 
			this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label70.Location = new System.Drawing.Point(125, 24);
			this.label70.Name = "label70";
			this.label70.Size = new System.Drawing.Size(35, 16);
			this.label70.TabIndex = 38;
			this.label70.Text = "Cn+4";
			this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nonp
			// 
			this.nonp.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.nonp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nonp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.nonp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nonp.Location = new System.Drawing.Point(85, 48);
			this.nonp.Name = "nonp";
			this.nonp.Size = new System.Drawing.Size(35, 16);
			this.nonp.TabIndex = 37;
			this.nonp.Text = "0";
			this.nonp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label68
			// 
			this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label68.Location = new System.Drawing.Point(85, 24);
			this.label68.Name = "label68";
			this.label68.Size = new System.Drawing.Size(35, 16);
			this.label68.TabIndex = 36;
			this.label68.Text = "/P";
			this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nong
			// 
			this.nong.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.nong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.nong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nong.Location = new System.Drawing.Point(45, 48);
			this.nong.Name = "nong";
			this.nong.Size = new System.Drawing.Size(35, 16);
			this.nong.TabIndex = 35;
			this.nong.Text = "0";
			this.nong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label66
			// 
			this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label66.Location = new System.Drawing.Point(45, 24);
			this.label66.Name = "label66";
			this.label66.Size = new System.Drawing.Size(35, 16);
			this.label66.TabIndex = 34;
			this.label66.Text = "/G";
			this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// y
			// 
			this.y.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.y.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.y.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.y.Location = new System.Drawing.Point(4, 48);
			this.y.Name = "y";
			this.y.Size = new System.Drawing.Size(37, 16);
			this.y.TabIndex = 33;
			this.y.Text = "0000";
			this.y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label64
			// 
			this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label64.Location = new System.Drawing.Point(5, 24);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(35, 16);
			this.label64.TabIndex = 32;
			this.label64.Text = "Y";
			this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.badr);
			this.panel4.Controls.Add(this.label62);
			this.panel4.Controls.Add(this.aadr);
			this.panel4.Controls.Add(this.label60);
			this.panel4.Controls.Add(this.dadr);
			this.panel4.Controls.Add(this.label58);
			this.panel4.Controls.Add(this.mux1);
			this.panel4.Controls.Add(this.label56);
			this.panel4.Controls.Add(this.mux0);
			this.panel4.Controls.Add(this.label54);
			this.panel4.Controls.Add(this.i20);
			this.panel4.Controls.Add(this.label50);
			this.panel4.Controls.Add(this.i53);
			this.panel4.Controls.Add(this.label48);
			this.panel4.Controls.Add(this.i86);
			this.panel4.Controls.Add(this.label46);
			this.panel4.Controls.Add(this.c);
			this.panel4.Controls.Add(this.label52);
			this.panel4.Location = new System.Drawing.Point(168, 96);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(128, 208);
			this.panel4.TabIndex = 18;
			// 
			// badr
			// 
			this.badr.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.badr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.badr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.badr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.badr.Location = new System.Drawing.Point(80, 128);
			this.badr.Name = "badr";
			this.badr.Size = new System.Drawing.Size(40, 16);
			this.badr.TabIndex = 37;
			this.badr.Text = "0000";
			this.badr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label62
			// 
			this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label62.Location = new System.Drawing.Point(80, 104);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(40, 16);
			this.label62.TabIndex = 36;
			this.label62.Text = "adrB";
			this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// aadr
			// 
			this.aadr.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.aadr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.aadr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.aadr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.aadr.Location = new System.Drawing.Point(8, 128);
			this.aadr.Name = "aadr";
			this.aadr.Size = new System.Drawing.Size(40, 16);
			this.aadr.TabIndex = 35;
			this.aadr.Text = "0000";
			this.aadr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label60
			// 
			this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label60.Location = new System.Drawing.Point(8, 104);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(40, 16);
			this.label60.TabIndex = 34;
			this.label60.Text = "adrA";
			this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dadr
			// 
			this.dadr.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dadr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dadr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.dadr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dadr.Location = new System.Drawing.Point(40, 86);
			this.dadr.Name = "dadr";
			this.dadr.Size = new System.Drawing.Size(40, 16);
			this.dadr.TabIndex = 33;
			this.dadr.Text = "0000";
			this.dadr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label58
			// 
			this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label58.Location = new System.Drawing.Point(40, 62);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(40, 16);
			this.label58.TabIndex = 32;
			this.label58.Text = "D";
			this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mux1
			// 
			this.mux1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.mux1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mux1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.mux1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mux1.Location = new System.Drawing.Point(48, 184);
			this.mux1.Name = "mux1";
			this.mux1.Size = new System.Drawing.Size(35, 16);
			this.mux1.TabIndex = 31;
			this.mux1.Text = "0";
			this.mux1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label56
			// 
			this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label56.Location = new System.Drawing.Point(48, 160);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(35, 16);
			this.label56.TabIndex = 30;
			this.label56.Text = "Mux1";
			this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mux0
			// 
			this.mux0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.mux0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mux0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.mux0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mux0.Location = new System.Drawing.Point(88, 184);
			this.mux0.Name = "mux0";
			this.mux0.Size = new System.Drawing.Size(35, 16);
			this.mux0.TabIndex = 29;
			this.mux0.Text = "0";
			this.mux0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label54
			// 
			this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label54.Location = new System.Drawing.Point(88, 160);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(35, 16);
			this.label54.TabIndex = 28;
			this.label54.Text = "Mux0";
			this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// i20
			// 
			this.i20.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.i20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.i20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.i20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.i20.Location = new System.Drawing.Point(86, 40);
			this.i20.Name = "i20";
			this.i20.Size = new System.Drawing.Size(35, 16);
			this.i20.TabIndex = 27;
			this.i20.Text = "000";
			this.i20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label50
			// 
			this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label50.Location = new System.Drawing.Point(86, 16);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(35, 16);
			this.label50.TabIndex = 26;
			this.label50.Text = "I20";
			this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// i53
			// 
			this.i53.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.i53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.i53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.i53.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.i53.Location = new System.Drawing.Point(46, 40);
			this.i53.Name = "i53";
			this.i53.Size = new System.Drawing.Size(35, 16);
			this.i53.TabIndex = 25;
			this.i53.Text = "000";
			this.i53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label48
			// 
			this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label48.Location = new System.Drawing.Point(46, 16);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(35, 16);
			this.label48.TabIndex = 24;
			this.label48.Text = "I53";
			this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// i86
			// 
			this.i86.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.i86.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.i86.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.i86.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.i86.Location = new System.Drawing.Point(6, 40);
			this.i86.Name = "i86";
			this.i86.Size = new System.Drawing.Size(35, 16);
			this.i86.TabIndex = 23;
			this.i86.Text = "000";
			this.i86.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label46
			// 
			this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label46.Location = new System.Drawing.Point(6, 16);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(35, 16);
			this.label46.TabIndex = 22;
			this.label46.Text = "I86";
			this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// c
			// 
			this.c.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.c.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.c.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.c.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.c.Location = new System.Drawing.Point(8, 184);
			this.c.Name = "c";
			this.c.Size = new System.Drawing.Size(35, 16);
			this.c.TabIndex = 14;
			this.c.Text = "0";
			this.c.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label52
			// 
			this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label52.Location = new System.Drawing.Point(8, 160);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(35, 16);
			this.label52.TabIndex = 13;
			this.label52.Text = "Cn";
			this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// f
			// 
			this.f.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.f.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.f.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.f.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.f.Location = new System.Drawing.Point(64, 288);
			this.f.Name = "f";
			this.f.Size = new System.Drawing.Size(40, 16);
			this.f.TabIndex = 17;
			this.f.Text = "0000";
			this.f.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label40
			// 
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label40.Location = new System.Drawing.Point(64, 264);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(40, 16);
			this.label40.TabIndex = 16;
			this.label40.Text = "F";
			this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ramGrid
			// 
			this.ramGrid.AllowNavigation = false;
			this.ramGrid.AllowSorting = false;
			this.ramGrid.BackgroundColor = System.Drawing.SystemColors.Control;
			this.ramGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ramGrid.CaptionBackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ramGrid.CaptionVisible = false;
			this.ramGrid.DataMember = "";
			this.ramGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.ramGrid.Location = new System.Drawing.Point(16, 96);
			this.ramGrid.Name = "ramGrid";
			this.ramGrid.ParentRowsVisible = false;
			this.ramGrid.ReadOnly = true;
			this.ramGrid.RowHeadersVisible = false;
			this.ramGrid.Size = new System.Drawing.Size(136, 160);
			this.ramGrid.TabIndex = 15;
			this.ramGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								this.dataGridTableStyle1});
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.AllowSorting = false;
			this.dataGridTableStyle1.DataGrid = this.ramGrid;
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "";
			this.dataGridTableStyle1.PreferredColumnWidth = 58;
			this.dataGridTableStyle1.ReadOnly = true;
			this.dataGridTableStyle1.RowHeadersVisible = false;
			// 
			// q0
			// 
			this.q0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.q0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.q0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.q0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.q0.Location = new System.Drawing.Point(253, 32);
			this.q0.Name = "q0";
			this.q0.Size = new System.Drawing.Size(35, 16);
			this.q0.TabIndex = 14;
			this.q0.Text = "0";
			this.q0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label38
			// 
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label38.Location = new System.Drawing.Point(253, 8);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(35, 16);
			this.label38.TabIndex = 13;
			this.label38.Text = "Q0";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// q
			// 
			this.q.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.q.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.q.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.q.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.q.Location = new System.Drawing.Point(212, 32);
			this.q.Name = "q";
			this.q.Size = new System.Drawing.Size(37, 16);
			this.q.TabIndex = 12;
			this.q.Text = "0000";
			this.q.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label36.Location = new System.Drawing.Point(212, 8);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(35, 16);
			this.label36.TabIndex = 11;
			this.label36.Text = "Q";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// q3
			// 
			this.q3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.q3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.q3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.q3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.q3.Location = new System.Drawing.Point(173, 32);
			this.q3.Name = "q3";
			this.q3.Size = new System.Drawing.Size(35, 16);
			this.q3.TabIndex = 10;
			this.q3.Text = "0";
			this.q3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label34.Location = new System.Drawing.Point(173, 8);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(35, 16);
			this.label34.TabIndex = 9;
			this.label34.Text = "Q3";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ram0
			// 
			this.ram0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ram0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ram0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ram0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ram0.Location = new System.Drawing.Point(104, 32);
			this.ram0.Name = "ram0";
			this.ram0.Size = new System.Drawing.Size(35, 16);
			this.ram0.TabIndex = 8;
			this.ram0.Text = "0";
			this.ram0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label32.Location = new System.Drawing.Point(104, 8);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(35, 16);
			this.label32.TabIndex = 7;
			this.label32.Text = "Ram0";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ramshift
			// 
			this.ramshift.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ramshift.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ramshift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ramshift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ramshift.Location = new System.Drawing.Point(63, 32);
			this.ramshift.Name = "ramshift";
			this.ramshift.Size = new System.Drawing.Size(37, 16);
			this.ramshift.TabIndex = 6;
			this.ramshift.Text = "0000";
			this.ramshift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label30.Location = new System.Drawing.Point(56, 8);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(56, 16);
			this.label30.TabIndex = 5;
			this.label30.Text = "Ramshift";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ram3
			// 
			this.ram3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ram3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ram3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ram3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ram3.Location = new System.Drawing.Point(24, 32);
			this.ram3.Name = "ram3";
			this.ram3.Size = new System.Drawing.Size(35, 16);
			this.ram3.TabIndex = 4;
			this.ram3.Text = "0";
			this.ram3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label43
			// 
			this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label43.Location = new System.Drawing.Point(40, 72);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(88, 16);
			this.label43.TabIndex = 21;
			this.label43.Text = "Ram";
			this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.next);
			this.panel2.Controls.Add(this.label82);
			this.panel2.Controls.Add(this.p);
			this.panel2.Controls.Add(this.label80);
			this.panel2.Controls.Add(this.r);
			this.panel2.Controls.Add(this.label78);
			this.panel2.Controls.Add(this.label44);
			this.panel2.Controls.Add(this.panel6);
			this.panel2.Location = new System.Drawing.Point(760, 32);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(224, 432);
			this.panel2.TabIndex = 2;
			// 
			// next
			// 
			this.next.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.next.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.next.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.next.Location = new System.Drawing.Point(96, 392);
			this.next.Name = "next";
			this.next.Size = new System.Drawing.Size(40, 16);
			this.next.TabIndex = 28;
			this.next.Text = "0000";
			this.next.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label82
			// 
			this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label82.Location = new System.Drawing.Point(24, 368);
			this.label82.Name = "label82";
			this.label82.Size = new System.Drawing.Size(176, 16);
			this.label82.TabIndex = 27;
			this.label82.Text = "Next Instruction";
			this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// p
			// 
			this.p.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.p.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.p.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.p.Location = new System.Drawing.Point(128, 40);
			this.p.Name = "p";
			this.p.Size = new System.Drawing.Size(40, 16);
			this.p.TabIndex = 26;
			this.p.Text = "0000";
			this.p.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label80
			// 
			this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label80.Location = new System.Drawing.Point(128, 16);
			this.label80.Name = "label80";
			this.label80.Size = new System.Drawing.Size(40, 16);
			this.label80.TabIndex = 25;
			this.label80.Text = "P";
			this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// r
			// 
			this.r.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.r.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.r.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.r.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.r.Location = new System.Drawing.Point(56, 40);
			this.r.Name = "r";
			this.r.Size = new System.Drawing.Size(40, 16);
			this.r.TabIndex = 24;
			this.r.Text = "0000";
			this.r.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label78
			// 
			this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label78.Location = new System.Drawing.Point(56, 16);
			this.label78.Name = "label78";
			this.label78.Size = new System.Drawing.Size(40, 16);
			this.label78.TabIndex = 23;
			this.label78.Text = "R";
			this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label44
			// 
			this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label44.Location = new System.Drawing.Point(32, 72);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(168, 16);
			this.label44.TabIndex = 22;
			this.label44.Text = "Microsequencer";
			this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel6
			// 
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel6.Controls.Add(this.nonzero);
			this.panel6.Controls.Add(this.label105);
			this.panel6.Controls.Add(this.cout);
			this.panel6.Controls.Add(this.label103);
			this.panel6.Controls.Add(this.cin);
			this.panel6.Controls.Add(this.label101);
			this.panel6.Controls.Add(this.pup);
			this.panel6.Controls.Add(this.label99);
			this.panel6.Controls.Add(this.nonfe);
			this.panel6.Controls.Add(this.label97);
			this.panel6.Controls.Add(this.s0);
			this.panel6.Controls.Add(this.label95);
			this.panel6.Controls.Add(this.s1);
			this.panel6.Controls.Add(this.label93);
			this.panel6.Controls.Add(this.d);
			this.panel6.Controls.Add(this.label91);
			this.panel6.Controls.Add(this.cmp);
			this.panel6.Controls.Add(this.label89);
			this.panel6.Controls.Add(this.label84);
			this.panel6.Controls.Add(this.label83);
			this.panel6.Controls.Add(this.stiva);
			this.panel6.Controls.Add(this.varf);
			this.panel6.Controls.Add(this.ist);
			this.panel6.Controls.Add(this.label87);
			this.panel6.Controls.Add(this.or);
			this.panel6.Controls.Add(this.label107);
			this.panel6.Location = new System.Drawing.Point(8, 96);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(208, 248);
			this.panel6.TabIndex = 0;
			// 
			// nonzero
			// 
			this.nonzero.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.nonzero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nonzero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.nonzero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nonzero.Location = new System.Drawing.Point(48, 224);
			this.nonzero.Name = "nonzero";
			this.nonzero.Size = new System.Drawing.Size(35, 16);
			this.nonzero.TabIndex = 64;
			this.nonzero.Text = "0";
			this.nonzero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label105
			// 
			this.label105.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label105.Location = new System.Drawing.Point(40, 200);
			this.label105.Name = "label105";
			this.label105.Size = new System.Drawing.Size(48, 16);
			this.label105.TabIndex = 63;
			this.label105.Text = "/ZERO";
			this.label105.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cout
			// 
			this.cout.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.cout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cout.Location = new System.Drawing.Point(160, 176);
			this.cout.Name = "cout";
			this.cout.Size = new System.Drawing.Size(35, 16);
			this.cout.TabIndex = 62;
			this.cout.Text = "0";
			this.cout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label103
			// 
			this.label103.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label103.Location = new System.Drawing.Point(160, 152);
			this.label103.Name = "label103";
			this.label103.Size = new System.Drawing.Size(35, 16);
			this.label103.TabIndex = 61;
			this.label103.Text = "Cout";
			this.label103.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cin
			// 
			this.cin.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.cin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cin.Location = new System.Drawing.Point(112, 176);
			this.cin.Name = "cin";
			this.cin.Size = new System.Drawing.Size(35, 16);
			this.cin.TabIndex = 60;
			this.cin.Text = "0";
			this.cin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label101
			// 
			this.label101.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label101.Location = new System.Drawing.Point(112, 152);
			this.label101.Name = "label101";
			this.label101.Size = new System.Drawing.Size(35, 16);
			this.label101.TabIndex = 59;
			this.label101.Text = "Cin";
			this.label101.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pup
			// 
			this.pup.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.pup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pup.Location = new System.Drawing.Point(64, 176);
			this.pup.Name = "pup";
			this.pup.Size = new System.Drawing.Size(35, 16);
			this.pup.TabIndex = 58;
			this.pup.Text = "0";
			this.pup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label99
			// 
			this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label99.Location = new System.Drawing.Point(64, 152);
			this.label99.Name = "label99";
			this.label99.Size = new System.Drawing.Size(35, 16);
			this.label99.TabIndex = 57;
			this.label99.Text = "PUP";
			this.label99.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nonfe
			// 
			this.nonfe.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.nonfe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nonfe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.nonfe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.nonfe.Location = new System.Drawing.Point(16, 176);
			this.nonfe.Name = "nonfe";
			this.nonfe.Size = new System.Drawing.Size(35, 16);
			this.nonfe.TabIndex = 56;
			this.nonfe.Text = "0";
			this.nonfe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label97
			// 
			this.label97.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label97.Location = new System.Drawing.Point(16, 152);
			this.label97.Name = "label97";
			this.label97.Size = new System.Drawing.Size(35, 16);
			this.label97.TabIndex = 55;
			this.label97.Text = "/FE";
			this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// s0
			// 
			this.s0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.s0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.s0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.s0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.s0.Location = new System.Drawing.Point(152, 128);
			this.s0.Name = "s0";
			this.s0.Size = new System.Drawing.Size(35, 16);
			this.s0.TabIndex = 54;
			this.s0.Text = "0";
			this.s0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label95
			// 
			this.label95.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label95.Location = new System.Drawing.Point(152, 104);
			this.label95.Name = "label95";
			this.label95.Size = new System.Drawing.Size(35, 16);
			this.label95.TabIndex = 53;
			this.label95.Text = "S0";
			this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// s1
			// 
			this.s1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.s1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.s1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.s1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.s1.Location = new System.Drawing.Point(88, 128);
			this.s1.Name = "s1";
			this.s1.Size = new System.Drawing.Size(35, 16);
			this.s1.TabIndex = 52;
			this.s1.Text = "0";
			this.s1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label93
			// 
			this.label93.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label93.Location = new System.Drawing.Point(88, 104);
			this.label93.Name = "label93";
			this.label93.Size = new System.Drawing.Size(35, 16);
			this.label93.TabIndex = 51;
			this.label93.Text = "S1";
			this.label93.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// d
			// 
			this.d.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.d.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.d.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.d.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.d.Location = new System.Drawing.Point(24, 128);
			this.d.Name = "d";
			this.d.Size = new System.Drawing.Size(37, 16);
			this.d.TabIndex = 50;
			this.d.Text = "0000";
			this.d.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label91
			// 
			this.label91.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label91.Location = new System.Drawing.Point(24, 104);
			this.label91.Name = "label91";
			this.label91.Size = new System.Drawing.Size(35, 16);
			this.label91.TabIndex = 49;
			this.label91.Text = "D";
			this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cmp
			// 
			this.cmp.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.cmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cmp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmp.Location = new System.Drawing.Point(144, 80);
			this.cmp.Name = "cmp";
			this.cmp.Size = new System.Drawing.Size(37, 16);
			this.cmp.TabIndex = 48;
			this.cmp.Text = "0000";
			this.cmp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label89
			// 
			this.label89.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label89.Location = new System.Drawing.Point(144, 56);
			this.label89.Name = "label89";
			this.label89.Size = new System.Drawing.Size(35, 16);
			this.label89.TabIndex = 47;
			this.label89.Text = "CMP";
			this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label84
			// 
			this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label84.Location = new System.Drawing.Point(96, 8);
			this.label84.Name = "label84";
			this.label84.Size = new System.Drawing.Size(72, 16);
			this.label84.TabIndex = 3;
			this.label84.Text = "Stack top";
			this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label83
			// 
			this.label83.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label83.Location = new System.Drawing.Point(8, 8);
			this.label83.Name = "label83";
			this.label83.Size = new System.Drawing.Size(64, 16);
			this.label83.TabIndex = 2;
			this.label83.Text = "Stack 4x4";
			this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// stiva
			// 
			this.stiva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.stiva.Enabled = false;
			this.stiva.IntegralHeight = false;
			this.stiva.Location = new System.Drawing.Point(16, 32);
			this.stiva.Name = "stiva";
			this.stiva.Size = new System.Drawing.Size(48, 64);
			this.stiva.TabIndex = 0;
			// 
			// varf
			// 
			this.varf.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.varf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.varf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.varf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.varf.Location = new System.Drawing.Point(120, 32);
			this.varf.Name = "varf";
			this.varf.Size = new System.Drawing.Size(37, 16);
			this.varf.TabIndex = 46;
			this.varf.Text = "0000";
			this.varf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ist
			// 
			this.ist.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ist.Location = new System.Drawing.Point(96, 80);
			this.ist.Name = "ist";
			this.ist.Size = new System.Drawing.Size(35, 16);
			this.ist.TabIndex = 39;
			this.ist.Text = "00";
			this.ist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label87
			// 
			this.label87.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label87.Location = new System.Drawing.Point(96, 56);
			this.label87.Name = "label87";
			this.label87.Size = new System.Drawing.Size(35, 16);
			this.label87.TabIndex = 38;
			this.label87.Text = "IS";
			this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// or
			// 
			this.or.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.or.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.or.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.or.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.or.Location = new System.Drawing.Point(128, 224);
			this.or.Name = "or";
			this.or.Size = new System.Drawing.Size(37, 16);
			this.or.TabIndex = 33;
			this.or.Text = "0000";
			this.or.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label107
			// 
			this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label107.Location = new System.Drawing.Point(128, 200);
			this.label107.Name = "label107";
			this.label107.Size = new System.Drawing.Size(35, 16);
			this.label107.TabIndex = 32;
			this.label107.Text = "OR[4]";
			this.label107.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(432, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Instruction List";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(456, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(288, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Arithmetic and Logic Unit AMD 2901";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(752, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(232, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Control Unit AMD 2909";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.adresaD);
			this.panel3.Controls.Add(this.label24);
			this.panel3.Controls.Add(this.adresaB);
			this.panel3.Controls.Add(this.label22);
			this.panel3.Controls.Add(this.adresaA);
			this.panel3.Controls.Add(this.label20);
			this.panel3.Controls.Add(this.cc);
			this.panel3.Controls.Add(this.label18);
			this.panel3.Controls.Add(this.sursa);
			this.panel3.Controls.Add(this.label16);
			this.panel3.Controls.Add(this.muxx0);
			this.panel3.Controls.Add(this.label14);
			this.panel3.Controls.Add(this.dest);
			this.panel3.Controls.Add(this.label12);
			this.panel3.Controls.Add(this.muxx1);
			this.panel3.Controls.Add(this.label10);
			this.panel3.Controls.Add(this.micro);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.salt);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.label26);
			this.panel3.Controls.Add(this.operatie);
			this.panel3.Location = new System.Drawing.Point(8, 472);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(432, 96);
			this.panel3.TabIndex = 6;
			// 
			// adresaD
			// 
			this.adresaD.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.adresaD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.adresaD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.adresaD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.adresaD.Location = new System.Drawing.Point(387, 64);
			this.adresaD.Name = "adresaD";
			this.adresaD.Size = new System.Drawing.Size(37, 16);
			this.adresaD.TabIndex = 20;
			this.adresaD.Text = "0000";
			this.adresaD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label24.Location = new System.Drawing.Point(384, 40);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(46, 16);
			this.label24.TabIndex = 19;
			this.label24.Text = "Data D";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// adresaB
			// 
			this.adresaB.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.adresaB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.adresaB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.adresaB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.adresaB.Location = new System.Drawing.Point(344, 64);
			this.adresaB.Name = "adresaB";
			this.adresaB.Size = new System.Drawing.Size(37, 16);
			this.adresaB.TabIndex = 18;
			this.adresaB.Text = "0000";
			this.adresaB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label22.Location = new System.Drawing.Point(344, 40);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(45, 16);
			this.label22.TabIndex = 17;
			this.label22.Text = "Adr B";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// adresaA
			// 
			this.adresaA.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.adresaA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.adresaA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.adresaA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.adresaA.Location = new System.Drawing.Point(304, 64);
			this.adresaA.Name = "adresaA";
			this.adresaA.Size = new System.Drawing.Size(37, 16);
			this.adresaA.TabIndex = 16;
			this.adresaA.Text = "0000";
			this.adresaA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label20.Location = new System.Drawing.Point(304, 40);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(45, 16);
			this.label20.TabIndex = 15;
			this.label20.Text = "Adr A";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cc
			// 
			this.cc.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.cc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cc.Location = new System.Drawing.Point(242, 64);
			this.cc.Name = "cc";
			this.cc.Size = new System.Drawing.Size(16, 16);
			this.cc.TabIndex = 14;
			this.cc.Text = "0000";
			this.cc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label18.Location = new System.Drawing.Point(240, 40);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(22, 16);
			this.label18.TabIndex = 13;
			this.label18.Text = "Cn";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// sursa
			// 
			this.sursa.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.sursa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sursa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.sursa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.sursa.Location = new System.Drawing.Point(196, 64);
			this.sursa.Name = "sursa";
			this.sursa.Size = new System.Drawing.Size(35, 16);
			this.sursa.TabIndex = 12;
			this.sursa.Text = "0000";
			this.sursa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label16.Location = new System.Drawing.Point(196, 40);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(40, 16);
			this.label16.TabIndex = 11;
			this.label16.Text = "Sour";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// muxx0
			// 
			this.muxx0.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.muxx0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.muxx0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.muxx0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.muxx0.Location = new System.Drawing.Point(164, 64);
			this.muxx0.Name = "muxx0";
			this.muxx0.Size = new System.Drawing.Size(14, 16);
			this.muxx0.TabIndex = 10;
			this.muxx0.Text = "0000";
			this.muxx0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.Location = new System.Drawing.Point(156, 40);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(38, 16);
			this.label14.TabIndex = 9;
			this.label14.Text = "Mux2";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dest
			// 
			this.dest.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.dest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dest.Location = new System.Drawing.Point(120, 64);
			this.dest.Name = "dest";
			this.dest.Size = new System.Drawing.Size(35, 16);
			this.dest.TabIndex = 8;
			this.dest.Text = "000";
			this.dest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.Location = new System.Drawing.Point(118, 40);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(35, 16);
			this.label12.TabIndex = 7;
			this.label12.Text = "Dest";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// muxx1
			// 
			this.muxx1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.muxx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.muxx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.muxx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.muxx1.Location = new System.Drawing.Point(90, 64);
			this.muxx1.Name = "muxx1";
			this.muxx1.Size = new System.Drawing.Size(14, 16);
			this.muxx1.TabIndex = 6;
			this.muxx1.Text = "0000";
			this.muxx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(80, 40);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(38, 16);
			this.label10.TabIndex = 5;
			this.label10.Text = "Mux1";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// micro
			// 
			this.micro.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.micro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.micro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.micro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.micro.Location = new System.Drawing.Point(44, 64);
			this.micro.Name = "micro";
			this.micro.Size = new System.Drawing.Size(37, 16);
			this.micro.TabIndex = 4;
			this.micro.Text = "0000";
			this.micro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(44, 40);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(35, 16);
			this.label8.TabIndex = 3;
			this.label8.Text = "µInstr";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// salt
			// 
			this.salt.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.salt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.salt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.salt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.salt.Location = new System.Drawing.Point(4, 64);
			this.salt.Name = "salt";
			this.salt.Size = new System.Drawing.Size(37, 16);
			this.salt.TabIndex = 2;
			this.salt.Text = "0000";
			this.salt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(4, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "Jump";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(416, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Current instruction in binary form";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label26.Location = new System.Drawing.Point(266, 40);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(35, 16);
			this.label26.TabIndex = 7;
			this.label26.Text = "ALU";
			// 
			// operatie
			// 
			this.operatie.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.operatie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.operatie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.operatie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.operatie.Location = new System.Drawing.Point(263, 64);
			this.operatie.Name = "operatie";
			this.operatie.Size = new System.Drawing.Size(35, 16);
			this.operatie.TabIndex = 8;
			this.operatie.Text = "0000";
			this.operatie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(592, 504);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 7;
			this.button1.Text = "&Back";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button2.Location = new System.Drawing.Point(688, 504);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 24);
			this.button2.TabIndex = 8;
			this.button2.Text = "&Step";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button3.Location = new System.Drawing.Point(824, 504);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(80, 24);
			this.button3.TabIndex = 9;
			this.button3.Text = "E&xit";
			this.button3.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// tt
			// 
			this.tt.AutomaticDelay = 400;
			this.tt.AutoPopDelay = 10000;
			this.tt.InitialDelay = 400;
			this.tt.ReshowDelay = 80;
			// 
			// simulator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 579);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.L_instr);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "simulator";
			this.Text = "Simulator";
			this.Closed += new System.EventHandler(this.simulator_Closed);
			((System.ComponentModel.ISupportInitialize)(this.L_instr)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ramGrid)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
/*		[STAThread]
/*		static void Main() 
		{
			Application.Run(new simulator());
		}
		*/


		//	Constructor cu parametrii vectorii de AMDinstruction si UserInstruction
		public simulator(ArrayList instrData,ArrayList user,Editor2 ed) 
		{

			InitializeComponent();
			ed_ref=ed;
			instructionData.AddRange(instrData);
			userData.AddRange(user);
			contor.Add(-1);
		
			String[] columnNames={"Nr", "Salt", "µInstr", "Mux", "Dest", "Sursa", "C", "UAL", "AdrA", "AdrB", "Data"};

			myModel = new MyTableModel(userData, columnNames);
			L_instr.SetDataBinding(myModel,myModel.TableName);


		
			String[] columns={"Poz", "RAM 16x4"};
			ArrayList ramdata=new ArrayList();
			for (int i=0;i<16;i++)
				ramdata.Add(new RamRow(i.ToString(),"0000"));
			ramModel=new RamTableModel(ramdata, columns);
			ramGrid.SetDataBinding(ramModel,ramModel.TableName);

	

			ArrayList stackdata=new ArrayList();
			for (int i=0;i<4;i++)
			{
				stackdata.Add("0000");
				stiva.Items.Add(stackdata[i].ToString());
			}		

			tt.SetToolTip(L_instr,"Microcode");
			tt.SetToolTip(ramGrid,"ALU Ram listing");
			tt.SetToolTip(ramshift,"Ram Shift register");
			tt.SetToolTip(q,"Q shift register");
			tt.SetToolTip(q3,"I/O terminal for Q's MSB");
			tt.SetToolTip(q0,"I/O terminal for Q's LSB");
			tt.SetToolTip(ram3,"I/O terminal for RAMSHIFT's MSB");
			tt.SetToolTip(ram0,"I/O terminal for RAMSHIFT's LSB");
			tt.SetToolTip(f,"ALU Output");
			tt.SetToolTip(i86,"Selects ALU destination");
			tt.SetToolTip(i53,"Selects ALU function");
			tt.SetToolTip(i20,"Selects ALU source");
			tt.SetToolTip(aadr,"Address A");
			tt.SetToolTip(badr,"Address B");
			tt.SetToolTip(dadr,"External Data");
			tt.SetToolTip(c,"Carry in, set in the microcode");
			tt.SetToolTip(mux1,"Command for the shift circuit's multiplexors");
			tt.SetToolTip(mux0,"Command for the shift circuit's multiplexors");
			tt.SetToolTip(y,"ALU Result");
			tt.SetToolTip(nonp,"Propagate carry flag");
			tt.SetToolTip(nong,"Generate carry flag");
			tt.SetToolTip(cn4,"Carry out, in the MSB");
			tt.SetToolTip(ovr,"Overflow flag");
			tt.SetToolTip(f0,"F=0 flag");
			tt.SetToolTip(f3,"F's MSB");
			tt.SetToolTip(r,"Jump address register");
			tt.SetToolTip(p,"Microinstruction register");
			tt.SetToolTip(stiva,"Stack");
			tt.SetToolTip(varf,"Stack top register");
			tt.SetToolTip(ist,"Stack Pointer (top position)");
			tt.SetToolTip(cmp,"Microprogram Counter - Current instruction address");
			tt.SetToolTip(d,"External Data - used here as an address");
			tt.SetToolTip(s1,"Selects the source for the next instruction");
			tt.SetToolTip(s0,"Selects the source for the next instruction");
			tt.SetToolTip(nonfe,"Stack active/inactive");
			tt.SetToolTip(pup,"Selects stack PUSH/POP command");
			tt.SetToolTip(cin,"Carry in");
			tt.SetToolTip(cout,"Carry out");

			





		}



		//	Converteste un numar intr-un sir de biti reprezentati ca String
		public String convertBinary(int numar, int biti)
	{
		String str="";
		int nr=numar;
		for(int i=0;i<biti;i++)
		{
			str=(nr%2).ToString() + str;
			nr=nr/2;
		}
		return str;
	}



		// step
		private void button2_Click(object sender, System.EventArgs e)
		{
			if (instructionData.Count!=0)
			{
				if (posProgram<instructionData.Count)
				{
					instr=(AMD_instruction)instructionData[posProgram];
					uc.SetInstruction(instr);
				}
			}
			button1.Enabled=true;//.setEnabled(true);
			ucData.Add(new UC_AMD2909(uc));
			contor.Add(posProgram);
			posProgram=uc.Executa();
				
			if (instructionData.Count<=posProgram)
			{
				button2.Enabled=false;//.setEnabled(false);
				//table.setSelectionBackground(new Color(255,204,204));
			}
			if (contor.Count!=1)
			{
				if((int)contor[contor.Count-2]>=0)
					L_instr.UnSelect((int)contor[contor.Count-2]);
				L_instr.Select((int)(contor[contor.Count-1]));
			}


			p.Text=convertBinary(uc.P,4);
			r.Text=convertBinary(uc.R,4);
			nonfe.Text=convertBinary(uc.microSecv.notFE,2);
			pup.Text=convertBinary(uc.microSecv.PUP,2);
			cin.Text=convertBinary(uc.microSecv.carryIn,1);
			cout.Text=convertBinary(uc.microSecv.carryOut,1);
			nonzero.Text=convertBinary(uc.microSecv.notZERO,1);
			or.Text=convertBinary(uc.microSecv.OR,4);
			d.Text=convertBinary(uc.microSecv.D,4);
			s1.Text=convertBinary(uc.microSecv.S1,2);
			s0.Text=convertBinary(uc.microSecv.S0,2);
			ist.Text=convertBinary(uc.microSecv.IS,2);
			stiva.SelectedIndex=uc.microSecv.IS;
			cmp.Text=convertBinary(uc.microSecv.CMP,3);
			varf.Text=convertBinary(uc.microSecv.topSTV,4);
			next.Text=convertBinary(posProgram,4);
				
				
			ram3.Text=convertBinary(uc.unitExec.RAM3,1);
			ram0.Text=convertBinary(uc.unitExec.RAM0,1);
			ramshift.Text=convertBinary(uc.unitExec.RAM,4);
			q3.Text=convertBinary(uc.unitExec.Q3,1);
			q0.Text=convertBinary(uc.unitExec.Q0,1);
			q.Text=convertBinary(uc.unitExec.Q,4);
				
			i86.Text=convertBinary(uc.unitExec.I86,3);
			i53.Text=convertBinary(uc.unitExec.I53,3);
			i20.Text=convertBinary(uc.unitExec.I20,3);
				
			dadr.Text=convertBinary(uc.unitExec.D,4);
			aadr.Text=convertBinary(uc.unitExec.Aadr,4);
			badr.Text=convertBinary(uc.unitExec.Badr,4);
				
			c.Text=convertBinary(uc.unitExec.carryIn,1);
			mux1.Text=convertBinary(uc.unitExec.MUX1,1);
			mux0.Text=convertBinary(uc.unitExec.MUX0,1);
				
			f.Text=convertBinary(uc.unitExec.F,4);
			y.Text=convertBinary(uc.unitExec.Y,4);
			nong.Text=convertBinary(uc.unitExec.nong,1);
			nonp.Text=convertBinary(uc.unitExec.nonp,1);
			cn4.Text=convertBinary(uc.unitExec.carryOut,1);
			ovr.Text=convertBinary(uc.unitExec.overFlow,1);
			f0.Text=convertBinary(uc.unitExec.zero,1);
			f3.Text=convertBinary(uc.unitExec.sign,1);
				
			for(int i=0;i<16;i++)
			{
				ramModel.replaceRow(i,new RamRow(i.ToString(),convertBinary(uc.unitExec.RAM16[i],4)));
			}
			for(int i=0;i<4;i++)
			{
				stiva.Items[i]=convertBinary(uc.microSecv.STV[i],4);
			}
				
			salt.Text=convertBinary(instr.R,4);
			micro.Text=convertBinary(instr.P,4);
			muxx1.Text=convertBinary(instr.MUX1,1);
			dest.Text=convertBinary(instr.I86,3);
			muxx0.Text=convertBinary(instr.MUX0,1);
			sursa.Text=convertBinary(instr.I20,3);
			cc.Text=convertBinary(instr.Cn,1);
			operatie.Text=convertBinary(instr.I53,3);
			adresaA.Text=convertBinary(instr.Aadr,4);
			adresaB.Text=convertBinary(instr.Badr,4);
			adresaD.Text=convertBinary(instr.Data,4);
		}



		// exit
		private void button3_Click_1(object sender, System.EventArgs e)
		{
			ed_ref.sim_on=false;
			this.Close();
		}



		//	back
		private void button1_Click(object sender, System.EventArgs e)
		{
			if (ucData.Count!=0)
				ucData.RemoveAt(ucData.Count-1);
			if (ucData.Count!=0)
			{
				uc=new UC_AMD2909((UC_AMD2909)(ucData[ucData.Count-1]));
				if (contor.Count!=1)
				{
					for(int i=0;i<instructionData.Count;i++)					
						L_instr.UnSelect(i);
					contor.RemoveAt(contor.Count-1);
				}
				posProgram=uc.Executa();
			}
			else
			{
				posProgram=0;
				instr=new AMD_instruction();
				uc=new UC_AMD2909();
				uc.SetInstruction(instr);
				button1.Enabled=false;
				if (contor.Count!=1)
				{
					for(int i=0;i<instructionData.Count;i++)					
						L_instr.UnSelect(i);
					contor.RemoveAt(contor.Count-1);
				}
			}
			button2.Enabled=true;
			//table.setSelectionBackground(new Color(204,255,204));
			//table.setRowSelectionInterval((int)contor.get(contor.size()-1),(int)contor.get(contor.size()-1));
			if (contor.Count!=1)
			{
				L_instr.Select((int)(contor[contor.Count-1]));
			}
				//table.setRowSelectionInterval(((Integer)(contor.get(contor.size()-1))).intValue(),
				//	((Integer)(contor.get(contor.size()-1))).intValue());
			else
			{					
				L_instr.UnSelect(1);
				//	table.removeRowSelectionInterval(0,myModel.getRowCount()-1);
			}
				
			p.Text=convertBinary(uc.P,4);
			r.Text=convertBinary(uc.R,4);
			nonfe.Text=convertBinary(uc.microSecv.notFE,2);
			pup.Text=convertBinary(uc.microSecv.PUP,2);
			cin.Text=convertBinary(uc.microSecv.carryIn,1);
			cout.Text=convertBinary(uc.microSecv.carryOut,1);
			nonzero.Text=convertBinary(uc.microSecv.notZERO,1);
			or.Text=convertBinary(uc.microSecv.OR,4);
			d.Text=convertBinary(uc.microSecv.D,4);
			s1.Text=convertBinary(uc.microSecv.S1,2);
			s0.Text=convertBinary(uc.microSecv.S0,2);
			ist.Text=convertBinary(uc.microSecv.IS,2);
			stiva.SelectedIndex=uc.microSecv.IS;
			cmp.Text=convertBinary(uc.microSecv.CMP,3);
			varf.Text=convertBinary(uc.microSecv.topSTV,4);
			next.Text=convertBinary(posProgram,4);
				
				
			ram3.Text=convertBinary(uc.unitExec.RAM3,1);
			ram0.Text=convertBinary(uc.unitExec.RAM0,1);
			ramshift.Text=convertBinary(uc.unitExec.RAM,4);
			q3.Text=convertBinary(uc.unitExec.Q3,1);
			q0.Text=convertBinary(uc.unitExec.Q0,1);
			q.Text=convertBinary(uc.unitExec.Q,4);
				
			i86.Text=convertBinary(uc.unitExec.I86,3);
			i53.Text=convertBinary(uc.unitExec.I53,3);
			i20.Text=convertBinary(uc.unitExec.I20,3);
				
			adresaD.Text=convertBinary(uc.unitExec.D,4);
			adresaA.Text=convertBinary(uc.unitExec.Aadr,4);
			adresaB.Text=convertBinary(uc.unitExec.Badr,4);
				
			c.Text=convertBinary(uc.unitExec.carryIn,1);
			mux1.Text=convertBinary(uc.unitExec.MUX1,1);
			mux0.Text=convertBinary(uc.unitExec.MUX0,1);
				
			f.Text=convertBinary(uc.unitExec.F,4);
			y.Text=convertBinary(uc.unitExec.Y,4);
			nong.Text=convertBinary(uc.unitExec.nong,1);
			nonp.Text=convertBinary(uc.unitExec.nonp,1);
			cn4.Text=convertBinary(uc.unitExec.carryOut,1);
			ovr.Text=convertBinary(uc.unitExec.overFlow,1);
			f0.Text=convertBinary(uc.unitExec.zero,1);
			f3.Text=convertBinary(uc.unitExec.sign,1);
				
			for(int i=0;i<16;i++)
			{
				ramModel.replaceRow(i,new RamRow(i.ToString(),convertBinary(uc.unitExec.RAM16[i],4)));
			}
			for(int i=0;i<4;i++)
			{
				stiva.Items[i]=convertBinary(uc.microSecv.STV[i],4);
			}
				
			salt.Text=convertBinary(uc.generalInstr.R,4);
			micro.Text=convertBinary(uc.generalInstr.P,4);
			muxx1.Text=convertBinary(uc.generalInstr.MUX1,1);
			dest.Text=convertBinary(uc.generalInstr.I86,3);
			muxx0.Text=convertBinary(uc.generalInstr.MUX0,1);
			sursa.Text=convertBinary(uc.generalInstr.I20,3);
			cc.Text=convertBinary(uc.generalInstr.Cn,1);
			operatie.Text=convertBinary(uc.generalInstr.I53,3);
			adresaA.Text=convertBinary(uc.generalInstr.Aadr,4);
			adresaB.Text=convertBinary(uc.generalInstr.Badr,4);
			adresaD.Text=convertBinary(uc.generalInstr.Data,4);		
		}

		private void simulator_Closed(object sender, System.EventArgs e)
		{
			ed_ref.sim_on=false;
		}


	}


	//	Clasa pentru descrierea unui rand din tabelul RAM
	public class RamRow
	{
		public String numar, data;
		public RamRow(String snumar, String sdata)
		{
			numar=new String(snumar.ToCharArray());
			data=new String(sdata.ToCharArray());
		}
	}
}
