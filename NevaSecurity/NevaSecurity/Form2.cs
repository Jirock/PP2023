using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NevaSecurity
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

            DataGridView1.DataSource = table;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            table.Clear();
            adapter.Fill(table);
            DataGridView1.DataSource = table;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (DataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"ФИО LIKE '%{textBox1.Text}%'";
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //adapter = new SqlDataAdapter($"SELECT * FROM Пользователи WHERE [_id] = {e.RowIndex}", sqlConnection);
            //table = new DataTable();
            // adapter.Fill(table);
            sqlConnection.Close();
            sqlConnection.Open();
            SqlCommand c = new SqlCommand($"SELECT * FROM Пользователи WHERE [_id] = {e.RowIndex+1}", sqlConnection);
            SqlDataReader dr;
            dr = c.ExecuteReader();

            string Name = "";

            while (dr.Read())
            {
                Name = $"id:{dr[0].ToString()}; ФИО:{dr[1].ToString()}; Номер телефона:{dr[2].ToString()}; Почта:{dr[3].ToString()}; Дата рождения:{dr[4].ToString()}; Данные паспорта:{dr[5].ToString()}; Логин:{dr[6].ToString()}; Пароль:{dr[7].ToString()}; Назначение:{dr[8].ToString()}";
            }
            sqlConnection.Close();
            MessageBox.Show(Name);


        }

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.пользователиTableAdapter.Fill(this.lessonpp_dbDataSet.Пользователи);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void пользователиBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.пользователиBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lessonpp_dbDataSet1);

        }
    }
}
