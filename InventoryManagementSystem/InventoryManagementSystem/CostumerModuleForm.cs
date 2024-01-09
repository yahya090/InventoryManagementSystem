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
    public partial class CostumerModuleForm : Form
    {
        // Veritabanı bağlantısı için SqlConnection tanımı.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();

        // Formun yapıcı metodu, form başlatıldığında çağrılır.
        public CostumerModuleForm()
        {
            InitializeComponent();
        }

        // Label'a tıklama olayları - Şu anda işlevsiz.
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        // TextBox değişiklik olayları - Şu anda işlevsiz.
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void txtTelefon_Click(object sender, EventArgs e) { }

        // "Kaydet" butonuna tıklama olayını işler. Müşteri bilgilerini veritabanına kaydeder.
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCName.Text) || string.IsNullOrWhiteSpace(txtCPhone.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (MessageBox.Show("Bu müşteriyi kaydetmek istediğinizden emin misiniz?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbCustomer(cname,cphone)VALUES(@cname,@cphone)", con);
                    cmd.Parameters.AddWithValue("@cname", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Müşteri başarıyla kaydedildi");
                        this.Close(); // Form kapatılır.
                    }
                    else
                    {
                        MessageBox.Show("Müşteri kaydedilemedi. Lütfen tekrar deneyin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Form yüklendiğinde çağrılır - Şu anda işlevsiz.
        private void CostumerModuleForm_Load(object sender, EventArgs e) { }

        // Giriş alanlarını temizler.
        public void Clear()
        {
            txtCName.Clear();
            txtCPhone.Clear();
        }

        // "Temizle" butonuna tıklandığında çağrılır. Giriş alanlarını temizler ve butonları ayarlar.
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        // PictureBox'a tıklama olayı - Formu kapatır.
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // "Güncelle" butonuna tıklama olayını işler. Müşteri bilgilerini günceller.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bu müşteriyi güncellemek istediğinizden emin misiniz?", "Kaydı Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbCustomer SET cname = @cname,cphone = @cphone WHERE cid LIKE '" + lblCid.Text + "'", con);
                    cmd.Parameters.AddWithValue("@cname", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Müşteri başarıyla güncellendi");
                        this.Dispose(); // Form kapatılır.
                    }
                    else
                    {
                        MessageBox.Show("Müşteri güncellenemedi. Lütfen tekrar deneyin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }
    }
}
