using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace CarDelear
{
    public partial class CarInfo : Form
    {
        public CarInfo()
        {
            InitializeComponent();
            this.CenterToScreen();
            comboBox1.SelectedIndex = 0;
        }

        SqlConnection con = new SqlConnection("Data Source=MARCOS;Initial Catalog=CarDealer;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        private void load_data()
        {
            cmd = new SqlCommand("Select * from CarDealerForm",con);
            da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void NClear()
        {
            txtcarmake.Text = txtcarmodel.Text = txtmiles.Text = txtprice.Text = txtvin.Text = txtyear.Text = "";
            comboBox1.SelectedIndex = 0;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CarInfo_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void CarInfo_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            NClear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView1.Rows[index];
            txtvin.Text = selectedrow.Cells[0].Value.ToString();
            txtcarmake.Text = selectedrow.Cells[1].Value.ToString();
            txtcarmodel.Text = selectedrow.Cells[2].Value.ToString();
            txtmiles.Text = selectedrow.Cells[3].Value.ToString();
            txtyear.Text = selectedrow.Cells[4].Value.ToString();
            txtprice.Text = selectedrow.Cells[5].Value.ToString();
            comboBox1.SelectedItem = selectedrow.Cells[6].Value.ToString();
        }

        private void Parameters()
        {
            cmd.Parameters.AddWithValue("vin", txtvin.Text);
            cmd.Parameters.AddWithValue("carmake", txtcarmake.Text);
            cmd.Parameters.AddWithValue("carmodel", txtcarmodel.Text);
            cmd.Parameters.AddWithValue("miles", txtmiles.Text);
            cmd.Parameters.AddWithValue("year", txtyear.Text);
            cmd.Parameters.AddWithValue("price", txtprice.Text);
            string lottxt = comboBox1.GetItemText(comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("lot", lottxt);

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtvin.Text) || String.IsNullOrEmpty(txtcarmake.Text) || String.IsNullOrEmpty(txtcarmodel.Text) || String.IsNullOrEmpty(txtmiles.Text) ||
                String.IsNullOrEmpty(txtprice.Text) || String.IsNullOrEmpty(txtyear.Text) || comboBox1.SelectedIndex == 0 )
            {
                MessageBox.Show("Complete all the required fields!");
                return;
            }
            else
            {
                cmd = new SqlCommand("Insert into CarDealerForm values (@vin, @carmake, @carmodel, @miles, @year, @price, @lot)", con);
                Parameters();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                load_data();
                MessageBox.Show("Record Added sucessfully");
                NClear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
           
            if (String.IsNullOrEmpty(txtvin.Text) || String.IsNullOrEmpty(txtcarmake.Text) || String.IsNullOrEmpty(txtcarmodel.Text) || String.IsNullOrEmpty(txtmiles.Text) ||
               String.IsNullOrEmpty(txtprice.Text) || String.IsNullOrEmpty(txtyear.Text) || comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Complete all the required fields!");
                return;
            }
            else
            {
                cmd = new SqlCommand("Update CarDealerForm set Vin = @vin, Car_Make = @carmake, Car_Model = @carmodel," +
                    "Miles = @miles, Year = @year, Price = @price,Lot# = @lot where Vin = @vin", con);
                Parameters();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                load_data();
                MessageBox.Show("Record Updated sucessfully");
                NClear();
            }
            
        }

        private void txtmiles_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar))
            {

                TextBox textBox = (TextBox)sender;

                if (textBox.Text.IndexOf('.') > -1 &&
                         textBox.Text.Substring(textBox.Text.IndexOf('.')).Length >= 3)
                {
                    e.Handled = true;
                }

            }

        }

        private void txtyear_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
                if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace         
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
        
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar))
            {

                TextBox textBox = (TextBox)sender;

                if (textBox.Text.IndexOf('.') > -1 &&
                         textBox.Text.Substring(textBox.Text.IndexOf('.')).Length >= 3)
                {
                    e.Handled = true;
                }

            }
        }

        private void txtvin_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
                if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace         
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Delete from CarDealerForm where Vin = @vin", con);
            Parameters();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            load_data();
            MessageBox.Show("Record Deleted sucessfully");
            NClear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from CarDealerForm Order by Car_Make", con);
            da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from CarDealerForm where Vin Like @search + '%' or Car_Make Like @search + '%' or Car_Model Like @search + '%' " +
                "or Miles Like @search + '%' or Year Like @search + '%' or Price Like @search + '%' or Lot# Like @search + '%' ", con);
            cmd.Parameters.AddWithValue("search", txtsearch.Text);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource= dt;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
    }
}