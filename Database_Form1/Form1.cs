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
using System.Configuration;
using System.Drawing.Imaging;


namespace Database_Form1
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SEM 4\ADT\ADT\Database_Form1\Database_Form1\Database1.mdf;Integrated Security=True");
        int selectedrow;
        
       

        private void Form1_Load(object sender, EventArgs e)                 // Form Load Event
        {
            // TODO: This line of code loads data into the 'database1DataSet3.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter3.Fill(this.database1DataSet3.Student);
            // TODO: This line of code loads data into the 'database1DataSet2.Student' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'database1DataSet1.Student' table. You can move, or remove it, as needed.
         //   this.studentTableAdapter1.Fill(this.database1DataSet1.Student);
            // TODO: This line of code loads data into the 'database1DataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.database1DataSet.Student);
            //// TODO: This line of code loads data into the 'studentsDataSet2.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter2.Fill(this.studentsDataSet2.Table);

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)            // Gridvew Display 
        {

            selectedrow = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Enroll_No"].Value.ToString());
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells[0].Value.ToString();
            textBox2.Text = row.Cells[1].Value.ToString();
            string ctext = row.Cells[2].Value.ToString();
            comboBox1.SelectedIndex = comboBox1.FindStringExact(ctext);
            textBox3.Text = row.Cells[3].Value.ToString();
      

        }

        private void button1_Click(object sender, EventArgs e)           // Insert Button
        {
            // SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SEM 4\ADT\PRACTICALS\Database_Form1\Database_Form1\Database1.mdf;Integrated Security=True");
            //SqlConnection con = new SqlConnection();
            // con.ConnectionString = ConfigurationManager.ConnectionStrings[@""].ToString();
        
            con.Open();
            int Enroll_No;
            string Branch, Address, Name, Sem, Gender;
            Enroll_No = Convert.ToInt32(textBox2.Text);
            Name = Convert.ToString(textBox3.Text);
            Branch = comboBox2.SelectedItem.ToString();
            Sem = comboBox1.SelectedItem.ToString();
            Address = Convert.ToString(textBox5.Text);
            Gender = "";
            if (radioButton1.Checked == true)
            {
                Gender = radioButton1.Text;
            }
            else
            {
                Gender = radioButton2.Text;
            }
            
            string q = "insert into Student values('" + Enroll_No + "', '" + Name + "', '" + Branch + "', '" + Sem + "','" + Address + "','" + Gender + "')";
          
            SqlCommand cmd = new SqlCommand(q, con);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("SuccessFully Inserted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

             private void button2_Click(object sender, EventArgs e)  // Update Button
        {
         //   SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SEM 4\ADT\PRACTICALS\Database_Form1\Database_Form1\Database1.mdf;Integrated Security=True");
            con.Open();
            string Gender = "";
            if (radioButton1.Checked == true)
            {
                Gender = radioButton1.Text;
            }
            else if (radioButton2.Checked == true)
            {
                Gender = radioButton2.Text;
            }
            else
            {
                Gender = null;
            }
            string q = "update Student set [Enroll_No]='" + textBox2.Text + "',[Name]='" + textBox3.Text + "',[Branch]='" + comboBox2.SelectedItem + "',[Sem]='" + comboBox1.SelectedItem + "',[Address]='" + textBox5.Text + "',[Gender]= '" + Gender + "' where Enroll_no = '" + textBox2.Text + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("SuccessFully Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)   // Delete Button
        {
            con.Open();
            string q = "delete from Student where Enroll_No=" + textBox2.Text + "";
            SqlCommand cmd1 = new SqlCommand(q, con);
            try
            {
                cmd1.ExecuteNonQuery();
                MessageBox.Show("SuccessFully Deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)      // Search Button
        {

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from Student", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();



        }

        private void button4_Click(object sender, EventArgs e)    // Search button
        {
            con.Open();
            int Enroll_No = Convert.ToInt32(textBox1.Text);
            SqlDataAdapter da = new SqlDataAdapter("select *from Student where Enroll_No = " + Enroll_No, con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                MessageBox.Show("Present in Database");
            }
            else
            {
                MessageBox.Show("Record does not exist in Database");
            }
           con.Close();
        }
    }



}
