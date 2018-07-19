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
    public partial class Form1 : Form
    {
        string connectionString = null;
        SqlConnection cnn;
        SqlCommand command;

        public Form1()
        {
         //   InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectionString = "Data Source=LAPTOP-66DUCL3F\\SQLEXPRESS;Initial Catalog = MegaBookDB; Integrated Security = SSPI; Persist Security Info = false";
           
            cnn = new SqlConnection(connectionString);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string queryString = "select*from dbo.BOOKS";

            try
            {
                cnn.Open();
                command = new SqlCommand(queryString, cnn);
                SqlDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();

                while (reader.Read()) {
                    this.listBox1.Items.Add(reader["Book_Name"].ToString() + "-" + reader["Author_Name"].ToString() + "-" + reader["Publish_Date"].ToString() + "-"
                        + reader["ISBN"].ToString() + "-" + reader["Book_ID"].ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                command = new SqlCommand("Insert into dbo.BOOKS values(@bookname, @authorname, @publishdate, @isbn)", cnn);

                command.Parameters.AddWithValue("@bookname", this.textBox1.Text);
                command.Parameters.AddWithValue("@authorname", this.textBox2.Text);
                command.Parameters.AddWithValue("@publishdate", this.dateTimePicker1.Text);
                command.Parameters.AddWithValue("@isbn", this.textBox4.Text);
               // command.Parameters.AddWithValue("@bookid", int.Parse(this.textBox6.Text));


                int r = command.ExecuteNonQuery();

                MessageBox.Show(r + "Book record added!!!");

                textBox1.Clear();
                textBox2.Clear();              
                textBox4.Clear();
               

                cnn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error in SQL! " + ex.Message);
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                command = new SqlCommand("UPDATE BOOKS SET Book_Name=@bookname, Author_Name=@authorname, Publish_Date=@publishdate, ISBN=@isbn WHERE Book_ID= @bookid", cnn);

                command.Parameters.AddWithValue("@bookname", this.textBox1.Text);
                command.Parameters.AddWithValue("@authorname", this.textBox2.Text);
                command.Parameters.AddWithValue("@publishdate", this.dateTimePicker1.Text);
                command.Parameters.AddWithValue("@isbn", this.textBox4.Text);
                //textBox6.ReadOnly = true;
                //textBox.BackColor = System.Drawing.SystemColors.Window;
                command.Parameters.AddWithValue("@bookid", int.Parse(this.textBox6.Text));

                int r = command.ExecuteNonQuery();

                MessageBox.Show(r + " Books record updated");

                textBox1.Clear();
                textBox2.Clear();
                textBox4.Clear();

                cnn.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("error in SQl!" + ex.Message);
            }

            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                command = new SqlCommand("DELETE FROM BOOKS WHERE Book_ID=@bookid", cnn);

                command.Parameters.AddWithValue("@bookid", int.Parse(this.textBox6.Text));

                int r = command.ExecuteNonQuery();

                MessageBox.Show(r + " Books record deleted");

                cnn.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("You are not allowed to delet from here attached to review");
            }

            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}
