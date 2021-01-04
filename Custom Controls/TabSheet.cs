using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Petrol_Somewhat_Unlimited_Ltd.Custom_Controls
{
    public partial class TabSheet : UserControl
    {
        private SqlConnection _connection;
        private string _connectionString, _query;
        private string _table;
        private Sheet Sheet;

        public TabSheet(Point point, Size size, string tableName)
        {
            InitializeComponent();
            Size = size; //set size
            Location = point; //set location (needs to be specific for table placement
            _table = tableName;

            _connectionString = ConfigurationManager
                .ConnectionStrings["Petrol_Somewhat_Unlimited_Ltd.Properties.Settings.ServiceLogsConnectionString"]
                .ConnectionString;
            Visible = false;
            //Optimal double buffer, causes notable delay between counter count and table count, but not by much.
            //Basically allows for rendering to be smoother by pre-calculating the graphics in a buffer before printing to screen
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); 
            InitSheet(); //Create sheet
            Sheet.SaveTable += SaveTable;
        }

        public void InjectData(string s)
        {

            switch (_table) //Select the connected table
            {
                case "Serviced":
                    _query =
                        $"INSERT INTO Serviced ([ID],[License Plate],[Vehicle Type],[Fuel Type],[Pump Used],[Fuel Purchased],[Fuel Cost],[Date],[Time]) VALUES ({s})";
                    break;
                case "Failed":
                    _query =
                        $"INSERT INTO Failed ([ID],[License Plate],[Vehicle Type],[Fuel Type],[Date],[Time]) VALUES ({s})";
                    break;
            }

            using (_connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(_query, _connection))
            {
                _connection.Open();
                command.ExecuteScalar();
            }
            ReadTable();
        }

        public void ReadTable()
        {

            _query = $"SELECT TOP 1 * FROM {_table} ORDER BY [ID] DESC"; //Set query up to table

            using (_connection = new SqlConnection(_connectionString))
            using (SqlCommand command  = new SqlCommand(_query,_connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable dataTable = new DataTable($"{_table}");
               adapter.Fill(dataTable);
                Sheet.InjectData(dataTable);
                dataTable.Dispose();
                Refresh();
                
                
            }
        }

        public void SaveTable(object source, EventArgs e)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();



                _query = $"SELECT * FROM {_table}"; //Set query up to table

                using (_connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(_query, _connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable($"{_table}");
                    adapter.Fill(dataTable);


                    StreamWriter wr = new StreamWriter($@"{currentDirectory}\{_table}.xls");



                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        wr.Write(dataTable.Columns[i].ToString().ToUpper() + "\t");
                    }

                    wr.WriteLine();

                    //write rows to excel file
                    for (int i = 0; i < (dataTable.Rows.Count); i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            if (dataTable.Rows[i][j] != null)
                            {
                                wr.Write(Convert.ToString(dataTable.Rows[i][j]) + "\t");
                            }
                            else
                            {
                                wr.Write("\t");
                            }
                        }
                        //go to next line
                        wr.WriteLine();
                    }
                    //close file
                    wr.Close();



                    MessageBox.Show($"File saved to {currentDirectory}", "Save Table", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Asterisk);
                }
            }
            catch
                (Exception exception)
            {
                MessageBox.Show($"File failed to save\n{exception.ToString()}", "Save Table", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Asterisk);
            }



        }

        private void InitSheet()
        {
            //Handle the creation/location of objects for columns
            _query = $"SELECT * FROM {_table}"; //Set query up to table
            
            using (_connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(_query, _connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable dataTable = new DataTable($"{_table}");
                adapter.Fill(dataTable);
                

                var columnNameList = new List<string>();

                foreach (DataColumn c in dataTable.Columns)
                {
                    //Create columns with titles
                    //Create a field with a fixed limit of 20 previous transactions
                    columnNameList.Add(c.ToString());

                }

                Sheet = new Sheet(columnNameList, Size);
                Sheet.DataChange += DataChange;
                Controls.Add(Sheet.CustomButtonList[2]); //Add the sheet buttons to the user-control
                Controls.Add(Sheet.CustomButtonList[1]);
                Controls.Add(Sheet.CustomButtonList[0]);
            }

            


        }

        public void DataChange(object source, EventArgs e)
        {
            Refresh();
            
        }

        public void PaintSheet(object source, PaintEventArgs e)
        {
            var g = e.Graphics;
            Sheet.Paint(g); //Paint the sheet
        }
    }
    }

