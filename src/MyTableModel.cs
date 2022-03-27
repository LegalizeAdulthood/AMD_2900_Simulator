using System;
using System.Data;
using System.Collections;

namespace simulator
{
	/// <summary>
	/// DataTable subclass used as data source for bindesg with the datagrid used to show the program being simulated 
	/// </summary>
	/// 
	public class MyTableModel : DataTable
	{
		String[] columnNames;
		ArrayList data;

		//===========================CONSTRUCTOR RECEIVING A DATA VECTOR AND A COLUMN NAME VECTOR==============
		public MyTableModel(ArrayList data, String[] columnNames)
		{	
			this.data=data;
			this.columnNames=columnNames;

			// creates table structure
			for(int i=0;i<columnNames.Length;i++)
				this.Columns.Add(columnNames[i],typeof(String));

			// inserts data in table
			DataRow r;
			for(int i=0;i<data.Count;i++)
			{
				r=this.NewRow();
				r[0]=((UserInstruction)data[i]).numar;
				r[1]=((UserInstruction)data[i]).salt;
				r[2]=((UserInstruction)data[i]).micro;
				r[3]=((UserInstruction)data[i]).mux;
				r[4]=((UserInstruction)data[i]).dest;
				r[5]=((UserInstruction)data[i]).sursa;
				r[6]=((UserInstruction)data[i]).c;
				r[7]=((UserInstruction)data[i]).operatie;
				r[8]=((UserInstruction)data[i]).adresaA;
				r[9]=((UserInstruction)data[i]).adresaB;
				r[10]=((UserInstruction)data[i]).adresaD;
				this.Rows.Add(r);
			}
		}



		//=========================DATA SET=====================================================
		public void setData(ArrayList data)
		{
			this.data.Clear();
			this.data.AddRange(data);

			if(Columns.Count==11)
			{
				DataRow r;
				for(int i=0;i<data.Count;i++)
				{
					r=this.NewRow();
					r[0]=((UserInstruction)data[i]).numar;
					r[1]=((UserInstruction)data[i]).salt;
					r[2]=((UserInstruction)data[i]).micro;
					r[3]=((UserInstruction)data[i]).mux;
					r[4]=((UserInstruction)data[i]).dest;
					r[5]=((UserInstruction)data[i]).sursa;
					r[6]=((UserInstruction)data[i]).c;
					r[7]=((UserInstruction)data[i]).operatie;
					r[8]=((UserInstruction)data[i]).adresaA;
					r[9]=((UserInstruction)data[i]).adresaB;
					r[10]=((UserInstruction)data[i]).adresaD;
					this.Rows.Add(r);
				}
			}

			
		}



		//=========================ADD ROW=========================================================
		public void addRow(int row, Object val)
		{
			// adds a new row at the end

			data.Add(val);
			if(Columns.Count==11)
			{
				DataRow r;
				r=NewRow();
				r[0]=((UserInstruction)val).numar;
				r[1]=((UserInstruction)val).salt;
				r[2]=((UserInstruction)val).micro;
				r[3]=((UserInstruction)val).mux;
				r[4]=((UserInstruction)val).dest;
				r[5]=((UserInstruction)val).sursa;
				r[6]=((UserInstruction)val).c;
				r[7]=((UserInstruction)val).operatie;
				r[8]=((UserInstruction)val).adresaA;
				r[9]=((UserInstruction)val).adresaB;
				r[10]=((UserInstruction)val).adresaD;
				this.Rows.Add(r);
			}
		}



		//========================INSERT ROW===============================
		public void insertRow(int row, Object val)
		{ 
			if(data.Count>row)
				data.Insert(row,val);

			if(Columns.Count==11 && Rows.Count > row)
			{
				DataRow r;
				r=NewRow();
				r[0]=((UserInstruction)val).numar;
				r[1]=((UserInstruction)val).salt;
				r[2]=((UserInstruction)val).micro;
				r[3]=((UserInstruction)val).mux;
				r[4]=((UserInstruction)val).dest;
				r[5]=((UserInstruction)val).sursa;
				r[6]=((UserInstruction)val).c;
				r[7]=((UserInstruction)val).operatie;
				r[8]=((UserInstruction)val).adresaA;
				r[9]=((UserInstruction)val).adresaB;
				r[10]=((UserInstruction)val).adresaD;
				this.Rows.InsertAt(r,row);			
			}
			
		}



		//===========================REPLACE ROW==============================================
		public void replaceRow(int row, Object val)
		{ 
			if(data.Count>row)
				data[row]=val;

			if(this.Columns.Count==11 && Rows.Count > row)
			{
				Rows[row][0]=((UserInstruction)val).numar;
				Rows[row][1]=((UserInstruction)val).salt;
				Rows[row][2]=((UserInstruction)val).micro;
				Rows[row][3]=((UserInstruction)val).mux;
				Rows[row][4]=((UserInstruction)val).dest;
				Rows[row][5]=((UserInstruction)val).sursa;
				Rows[row][6]=((UserInstruction)val).c;
				Rows[row][7]=((UserInstruction)val).operatie;
				Rows[row][8]=((UserInstruction)val).adresaA;
				Rows[row][9]=((UserInstruction)val).adresaB;
				Rows[row][10]=((UserInstruction)val).adresaD;			
			}
			
		}

		
		
		//===========================DELETE ROW===================================================
		public void deleteRow(int row)
		{ 
			if(row<data.Count && row<Rows.Count)
			{
				data.RemoveAt(row);
				Rows.RemoveAt(row);
			}
		
		}



		//===========================TABLE I/O FUNCTIONS==========================================
		public int getColumnCount() 
		{
			return columnNames.Length;
		}
		public int getRowCount() 
		{
			return data.Count;
		}
		public String getColumnName(int col) 
		{
			return columnNames[col];
		}
		public Object getElementAt(int row)
		{
			return data[row];
		}
		public Object getValueAt(int row, int col) 
		{
			UserInstruction temp=(UserInstruction)(data[row]);
			switch(col) 
			{
				case 0: return temp.numar;
				case 1: return temp.salt;
				case 2: return temp.micro;
				case 3: return temp.mux;
				case 4: return temp.dest;
				case 5: return temp.sursa;
				case 6: return temp.c;
				case 7: return temp.operatie;
				case 8: return temp.adresaA;
				case 9: return temp.adresaB;
				case 10: return temp.adresaD;
				default:
					return 0;
			}
			return "";
		}				
		
		public void setvalAt(Object val, int row, int col) 
		{
			if(Rows.Count<=row || Columns.Count <= col)
				return;
			UserInstruction temp=(UserInstruction)data[row];
			switch(col) 
			{
				case 0:
				{
					temp.numar=(String)val;					
					break;
				}
				case 1: 
				{
					temp.salt=(String)val;
					break;
				}
				case 2:
				{
					temp.micro=(String)val;
					break;
				}
				case 3:
				{
					temp.mux=(String)val;
					break;
				}
				case 4:
				{
					temp.dest=(String)val;
					break;
				}
				case 5:
				{
					temp.sursa=(String)val;
					break;
				}
				case 6:
				{
					temp.c=(String)val;
					break;
				}
				case 7:
				{
					temp.operatie=(String)val;
					break;
				}
				case 8:
				{
					temp.adresaA=(String)val;
					break;
				}
				case 9:
				{
					temp.adresaB=(String)val;
					break;
				}
				case 10:
				{
					temp.adresaD=(String)val;
					break;
				}
			}			
			data[row]=temp;
			Rows[row][col]=val;
		}
	}
}
