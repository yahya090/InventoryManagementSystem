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
    public partial class LoginForm : Form
    {
        // Veritabanı bağlantısı için SqlConnection tanımı.
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        // Formun yapıcı metodu, form başlatıldığında çağrılır. Şifre alanının gizliliğini ayarlar.
        public LoginForm()
        {
            InitializeComponent();
            txtPass.UseSystemPasswordChar = true;
        }

        // Form yüklendiğinde çağrılır - Şu anda işlevsiz.
        private void LoginForm_Load(object sender, EventArgs e) { }

        // Label ve PictureBox tıklama olayları - Şu anda işlevsiz.
        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        // "Giriş Yap" butonuna tıklama olayını işler. Kullanıcı adı ve şifreyi kontrol eder.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SELECT * FROM tbUser WHERE username = @username  AND password = @password", con);
                cmd.Parameters.AddWithValue("username", txtName.Text);
                cmd.Parameters.AddWithValue("password", txtPass.Text);
                con.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show("Hoşgeldiniz " + dr["fullname"].ToString() + " ! ", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm main = new MainForm(); // Ana formu açar.
                    this.Hide();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Geçersiz kullanıcı adı veya şifre!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Şifre göster/gizle checkbox'ını işler.
        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !checkBoxPass.Checked;
        }

        // PictureBox'a tıklama olayı - Uygulamadan çıkışı onaylar.
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamadan çıkmak istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // TextBox değişiklik olayı - Şu anda işlevsiz.
        private void txtPass_TextChanged(object sender, EventArgs e) { }
    }
}
