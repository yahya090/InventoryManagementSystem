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

namespace InventoryManagementSystem
{
    public partial class CustomerForm : Form
    {
        // Veritabanı bağlantısı için SqlConnection tanımı.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        // Formun yapıcı metodu, form başlatıldığında çağrılır ve müşterileri yükler.
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer(); // Müşteri listesini yükler.
        }

        // Müşterileri veritabanından yükleyip DataGridView'a ekler.
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();
        }

        // DataGridView'da bir hücreye tıklandığında çağrılır.
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                // Düzenleme formunu açar ve mevcut müşteri bilgileri ile doldurur.
                CostumerModuleForm customerModule = new CostumerModuleForm();
                customerModule.StartPosition = FormStartPosition.CenterScreen;

                customerModule.lblCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModule.txtCPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();

                customerModule.btnSave.Enabled = false;
                customerModule.btnUpdate.Enabled = true;
                customerModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                // Seçilen müşteriyi silme işlemini onaylar ve gerçekleştirir.
                if (MessageBox.Show("Bu kullanıcıyı silmek istediğinizden emin misiniz?", "Kaydı Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE FROM tbCustomer WHERE cid LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Kayıt başarıyla silindi!");

                        LoadCustomer(); // Müşteri listesini yeniden yükler.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        con.Close();
                    }
                }
            }
        }

        // "Ekle" butonuna basıldığında yeni müşteri ekleme formunu açar.
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            CostumerModuleForm moduleForm = new CostumerModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadCustomer(); // Müşteri listesini günceller.
        }

        // Diğer metodlar ve olaylar (şu an işlevsiz).
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}
