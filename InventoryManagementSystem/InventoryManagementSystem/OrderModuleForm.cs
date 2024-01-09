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

namespace InventoryManagementSystem
{

    public partial class OrderModuleForm : Form
    {
        int qty = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONCAT(cid,cname) LIKE '%" +txtSearchCust.Text+"%'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();

        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid,pname,pprice,pdescription,pcategory) LIKE '%" + txtSearchProd.Text + "%'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();

        }



        private void OrderModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct(); 
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            


        }

        private void txtSearchCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

      
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt16(txtQty.Value) > qty)
            {
                MessageBox.Show("Stok miktarı yeterli değil!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQty.Value = txtQty.Value - 1;
                return;
            }
            if (Convert.ToInt16(txtQty.Value) >0)
            {
                int total = Convert.ToInt16(txtPprice.Text) * Convert.ToInt16(txtQty.Value);
                txtTotal.Text = total.ToString();

            }
           
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCld.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPprice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            // qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void dgvProduct_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCld.Text == "")
                {
                    MessageBox.Show("Lütfen müşteri seçin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPid.Text == "")
                {
                    MessageBox.Show("Lütfen bir ürün seçin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                    if (MessageBox.Show("Bu siparişi eklemek istediğinizden emin misiniz?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("INSERT INTO tbOrder(odate, pid, cid, qty, price, total)VALUES(@odate, @pid, @cid, @qty, @price, @total)", con);
                        cmd.Parameters.AddWithValue("@odate", dtOrder.Value);
                        cmd.Parameters.AddWithValue("@pid", Convert.ToInt16(txtPid.Text));
                        cmd.Parameters.AddWithValue("@cid", Convert.ToInt16(txtCld.Text));
                        cmd.Parameters.AddWithValue("@qty", Convert.ToInt16(txtQty.Text));
                        cmd.Parameters.AddWithValue("@price", Convert.ToInt16(txtPprice.Text));
                        cmd.Parameters.AddWithValue("@total", Convert.ToInt16(txtTotal.Text));
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Sipariş başarıyla eklendi");
                        Clear();

                    cmd = new SqlCommand("UPDATE tbProduct SET pqty=(pqty-@pqty) WHERE pid LIKE '"+txtPid.Text+"' ", con);               
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtQty.Text));                 
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();



                }


            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtCld.Clear();
            txtCName.Clear();
            txtPid.Clear();
            txtPName.Clear();
            txtPprice.Clear();
            txtQty.Value = 0;
            txtTotal.Clear();
            dtOrder.Value = DateTime.Now;


        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void GetQty()
        {
            cmd = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid='"+txtPid.Text+"' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
    }
}
