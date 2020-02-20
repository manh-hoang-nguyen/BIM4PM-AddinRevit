using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Utils.RevitUtils
{
   public static class ScheduleUtil
    {
      public  class ScheduleDataParser
        {
            /// <summary>
            /// Default schedule data file field delimiter.
            /// </summary>
            static char[] _tabs = new char[] { '\t' };

            /// <summary>
            /// Strip the quotes around text strings 
            /// in the schedule data file.
            /// </summary>
            static char[] _quotes = new char[] { '"' };

            string _name = null;
            DataTable _table = null;

            /// <summary>
            /// Schedule name
            /// </summary>
            public string Name
            {
                get { return _name; }
            }

            /// <summary>
            /// Schedule columns and row data
            /// </summary>
            public DataTable Table
            {
                get { return _table; }
            }
            /// <summary>
            /// https://thebuildingcoder.typepad.com/blog/2012/05/the-schedule-api-and-access-to-schedule-data.html
            /// </summary>
            /// <param name="filename"></param>
            public ScheduleDataParser(string filename)
            {
                StreamReader stream = File.OpenText(filename);
                
                string line;
                string[] a;

                while (null != (line = stream.ReadLine()))
                {
                    a = line
                      .Split(_tabs)
                      .Select<string, string>(s => s.Trim(_quotes))
                      .ToArray();

                    // First line of text file contains 
                    // schedule name

                    if (null == _name)
                    {
                        _name = a[0];
                        continue;
                    }

                    // Second line of text file contains 
                    // schedule column names

                    if (null == _table)
                    {
                        _table = new DataTable();

                        foreach (string column_name in a)
                        {
                            DataColumn column = new DataColumn();
                            column.DataType = typeof(string);
                            column.ColumnName = column_name;
                            _table.Columns.Add(column);
                        }

                        _table.BeginLoadData();

                        continue;
                    }

                    // Remaining lines define schedula data

                    DataRow dr = _table.LoadDataRow(a, true);
                }
                _table.EndLoadData();
                stream.Close();
            }
        }
    }
}
