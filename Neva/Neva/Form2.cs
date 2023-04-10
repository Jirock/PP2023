using Neva.lessonpp_dbDataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Neva
{
    public partial class Form2 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter adapter = null;
        private DataTable table = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "lessonpp_dbDataSet1.Пользователи". При необходимости она может быть перемещена или удалена.
            this.пользователиTableAdapter.Fill(this.lessonpp_dbDataSet1.Пользователи);
            sqlConnection = new SqlConnection(@"Data Source=AVCHOR;Initial Catalog=lessonpp_db;Integrated Security=True");
            sqlConnection.Open();

            adapter = new SqlDataAdapter("SELECT * FROM Пользователи", sqlConnection);
            table = new DataTable();
            adapter.Fill(table);

            DataGridView.DataSource = table;
        }
         
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            table.Clear();
            adapter.Fill(table);
            DataGridView.DataSource = table;
        }

        private void пользователиBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.пользователиBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lessonpp_dbDataSet1);

        }
    }
}
