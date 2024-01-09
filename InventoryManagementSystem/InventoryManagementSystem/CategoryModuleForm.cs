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
    public partial class CategoryModuleForm : Form
    {
        // Veritabanı bağlantısı ayarları.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();

        // Formun yapıcı metodu, form başlatıldığında çağrılır.
        public CategoryModuleForm()
        {
            InitializeComponent();
        }

        // PictureBox'a tıklandığında çağrılır, formu kapatır.
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // lblCid etiketine tıklama olayı - Şu anda herhangi bir işlem yapılmıyor.
        private void lblCid_Click(object sender, EventArgs e)
        {
            // İşlevsiz
        }

        // "Kaydet" butonuna tıklama olayını işler. Kategori adını kontrol eder ve veritabanına kaydeder.
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCatName.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                // Kullanıcı onayı alındıktan sonra kategori kaydetme işlemi yapılır.
                if (MessageBox.Show("Bu kategoriyi kaydetmek istediğinizden emin misiniz?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbCategory(catname)VALUES(@catname)", con);
                    cmd.Parameters.AddWithValue("@catname", txtCatName.Text);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    // Ekleme işlemi başarılıysa kullanıcıya bildirim yapılır.
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kategori başarıyla kaydedildi");
                        this.Close(); // Form kapatılır.
                    }
                    else
                    {
                        MessageBox.Show("Kategori kaydedilemedi. Lütfen tekrar deneyin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Giriş alanlarını temizler.
        public void Clear()
        {
            txtCatName.Clear();
        }

        // "Güncelle" butonuna tıklama olayını işler. Kategoriyi günceller.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcı onayı alındıktan sonra güncelleme işlemi yapılır.
                if (MessageBox.Show("Bu kategori güncellemek istediğinizden emin misiniz?", "Kategoriyi Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbCategory SET catname = @catname WHERE catid LIKE '" + lblCatid.Text + "' ", con);
                    cmd.Parameters.AddWithValue("@catname", txtCatName.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    // Güncelleme işlemi başarılıysa kullanıcıya bildirim yapılır.
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kategori başarıyla güncellendi");
                        this.Dispose(); // Form kapatılır.
                    }
                    else
                    {
                        MessageBox.Show("Kategori güncellenemedi. Lütfen tekrar deneyin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        // Form yüklenirken çağrılır - Şu anda herhangi bir işlem yapılmıyor.
        private void CategoryModuleForm_Load(object sender, EventArgs e)
        {
            // İşlevsiz
        }

        // "Temizle" butonuna tıklandığında çağrılır. Giriş alanlarını temizler.
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
    }
}
