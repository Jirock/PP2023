using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Neva
{
    public partial class Form1 : Form
    {
        Database database = new Database();

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            tb_code.MaxLength = 50;
        }

        internal static void ShowDataGridViewRow(DataGridViewRow currentRow)
        {
            throw new NotImplementedException();
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
           
            string code = tb_code.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();   
            DataTable table = new DataTable();

            string querystring = $"select ид, [Код сотрудника] from Сотрудники where [Код сотрудника] = '{code}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count == 1)
            {
                Form2 form = new Form2();
                form.ShowDialog();
                this.Show();
            }
            else
            {
                if(tb_code.Text.Length == 0)
                {
                    MessageBox.Show("Пустое поле");
                }
                else
                {
                    MessageBox.Show("Некорректный ввод");
                }
            }
        }
    }
}
