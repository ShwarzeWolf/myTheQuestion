/* связываешь с бд
 * пишешь простые запросы на отображение выборки
 * связываешь их с методами
 * реализуешь методы добавления вопроса
 * реализуешь методы добавления ответа
 * 
 * тестишь
 * рефакторишь
 * красиво оформляешь 
 * 
 * на выходе команда получает три приложения, каждое из которых делает свою часть 
 * радуем Хлопотова
 * profit
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyTheQuestion
{

    public partial class Form1 : Form
    {

        SqlConnection sqlConnection;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            #region addingListBox
            ListBox listBox1 = new ListBox();
          
            listBox1.Size = new System.Drawing.Size(200, 100);
            listBox1.Location = new System.Drawing.Point(10, 10);
            this.Controls.Add(listBox1);
            listBox1.MultiColumn = true;
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            #endregion

            #region establishingConnectionWithDatabase
            string connectionString = @"Data Source=DESKTOP-4FH9G41\SQLEXPRESS;Initial Catalog=Store;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [CATEGORY]", sqlConnection);
            #endregion

            try
            {
                sqlReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["categoryName"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString());
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
            }
        }
    }    
}
