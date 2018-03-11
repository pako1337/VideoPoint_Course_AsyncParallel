using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace WinForms_Async
{
    public partial class BestProductReport : Form
    {
        public BestProductReport()
        {
            InitializeComponent();
        }

        private void btnRaport_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AdventureWorks2017;Data Source=localhost\\SQLEXPRESS"))
            {
                connection.Open();

                var queryContent = File.ReadAllText("./Report.sql");
                var reportCommand = new SqlCommand(queryContent);
                reportCommand.Connection = connection;

                var resultReader = reportCommand.ExecuteReader();

                var resultTable = new DataTable();
                resultTable.Load(resultReader);

                resultGrid.DataSource = resultTable;
            }
        }
    }
}
