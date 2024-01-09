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
    public partial class ProductModuleForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboCat.Items.Clear();
            cmd = new SqlCommand("SELECT catname FROM tbCategory", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboCat.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (MessageBox.Show("Bu ürünü kaydetmek istediğinizden emin misiniz?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory)", con);
                    cmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Ürün başarıyla kaydedildi");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtPName.Clear();
            txtPQty.Clear();
            txtPPrice.Clear();
            txtPDes.Clear();
            comboCat.Text = "";
        }

    

    private void ProductModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bu ürünü güncellemek istediğinizden emin misiniz?", "Ürünü Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbProduct SET pname = @pname, pqty = @pqty, pprice = @pprice, pdescription = @pdescription, pcategory = @pcategory WHERE pid = @pid", con);
                    cmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.Text);
                    cmd.Parameters.AddWithValue("@pid", lblPid.Text); // Assuming lblPid.Text contains the product ID

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Ürün başarıyla güncellendi");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
