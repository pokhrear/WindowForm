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

namespace WindowBookFormApplication
{
    public partial class Form3 : Form
    {
        string connectionString = null;
        SqlConnection cnn;
        SqlCommand command;

        public Form3()
        {
            InitializeComponent();
            fillCombobox();
        }

        void fillCombobox()
        {
            try
            {
                connectionString = "Data Source=LAPTOP-66DUCL3F\\SQLEXPRESS;Initial Catalog = MegaBookDB; Integrated Security = SSPI; Persist Security Info = false";

                cnn = new SqlConnection(connectionString);

                string queryString = "SELECT b.Book_Name, r.Reviewer_Name FROM dbo.BOOKS b, dbo.REVIEWS r WHERE b.Book_ID = r.Book_ID";
                    

                cnn.Open();
                command = new SqlCommand(queryString, cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    this.comboBox1.Items.Add(reader["Book_Name"].ToString());
                    this.comboBox2.Items.Add(reader["Reviewer_Name"].ToString());

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error in SQL!" + ex.Message);
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connectionString = "Data Source=LAPTOP-66DUCL3F\\SQLEXPRESS;Initial Catalog = MegaBookDB; Integrated Security = SSPI; Persist Security Info = false";

                cnn = new SqlConnection(connectionString);

                string queryString = "SELECT b.Book_ID, b.Author_Name, b.Publish_Date, b.ISBN, r.Review, r.Review_date, r.Rating FROM dbo.BOOKS b INNER JOIN dbo.REVIEWS r ON b.Book_ID=r.Book_ID WHERE b.Book_Name ='" + comboBox1.Text + "' and r.Reviewer_Name='"+comboBox2.Text+"'";
                cnn.Open();
                command = new SqlCommand(queryString, cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    String bookid = reader.GetInt32(0).ToString();
                    String aname = reader.GetString(1);

                    String pDate = reader.GetDateTime(2).ToString();
                    String isbn = reader.GetString(3).ToString();

                    String rView = reader.GetString(4);
                    String rDate = reader.GetDateTime(5).ToString();

                    String rating = reader.GetInt32(6).ToString();

                    textBox1.Text = aname;
                    textBox2.Text = pDate;
                    textBox3.Text = isbn;
                    textBox4.Text = bookid;
                    textBox5.Text = rView;
                    textBox6.Text = rating;
                    textBox7.Text = rDate;

                }


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error in SQL!" + ex.Message);
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }

            }
        }
    }
}
