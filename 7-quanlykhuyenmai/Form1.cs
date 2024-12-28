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

namespace _7_quanlykhuyenmai
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string dulieu = @"Data Source=LAPTOP-QJB0H525;Initial Catalog=baitaplon;Integrated Security=True;Encrypt=False";
        string sql;
        SqlConnection ketnoi;
        SqlCommand th;
        SqlDataAdapter da;
        DataTable dt;

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection ketnoi = new SqlConnection(dulieu);
            hienthi();
        }
        public void hienthi() {
            sql = "select * from quanlykhuyenmai";
            ketnoi = new SqlConnection(dulieu);
            th = new SqlCommand(sql, ketnoi);
            da = new SqlDataAdapter(th);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int sodong = -1;
            sodong=dataGridView1.CurrentCell.RowIndex;
            if (sodong >= 0)
            {
                txtidkhuyenmai.Text = dataGridView1[0,sodong].Value.ToString();
               txttenkhuyenmai.Text = dataGridView1[1, sodong].Value.ToString();
                txtmota.Text = dataGridView1[2, sodong].Value.ToString();
                txtngaybatdau.Text = dataGridView1[3, sodong].Value.ToString();
                txtngayketthuc.Text = dataGridView1[4, sodong].Value.ToString();
                txtgiatrigiamgia.Text = dataGridView1[5, sodong].Value.ToString();
            }
        }

        private void buttonthem_Click(object sender, EventArgs e)
        {
            ketnoi.Open();
            sql = @"insert into quanlykhuyenmai values (@idkhuyenmai,@tenkhuyenmai,@mota,@ngaybatdau,@ngayketthuc,@giatrigiamgia)";
            th = new SqlCommand(sql, ketnoi);
            th.Parameters.AddWithValue("@idkhuyenmai", txtidkhuyenmai.Text);
            th.Parameters.AddWithValue("@tenkhuyenmai", txttenkhuyenmai.Text);
            th.Parameters.AddWithValue("@mota", txtmota.Text);
            th.Parameters.AddWithValue("@ngaybatdau",DateTime.Parse( txtngaybatdau.Text));
            th.Parameters.AddWithValue("@ngayketthuc",DateTime.Parse( txtngayketthuc.Text));
            th.Parameters.AddWithValue("@giatrigiamgia",int.Parse( txtgiatrigiamgia.Text));
            th.ExecuteNonQuery();
            MessageBox.Show("Thêm thành công");
            hienthi();
            ketnoi.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ketnoi.Open();
            sql = @"update quanlykhuyenmai set tenkhuyenmai=@tenkhuyenmai,mota=@mota,ngaybatdau=@ngaybatdau,ngayketthuc=@ngayketthuc,giatrigiamgia=@giatrigiamgia where idkhuyenmai=@idkhuyenmai";
            th = new SqlCommand(sql, ketnoi);
            th.Parameters.AddWithValue("@idkhuyenmai", txtidkhuyenmai.Text);
            th.Parameters.AddWithValue("@tenkhuyenmai", txttenkhuyenmai.Text);
            th.Parameters.AddWithValue("@mota", txtmota.Text);
            th.Parameters.AddWithValue("@ngaybatdau", DateTime.Parse(txtngaybatdau.Text));
            th.Parameters.AddWithValue("@ngayketthuc", DateTime.Parse(txtngayketthuc.Text));
            th.Parameters.AddWithValue("@giatrigiamgia", int.Parse(txtgiatrigiamgia.Text));
            th.ExecuteNonQuery();
            MessageBox.Show("Sửa thành công");
            hienthi();
            ketnoi.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ketnoi.Open();
            sql = @"delete from quanlykhuyenmai where idkhuyenmai=@idkhuyenmai";
            th = new SqlCommand(sql, ketnoi);
            th.Parameters.AddWithValue("@idkhuyenmai", txtidkhuyenmai.Text);
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                th.ExecuteNonQuery();
            }
            hienthi();
            ketnoi.Close();

        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ketnoi.Open();
            sql = @"select * from quanlykhuyenmai where idkhuyenmai ='"+txttimkiem.Text+"'";
            th = new SqlCommand(sql, ketnoi);
            da = new SqlDataAdapter(th);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            ketnoi.Close();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
