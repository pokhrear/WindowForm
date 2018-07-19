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
    public partial class Form2 : Form
    {
        string connectionString = null;
        SqlConnection cnn;
        SqlCommand command;

        public Form2()
        {
            InitializeComponent();
            Fillcombo();
        }

        void Fillcombo()
        {
            
            try
            {
                connectionString = "Data Source=LAPTOP-66DUCL3F\\SQLEXPRESS;Initial Catalog = MegaBookDB; Integrated Security = SSPI; Persist Security Info = false";

                cnn = new SqlConnection(connectionString);

                string queryString = "select Book_Name from dbo.BOOKS";
                cnn.Open();
                command = new SqlCommand(queryString, cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    this.comboBox1.Items.Add(reader["Book_Name"].ToString());

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

        private void Form2_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                command = new SqlCommand("Insert into dbo.REVIEWS values( @bookid, @reviewername, @review, DEFAULT, @rating)", cnn);

               // command.Parameters.AddWithValue("@reviewid", int.Parse(this.textBox4.Text));
                command.Parameters.AddWithValue("@bookid", int.Parse(this.textBox5.Text));
                command.Parameters.AddWithValue("@reviewername", this.textBox1.Text);
                command.Parameters.AddWithValue("@review", this.textBox2.Text);
                //command.Parameters.AddWithValue("@reviewdate", Convert.ToDateTime(this.textBox6.Text).ToString());
                command.Parameters.AddWithValue("@rating", this.textBox3.Text);
                


                int r = command.ExecuteNonQuery();

                MessageBox.Show(r + "Review added!!!");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox5.Clear();
                
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connectionString = "Data Source=LAPTOP-66DUCL3F\\SQLEXPRESS;Initial Catalog = MegaBookDB; Integrated Security = SSPI; Persist Security Info = false";

                cnn = new SqlConnection(connectionString);

                string queryString = "select Book_ID from dbo.BOOKS where Book_Name ='"+comboBox1.Text+"' ";
                cnn.Open();
                command = new SqlCommand(queryString, cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    String bookid = reader.GetInt32(0).ToString();
                    textBox5.Text=bookid;  
 
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                command = new SqlCommand("DELETE FROM REVIEWS WHERE Review_ID=@rid", cnn);

                command.Parameters.AddWithValue("@rid", int.Parse(this.textBox4.Text));

                int r = command.ExecuteNonQuery();

                MessageBox.Show(r + " Review record deleted");

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
            string queryString = "select*from dbo.REVIEWS";

            try
            {
                cnn.Open();
                command = new SqlCommand(queryString, cnn);
                SqlDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();
                while (reader.Read())
                {
                    this.listBox1.Items.Add(reader["Review_ID"].ToString() + " - " + reader["Book_ID"].ToString() + " - " + reader["Reviewer_Name"].ToString() + " - "
                        + reader["Review"].ToString() + " - " + reader["Review_Date"].ToString()+" - "+ reader["Rating"].ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                command = new SqlCommand("UPDATE REVIEWS SET Reviewer_Name=@rname, Review=@rev, Rating=@rating WHERE Review_ID= @rid", cnn);

                command.Parameters.AddWithValue("@rname", this.textBox1.Text);
                command.Parameters.AddWithValue("@rev", this.textBox2.Text);
                command.Parameters.AddWithValue("@rating", int.Parse(this.textBox3.Text));
               // command.Parameters.AddWithValue("@isbn", this.textBox4.Text);
                command.Parameters.AddWithValue("@rid", int.Parse(this.textBox4.Text));

                int r = command.ExecuteNonQuery();

                MessageBox.Show(r + " Review record updated");


                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }
    }
}
