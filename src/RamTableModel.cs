using System;
using System.Collections;
using System.Data;

namespace simulator
{
	/// <summary>
	/// DataTable subclass describing ram content
	/// same as myTableModel
	/// </summary>

	
	
	public class RamTableModel : DataTable
	{
		String[] columnNames;
		ArrayList data;

		public RamTableModel(ArrayList data, String[] columnNames)
		{
			this.data=data;
			this.columnNames=columnNames;
			// creating schema for the table
			for(int i=0;i<columnNames.Length;i++)
				this.Columns.Add(columnNames[i],typeof(String));
			
			// populating table
			DataRow r;
			for(int i=0;i<data.Count;i++)
			{
				r=this.NewRow();
				r[0]=((RamRow)data[i]).numar;
				r[1]=((RamRow)data[i]).data;				
				this.Rows.Add(r);
			}
		}

		public void setData(ArrayList adata)
		{
			this.data.Clear();
			this.data.AddRange(adata);

			Rows.Clear();
			DataRow r;
			for(int i=0;i<data.Count;i++)
			{
				r=this.NewRow();
				r[0]=((RamRow)data[i]).numar;
				r[1]=((RamRow)data[i]).data;				
				this.Rows.Add(r);
			}
		}
		public void addRow(int row, Object val)
		{
			data.Add(val);
			if(Columns.Count==2)
			{
				DataRow r;
				r=NewRow();
				r[0]=((RamRow)val).numar;
				r[1]=((RamRow)val).data;	
				this.Rows.Add(r);
			}
		}
		public void insertRow(int row, Object val)
		{ 
			data.Insert(row,val);
			
			DataRow r;
			r=NewRow();
			r[0]=((RamRow)val).numar;
			r[1]=((RamRow)val).data;	
			Rows.InsertAt(r,row);
		}

		public void replaceRow(int row, Object val)
		{ 
			data[row]=val;

			if(this.Columns.Count==2 && Rows.Count > row)
			{
				Rows[row][0]=((RamRow)val).numar;
				Rows[row][1]=((RamRow)val).data;
			}
		}
		public void deleteRow(int row)
		{ 
			if(row < Rows.Count)
			{
				data.RemoveAt(row);
				Rows.RemoveAt(row);
			}
		}
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
			if(row<data.Count)
				return (RamRow)(data[row]);
			else
				return null;
		}
		public Object getValueAt(int row, int col) 
		{
			if(row<Rows.Count && col<Columns.Count)
			{
				RamRow temp=(RamRow)(data[row]);
				switch(col) 
				{
					case 0: return temp.numar;
					case 1: return temp.data;
				}
			}
			return "";
		}

		public void setValueAt(Object val, int row, int col) 
		{
			if(row<Rows.Count && col<Rows.Count)
			{
				RamRow temp=(RamRow)(data[row]);
				switch(col) 
				{
					case 0:
					{
						temp.numar=(String)val;
						break;
					}
					case 1:
					{
						temp.data=(String)val;
						break;
					}
				}
				data[row]=temp;
				Rows[row][col]=val;
			}
		}
	}
}
