//Copyright (c) 2014 MagicCard
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MCChayAnhDong
{
    public class MChayAnhDong
    {
        private Rectangle ktmanhinh;//kich thuoc man hinh lan luot la rong chuan, dai chuan, rong dien thoai, dai dien thoai
        private Vector2 tile, gstoado;//ti le rong, ti le dai, gia so toa do x, toa do y
        private Vector2 toadoanh, tamxoay;
        private Vector2 tdhtthuong, tlhtthuong, tdhtxoay, tlhtxoay;
        private Texture2D[] manganh;
        private Texture2D anhhienthi, anhkichhoat;
        private int chisolop, chayhieuung, dochamkichhoat = 5, tongsoanh, bienchay, hoatcanh, sodemluot;
        private float chieurong, chieudai, gocxoay, gtgocxoay, gsx, gsy;
        private Boolean baolaplai, baochay = true;
        private SoundEffect amkichhoat;
        public MChayAnhDong(int chiso, Texture2D[] hinhanhthuong, Texture2D hinhanhkichhoat, Vector2 toado, Boolean laplai, SoundEffect amthanhkichhoat, Rectangle kichthuocmanhinh, Vector2 tilemanhinh)
        {
            chisolop = chiso;
            ktmanhinh = kichthuocmanhinh;
            tile = tilemanhinh;
            gstoado = new Vector2((ktmanhinh.Width - ktmanhinh.X * tile.X) / 2, (ktmanhinh.Height - ktmanhinh.Y * tile.Y) / 2);
            baolaplai = laplai;
            NapHinhAnh(hinhanhthuong, hinhanhkichhoat);
            DiChuyen(toado);
            tamxoay = new Vector2(chieurong / 2, chieudai / 2);
            amkichhoat = amthanhkichhoat;
        }
        public Vector2 LayToaDo()
        {
            return toadoanh;
        }
        public Texture2D LayHinhAnh()
        {
            return anhhienthi;
        }
        public float LayChieuRong()
        {
            return chieurong;
        }
        public float LayChieuDai()
        {
            return chieudai;
        }
        public float LayGocXoay()
        {
            return gocxoay;
        }
        public void NapChiSoLop(int chiso)
        {
            chisolop = chiso;
        }
        public void CaiDoChamKichHoat(int docham)
        {
            dochamkichhoat = docham;
        }
        public void NapHinhAnh(Texture2D[] hinhanhthuong, Texture2D hinhanhkichhoat)
        {
            if (hinhanhthuong != null)
            {
                manganh = hinhanhthuong;
                anhhienthi = manganh[0];
                anhkichhoat = hinhanhkichhoat;
                tongsoanh = manganh.Length;
                chieurong = manganh[0].Width;
                chieudai = manganh[0].Height;
                DoiKichThuoc(chieurong, chieudai);
            }
        }
        public void DiChuyen(Vector2 toado)
        {
            toadoanh = toado;
            tdhtthuong = new Vector2(toadoanh.X * tile.X + gstoado.X, toadoanh.Y * tile.Y + gstoado.Y);
            tdhtxoay = tdhtthuong + new Vector2(chieurong / 2 * tile.X, chieudai / 2 * tile.Y);
        }
        public void DoiKichThuoc(float rong, float dai)
        {
            if (rong > 0)
            {
                chieurong = rong;
                tlhtthuong.X = chieurong / (float)anhhienthi.Width * tile.X;
                tlhtxoay.X = chieurong / (float)anhhienthi.Width * tile.X * gsx;
            }
            if (dai > 0)
            {
                chieudai = dai;
                tlhtthuong.Y = chieudai / (float)anhhienthi.Height * tile.Y;
                tlhtxoay.Y = chieudai / (float)anhhienthi.Height * tile.Y * gsy;
            }
        }
        public void XoayAnh(float goc, Vector2 tam)
        {
            gocxoay = goc;
            tamxoay = tam;
            int dau = 1;
            if (gocxoay < 0) dau = -1;
            int chgoc = (Math.Abs(Convert.ToInt32(gocxoay)) % 360) * dau;
            if (dau == -1) chgoc += 360;
            gtgocxoay = chgoc * (float)Math.PI / 180;
            float gstlx = tile.Y / tile.X, gstly = tile.X / tile.Y;
            float pi1p2 = (float)(Math.PI / 2);
            if (chgoc < 90)
            {
                gsx = (float)((pi1p2 - (1 - gstlx) * gtgocxoay) / pi1p2);//pt duong thang tu diem (0, 1) den (pi/2, gstlx)
                gsy = (float)((pi1p2 - (1 - gstly) * gtgocxoay) / pi1p2);//pt duong thang tu diem (0, 1) den (pi/2, gstly)
            }
            else if (chgoc < 180)
            {
                gsx = (float)((pi1p2 + (gstlx - 1) * Math.PI - (gstlx - 1) * gtgocxoay) / pi1p2);//pt duong thang tu diem (pi/2, gstlx) den (pi, 1)
                gsy = (float)((pi1p2 + (gstly - 1) * Math.PI - (gstly - 1) * gtgocxoay) / pi1p2);//pt duong thang tu diem (pi/2, gstlx) den (pi, 1)
            }
            else if (chgoc < 270)
            {
                gsx = (float)((pi1p2 + (1 - gstlx) * Math.PI - (1 - gstlx) * gtgocxoay) / pi1p2);//pt duong thang tu diem (pi, 1) den (3/2 pi, gstlx)
                gsy = (float)((pi1p2 + (1 - gstly) * Math.PI - (1 - gstly) * gtgocxoay) / pi1p2);//pt duong thang tu diem (pi, 1) den (3/2 pi, gstly)
            }
            else
            {
                gsx = (float)((pi1p2 + (gstlx - 1) * Math.PI * 2 - (gstlx - 1) * gtgocxoay) / pi1p2);//pt duong thang tu diem (3/2 pi, gstlx) den (2pi, 1)
                gsy = (float)((pi1p2 + (gstly - 1) * Math.PI * 2 - (gstly - 1) * gtgocxoay) / pi1p2);//pt duong thang tu diem (3/2 pi, gstly) den (2pi, 1)
            }
            tdhtxoay = tdhtthuong + new Vector2(chieurong / 2 * tile.X, chieudai / 2 * tile.Y);
            tlhtxoay.X = chieurong / (float)anhhienthi.Width * tile.X * gsx;
            tlhtxoay.Y = chieudai / (float)anhhienthi.Height * tile.Y * gsy;
        }
        public void ChayAnhDong(int tocdo)
        {
            if (baochay)
            {
                bienchay++;
                if (bienchay == tocdo)
                {
                    if (hoatcanh < tongsoanh - 1) hoatcanh++;
                    else
                    {
                        hoatcanh = 0;
                        if (baolaplai == false) baochay = false;
                    }
                    anhhienthi = manganh[hoatcanh];
                    bienchay = 0;
                }
            }
        }
        public Boolean KetThuc()
        {
            Boolean ketqua = true;
            if (baochay) ketqua = false;
            return ketqua;
        }
        public void ChayMoi()
        {
            baochay = true;
        }
        public void DungChay()
        {
            hoatcanh = 0;
            anhhienthi = manganh[hoatcanh];
            bienchay = 0;
            baochay = false;
        }
        public void KichHoatNhanh(int chiso, MouseState trohientai, MouseState trotruocdo, string giatricap, ref string giatrilay, float kichthuocamthanh)
        {
            if ((chisolop == chiso) & (giatrilay == "null"))
            {
                if (trohientai.LeftButton == ButtonState.Pressed && trotruocdo.LeftButton == ButtonState.Released)
                {
                    //chuyen the toa do kich theo man hinh chuan
                    int toadokichx = Convert.ToInt32((trohientai.X - gstoado.X) / tile.X);
                    int toadokichy = Convert.ToInt32((trohientai.Y - gstoado.Y) / tile.Y);
                    if (KiemTraChua(new Vector2(toadokichx, toadokichy), new Rectangle(Convert.ToInt32(toadoanh.X), Convert.ToInt32(toadoanh.Y), Convert.ToInt32(chieurong), Convert.ToInt32(chieudai))))
                    {
                        if (amkichhoat != null) amkichhoat.Play(kichthuocamthanh, 0, 0);
                        giatrilay = giatricap;
                    }
                }
            }
        }
        public void KichHoatThuong(ref int chiso, MouseState trohientai, MouseState trotruocdo, string giatricap, ref string giatrilay, float kichthuocamthanh)
        {
            if ((chisolop == chiso) & (giatrilay == "null"))
            {
                if (trohientai.LeftButton == ButtonState.Pressed && trotruocdo.LeftButton == ButtonState.Released)
                {
                    //chuyen the toa do kich theo man hinh chuan
                    int toadokichx = Convert.ToInt32((trohientai.X - gstoado.X) / tile.X);
                    int toadokichy = Convert.ToInt32((trohientai.Y - gstoado.Y) / tile.Y);
                    if (KiemTraChua(new Vector2(toadokichx, toadokichy), new Rectangle(Convert.ToInt32(toadoanh.X), Convert.ToInt32(toadoanh.Y), Convert.ToInt32(chieurong), Convert.ToInt32(chieudai))))
                    {
                        if (chayhieuung == 0)
                        {
                            if (amkichhoat != null) amkichhoat.Play(kichthuocamthanh, 0, 0);
                            giatrilay = "null";
                            chayhieuung++;
                            chiso++;
                        }
                    }
                }
            }
            else if (chayhieuung != 0)
            {
                if (chayhieuung > dochamkichhoat)
                {
                    giatrilay = giatricap;
                    chayhieuung = 0;
                    chiso--;
                }
                else chayhieuung++;
            }
        }
        public void KichHoatChon(int chiso, MouseState trohientai, MouseState trotruocdo, string giatricap, ref string giatrilay, float kichthuocamthanh)
        {
            if (chisolop == chiso)
            {
                if (trohientai.LeftButton == ButtonState.Pressed && trotruocdo.LeftButton == ButtonState.Released)
                {
                    //chuyen the toa do kich theo man hinh chuan
                    int toadokichx = Convert.ToInt32((trohientai.X - gstoado.X) / tile.X);
                    int toadokichy = Convert.ToInt32((trohientai.Y - gstoado.Y) / tile.Y);
                    if (KiemTraChua(new Vector2(toadokichx, toadokichy), new Rectangle(Convert.ToInt32(toadoanh.X), Convert.ToInt32(toadoanh.Y), Convert.ToInt32(chieurong), Convert.ToInt32(chieudai))) & (giatrilay == "null"))
                    {
                        if ((amkichhoat != null) & (sodemluot > 0)) amkichhoat.Play(kichthuocamthanh, 0, 0);
                        if (sodemluot < 2) sodemluot++;
                        giatrilay = giatricap + "-" + sodemluot.ToString();
                    }
                    else LamMoiAnhChon();
                }
            }
        }
        public void LamMoiAnhChon()
        {
            sodemluot = 0;
        }
        public Boolean DangHoatDong()
        {
            Boolean ketqua = false;
            if (chayhieuung != 0) ketqua = true;
            return ketqua;
        }
        public void HienThiThuongBT(SpriteBatch nenve)
        {
            nenve.Draw(anhhienthi, tdhtthuong, null, Color.White, 0, Vector2.Zero, tlhtthuong, SpriteEffects.None, 0f);
        }
        public void HienThiThuongKH(SpriteBatch nenve)
        {
            if ((DangHoatDong()) | (sodemluot != 0)) nenve.Draw(anhkichhoat, tdhtthuong, null, Color.White, 0, Vector2.Zero, tlhtthuong, SpriteEffects.None, 0f);
        }
        public void HienThiThuong(SpriteBatch nenve)
        {
            if ((!DangHoatDong()) & (sodemluot == 0)) HienThiThuongBT(nenve);
            HienThiThuongKH(nenve);
        }
        public void HienThiXoayBT(SpriteBatch nenve)
        {
            XoayHinhAnh(nenve, anhhienthi, gtgocxoay, tamxoay);
        }
        public void HienThiXoayKH(SpriteBatch nenve)
        {
            if ((DangHoatDong()) | (sodemluot != 0)) XoayHinhAnh(nenve, anhkichhoat, gtgocxoay, tamxoay);
        }
        public void HienThiXoay(SpriteBatch nenve)
        {
            if ((!DangHoatDong()) & (sodemluot == 0)) HienThiXoayBT(nenve);
            HienThiXoayKH(nenve);
        }
        private Boolean KiemTraChua(Vector2 diem, Rectangle vungchua)
        {
            Boolean ketqua = false;
            if ((diem.X >= vungchua.X) & (diem.X < vungchua.X + vungchua.Width) & (diem.Y >= vungchua.Y) & (diem.Y < vungchua.Y + vungchua.Height))
                ketqua = true;
            return ketqua;
        }
        private void XoayHinhAnh(SpriteBatch nenve, Texture2D hinhanh, float giatrigocxoay, Vector2 toadotamxoay)
        {
            //tam xoay phai tinh theo kich thuoc that cua hinh anh
            nenve.Draw(hinhanh, tdhtxoay, null, Color.White, giatrigocxoay, toadotamxoay, tlhtxoay, SpriteEffects.None, 0f);
            /*tam xoay la tam tinh theo kich thuoc that cua hinh anh cho du co su dung ham DoiKichThuoc() thi van la tam co dinh
            neu tam = (0,0) thi la xoay theo goc trai tren, (anh.width,0) thi goc phai tren...
            vung anh sau khi xoay van tinh theo vung anh cua bohinhanh(x,y,width,height), neu co ty so thi hien thi teo ty so bien dang.*/
        }
    }
}