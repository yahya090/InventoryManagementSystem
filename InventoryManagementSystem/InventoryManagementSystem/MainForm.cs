using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InventoryManagementSystem
{
    public partial class MainForm : Form
    {
        // Ana formun yapıcı metodu.
        public MainForm()
        {
            InitializeComponent();
        }

        // Aktif alt formu tutmak için bir değişken.
        private Form activeForm = null;

        // Bir alt formu açmak için kullanılan metod.
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close(); // Eğer aktif bir form varsa, önce onu kapatır.
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.AutoSize = true;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm); // Ana panel içerisine alt formu ekler.
            panelMain.Tag = childForm;
            childForm.BringToFront(); // Formu en öne getirir.
            childForm.Show(); // Formu gösterir.
        }

        // Panel ve Label olayları - Şu anda işlevsiz.
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void panelMain_Paint(object sender, PaintEventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void customerButton1_Click(object sender, EventArgs e) { }

        // Ürünler butonuna tıklanınca ProductForm'u açar.
        private void btnProduct_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        // Müşteriler butonuna tıklanınca CustomerForm'u açar.
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm());
        }

        // Kullanıcılar butonuna tıklanınca UserForm'u açar.
        private void btnUser_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        // Kategoriler butonuna tıklanınca CategoryForm'u açar.
        private void btnCategory_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoryForm());
        }

        // Siparişler butonuna tıklanınca OrderForm'u açar.
        private void btnOrder_Click(object sender, EventArgs e)
        {
            openChildForm(new OrderForm());
        }
    }
}
