using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;


namespace Game_Xep_Hinh
{
    public partial class frmChinh : Form
    {
        private int So_Hang = 22, So_Cot = 12, Kich_Thuoc = 20, So_Mau = 8, Thua, Time_Thua, So_Time_Thua = 50, Cho_Phep, Dinh_Hang, Dinh_Hinh, Time_MacDinh, Time_Max = 20, Nhap_Nhay, Tang_Toc = 5, CapDo_Max = 10;
        private int Phut, Giay, Choi_Nhac, Diem, Diem_Max = 10, Cap_Do, So_Hinh = 5, So_Gach = 4, ran_Mau, ran_Hinh, Loai_Hinh, Loai_Mau, Dai_Hinh, Bat_Dau, Xuong_Di, Dang_Choi, Tiep, Max_HT;
        private int[,] Mau_Gach, Mau_Cu;
        private int[] ViTri_X, ViTri_Y, arrHang_An;
        private Rectangle[,] Gach;
        private Rectangle[] Gach_Tiep;
        private SolidBrush Co_Ve_Xoa, Co_Ve_Mau, Co_Ve_Tiep;
        private Graphics Gra, Gra_Tiep;
        private Random ran;
        private string Duong_Dan = System.IO.Directory.GetCurrentDirectory() + "\\Tai_Nguyen\\";

        public int Bat_Dau1 { get => Bat_Dau; set => Bat_Dau = value; }
        public int Ran_Mau { get => ran_Mau; set => ran_Mau = value; }

        public frmChinh()
        {
            InitializeComponent();
        }
                private void frmChinh_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = new Bitmap(Duong_Dan + "Anh\\Nen.png");
            Gach = new Rectangle[So_Hang, So_Cot]; Mau_Cu = new int[So_Hang, So_Cot];
            Gach_Tiep = new Rectangle[So_Gach]; ViTri_X = new int[So_Gach]; ViTri_Y = new int[So_Gach];
            ran = new Random(); arrHang_An = new int[So_Gach];
            for (int i = 0; i < So_Hang; i++)
            {
                for (int j = 0; j < So_Cot; j++)
                {
                    Gach[i, j] = new Rectangle((j + 1) * Kich_Thuoc, i * Kich_Thuoc, Kich_Thuoc, Kich_Thuoc);
                    Mau_Cu[i, j] = So_Mau;
                }
            }

            #region Mau gach

            Mau_Gach = new int[So_Mau, 3];
            Mau_Gach[0, 0] = 253; Mau_Gach[0, 1] = 217; Mau_Gach[0, 2] = 41;
            Mau_Gach[1, 0] = 237; Mau_Gach[1, 1] = 33; Mau_Gach[1, 2] = 36;
            Mau_Gach[2, 0] = 63; Mau_Gach[2, 1] = 195; Mau_Gach[2, 2] = 216;
            Mau_Gach[3, 0] = 84; Mau_Gach[3, 1] = 168; Mau_Gach[3, 2] = 72;
            Mau_Gach[4, 0] = 127; Mau_Gach[4, 1] = 55; Mau_Gach[4, 2] = 137;
            Mau_Gach[5, 0] = 202; Mau_Gach[5, 1] = 123; Mau_Gach[5, 2] = 180;
            Mau_Gach[6, 0] = 248; Mau_Gach[6, 1] = 151; Mau_Gach[6, 2] = 44;
            Mau_Gach[7, 0] = 22; Mau_Gach[7, 1] = 72; Mau_Gach[7, 2] = 143;
            
            #endregion

            Co_Ve_Xoa = new SolidBrush(Color.Black);
            Co_Ve_Mau = new SolidBrush(Color.Black); Co_Ve_Tiep = new SolidBrush(Color.Black);//khoi tao tranh loi
            Gra = panel_Chinh.CreateGraphics(); Gra_Tiep = panel_Tiep.CreateGraphics();
            lblCap_Do.Location = new Point(20, 40); lblTime.Location = new Point(20, 80);
            lblDiem.Location = new Point(20, 120); panel_Tiep.Location = new Point(20, 160);
            lblHelp.Location = new Point(20, 280); lblAm_Thanh.Location = new Point(80, 280);
            lblHelp.Image = new Bitmap(Duong_Dan + "Anh\\Help.png");
            Huy_Game();
            if (Choi_Nhac == 1)
            {
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Co_Am.png");
            }
            else
            {
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Ko_Am.png");
            }
        }

        private void New_Game()
        {
                Phut = 0; Giay = 0; lblTime.Text = "00 : 00";
                Diem = 0; lblDiem.Text = "0";
                Cap_Do = 1; lblCap_Do.Text = "LEVEL : 01";
                for (int i = 0; i < So_Hang; i++)
                {
                    for (int j = 0; j < So_Cot; j++)
                    {
                        Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                    }
                }
                ran_Hinh = ran.Next(0, So_Hinh); Ran_Mau = ran.Next(0, So_Mau); Dinh_Hang = So_Hang - 1;
                Co_Ve_Tiep = new SolidBrush(Color.FromArgb(255, Mau_Gach[Ran_Mau, 0], Mau_Gach[Ran_Mau, 1], Mau_Gach[Ran_Mau, 2]));
                Load_Tiep(ran_Hinh); Chon_Hinh(); Load_Tam(); Load_Hinh();
            

           Time_MacDinh = 600; Time_MacDinh = Time_MacDinh - ((Cap_Do - 1) * Tang_Toc); timer_Gach.Interval = Time_MacDinh; timer_Time.Interval = 1000;
            Bat_Dau1 = 1; Cho_Phep = 1; Dang_Choi = 1; Tiep = 1; Thua = 0; Max_HT = Cap_Do * Diem_Max;
           timer_Gach.Start(); timer_Time.Start(); if (Choi_Nhac == 1) { Am_Thanh("An"); } lblHelp.Enabled = false;
        }

        private void Huy_Game()
        {
            Bat_Dau1 = 0; Cho_Phep = 0; Dang_Choi = 0; Tiep = 0; Thua = 0; lblHelp.Enabled = true;
            timer_Gach.Stop(); timer_Time.Stop();
            Diem = 0; lblDiem.Text = "0";
            Cap_Do = 0; lblCap_Do.Text = "LEVEL : 00";
            Phut = 0; Giay = 0; lblTime.Text = "00 : 00";
            for (int i = 0; i < So_Hang; i++)
            {
                for (int j = 0; j < So_Cot; j++)
                {
                    Mau_Cu[i, j] = So_Mau;
                    Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                }
            }
            for (int i = 0; i < So_Gach; i++)
            {
                Gra_Tiep.FillRectangle(Co_Ve_Xoa, Gach_Tiep[i]);
            }
        }
        private void timer_Time_Tick(object sender, EventArgs e)
        {
            if (Thua == 0)
            {
                string Time;
                Giay++; if (Giay == 60) { Giay = 0; Phut++; }
                if (Phut < 10) { Time = "0"; }
                else { Time = ""; }
                Time = Time + Phut.ToString() + " : ";
                if (Giay < 10) { Time = Time + "0"; }
                lblTime.Text = Time + Giay.ToString();
            }
            else
            {
                Time_Thua--; Thua_Nhap_Nhay();
                if (Time_Thua == 0)
                {
                    timer_Time.Stop();
                    if (Cap_Do > CapDo_Max)
                    {
                        MessageBox.Show("Chúc Mừng !\nBạn đã chiến thắng");
                    }
                    else
                    {
                        MessageBox.Show("Rất Tiếc !\nChúc Bạn may mắn lần sau");
                    }
                    Huy_Game();
                    New_Game();
                }
            }
        }

        private void timer_Gach_Tick(object sender, EventArgs e)
        {
            if ((Xuong_Di == 1) && (Cho_Phep == 1))
            {
                Cho_Phep = 0; Xuong_Duoi();
                if (KiemTra_Duoi() == 0)
                {
                    Xuong_Di = 0; if (Choi_Nhac == 1) { Am_Thanh("Click"); }
                }
                else { Xuong_Di = 1; }
                if (Dai_Hinh > 0)
                {
                    Dai_Hinh--;
                    if (Dai_Hinh == 0)
                    {
                        ran_Hinh = ran.Next(0, So_Hinh); Ran_Mau = ran.Next(0, So_Mau);
                        Co_Ve_Tiep = new SolidBrush(Color.FromArgb(255, Mau_Gach[Ran_Mau, 0], Mau_Gach[Ran_Mau, 1], Mau_Gach[Ran_Mau, 2]));
                        Load_Tiep(ran_Hinh);
                    }
                }
                Cho_Phep = 1;
            }
            else
            {
                if (Dai_Hinh > 0)
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau1 = 0; Dang_Choi = 0;
                }
                else
                {
                    Xoa_An();
                    if (Thua == 0)
                    {
                        Chon_Hinh(); Load_Tam(); Load_Hinh();
                        if (Thua == 0)
                        {
                            timer_Gach.Interval = Time_MacDinh; timer_Gach.Start(); Cho_Phep = 1;
                        }
                    }
                }
            }
        }

        private void frmChinh_KeyPress(object sender, KeyPressEventArgs e_Press)
        {
            #region ENTER : Bat dau - Huy

            if (e_Press.KeyChar == 13)
            {
                if (Bat_Dau1 == 0)
                {
                    New_Game();
                }
                else
                {
                    timer_Gach.Stop(); timer_Time.Stop(); Cho_Phep = 0;
                    DialogResult Dre = MessageBox.Show("Bạn có muốn chơi lại không?\nNhấn Yes để chơi lại\nNhấn No để Tiếp tục", "Xác Nhận Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Dre == DialogResult.Yes)
                    {
                        Huy_Game();
                        New_Game();
                    }
                    else
                    {
                        if (Dang_Choi == 1)
                        {
                            timer_Gach.Start(); timer_Time.Start(); Cho_Phep = 1;
                        }
                    }
                }
            }

            #endregion

            #region ESC : Thoat

            if (e_Press.KeyChar == 27)
            {
                if (Bat_Dau1 == 1)
                {
                    if (Tiep == 1)
                    {
                        timer_Gach.Stop(); timer_Time.Stop(); Cho_Phep = 0;
                        DialogResult Dre = MessageBox.Show("Bạn có muốn thoát không ?\nNhấn Yes Thoát\nNhấn No để để Tiếp tục chơi", "Xác Nhận Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (DialogResult.No == Dre)
                        {
                            if (Dang_Choi == 1)
                            {
                                timer_Gach.Start(); timer_Time.Start(); Cho_Phep = 1;
                            }
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    this.Close();
                }
            }

            #endregion

            #region SPACE : Tiep - Dung

            if ((Bat_Dau1 == 1) && (e_Press.KeyChar == 32))
            {
                if (Dang_Choi == 1)
                {
                    timer_Gach.Stop(); timer_Time.Stop(); Dang_Choi = 0; Cho_Phep = 0;
                }
                else
                {
                    timer_Gach.Start(); timer_Time.Start(); Dang_Choi = 1; Cho_Phep = 1;
                }
            }

            #endregion
        }
        private void Am_Thanh(string Ten_File)
        {
            SoundPlayer Nhac = new SoundPlayer(Duong_Dan + "Am_Thanh\\" + Ten_File + ".wav");
            Nhac.LoadAsync();
            Nhac.Play();
        }
        private void lblAm_Thanh_Click(object sender, EventArgs e)
        {
            if (Choi_Nhac == 0)
            {
                Am_Thanh("Menu"); 
                Choi_Nhac = 1;
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Co_Am.png");
            }
            else
            {
                Choi_Nhac = 0;
                lblAm_Thanh.Image = new Bitmap(Duong_Dan + "Anh\\Ko_Am.png");
            }
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            if (Choi_Nhac == 1) { Am_Thanh("Click"); }
            MessageBox.Show("Bạn hãy sử dụng 3 phím Mũi Tên để di chuyển các ô gạch\nMũi Tên Trái để di chuyển ô gạch Sang Trái\nMũi Tên Phải để di chuyển ô gạch Sang Phải\nMũi Tên Xuống Dưới để di chuyển ô gach xuống dưới thật nhanh\nMũi Tên Lên để xoay ô gạch\nNếu xếp được 1 hàng gạch kín, Bạn được 12 điểm và hàng gạch đó sẽ tự động biến mất\nPhím ENTER để Bắt Đàu - Hủy lượt chơi\nPhím SPACE để Tiếp Tục - Tạm Dừng lượt chơi\nPhím ESC để Thoát\nCách chơi vô cùng đơn giản.");
        }

        private void Xoa_Gach(int xoa)
        {
            Mau_Cu[ViTri_Y[xoa], ViTri_X[xoa]] = So_Mau;
            Gra.FillRectangle(Co_Ve_Xoa, Gach[ViTri_Y[xoa], ViTri_X[xoa]]);
        }

        private void Ve_Gach(int ve)
        {
            Mau_Cu[ViTri_Y[ve], ViTri_X[ve]] = Loai_Mau;
            Gra.FillRectangle(Co_Ve_Mau, Gach[ViTri_Y[ve], ViTri_X[ve]]);
        }

        private void Chon_Hinh()
        {
            Loai_Hinh = ran_Hinh; Loai_Mau = Ran_Mau;
            Co_Ve_Mau = new SolidBrush(Color.FromArgb(255, Mau_Gach[Loai_Mau, 0], Mau_Gach[Loai_Mau, 1], Mau_Gach[Loai_Mau, 2]));
            if (Loai_Hinh == 0) { Dai_Hinh = 2; }//chu T
            if (Loai_Hinh == 1) { Dai_Hinh = 3; }//chu Z
            if (Loai_Hinh == 2) { Dai_Hinh = 2; }//hinh Vuong
            if (Loai_Hinh == 3) { Dai_Hinh = 4; }//thanh Doc
            if (Loai_Hinh == 4) { Dai_Hinh = 3; }//chu L
        }

        private void Load_Tam()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[0] = ran.Next(1, So_Cot - 1); ViTri_Y[0] = -1;
            }
            if (Loai_Hinh == 1)//chu Z
            {
                ViTri_X[0] = ran.Next(0, So_Cot - 1); ViTri_Y[0] = -1;
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                ViTri_X[0] = ran.Next(0, So_Cot - 1); ViTri_Y[0] = -1;
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                ViTri_X[0] = ran.Next(0, So_Cot); ViTri_Y[0] = -2;
            }
            if (Loai_Hinh == 4)//chu L
            {
                ViTri_X[0] = ran.Next(0, So_Cot - 1); ViTri_Y[0] = -1;
            }
        }

        private void Load_Hinh()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[1] = ViTri_X[0] - 1; ViTri_Y[1] = ViTri_Y[0];//toa do Trai
                ViTri_X[2] = ViTri_X[0] + 1; ViTri_Y[2] = ViTri_Y[0];//toa do Phai
                ViTri_X[3] = ViTri_X[0]; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Duoi
                if (Mau_Cu[ViTri_Y[3], ViTri_X[3]] == So_Mau)
                {
                    Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau1 = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] - 1;//toa do Tren
                ViTri_X[2] = ViTri_X[0] + 1; ViTri_Y[2] = ViTri_Y[0];//toa do Phai
                ViTri_X[3] = ViTri_X[0] + 1; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Duoi Phai
                if (Mau_Cu[ViTri_Y[3], ViTri_X[3]] == So_Mau)
                {
                    Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau1 = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                ViTri_X[1] = ViTri_X[0] + 1; ViTri_Y[1] = ViTri_Y[0];//toa do Phai
                ViTri_X[2] = ViTri_X[0] + 1; ViTri_Y[2] = ViTri_Y[0] + 1;//toa do Duoi Phai
                ViTri_X[3] = ViTri_X[0]; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Duoi
                if ((Mau_Cu[ViTri_Y[2], ViTri_X[2]] == So_Mau) && (Mau_Cu[ViTri_Y[3], ViTri_X[3]] == So_Mau))
                {
                    Ve_Gach(2); Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau1 = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] - 1;//toa do Tren
                ViTri_X[2] = ViTri_X[0]; ViTri_Y[2] = ViTri_Y[0] + 1;//toa do Duoi
                ViTri_X[3] = ViTri_X[0]; ViTri_Y[3] = ViTri_Y[0] + 2;//toa do Duoi Cung
                if (Mau_Cu[ViTri_Y[3], ViTri_X[3]] == So_Mau)
                {
                    Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau1 = 0; Dang_Choi = 0;
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] - 1;//toa do Tren
                ViTri_X[2] = ViTri_X[0]; ViTri_Y[2] = ViTri_Y[0] + 1;//toa do Duoi
                ViTri_X[3] = ViTri_X[0] + 1; ViTri_Y[3] = ViTri_Y[0] + 1;//toa do Phai
                if ((Mau_Cu[ViTri_Y[2], ViTri_X[2]] == So_Mau) && (Mau_Cu[ViTri_Y[3], ViTri_X[3]] == So_Mau))
                {
                    Ve_Gach(2); Ve_Gach(3); Dai_Hinh--;
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thua_KT"); }
                    Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau1 = 0; Dang_Choi = 0;
                }
            }
            if (Thua == 0)
            {
                if (KiemTra_Duoi() == 0)
                {
                    Xuong_Di = 0; if (Choi_Nhac == 1) { Am_Thanh("Click"); }
                }
                else { Xuong_Di = 1; }
            }
        }//load Hang Gach Duoi Cung cua hinh

        private int KiemTra_Duoi()
        {
            int KQ_Duoi = 0;
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_Y[0] < So_Hang - 3)
                {
                    if (Mau_Cu[ViTri_Y[0] + 3, ViTri_X[0]] == So_Mau) { KQ_Duoi = 1; }
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                if (ViTri_Y[0] < So_Hang - 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                if (ViTri_Y[0] < So_Hang - 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                if (ViTri_Y[0] < So_Hang - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                if (ViTri_Y[0] < So_Hang - 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Duoi = 1;
                    }
                }
            }
            return KQ_Duoi;
        }

        private void Xuong_Duoi()
        {
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_Y[0] >= 0) { Xoa_Gach(0); }//xoa Tam
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                if (ViTri_Y[2] >= 0) { Xoa_Gach(2); }//xoa Phai
                ViTri_Y[2]++; Ve_Gach(2);//dich Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Tren
                ViTri_Y[1]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                if (ViTri_Y[2] >= 0) { Xoa_Gach(2); }//xoa Phai
                ViTri_Y[2]++;//dich Phai -> Duoi Phai(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi Phai
                Dinh_Hinh = ViTri_Y[1];
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_Y[0] >= 0) { Xoa_Gach(0); }//xoa Tam
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Phai
                ViTri_Y[1]++;//dich Phai -> Duoi Phai(khong xoa)
                ViTri_Y[2]++; Ve_Gach(2);//dich Duoi Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Tren
                ViTri_Y[1]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[2]++;//dich Duoi -> Duoi Cung(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi Cung
                Dinh_Hinh = ViTri_Y[1];
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_Y[1] >= 0) { Xoa_Gach(1); }//xoa Tren
                ViTri_Y[1]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[2]++; Ve_Gach(2);//dich Duoi
                Xoa_Gach(3);//xoa Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Phai
                Dinh_Hinh = ViTri_Y[1];
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[2];
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                Xoa_Gach(0);//xoa Tam
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                Xoa_Gach(2);//xoa Phai
                ViTri_Y[2]++; Ve_Gach(2);//dich Phai
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                for (int i = 0; i < So_Gach; i++)
                {
                    Xoa_Gach(i); ViTri_Y[i]++; Ve_Gach(i);
                }
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                Xoa_Gach(1);//xoa Phai
                ViTri_Y[1]++; Ve_Gach(1);//dich Phai
                Xoa_Gach(0);//xoa Tam
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa trai
                ViTri_Y[2]++;//dich Trai -> Duoi(khong xoa)
                ViTri_Y[3]++; Ve_Gach(3);//dich Duoi
                Dinh_Hinh = ViTri_Y[0];
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                Xoa_Gach(3);//xoa Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Phai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                Dinh_Hinh = ViTri_Y[2];
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                Xoa_Gach(3);//xoa Trai
                ViTri_Y[3]++; Ve_Gach(3);//dich Trai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[1]++; Ve_Gach(1);//dich Duoi
                Dinh_Hinh = ViTri_Y[3];
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                Xoa_Gach(3);//xoa Phai
                ViTri_Y[3]++; Ve_Gach(3);//dich Phai
                Xoa_Gach(2);//xoa Tren
                ViTri_Y[2]++;//dich Tren -> Tam(khong xoa)
                ViTri_Y[0]++;//dich Tam -> Duoi(khong xoa)
                ViTri_Y[1]++; Ve_Gach(1);//dich Duoi
                Dinh_Hinh = ViTri_Y[2];
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                Xoa_Gach(1);//xoa Trai
                ViTri_Y[1]++; Ve_Gach(1);//dich Trai
                Xoa_Gach(0);//xoa Tam
                ViTri_Y[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(3);//xoa Tren
                ViTri_Y[3]++;//dich Tren -> Phai(khong xoa)
                ViTri_Y[2]++; Ve_Gach(2);//dich Phai
                Dinh_Hinh = ViTri_Y[3];
            }
        }
        
        private int KiemTra_Phai()
        {
            int KQ_Phai = 0;
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 2] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 2] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_X[0] < So_Cot - 1)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 2] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                if (ViTri_X[0] < So_Cot - 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau)
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                if (ViTri_X[0] < So_Cot - 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                if (ViTri_X[0] < So_Cot - 2)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 2] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 2] == So_Mau))
                    {
                        KQ_Phai = 1;
                    }
                }
            }
            return KQ_Phai;
        }

        private void Sang_Phai()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi

            }
            if (Loai_Hinh == 1)//chu Z
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]++; Ve_Gach(1);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
                Xoa_Gach(3);//xoa Duoi Phai
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi Phai
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++;//dich Duoi -> Duoi Phai(khong xoa)
                ViTri_X[1]++; Ve_Gach(1);//dich Phai
                ViTri_X[2]++; Ve_Gach(2);//dich Duoi Phai
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                for (int i = 0; i < So_Gach; i++)
                {
                    Xoa_Gach(i); ViTri_X[i]++; Ve_Gach(i);
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]++; Ve_Gach(1);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa Duoi
                ViTri_X[2]++;//dich Duoi -> Phai(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Phai
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++; Ve_Gach(0);//dich Tam
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Duoi(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                Xoa_Gach(3);//xoa Trai Cung
                ViTri_X[3]++;//dich Trai Cung -> Trai(khong xoa)
                ViTri_X[2]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[1]++; Ve_Gach(1);//dich Phai
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]++; Ve_Gach(3);//dich Duoi
                Xoa_Gach(2);//xoa Trai
                ViTri_X[2]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[1]++; Ve_Gach(1);//dich Phai
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Phai
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                Xoa_Gach(3);//xoa Trai
                ViTri_X[3]++;//dich Trai -> Tren(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++; Ve_Gach(0);//dich Tam
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]++; Ve_Gach(1);//dich Duoi
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]++; Ve_Gach(1);//dich Duoi
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]++; Ve_Gach(2);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[3]++; Ve_Gach(3);//dich Phai
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                Xoa_Gach(3);//xoa Tren
                ViTri_X[3]++; Ve_Gach(3);//dich Tren
                Xoa_Gach(1);//xoa Trai
                ViTri_X[1]++;//dich Trai -> Tam(khong xoa)
                ViTri_X[0]++;//dich Tam -> Phai(khong xoa)
                ViTri_X[2]++; Ve_Gach(2);//dich Phai
            }
        }

        private int KiemTra_Trai()
        {
            int KQ_Trai = 0;
            if (Loai_Hinh == 0)//chu T
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 2] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 1)//chu Z
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] - 1] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 2] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                if (ViTri_X[0] > 2)
                {
                    if (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 3] == So_Mau)
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Mau_Cu[ViTri_Y[0], ViTri_X[0] - 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 2] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 2] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 2] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                if (ViTri_X[0] > 0)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                if (ViTri_X[0] > 1)
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 2] == So_Mau))
                    {
                        KQ_Trai = 1;
                    }
                }
            }
            return KQ_Trai;
        }

        private void Sang_Trai()
        {
            if (Loai_Hinh == 0)//chu T
            {
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi

            }
            if (Loai_Hinh == 1)//chu Z
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]--; Ve_Gach(1);//dich Tren
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                Xoa_Gach(3);//xoa Duoi Phai
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi Phai
            }
            if (Loai_Hinh == 2)//hinh Vuong
            {
                Xoa_Gach(1);//xoa Phai
                ViTri_X[1]--;//dich Phai -> Tam(khong xoa)
                Xoa_Gach(2);//xoa Duoi Phai
                ViTri_X[2]--;//dich Duoi Phai -> Duoi(khong xoa)
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi
            }
            if (Loai_Hinh == 3)//thanh Doc
            {
                for (int i = 0; i < So_Gach; i++)
                {
                    Xoa_Gach(i); ViTri_X[i]--; Ve_Gach(i);
                }
            }
            if (Loai_Hinh == 4)//chu L
            {
                Xoa_Gach(1);//xoa Tren
                ViTri_X[1]--; Ve_Gach(1);//dich Tren
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                ViTri_X[2]--; Ve_Gach(2);//dich Duoi
                Xoa_Gach(3);//xoa Phai
                ViTri_X[3]--;//dich Phai -> Duoi(khong xoa)
            }
            if (Loai_Hinh == 5)//chu T trai
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--; Ve_Gach(2);//dich Tren
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
            if (Loai_Hinh == 6)//chu Z ngang
            {
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--;//dich Duoi -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
            if (Loai_Hinh == 8)//thanh Ngang
            {
                Xoa_Gach(1);//xoa Phai
                ViTri_X[1]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[2]--;//dich Trai -> Trai Cung(khong xoa)
                ViTri_X[3]--; Ve_Gach(3);//dich Trai Cung
            }
            if (Loai_Hinh == 9)//chu L xap
            {
                Xoa_Gach(3);//xoa Duoi
                ViTri_X[3]--; Ve_Gach(3);//dich Duoi
                Xoa_Gach(1);//xoa Phai
                ViTri_X[1]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[2]--; Ve_Gach(2);//dich Trai
            }
            if (Loai_Hinh == 10)//chu T lon nguoc
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--; Ve_Gach(2);//dich Tren
                Xoa_Gach(3);//xoa Phai
                ViTri_X[3]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
            if (Loai_Hinh == 14)//chu L trai
            {
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]--; Ve_Gach(1);//dich Duoi
                Xoa_Gach(0);//xoa Tam
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--;//dich Tren -> Trai(khong xoa)
                ViTri_X[3]--; Ve_Gach(3);//dich Trai
            }
            if (Loai_Hinh == 15)//chu T phai
            {
                Xoa_Gach(2);//xoa Tren
                ViTri_X[2]--; Ve_Gach(2);//dich Tren
                Xoa_Gach(1);//xoa Duoi
                ViTri_X[1]--; Ve_Gach(1);//dich Duoi
                Xoa_Gach(3);//xoa Phai
                ViTri_X[3]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--; Ve_Gach(0);//dich Tam
            }
            if (Loai_Hinh == 19)//chu L ngua
            {
                Xoa_Gach(3);//xoa Tren
                ViTri_X[3]--; Ve_Gach(3);//dich Tren
                Xoa_Gach(2);//xoa Phai
                ViTri_X[2]--;//dich Phai -> Tam(khong xoa)
                ViTri_X[0]--;//dich Tam -> Trai(khong xoa)
                ViTri_X[1]--; Ve_Gach(1);//dich Trai
            }
        }

        private void Load_Tiep(int _Loai_Hinh)
        {
            for (int t = 0; t < So_Gach; t++)
            {
                Gra_Tiep.FillRectangle(Co_Ve_Xoa, Gach_Tiep[t]);
            }
            if (_Loai_Hinh == 0)//chu T
            {
                Gach_Tiep[0] = new Rectangle(Kich_Thuoc, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(Kich_Thuoc * 2, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(Kich_Thuoc * 3, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(Kich_Thuoc * 2, 50, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 1)//chu Z
            {
                Gach_Tiep[0] = new Rectangle(30, Kich_Thuoc, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(30, Kich_Thuoc * 2, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(50, Kich_Thuoc * 2, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(50, Kich_Thuoc * 3, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 2)//hinh Vuong
            {
                Gach_Tiep[0] = new Rectangle(30, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(30, 50, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(50, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(50, 50, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 3)//thanh Doc
            {
                Gach_Tiep[0] = new Rectangle(Kich_Thuoc * 2, 10, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(Kich_Thuoc * 2, 30, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(Kich_Thuoc * 2, 50, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(Kich_Thuoc * 2, 70, Kich_Thuoc, Kich_Thuoc);
            }
            if (_Loai_Hinh == 4)//chu L
            {
                Gach_Tiep[0] = new Rectangle(30, Kich_Thuoc, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[1] = new Rectangle(30, Kich_Thuoc * 2, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[2] = new Rectangle(30, Kich_Thuoc * 3, Kich_Thuoc, Kich_Thuoc);
                Gach_Tiep[3] = new Rectangle(50, Kich_Thuoc * 3, Kich_Thuoc, Kich_Thuoc);
            }
            for (int t = 0; t < So_Gach; t++)
            {
                Gra_Tiep.FillRectangle(Co_Ve_Tiep, Gach_Tiep[t]);
            }
        }
        private void frmChinh_KeyDown(object sender, KeyEventArgs e_Down)
        {
            if ((Cho_Phep == 1) && (Dai_Hinh == 0))
            {
                if (e_Down.KeyCode == Keys.Left)
                {
                    if (KiemTra_Trai() == 1)
                    {
                        Cho_Phep = 0; Sang_Trai();
                    }
                }
                if (e_Down.KeyCode == Keys.Right)
                {
                    if (KiemTra_Phai() == 1)
                    {
                        Cho_Phep = 0; Sang_Phai();
                    }
                }
                if (e_Down.KeyCode == Keys.Up)
                {
                    if (KiemTra_Xoay() == 1)
                    {
                        Cho_Phep = 0; Xoay_Hinh();
                    }
                }

                #region kiem tra Duoi

                if (KiemTra_Duoi() == 0)
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Click"); }
                    Xoa_An();
                    if (Thua == 0)
                    {
                        Chon_Hinh(); Load_Tam(); Load_Hinh();
                        if (Thua == 0)
                        {
                            timer_Gach.Interval = Time_MacDinh; timer_Gach.Start(); Cho_Phep = 1;
                        }
                    }
                }
                else { Xuong_Di = 1; Cho_Phep = 1; }

                #endregion

                if (e_Down.KeyCode == Keys.Down)
                {
                    if (Xuong_Di == 1)
                    {
                        timer_Gach.Interval = Time_Max;
                    }
                    else
                    {
                        timer_Gach.Interval = Time_MacDinh;
                    }
                }
            }
        }

        private void frmChinh_KeyUp(object sender, KeyEventArgs e_Up)
        {
            if ((Cho_Phep == 1) && (Thua == 0) && (e_Up.KeyCode != Keys.Space))
            {
                if (e_Up.KeyCode == Keys.Down) { timer_Gach.Interval = Time_MacDinh; }
            }
        }

        private int KiemTra_Xoay()
        {
            int KQ_Xoay = 0;

            #region kiem tra chu T

            if (Loai_Hinh == 0)//chu T -> T trai
            {
                if ((ViTri_Y[0] > 0) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0]] == So_Mau))
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 5)//chu T trai -> T lon nguoc
            {
                if ((ViTri_X[0] < So_Cot - 1) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau))
                {
                    if ((Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 10)//chu T lon nguoc -> T phai
            {
                if ((ViTri_Y[0] < So_Hang - 1) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau))
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 15)//chu T phai -> T cu
            {
                if ((ViTri_X[0] > 0) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau))
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            #region kiem tra chu Z

            if (Loai_Hinh == 1)//chu Z -> Z ngang
            {
                if ((ViTri_X[0] > 0) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau))
                {
                    if (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 6)//chu Z ngang -> Z
            {
                if ((ViTri_Y[0] > 0) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                {
                    if (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            #region kiem tra thanh Doc + Ngang

            if (Loai_Hinh == 3)//thanh Doc -> Ngang
            {
                if ((ViTri_X[0] > 1) && (ViTri_X[0] < So_Cot - 1) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 2] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau))
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] - 2] == So_Mau))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 8)//thanh Ngang -> Doc
            {
                if ((ViTri_Y[0] > 0) && (ViTri_Y[0] < So_Hang - 2) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0]] == So_Mau))
                {
                    if ((Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 2] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 2, ViTri_X[0] - 2] == So_Mau))//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            #region kiem tra chu L

            if (Loai_Hinh == 4)//chu L -> L xap
            {
                if ((ViTri_X[0] > 0) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau))
                {
                    if (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 9)//chu L xap -> L trai
            {
                if ((ViTri_Y[0] > 0) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau))
                {
                    if (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 14)//chu L trai -> L ngua
            {
                if ((ViTri_X[0] < So_Cot - 1) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] - 1] == So_Mau) && (Mau_Cu[ViTri_Y[0], ViTri_X[0] + 1] == So_Mau) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] + 1] == So_Mau))
                {
                    if (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] - 1] == So_Mau)//pham vi xoay
                    {
                        KQ_Xoay = 1;
                    }
                }
            }
            if (Loai_Hinh == 19)//chu L ngua -> nhu cu
            {
                if ((ViTri_Y[0] < So_Hang - 1) && (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0]] == So_Mau) && (Mau_Cu[ViTri_Y[0] + 1, ViTri_X[0] + 1] == So_Mau))
                {
                    if (Mau_Cu[ViTri_Y[0] - 1, ViTri_X[0] - 1] == So_Mau)
                    {
                        KQ_Xoay = 1;
                    }
                }
            }

            #endregion

            return KQ_Xoay;
        }

        private void Xoay_Hinh()
        {
            if (Loai_Hinh != 2) { Loai_Hinh = Loai_Hinh + So_Hinh; }

            #region xoay chu T

            if (Loai_Hinh % So_Hinh == 0)//chu T
            {
                if (Loai_Hinh == 5)//chu T trai
                {
                    Xoa_Gach(2);//xoa Phai
                    ViTri_X[2] = ViTri_X[0]; ViTri_Y[2] = ViTri_Y[0] - 1;//dich Phai -> vi tri moi = Tren
                    Ve_Gach(2);//load Phai -> vi tri moI = Tren
                }
                if (Loai_Hinh == 10)//chu T lon nguoc
                {
                    Xoa_Gach(3);//xoa Duoi
                    ViTri_Y[3] = ViTri_Y[0]; ViTri_X[3] = ViTri_X[0] + 1;//dich Duoi -> vi tri moi = Phai
                    Ve_Gach(3);//load Duoi -> vi tri moi = Phai
                }
                if (Loai_Hinh == 15)//chu T phai
                {
                    Xoa_Gach(1);//xoa Trai
                    ViTri_X[1] = ViTri_X[0]; ViTri_Y[1] = ViTri_Y[0] + 1;//dich Trai -> vi tri moi = Duoi
                    Ve_Gach(1);//load Trai -> vi tri moi = Duoi
                }
                if (Loai_Hinh == 20)//tro lai nhu cu
                {
                    Loai_Hinh = 0;
                    Xoa_Gach(2);//xoa Tren
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] - 1;//dich Trai ve cho cu
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] + 1;//dich Phai ve cho cu
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0];//dich Duoi ve cho cu
                    Ve_Gach(1);//load Trai
                }
            }

            #endregion

            #region xoay chu Z

            if ((Loai_Hinh == 6) || (Loai_Hinh == 11))//chu Z
            {
                if (Loai_Hinh == 6)//chu Z ngang
                {
                    Xoa_Gach(1);//xoa Tren
                    ViTri_Y[1] = ViTri_Y[0] + 1; ViTri_X[1] = ViTri_X[0] - 1;//dich Tren -> Trai
                    Ve_Gach(1);//load Tren -> Trai
                    Xoa_Gach(3);//xoa Duoi Phai
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0];//dich Duoi Phai -> Duoi
                    Ve_Gach(3);//load Duoi Phai -> Duoi
                }
                if (Loai_Hinh == 11)//tro lai nhu cu
                {
                    Loai_Hinh = 1;
                    Xoa_Gach(1);//xoa Trai
                    Xoa_Gach(3);//xoa Duoi
                    ViTri_Y[1] = ViTri_Y[0] - 1; ViTri_X[1] = ViTri_X[0];//dich Tren ve cho cu
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0] + 1;//dich Duoi Phai ve cho cu
                    Ve_Gach(1);//load Tren ve cho cu
                    Ve_Gach(3);//load Phai ve cho cu
                }
            }

            #endregion

            #region xoay thanh Doc

            if ((Loai_Hinh == 8) || (Loai_Hinh == 13))//thanh Doc
            {
                if (Loai_Hinh == 8)//thanh Ngang
                {
                    Xoa_Gach(1);//xoa Tren
                    Xoa_Gach(2);//xoa Duoi
                    Xoa_Gach(3);//xoa Duoi Cung
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] + 1;//dich Tren -> Phai
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] - 1;//dich Duoi -> Trai
                    ViTri_Y[3] = ViTri_Y[0]; ViTri_X[3] = ViTri_X[0] - 2;//dich Duoi Cung -> Trai Cung
                    Ve_Gach(1);//load Tren -> Trai
                    Ve_Gach(2);//load Duoi -> Phai
                    Ve_Gach(3);//load Duoi Cung -> Phai Cung
                }
                if (Loai_Hinh == 13)//tro lai nhu cu
                {
                    Loai_Hinh = 3;
                    Xoa_Gach(1);//xoa Phai
                    Xoa_Gach(2);//xoa Trai
                    Xoa_Gach(3);//xoa Trai Cung
                    ViTri_Y[1] = ViTri_Y[0] - 1; ViTri_X[1] = ViTri_X[0];//dich Tren ve cho cu
                    ViTri_Y[2] = ViTri_Y[0] + 1; ViTri_X[2] = ViTri_X[0];//dich Duoi ve cho cu
                    ViTri_Y[3] = ViTri_Y[0] + 2; ViTri_X[3] = ViTri_X[0];//dich Duoi Cung ve cho cu
                    Ve_Gach(1);//load Tren ve cho cu
                    Ve_Gach(2);//load Duoi ve cho cu
                    Ve_Gach(3);//load Duoi Cung ve cho cu
                }
            }

            #endregion

            #region xoay chu L

            if ((Loai_Hinh == 9) || (Loai_Hinh == 14) || (Loai_Hinh == 19) || (Loai_Hinh == 24))//chu L
            {
                Xoa_Gach(1); Xoa_Gach(2); Xoa_Gach(3);
                if (Loai_Hinh == 9)//chu L xap
                {
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] + 1;//dich Tren -> Phai
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] - 1;//dich Duoi -> Trai
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0] - 1;//dich Phai -> Duoi
                }
                if (Loai_Hinh == 14)//chu L trai
                {
                    ViTri_Y[1] = ViTri_Y[0] + 1; ViTri_X[1] = ViTri_X[0];//dich Phai -> Duoi
                    ViTri_Y[2] = ViTri_Y[0] - 1; ViTri_X[2] = ViTri_X[0];//dich Trai -> Tren
                    ViTri_Y[3] = ViTri_Y[0] - 1; ViTri_X[3] = ViTri_X[0] - 1;//dich Duoi -> Trai
                }
                if (Loai_Hinh == 19)//chu L ngua
                {
                    ViTri_Y[1] = ViTri_Y[0]; ViTri_X[1] = ViTri_X[0] - 1;//dich Duoi -> Trai
                    ViTri_Y[2] = ViTri_Y[0]; ViTri_X[2] = ViTri_X[0] + 1;//dich Tren -> Phai
                    ViTri_Y[3] = ViTri_Y[0] - 1; ViTri_X[3] = ViTri_X[0] + 1;//dich Trai -> Tren
                }
                if (Loai_Hinh == 24)//tro lai nhu cu
                {
                    Loai_Hinh = 4;
                    ViTri_Y[1] = ViTri_Y[0] - 1; ViTri_X[1] = ViTri_X[0];//dich Trai -> Tren
                    ViTri_Y[2] = ViTri_Y[0] + 1; ViTri_X[2] = ViTri_X[0];//dich Phai -> Duoi
                    ViTri_Y[3] = ViTri_Y[0] + 1; ViTri_X[3] = ViTri_X[0] + 1;//dich Tren -> Phai
                }
                Ve_Gach(1); Ve_Gach(2); Ve_Gach(3);
            }

            #endregion
        }

        private void Xoa_An()
        {
            int Cot_An, Hang_An = 0;
            timer_Gach.Stop(); Cho_Phep = 0;
            if (Dinh_Hinh < Dinh_Hang) { Dinh_Hang = Dinh_Hinh; }

            #region Tim hang an

            for (int i = Dinh_Hang; i < So_Hang; i++)
            {
                Cot_An = 0;
                for (int j = 0; j < So_Cot; j++)
                {
                    if (Mau_Cu[i, j] != So_Mau) { Cot_An++; }
                    else { break; }
                }
                if (Cot_An == So_Cot)
                {
                    Diem = Diem + So_Cot;
                    arrHang_An[Hang_An] = i; Hang_An++;
                    if (Hang_An == So_Gach) { break; }
                }
            }

            #endregion

            #region Dich hang an

            if (Hang_An > 0)
            {
                int Hien_Tai = Hang_An - 1, Da_Lay = arrHang_An[Hien_Tai], Dem_Khac = 0;
                for (int i = arrHang_An[Hang_An - 1]; i >= Dinh_Hang; i--)
                {
                    if (i > Dinh_Hang + (Hang_An - 1))
                    {
                        if (Hien_Tai > -1)
                        {
                            if (i == arrHang_An[Hien_Tai]) { Hien_Tai--; }
                            for (int h = Da_Lay - 1; h >= Dinh_Hang; h--)
                            {
                                Dem_Khac = 0;
                                for (int s = 0; s <= Hien_Tai; s++)
                                {
                                    if (h == arrHang_An[s]) { break; }
                                    else { Dem_Khac++; }
                                }
                                if (Dem_Khac == Hien_Tai + 1) { Da_Lay = h; break; }
                            }
                            if (Dem_Khac == Hien_Tai + 1)
                            {
                                for (int j = 0; j < So_Cot; j++)
                                {
                                    Mau_Cu[i, j] = Mau_Cu[Da_Lay, j];
                                    if (Mau_Cu[i, j] != So_Mau)
                                    {
                                        Gra.FillRectangle(new SolidBrush(Color.FromArgb(255, Mau_Gach[Mau_Cu[i, j], 0], Mau_Gach[Mau_Cu[i, j], 1], Mau_Gach[Mau_Cu[i, j], 2])), Gach[i, j]);
                                    }
                                    else { Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]); }
                                }
                            }
                            else
                            {
                                Da_Lay = i - 1;
                            }
                        }
                        else
                        {
                            Da_Lay = Da_Lay - 1;
                            for (int j = 0; j < So_Cot; j++)
                            {
                                Mau_Cu[i, j] = Mau_Cu[Da_Lay, j];
                                if (Mau_Cu[i, j] != So_Mau)
                                {
                                    Gra.FillRectangle(new SolidBrush(Color.FromArgb(255, Mau_Gach[Mau_Cu[i, j], 0], Mau_Gach[Mau_Cu[i, j], 1], Mau_Gach[Mau_Cu[i, j], 2])), Gach[i, j]);
                                }
                                else { Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]); }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < So_Cot; j++)
                        {
                            Mau_Cu[i, j] = So_Mau;
                            Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                        }
                    }
                }
                lblDiem.Text = Diem.ToString(); Dinh_Hang = Dinh_Hang + Hang_An;
                if (Diem >= Max_HT)
                {
                    if (Choi_Nhac == 1) { Am_Thanh("Thang_KT"); }
                    Cap_Do++; Max_HT = Cap_Do * Diem_Max;
                    if (Cap_Do <= CapDo_Max)
                    {
                        Time_MacDinh = Time_MacDinh - ((Cap_Do - 1) * Tang_Toc); timer_Gach.Interval = Time_MacDinh;
                        if (Cap_Do < 10) { lblCap_Do.Text = "LEVEL : 0" + Cap_Do.ToString(); }
                        else { lblCap_Do.Text = "LEVEL : " + Cap_Do.ToString(); }
                    }
                    else
                    {
                        Thua = 1; Tiep = 0; Time_Thua = So_Time_Thua; timer_Gach.Stop(); Cho_Phep = 0; Nhap_Nhay = 1; timer_Time.Interval = 100; Bat_Dau1 = 0; Dang_Choi = 0;
                    }
                }
                else
                {
                    if (Choi_Nhac == 1) { Am_Thanh("An"); }
                }
            }

            #endregion

        }
        private void Thua_Nhap_Nhay()
        {
            if (Nhap_Nhay == 1)
            {
                Nhap_Nhay = 0;
                for (int i = 0; i < So_Hang; i++)
                {
                    for (int j = 0; j < So_Cot; j++)
                    {
                        if (Mau_Cu[i, j] != So_Mau)
                        {
                            Gra.FillRectangle(Co_Ve_Xoa, Gach[i, j]);
                        }
                    }
                }
                
            }
            else
            {
                Nhap_Nhay = 1;
                for (int i = 0; i < So_Hang; i++)
                {
                    for (int j = 0; j < So_Cot; j++)
                    {
                        if (Mau_Cu[i, j] != So_Mau)
                        {
                            Gra.FillRectangle(new SolidBrush(Color.FromArgb(255, Mau_Gach[Mau_Cu[i, j], 0], Mau_Gach[Mau_Cu[i, j], 1], Mau_Gach[Mau_Cu[i, j], 2])), Gach[i, j]);
                        }
                    }
                }
            }
        }

    }
}