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
    public partial class UserModuleForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kalka\OneDrive\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*'; 
            txtRePassword.PasswordChar = '*';
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

           private void btnSave_Click(object sender, EventArgs e)
            {

            if (string.IsNullOrWhiteSpace(txtUserName.Text) ||
                 string.IsNullOrWhiteSpace(txtFullName.Text) ||
                 string.IsNullOrWhiteSpace(txtPassword.Text) ||
                 string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
                {
                    if (txtPassword.Text != txtRePassword.Text)
                    {
                        MessageBox.Show("Şifreniz eşleşmedi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (MessageBox.Show("Bu kullanıcıyı kaydetmek istediğinizden emin misiniz?", "Kaydetme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("INSERT INTO tbUser(username,fullname,password,phone)VALUES(@username,@fullname,@password,@phone)", con);
                        cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@fullname", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kullanıcı başarıyla kaydedildi");
                            this.Close(); // or this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı kaydedilemedi. Lütfen tekrar deneyin.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
        public void Clear()
        {
             txtUserName.Clear();   
             txtFullName.Clear();   
             txtPassword.Clear();
            txtRePassword.Clear();
             txtPhone.Clear();   
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtRePassword.Text)
                {
                    MessageBox.Show("Şifreniz eşleşmedi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Bu kullanıcıyı güncellemek istediğinizden emin misiniz?", "Kaydı Güncelle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbUser SET fullname = @fullname, password = @password, phone = @phone WHERE username = @username", con);
                    cmd.Parameters.AddWithValue("@fullname", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@username", txtUserName.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kullanıcı başarıyla güncellendi");
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı güncellenemedi. Lütfen tekrar deneyin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }


        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtName_Click(object sender, EventArgs e)
        {

        }

        private void txtPass_Click(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefon_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
   