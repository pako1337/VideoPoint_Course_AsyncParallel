using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WinForms_Async
{
    public partial class BestProductReport : Form
    {
        private const string ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AdventureWorks2017;Data Source=localhost\\SQLEXPRESS";

        public BestProductReport()
        {
            InitializeComponent();
        }

        private void btnRaport_Click(object sender, EventArgs e)
        {
            using (var connection = CreateOpenConnection())
            {
                SqlCommand reportCommand = CreateReportCommand(connection);

                var resultReader = reportCommand.ExecuteReader();

                resultGrid.DataSource = LoadDataIntoDataTable(resultReader);
            }
        }

        private static DataTable LoadDataIntoDataTable(SqlDataReader resultReader)
        {
            var resultTable = new DataTable();
            resultTable.Load(resultReader);
            return resultTable;
        }

        private static SqlConnection CreateOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }

        private static SqlCommand CreateReportCommand(SqlConnection connection)
        {
            var queryContent = File.ReadAllText("./Report.sql");
            var reportCommand = new SqlCommand(queryContent);
            reportCommand.Connection = connection;
            return reportCommand;
        }

        private void btnThread_Click(object sender, EventArgs e)
        {
            var syncContext = SynchronizationContext.Current;
            var thread = new Thread(GenerateReportInNewThread);
            thread.Start(syncContext);
        }
        
        private void GenerateReportInNewThread(object state)
        {
            var syncContext = (SynchronizationContext)state;
            using (var connection = CreateOpenConnection())
            {
                SqlCommand reportCommand = CreateReportCommand(connection);

                var resultReader = reportCommand.ExecuteReader();

                var newThreadResult = LoadDataIntoDataTable(resultReader);

                syncContext.Post(dt =>
                {
                    resultGrid.DataSource = dt;
                }, newThreadResult);

                //resultGrid.BeginInvoke((Action)(() =>
                //{
                //    resultGrid.DataSource = newThreadResult;
                //}));
            }
        }

        private void btnThreadPool_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using (var connection = CreateOpenConnection())
                {
                    SqlCommand reportCommand = CreateReportCommand(connection);

                    var resultReader = reportCommand.ExecuteReader();

                    var newThreadResult = LoadDataIntoDataTable(resultReader);

                    resultGrid.BeginInvoke((Action)(() =>
                    {
                        resultGrid.DataSource = newThreadResult;
                    }));
                }
            });
        }
    }
}
