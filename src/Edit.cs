using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace simulator
{
	/// <summary>
	/// Editor window class
	/// </summary>
	public class Editor2 : System.Windows.Forms.Form
	{
		
		//true if there is an opened simulator window
		public bool sim_on=false;

		
		//strings for the contents of the comboboxes that make up the microinstruction
		String[] nrStrings={"0","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15"};
		String[] operatieStrings = { "ADD", "SUBR", "SUBS", "OR", "AND", "NOTRS", "EXOR", "EXNOR" };
		String[] sursaStrings = { "AQ", "AB", "ZQ", "ZB", "ZA", "DA", "DQ", "DZ" };
		String[] destStrings = { "QREG", "NOP", "RAMA", "RAMF", "RAMQD", "RAMD", "RAMQU", "RAMU" };
		String[] muxStrings = { "ZERO", "ROT", "ROTD", "SHD" };
		String[] microStrings = { "JRNZF", "JR", "CONT", "JD", "JSRNZF", "JSR", "RS", "JSTV", "TCPOZF", "PUCONT", "POCONT", "TCPOC", "JRZF", "JRF3", "JROVR", "JRC"};
		
		//editline=number of line to edit and editing is true after pushing the edit button
		int editLine=0;
		bool editing=false;
		//insertLine=number of line to insert and inserting is true after pushing the insert button
		int insertLine=-1;
		bool inserting=false;

		//program instruction data in numerical (instructionData) and string (userData) form for the simulator
		private ArrayList userData;
		private ArrayList instructionData;
		
		//editor window components
		private System.Windows.Forms.Panel top;
		private System.Windows.Forms.Panel edit;
		private System.Windows.Forms.Panel bottom;
		private System.Windows.Forms.Button save;
		private System.Windows.Forms.Button load;
		private System.Windows.Forms.Button exit;
		private System.Windows.Forms.Button insert;
		private System.Windows.Forms.Button delete;
		private System.Windows.Forms.Button test;
		private System.Windows.Forms.Button set_instr;
		private System.Windows.Forms.ComboBox minstr;
		private System.Windows.Forms.ComboBox salt;
		private System.Windows.Forms.ComboBox mux;
		private System.Windows.Forms.ComboBox data;
		private System.Windows.Forms.ComboBox addrb;
		private System.Windows.Forms.ComboBox addra;
		private System.Windows.Forms.ComboBox ual;
		private System.Windows.Forms.ComboBox c;
		private System.Windows.Forms.ComboBox sursa;
		private System.Windows.Forms.ComboBox dest;
		private System.Windows.Forms.Label lsalt;
		private System.Windows.Forms.Label lminstr;
		private System.Windows.Forms.Label lmux;
		private System.Windows.Forms.Label ldest;
		private System.Windows.Forms.Label lsursa;
		private System.Windows.Forms.Label lc;
		private System.Windows.Forms.Label lual;
		private System.Windows.Forms.Label laddra;
		private System.Windows.Forms.Label laddrb;
		private System.Windows.Forms.Label ldata;
		private System.Windows.Forms.Label bsursa;
		private System.Windows.Forms.Label bdest;
		private System.Windows.Forms.Label bmux;
		private System.Windows.Forms.Label bminstr;
		private System.Windows.Forms.Label bsalt;
		private System.Windows.Forms.Label bdata;
		private System.Windows.Forms.Label baddrb;
		private System.Windows.Forms.Label baddra;
		private System.Windows.Forms.Label bual;
		private System.Windows.Forms.Label bc;
		private System.Windows.Forms.OpenFileDialog openAMD;
		private System.Windows.Forms.SaveFileDialog saveAMD;
		private System.Data.DataSet dataset;
		private System.Data.DataTable amdProg;
		private System.Windows.Forms.DataGrid dataGrid;
		private System.Data.DataColumn tnr;
		private System.Data.DataColumn tsalt;
		private System.Data.DataColumn tminstr;
		private System.Data.DataColumn tmux;
		private System.Data.DataColumn tdest;
		private System.Data.DataColumn tsursa;
		private System.Data.DataColumn tc;
		private System.Data.DataColumn tual;
		private System.Data.DataColumn taddra;
		private System.Data.DataColumn taddrb;
		private System.Data.DataColumn tdata;
		private System.Data.DataView dataView;
		private System.Windows.Forms.ToolTip tt1;
		private System.Windows.Forms.Button about;
		private System.Windows.Forms.TextBox comment;
		private System.ComponentModel.IContainer components;

		public Editor2()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitializeInterface();


			// Sets ToolTips for comboboxes
			tt1.SetToolTip(salt,"Jump Address - will be loaded in the R register of the Control Unit (CU)");
			tt1.SetToolTip(minstr,"Specifies how the next microinstruction is fetched : \n JRNZF - Jump to R address R if F!=0 \n JR - Unconditioned jump to R address \n CONT - Continue \n JD - Jump to D address \n JSRNZF - Jump to subroutine with address R if F!=0 \n JSR - Jump to subroutine with address R \n RS - Return from subroutine \n "+
					"JSTV - Jump to the address at the top of the stack (without POP) \n TCPOZF - End cycle and POP if F=0 \n PUCONT - PUSH and CONTINUE \n POCONT - POP and CONTINUE \n TCPOC - End cycle and POP if Cn-4=1 \n JRZF - Jump to address R if F=0 \n JRF3 - Jump to address R if F3=1 \n JROVR - Jump to address R if OVR=1 \n JRC - Jump to address R if Cn-4=1");
			tt1.SetToolTip(mux,"Controls the RAMSHIFT and QSHIFT circuits");
			tt1.SetToolTip(dest,"ALU Destination : \n QREG - load Q<-F, output Y<-F \n NOP - output Y<-F \n RAMA - load B<-F , output Y<-A \n RAMF - load B<-F, output Y<-F \n"+
				" RAMQD - load B<-F/2 , Q<-Q/2, output Y<-F \n RAMD - load B<-F/2, output Y<-F \n RAMQU - load B<-2F, Q<-2Q, output Y<-F \n RAMU - load B<-2F, output Y<-F");
			tt1.SetToolTip(sursa,"Selects the source data for the ALU \n the inputs R and S are pears of A,B,D,Q or 0");
			tt1.SetToolTip(c,"Carry in - Carry in the least significant rank of the ALU");
			tt1.SetToolTip(ual,"Selects ALU function");
			tt1.SetToolTip(addra,"Address A");
			tt1.SetToolTip(addrb,"Address B");
			tt1.SetToolTip(data,"External data, integrated in the instruction");


		}

		
		//initializes the data in the comboboxes and the binary format labels under them
		private void InitializeInterface()
		{
			dataGrid.AllowDrop=false;
			dataGrid.AllowNavigation=false;
			//init program data vectors
			userData=new ArrayList();//instructions in string format
			instructionData=new ArrayList();//instructions in binary format
		

			//init comboboxes
			this.salt.Items.AddRange(nrStrings);
			this.salt.SelectedIndex=0;
			this.data.Items.AddRange(nrStrings);
			this.data.SelectedIndex=0;
			this.addrb.Items.AddRange(nrStrings);
			this.addrb.SelectedIndex=0;
			this.addra.Items.AddRange(nrStrings);
			this.addra.SelectedIndex=0;
			this.ual.Items.AddRange(operatieStrings);
			this.ual.SelectedIndex=0;
			this.sursa.Items.AddRange(sursaStrings);
			this.sursa.SelectedIndex=0;
			this.dest.Items.AddRange(destStrings);
			this.dest.SelectedIndex=0;
			this.mux.Items.AddRange(muxStrings);
			this.mux.SelectedIndex=0;
			this.minstr.Items.AddRange(microStrings);
			this.minstr.SelectedIndex=0;
			this.c.SelectedIndex=0;
			
			//init labels
			this.bsalt.Text=convertBinary(0,4);
			this.bminstr.Text=convertBinary(0,4);
			this.bmux.Text=convertBinary(0,2);
			this.bdest.Text=convertBinary(0,3);
			this.bsursa.Text=convertBinary(0,3);
			this.bual.Text=convertBinary(0,3);
			this.bc.Text=convertBinary(0,1);
			this.baddra.Text=convertBinary(0,4);
			this.baddrb.Text=convertBinary(0,4);
			this.bdata.Text=convertBinary(0,4);
		}

		
		//converts numar to binary form on bits number of bits
		public String convertBinary(int numar, int bits)
		{
			String str="";
			int nr=numar;
			for(int i=0;i<bits;i++)
			{
				str=nr%2 + str;
				nr=nr/2;
			}
			return str;
		}


		//constructs the amd instruction from the current selection in the comboboxes (used when set button is pressed)
		public AMD_instruction getAMD()
		{
			AMD_instruction amd=new AMD_instruction();
			amd.R=salt.SelectedIndex;
			amd.P=minstr.SelectedIndex;
			amd.MUX1=mux.SelectedIndex/2;
			amd.MUX0=mux.SelectedIndex%2;
			amd.Cn=c.SelectedIndex;
			amd.I86=dest.SelectedIndex;
			amd.I53=ual.SelectedIndex;
			amd.I20=sursa.SelectedIndex;
			amd.Aadr=addra.SelectedIndex;
			amd.Badr=addrb.SelectedIndex;
			amd.Data=data.SelectedIndex;
			return amd;
		}


		//gets the strings from a UserInstruction 
		public Object[] getStrings(UserInstruction usr)
		{
			Object[] sa=new Object[11];
			sa[0]=Convert.ToInt32(usr.numar.Remove(0,1));
			sa[1]=usr.salt;
			sa[2]=usr.micro;
			sa[3]=usr.mux;
			sa[4]=usr.dest;
			sa[5]=usr.sursa;
			sa[6]=usr.c;
			sa[7]=usr.operatie;
			sa[8]=usr.adresaA;
			sa[9]=usr.adresaB;
			sa[10]=usr.adresaD;
			return sa;
		
		}

		
		//sets the comboboxes to the instruction indicated in UserInstruction (used to update the comboboxes content when editing a line)
		public void initInstruction(UserInstruction instr)
		{
			addra.SelectedItem=instr.adresaA;
			addrb.SelectedItem=instr.adresaB;
			data.SelectedItem=instr.adresaD;
			dest.SelectedItem=instr.dest;
			c.SelectedItem=instr.c;
			minstr.SelectedItem=instr.micro;
			mux.SelectedItem=instr.mux;
			ual.SelectedItem=instr.operatie;
			salt.SelectedItem=instr.salt;
			sursa.SelectedItem=instr.sursa;
		}
		
		//sets the comboboxes to the first item
		public void initZeroInstruction()
		{
			
			this.salt.SelectedIndex=0;
			this.data.SelectedIndex=0;
			this.addrb.SelectedIndex=0;
			this.addra.SelectedIndex=0;
			this.ual.SelectedIndex=0;
			this.sursa.SelectedIndex=0;
			this.dest.SelectedIndex=0;
			this.mux.SelectedIndex=0;
			this.minstr.SelectedIndex=0;
			this.c.SelectedIndex=0;
		}

		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.top = new System.Windows.Forms.Panel();
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.amdProg = new System.Data.DataTable();
			this.tnr = new System.Data.DataColumn();
			this.tsalt = new System.Data.DataColumn();
			this.tminstr = new System.Data.DataColumn();
			this.tmux = new System.Data.DataColumn();
			this.tdest = new System.Data.DataColumn();
			this.tsursa = new System.Data.DataColumn();
			this.tc = new System.Data.DataColumn();
			this.tual = new System.Data.DataColumn();
			this.taddra = new System.Data.DataColumn();
			this.taddrb = new System.Data.DataColumn();
			this.tdata = new System.Data.DataColumn();
			this.dataView = new System.Data.DataView();
			this.edit = new System.Windows.Forms.Panel();
			this.comment = new System.Windows.Forms.TextBox();
			this.bdata = new System.Windows.Forms.Label();
			this.baddrb = new System.Windows.Forms.Label();
			this.baddra = new System.Windows.Forms.Label();
			this.bual = new System.Windows.Forms.Label();
			this.bc = new System.Windows.Forms.Label();
			this.bsursa = new System.Windows.Forms.Label();
			this.bdest = new System.Windows.Forms.Label();
			this.bmux = new System.Windows.Forms.Label();
			this.bminstr = new System.Windows.Forms.Label();
			this.bsalt = new System.Windows.Forms.Label();
			this.ldata = new System.Windows.Forms.Label();
			this.laddrb = new System.Windows.Forms.Label();
			this.laddra = new System.Windows.Forms.Label();
			this.lual = new System.Windows.Forms.Label();
			this.lc = new System.Windows.Forms.Label();
			this.lsursa = new System.Windows.Forms.Label();
			this.ldest = new System.Windows.Forms.Label();
			this.lmux = new System.Windows.Forms.Label();
			this.lminstr = new System.Windows.Forms.Label();
			this.lsalt = new System.Windows.Forms.Label();
			this.salt = new System.Windows.Forms.ComboBox();
			this.data = new System.Windows.Forms.ComboBox();
			this.addrb = new System.Windows.Forms.ComboBox();
			this.addra = new System.Windows.Forms.ComboBox();
			this.ual = new System.Windows.Forms.ComboBox();
			this.c = new System.Windows.Forms.ComboBox();
			this.sursa = new System.Windows.Forms.ComboBox();
			this.dest = new System.Windows.Forms.ComboBox();
			this.mux = new System.Windows.Forms.ComboBox();
			this.minstr = new System.Windows.Forms.ComboBox();
			this.set_instr = new System.Windows.Forms.Button();
			this.delete = new System.Windows.Forms.Button();
			this.insert = new System.Windows.Forms.Button();
			this.bottom = new System.Windows.Forms.Panel();
			this.about = new System.Windows.Forms.Button();
			this.test = new System.Windows.Forms.Button();
			this.exit = new System.Windows.Forms.Button();
			this.load = new System.Windows.Forms.Button();
			this.save = new System.Windows.Forms.Button();
			this.openAMD = new System.Windows.Forms.OpenFileDialog();
			this.saveAMD = new System.Windows.Forms.SaveFileDialog();
			this.dataset = new System.Data.DataSet();
			this.tt1 = new System.Windows.Forms.ToolTip(this.components);
			this.top.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.amdProg)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
			this.edit.SuspendLayout();
			this.bottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
			this.SuspendLayout();
			// 
			// top
			// 
			this.top.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.top.AutoScroll = true;
			this.top.Controls.Add(this.dataGrid);
			this.top.Location = new System.Drawing.Point(0, 0);
			this.top.Name = "top";
			this.top.Size = new System.Drawing.Size(864, 306);
			this.top.TabIndex = 0;
			// 
			// dataGrid
			// 
			this.dataGrid.AllowNavigation = false;
			this.dataGrid.AllowSorting = false;
			this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGrid.CaptionVisible = false;
			this.dataGrid.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.amdProg, "Nr"));
			this.dataGrid.DataMember = "";
			this.dataGrid.DataSource = this.dataView;
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.ReadOnly = true;
			this.dataGrid.SelectionBackColor = System.Drawing.Color.White;
			this.dataGrid.SelectionForeColor = System.Drawing.Color.DarkBlue;
			this.dataGrid.Size = new System.Drawing.Size(864, 304);
			this.dataGrid.TabIndex = 0;
			this.dataGrid.Click += new System.EventHandler(this.dataGrid_Click);
			this.dataGrid.DoubleClick += new System.EventHandler(this.dataGrid_DoubleClick);
			// 
			// amdProg
			// 
			this.amdProg.Columns.AddRange(new System.Data.DataColumn[] {
																		   this.tnr,
																		   this.tsalt,
																		   this.tminstr,
																		   this.tmux,
																		   this.tdest,
																		   this.tsursa,
																		   this.tc,
																		   this.tual,
																		   this.taddra,
																		   this.taddrb,
																		   this.tdata});
			this.amdProg.TableName = "AmdProg";
			// 
			// tnr
			// 
			this.tnr.AllowDBNull = false;
			this.tnr.Caption = "Instruction No.";
			this.tnr.ColumnName = "Nr";
			this.tnr.DataType = typeof(int);
			// 
			// tsalt
			// 
			this.tsalt.Caption = "Jump";
			this.tsalt.ColumnName = "Jump";
			// 
			// tminstr
			// 
			this.tminstr.ColumnName = "µInstr";
			// 
			// tmux
			// 
			this.tmux.ColumnName = "Mux";
			// 
			// tdest
			// 
			this.tdest.ColumnName = "Dest";
			// 
			// tsursa
			// 
			this.tsursa.Caption = "Source";
			this.tsursa.ColumnName = "Source";
			// 
			// tc
			// 
			this.tc.Caption = "Carry";
			this.tc.ColumnName = "Carry";
			// 
			// tual
			// 
			this.tual.Caption = "ALU";
			this.tual.ColumnName = "ALU";
			// 
			// taddra
			// 
			this.taddra.Caption = "Address A";
			this.taddra.ColumnName = "Address A";
			// 
			// taddrb
			// 
			this.taddrb.Caption = "Address B";
			this.taddrb.ColumnName = "Address B";
			// 
			// tdata
			// 
			this.tdata.ColumnName = "Data";
			// 
			// dataView
			// 
			this.dataView.AllowDelete = false;
			this.dataView.AllowEdit = false;
			this.dataView.AllowNew = false;
			this.dataView.ApplyDefaultSort = true;
			this.dataView.Sort = "Nr";
			this.dataView.Table = this.amdProg;
			// 
			// edit
			// 
			this.edit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.edit.Controls.Add(this.comment);
			this.edit.Controls.Add(this.bdata);
			this.edit.Controls.Add(this.baddrb);
			this.edit.Controls.Add(this.baddra);
			this.edit.Controls.Add(this.bual);
			this.edit.Controls.Add(this.bc);
			this.edit.Controls.Add(this.bsursa);
			this.edit.Controls.Add(this.bdest);
			this.edit.Controls.Add(this.bmux);
			this.edit.Controls.Add(this.bminstr);
			this.edit.Controls.Add(this.bsalt);
			this.edit.Controls.Add(this.ldata);
			this.edit.Controls.Add(this.laddrb);
			this.edit.Controls.Add(this.laddra);
			this.edit.Controls.Add(this.lual);
			this.edit.Controls.Add(this.lc);
			this.edit.Controls.Add(this.lsursa);
			this.edit.Controls.Add(this.ldest);
			this.edit.Controls.Add(this.lmux);
			this.edit.Controls.Add(this.lminstr);
			this.edit.Controls.Add(this.lsalt);
			this.edit.Controls.Add(this.salt);
			this.edit.Controls.Add(this.data);
			this.edit.Controls.Add(this.addrb);
			this.edit.Controls.Add(this.addra);
			this.edit.Controls.Add(this.ual);
			this.edit.Controls.Add(this.c);
			this.edit.Controls.Add(this.sursa);
			this.edit.Controls.Add(this.dest);
			this.edit.Controls.Add(this.mux);
			this.edit.Controls.Add(this.minstr);
			this.edit.Controls.Add(this.set_instr);
			this.edit.Controls.Add(this.delete);
			this.edit.Controls.Add(this.insert);
			this.edit.Location = new System.Drawing.Point(0, 306);
			this.edit.Name = "edit";
			this.edit.Size = new System.Drawing.Size(864, 208);
			this.edit.TabIndex = 1;
			// 
			// comment
			// 
			this.comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.comment.Location = new System.Drawing.Point(16, 8);
			this.comment.Multiline = true;
			this.comment.Name = "comment";
			this.comment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.comment.Size = new System.Drawing.Size(832, 48);
			this.comment.TabIndex = 34;
			this.comment.Text = "insert program comments here";
			// 
			// bdata
			// 
			this.bdata.Location = new System.Drawing.Point(776, 144);
			this.bdata.Name = "bdata";
			this.bdata.Size = new System.Drawing.Size(32, 16);
			this.bdata.TabIndex = 33;
			this.bdata.Text = "Data";
			// 
			// baddrb
			// 
			this.baddrb.Location = new System.Drawing.Point(680, 144);
			this.baddrb.Name = "baddrb";
			this.baddrb.Size = new System.Drawing.Size(40, 16);
			this.baddrb.TabIndex = 32;
			this.baddrb.Text = "AddrB";
			// 
			// baddra
			// 
			this.baddra.Location = new System.Drawing.Point(600, 144);
			this.baddra.Name = "baddra";
			this.baddra.Size = new System.Drawing.Size(40, 16);
			this.baddra.TabIndex = 31;
			this.baddra.Text = "AddrA";
			// 
			// bual
			// 
			this.bual.Location = new System.Drawing.Point(504, 144);
			this.bual.Name = "bual";
			this.bual.Size = new System.Drawing.Size(32, 16);
			this.bual.TabIndex = 30;
			this.bual.Text = "UAL";
			// 
			// bc
			// 
			this.bc.Location = new System.Drawing.Point(424, 144);
			this.bc.Name = "bc";
			this.bc.Size = new System.Drawing.Size(16, 16);
			this.bc.TabIndex = 29;
			this.bc.Text = "C";
			// 
			// bsursa
			// 
			this.bsursa.Location = new System.Drawing.Point(336, 144);
			this.bsursa.Name = "bsursa";
			this.bsursa.Size = new System.Drawing.Size(40, 16);
			this.bsursa.TabIndex = 28;
			this.bsursa.Text = "Sursa";
			// 
			// bdest
			// 
			this.bdest.Location = new System.Drawing.Point(264, 144);
			this.bdest.Name = "bdest";
			this.bdest.Size = new System.Drawing.Size(32, 16);
			this.bdest.TabIndex = 27;
			this.bdest.Text = "Dest";
			// 
			// bmux
			// 
			this.bmux.Location = new System.Drawing.Point(192, 144);
			this.bmux.Name = "bmux";
			this.bmux.Size = new System.Drawing.Size(32, 16);
			this.bmux.TabIndex = 26;
			this.bmux.Text = "Mux";
			// 
			// bminstr
			// 
			this.bminstr.Location = new System.Drawing.Point(96, 144);
			this.bminstr.Name = "bminstr";
			this.bminstr.Size = new System.Drawing.Size(40, 16);
			this.bminstr.TabIndex = 25;
			this.bminstr.Text = "µInstr";
			// 
			// bsalt
			// 
			this.bsalt.Location = new System.Drawing.Point(32, 144);
			this.bsalt.Name = "bsalt";
			this.bsalt.Size = new System.Drawing.Size(40, 16);
			this.bsalt.TabIndex = 24;
			this.bsalt.Text = "Salt";
			// 
			// ldata
			// 
			this.ldata.Location = new System.Drawing.Point(776, 104);
			this.ldata.Name = "ldata";
			this.ldata.Size = new System.Drawing.Size(32, 16);
			this.ldata.TabIndex = 23;
			this.ldata.Text = "Data";
			// 
			// laddrb
			// 
			this.laddrb.Location = new System.Drawing.Point(672, 104);
			this.laddrb.Name = "laddrb";
			this.laddrb.Size = new System.Drawing.Size(56, 16);
			this.laddrb.TabIndex = 22;
			this.laddrb.Text = "Address B";
			// 
			// laddra
			// 
			this.laddra.Location = new System.Drawing.Point(592, 104);
			this.laddra.Name = "laddra";
			this.laddra.Size = new System.Drawing.Size(56, 16);
			this.laddra.TabIndex = 21;
			this.laddra.Text = "Address A";
			// 
			// lual
			// 
			this.lual.Location = new System.Drawing.Point(504, 104);
			this.lual.Name = "lual";
			this.lual.Size = new System.Drawing.Size(32, 16);
			this.lual.TabIndex = 20;
			this.lual.Text = "ALU";
			// 
			// lc
			// 
			this.lc.Location = new System.Drawing.Point(416, 104);
			this.lc.Name = "lc";
			this.lc.Size = new System.Drawing.Size(32, 16);
			this.lc.TabIndex = 19;
			this.lc.Text = "Carry";
			// 
			// lsursa
			// 
			this.lsursa.Location = new System.Drawing.Point(336, 104);
			this.lsursa.Name = "lsursa";
			this.lsursa.Size = new System.Drawing.Size(40, 16);
			this.lsursa.TabIndex = 18;
			this.lsursa.Text = "Source";
			// 
			// ldest
			// 
			this.ldest.Location = new System.Drawing.Point(248, 104);
			this.ldest.Name = "ldest";
			this.ldest.Size = new System.Drawing.Size(64, 16);
			this.ldest.TabIndex = 17;
			this.ldest.Text = "Destination";
			// 
			// lmux
			// 
			this.lmux.Location = new System.Drawing.Point(192, 104);
			this.lmux.Name = "lmux";
			this.lmux.Size = new System.Drawing.Size(32, 16);
			this.lmux.TabIndex = 16;
			this.lmux.Text = "Mux";
			// 
			// lminstr
			// 
			this.lminstr.Location = new System.Drawing.Point(80, 104);
			this.lminstr.Name = "lminstr";
			this.lminstr.Size = new System.Drawing.Size(64, 16);
			this.lminstr.TabIndex = 15;
			this.lminstr.Text = "µInstruction";
			// 
			// lsalt
			// 
			this.lsalt.Location = new System.Drawing.Point(32, 104);
			this.lsalt.Name = "lsalt";
			this.lsalt.Size = new System.Drawing.Size(40, 16);
			this.lsalt.TabIndex = 14;
			this.lsalt.Text = "Jump";
			// 
			// salt
			// 
			this.salt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.salt.Location = new System.Drawing.Point(24, 120);
			this.salt.Name = "salt";
			this.salt.Size = new System.Drawing.Size(48, 21);
			this.salt.TabIndex = 13;
			this.salt.SelectedIndexChanged += new System.EventHandler(this.salt_SelectedIndexChanged);
			// 
			// data
			// 
			this.data.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.data.Location = new System.Drawing.Point(760, 120);
			this.data.Name = "data";
			this.data.Size = new System.Drawing.Size(72, 21);
			this.data.TabIndex = 12;
			this.data.SelectedIndexChanged += new System.EventHandler(this.data_SelectedIndexChanged);
			// 
			// addrb
			// 
			this.addrb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.addrb.Location = new System.Drawing.Point(664, 120);
			this.addrb.Name = "addrb";
			this.addrb.Size = new System.Drawing.Size(72, 21);
			this.addrb.TabIndex = 11;
			this.addrb.SelectedIndexChanged += new System.EventHandler(this.addrb_SelectedIndexChanged);
			// 
			// addra
			// 
			this.addra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.addra.Location = new System.Drawing.Point(584, 120);
			this.addra.Name = "addra";
			this.addra.Size = new System.Drawing.Size(72, 21);
			this.addra.TabIndex = 10;
			this.addra.SelectedIndexChanged += new System.EventHandler(this.addra_SelectedIndexChanged);
			// 
			// ual
			// 
			this.ual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ual.Location = new System.Drawing.Point(480, 120);
			this.ual.Name = "ual";
			this.ual.Size = new System.Drawing.Size(80, 21);
			this.ual.TabIndex = 9;
			this.ual.SelectedIndexChanged += new System.EventHandler(this.ual_SelectedIndexChanged);
			// 
			// c
			// 
			this.c.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c.Items.AddRange(new object[] {
												   "0",
												   "1"});
			this.c.Location = new System.Drawing.Point(416, 120);
			this.c.Name = "c";
			this.c.Size = new System.Drawing.Size(40, 21);
			this.c.TabIndex = 8;
			this.c.SelectedIndexChanged += new System.EventHandler(this.c_SelectedIndexChanged);
			// 
			// sursa
			// 
			this.sursa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.sursa.Location = new System.Drawing.Point(320, 120);
			this.sursa.Name = "sursa";
			this.sursa.Size = new System.Drawing.Size(72, 21);
			this.sursa.TabIndex = 7;
			this.sursa.SelectedIndexChanged += new System.EventHandler(this.sursa_SelectedIndexChanged);
			// 
			// dest
			// 
			this.dest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dest.Location = new System.Drawing.Point(248, 120);
			this.dest.Name = "dest";
			this.dest.Size = new System.Drawing.Size(64, 21);
			this.dest.TabIndex = 6;
			this.dest.SelectedIndexChanged += new System.EventHandler(this.dest_SelectedIndexChanged);
			// 
			// mux
			// 
			this.mux.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mux.Location = new System.Drawing.Point(176, 120);
			this.mux.Name = "mux";
			this.mux.Size = new System.Drawing.Size(64, 21);
			this.mux.TabIndex = 5;
			this.mux.SelectedIndexChanged += new System.EventHandler(this.mux_SelectedIndexChanged);
			// 
			// minstr
			// 
			this.minstr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.minstr.Location = new System.Drawing.Point(80, 120);
			this.minstr.Name = "minstr";
			this.minstr.Size = new System.Drawing.Size(72, 21);
			this.minstr.TabIndex = 4;
			this.minstr.SelectedIndexChanged += new System.EventHandler(this.minstr_SelectedIndexChanged);
			// 
			// set_instr
			// 
			this.set_instr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.set_instr.Location = new System.Drawing.Point(16, 174);
			this.set_instr.Name = "set_instr";
			this.set_instr.TabIndex = 2;
			this.set_instr.Text = "S&et";
			this.set_instr.Click += new System.EventHandler(this.set_instr_Click);
			// 
			// delete
			// 
			this.delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.delete.Location = new System.Drawing.Point(784, 72);
			this.delete.Name = "delete";
			this.delete.TabIndex = 1;
			this.delete.Text = "&Delete Line";
			this.delete.Click += new System.EventHandler(this.delete_Click);
			// 
			// insert
			// 
			this.insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.insert.Location = new System.Drawing.Point(696, 72);
			this.insert.Name = "insert";
			this.insert.TabIndex = 0;
			this.insert.Text = "&Insert Line";
			this.insert.Click += new System.EventHandler(this.insert_Click);
			// 
			// bottom
			// 
			this.bottom.Controls.Add(this.about);
			this.bottom.Controls.Add(this.test);
			this.bottom.Controls.Add(this.exit);
			this.bottom.Controls.Add(this.load);
			this.bottom.Controls.Add(this.save);
			this.bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottom.Location = new System.Drawing.Point(0, 515);
			this.bottom.Name = "bottom";
			this.bottom.Size = new System.Drawing.Size(864, 48);
			this.bottom.TabIndex = 2;
			// 
			// about
			// 
			this.about.Location = new System.Drawing.Point(24, 16);
			this.about.Name = "about";
			this.about.TabIndex = 4;
			this.about.Text = "&About...";
			this.about.Click += new System.EventHandler(this.about_Click);
			// 
			// test
			// 
			this.test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.test.BackColor = System.Drawing.SystemColors.Control;
			this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.test.Location = new System.Drawing.Point(504, 16);
			this.test.Name = "test";
			this.test.TabIndex = 3;
			this.test.Text = "&TEST";
			this.test.Click += new System.EventHandler(this.test_Click);
			// 
			// exit
			// 
			this.exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.exit.Location = new System.Drawing.Point(792, 16);
			this.exit.Name = "exit";
			this.exit.Size = new System.Drawing.Size(56, 23);
			this.exit.TabIndex = 2;
			this.exit.Text = "E&xit";
			this.exit.Click += new System.EventHandler(this.exit_Click);
			// 
			// load
			// 
			this.load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.load.Location = new System.Drawing.Point(720, 16);
			this.load.Name = "load";
			this.load.Size = new System.Drawing.Size(56, 23);
			this.load.TabIndex = 1;
			this.load.Text = "&Load";
			this.load.Click += new System.EventHandler(this.load_Click);
			// 
			// save
			// 
			this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.save.Location = new System.Drawing.Point(648, 16);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(56, 23);
			this.save.TabIndex = 0;
			this.save.Text = "&Save";
			this.save.Click += new System.EventHandler(this.save_Click);
			// 
			// openAMD
			// 
			this.openAMD.Filter = "AMD File|*.amd|All files|*.*";
			this.openAMD.InitialDirectory = ".";
			this.openAMD.Title = "Load AMD Program";
			// 
			// saveAMD
			// 
			this.saveAMD.DefaultExt = "amd";
			this.saveAMD.Filter = "AMD File|*.amd|All files|*.*";
			this.saveAMD.InitialDirectory = ".";
			this.saveAMD.Title = "Save AMD program";
			// 
			// dataset
			// 
			this.dataset.DataSetName = "AMD";
			this.dataset.Locale = new System.Globalization.CultureInfo("en-US");
			this.dataset.Tables.AddRange(new System.Data.DataTable[] {
																		 this.amdProg});
			// 
			// tt1
			// 
			this.tt1.AutoPopDelay = 10000;
			this.tt1.InitialDelay = 500;
			this.tt1.ReshowDelay = 100;
			// 
			// Editor2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(864, 563);
			this.Controls.Add(this.bottom);
			this.Controls.Add(this.edit);
			this.Controls.Add(this.top);
			this.Name = "Editor2";
			this.Text = "AMD#";
			this.top.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.amdProg)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
			this.edit.ResumeLayout(false);
			this.bottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataset)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			Application.Run(new Editor2());

		}

		
		//window closing event
		private void exit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		
		//comboboxes events

		private void salt_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bsalt.Text=convertBinary(this.salt.SelectedIndex,4);
		}

		private void minstr_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bminstr.Text=convertBinary(this.minstr.SelectedIndex,4);
		}

		private void mux_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bmux.Text=convertBinary(this.mux.SelectedIndex,2);
		}

		private void dest_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bdest.Text=convertBinary(this.dest.SelectedIndex,3);
		}

		private void sursa_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bsursa.Text=convertBinary(this.sursa.SelectedIndex,3);
		}

		private void c_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bc.Text=convertBinary(this.c.SelectedIndex,1);
		}

		private void ual_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bual.Text=convertBinary(this.ual.SelectedIndex,3);
		}

		private void addra_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.baddra.Text=convertBinary(this.addra.SelectedIndex,4);
		}

		private void addrb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.baddrb.Text=convertBinary(this.addrb.SelectedIndex,4);
		}

		private void data_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.bdata.Text=convertBinary(this.data.SelectedIndex,4);
		}

		
		//a line can be edited by doubleclicking on it
		private void dataGrid_DoubleClick(object sender, System.EventArgs e)
		{
			if(!inserting)//cannot edit a line while inserting
			{
				dataGrid.AllowNavigation=false;
				editing=true;
				editLine = dataGrid.CurrentRowIndex;
				initInstruction((UserInstruction)userData[editLine]);
				dataGrid.SelectionBackColor=Color.White;
				dataGrid.SelectionForeColor=Color.Red;
			}
		}

		
		//sets the instruction currently created in the comboboxes
		//3 cases : while inserting line ; while editing line ; non of the above (inserting a new line at the end of the program)
		private void set_instr_Click(object sender, System.EventArgs e)
		{
			if(!inserting&&amdProg.Rows.Count>15 || inserting&&amdProg.Rows.Count>16)
			{
				System.Windows.Forms.MessageBox.Show("You have reached the maximum allowed program length for the 4-bit Control Unit");

			}
			else
			{
				if(editing&&!inserting)
				{
					AMD_instruction amd=getAMD();
					instructionData.RemoveAt(editLine);
					instructionData.Insert(editLine,amd);
					UserInstruction usr=new UserInstruction(amd,"#"+editLine);
					userData.RemoveAt(editLine);
					userData.Insert(editLine,usr);
					amdProg.Rows.RemoveAt(editLine);
					System.Data.DataRow dr=amdProg.NewRow();
					dr.ItemArray=getStrings(usr);
					amdProg.Rows.InsertAt(dr,editLine);
					editing=false;
					//reset comboboxes
					initZeroInstruction();
					dataGrid.SelectionBackColor=Color.White;
					dataGrid.SelectionForeColor=Color.DarkBlue;
				
					return;
				}
				if(!editing&&inserting)
				{
					AMD_instruction amd=getAMD();
				
					instructionData.Insert(insertLine,amd);
					UserInstruction usr=new UserInstruction(amd,"#"+insertLine);
				
					userData.Insert(insertLine,usr);
					amdProg.Rows.RemoveAt(insertLine);
				
					System.Data.DataRow dr=amdProg.NewRow();
					dr.ItemArray=getStrings(usr);
					amdProg.Rows.InsertAt(dr,insertLine);
					inserting=false;
					//reset comboboxes
					initZeroInstruction();
					dataGrid.SelectionBackColor=Color.White;
					dataGrid.SelectionForeColor=Color.DarkBlue;
					return;
				}
				if(!editing&&!inserting)
				{
					AMD_instruction amd=getAMD();
					instructionData.Add(amd);
					UserInstruction usr=new UserInstruction(amd,"#"+userData.Count);
					userData.Add(usr);
					amdProg.Rows.Add(getStrings(usr));
				
				
				}
			}

			
		}

		
		//insert button clicked event
		private void insert_Click(object sender, System.EventArgs e)
		{
			if(!inserting&&!editing&&amdProg.Rows.Count>0)//reactioneaza numai la prima apasare a butonului; daca e in cursul editarii nu face nimic
			{
				
				inserting=true;
				insertLine=dataGrid.CurrentRowIndex;
				System.Data.DataRow dr=amdProg.NewRow();
				dr.ItemArray=getStrings(new UserInstruction("#"+insertLine));
				amdProg.Rows.InsertAt(dr,insertLine);
											
				for(int i=insertLine+1;i<amdProg.Rows.Count;i++)
				{
					amdProg.Rows[i]["Nr"]=i;
				}
			
				dataGrid.Select(insertLine);
				dataGrid.SelectionBackColor=Color.White;
				dataGrid.SelectionForeColor=Color.Red;
			}
		}

		
		//delete button clicked event
		private void delete_Click(object sender, System.EventArgs e)
		{
			if (!editing&&!inserting)//!inserting to be sure noone deletes an empty line being inserted
			{
				if(amdProg.Rows.Count>0)
				{
					int index=dataGrid.CurrentRowIndex;
				
					amdProg.Rows.RemoveAt(index);
					
					for(int i=index;i<amdProg.Rows.Count;i++)
					{
						amdProg.Rows[i]["Nr"]=i;
						
					}
					
					instructionData.RemoveAt(index);
					userData.RemoveAt(index);
				}
			}
			
		}

		
		//test button clicked event ; starts the simulation
		private void test_Click(object sender, System.EventArgs e)
		{
			if(!inserting && !editing && instructionData.Count>0 && !sim_on)
			{
				simulator sim=new simulator(instructionData, userData, this);
				sim_on=true;
				sim.Show();
			}
		}

		
		//save button clicked event ; saves the current program in binary format
		private void save_Click(object sender, System.EventArgs e)
		{
			if(saveAMD.ShowDialog(this)==DialogResult.OK)
			{
				Editor2.ActiveForm.Text="AMD# - "+saveAMD.FileName;
				writeFile(saveAMD.FileName);
			}
		}

		
		//load button clicked event ; loads a previously saved program
		private void load_Click(object sender, System.EventArgs e)
		{
			if(openAMD.ShowDialog(this)==DialogResult.OK)
			{	
				comment.Text="insert program comments here";
				Editor2.ActiveForm.Text="AMD# - "+openAMD.FileName;
				readFile(openAMD.FileName);
				
				
			}
		}

		
		//writes data when saving to file
		private void writeFile(string fn)
		{
			FileStream fs=new FileStream(fn, FileMode.Create, FileAccess.Write);
			BinaryWriter br=new BinaryWriter(fs);
			br.Write((int)instructionData.Count);
			
			//lame way of writing objects to file
			for(int i=0;i<instructionData.Count;i++)
			{
				//AMD_instruction amd=(AMD_instruction)instructionData[i];
				br.Write((int)((AMD_instruction)instructionData[i]).Aadr);
				br.Write((int)((AMD_instruction)instructionData[i]).Badr);
				br.Write((int)((AMD_instruction)instructionData[i]).Cn);
				br.Write((int)((AMD_instruction)instructionData[i]).Data);
				br.Write((int)((AMD_instruction)instructionData[i]).I20);
				br.Write((int)((AMD_instruction)instructionData[i]).I53);
				br.Write((int)((AMD_instruction)instructionData[i]).I86);
				br.Write((int)((AMD_instruction)instructionData[i]).MUX0);
				br.Write((int)((AMD_instruction)instructionData[i]).MUX1);
				br.Write((int)((AMD_instruction)instructionData[i]).P);
				br.Write((int)((AMD_instruction)instructionData[i]).R);
			
			
			}
			br.Write((string) comment.Text);
			
			br.Close();
			fs.Close();
		}
		
		
		//reads data from file
		private void readFile(string fn)
		{
			FileStream fs=new FileStream(fn, FileMode.Open, FileAccess.Read);
			BinaryReader br=new BinaryReader(fs);
			int len=0;
			try
			{
				len=br.ReadInt32();
					
				instructionData.Clear();
				for(int i=0;i<len;i++)
				{
					AMD_instruction amd=new AMD_instruction();
					amd.Aadr=br.ReadInt32();
					amd.Badr=br.ReadInt32();
					amd.Cn=br.ReadInt32();
					amd.Data=br.ReadInt32();
					amd.I20=br.ReadInt32();
					amd.I53=br.ReadInt32();
					amd.I86=br.ReadInt32();
					amd.MUX0=br.ReadInt32();
					amd.MUX1=br.ReadInt32();
					amd.P=br.ReadInt32();
					amd.R=br.ReadInt32();
					instructionData.Add(amd);

				
				}
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show("The file format is incorrect!\n"+e.Message);
				

			}
			try
			{	
				comment.Text=br.ReadString();
			}
			catch(Exception e)
			{}
			
				br.Close();
				fs.Close();

				userData.Clear();
				amdProg.Clear();
				for(int i=0;i<len;i++)
				{
					userData.Add(new UserInstruction((AMD_instruction)instructionData[i],"#"+i));
					amdProg.Rows.Add(getStrings((UserInstruction)userData[i]));
				}
			
			
			
		}

		
		//single click on the data grid event
		private void dataGrid_Click(object sender, System.EventArgs e)
		{
			
			if(inserting)	//leaves the currently inserted line and deletes it
			{
				if(dataGrid.CurrentRowIndex!=insertLine)
				{
					inserting=false;
					amdProg.Rows.RemoveAt(insertLine);
					for(int i=insertLine;i<amdProg.Rows.Count;i++)
					{
						amdProg.Rows[i]["Nr"]=i;
					}
					dataGrid.SelectionBackColor=Color.White;
					dataGrid.SelectionForeColor=Color.DarkBlue;
				}

				
			}
			if(editing)		//lets you unselect the line being edited without changing its content
			{
				if(dataGrid.CurrentRowIndex!=editLine)
				{
					editing=false;
					dataGrid.SelectionBackColor=Color.White;
					dataGrid.SelectionForeColor=Color.DarkBlue;
				}
				
			}
		}

		
		//about button
		private void about_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show("\t\t AMD 2091 ALU and 2909 CU Simulator \n\n\t\t\t\t by \n\n Sorin Toma (email: sorintoma50@xnet.ro) and Vlad Ion (email: vladion@home.ro) \n\n Project Coordinator: Ph.D. Decebal Popescu (email: decebal@csit-sun.pub.ro) \n\n University Politehnica of Bucharest \n Computer Science Faculty \n 2003 (c) \n\n Additional Info: http://rg.csit-sun.pub.ro/","AMD 2091 ALU and 2909 CU Simulator");
		}
	}
}
