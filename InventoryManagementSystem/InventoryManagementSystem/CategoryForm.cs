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
    public partial class CategoryForm : Form
    {
        // Veritabanı bağlantısı için SqlConnection tanımı.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        // CategoryForm sınıfının yapıcı metodu. Form başladığında çağrılır.
        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory(); // Kategori verilerini yükler.
        }

        // Kategorileri veritabanından yükleyip DataGridView'a ekler.
        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbCategory", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        // DataGridView'da bir hücreye tıklandığında çağrılır.
        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                // Düzenleme modülü formunu açar.
                CategoryModuleForm formModule = new CategoryModuleForm();
                formModule.StartPosition = FormStartPosition.CenterScreen;

                // Seçilen kategori bilgilerini form modülüne aktarır.
                formModule.lblCatid.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                formModule.txtCatName.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();

                formModule.btnSave.Enabled = false;
                formModule.btnUpdate.Enabled = true;
                formModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                // Kategori silme işlemi.
                if (MessageBox.Show("Bu kategoriyi silmek istediğinizden emin misiniz?", "Kategoriyi Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE FROM tbCategory WHERE catid LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Kategori başarıyla silindi!");

                        LoadCategory(); // Kategori listesini yeniden yükler.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        con.Close();
                    }
                }
            }
        }

        // Yeni kategori ekleme butonuna basıldığında çağrılır.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModuleForm formModule = new CategoryModuleForm();
            formModule.btnSave.Enabled = true;
            formModule.btnUpdate.Enabled = false;
            formModule.ShowDialog();
            LoadCategory(); // Kategori listesini günceller.
        }

        // Diğer metodlar ve olaylar (şu an boş ve işlevsiz).
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}
