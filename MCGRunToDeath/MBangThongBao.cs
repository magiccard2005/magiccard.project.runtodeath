//Copyright (c) 2014 MagicCard
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MCChayAnhThuong;
using MCKhungCuaSo;

namespace MCBangThongBao
{
    public class MBangThongBao
    {
        private GraphicsDevice thietbidohoa;
        private string ma = "null";
        private Rectangle ktmanhinh;//kich thuoc man hinh lan luot la rong chuan, dai chuan, rong dien thoai, dai dien thoai
        private Vector2 tile, gstoado;//ti le rong, ti le dai, gia so toa do x, toa do y
        //cac bien thiet lap
        private Texture2D anhnen, diemmau;
        private Rectangle bovien;//padding cua 4 duong vien: left-top-right-bottom
        private Vector2 tdhtbang, tlhtbang;//toa do hien thi, ti le hien thi bang
        private Vector2 tdhtdiemmau, tlhtdiemmau;
        private int rongbang, daibang;
        private MChayAnhThuong[] nut = new MChayAnhThuong[2];//0 xac nhan, 1 huy bo
        private Boolean baohoi;
        private string giatri;
        //bien chay hieu ung
        private int kieuchay;//0 hien luon, 1 chay sang phai, 2 chay xuong, 3 chay sang trai, 4 chay len
        private int bienchay, slchay, tocdo, chaysangtoi;
        private Boolean chaybangvao, chaybangra, chaytoi, chaysang, ktchaybang, ktchaysangtoi;
        private Vector2 toadocuoi;
        //cac bien hoat dong
        private Vector2 toado;
        private int slanh, slkhung;
        private Texture2D[] danhsachanh;
        private Vector2[] toadodanhsachanh;
        private Rectangle[] bodanhsachanh;
        private MKhungCuaSo[] khungcuaso;
        private Vector2[] toadokhungcuaso;
        private Boolean htbangthongbao, khnhanh;
        private string luachon = "null";

        public MBangThongBao(GraphicsDevice dohoa, Texture2D anhnenbang, Rectangle borongvien, Texture2D anhxacnhanthuong, Texture2D anhxacnhankichhoat, Texture2D anhhuybothuong, Texture2D anhhuybokichhoat, SoundEffect amkichhoat, Rectangle kichthuocmanhinh, Vector2 tilemanhinh)
        {
            thietbidohoa = dohoa;
            ktmanhinh = kichthuocmanhinh;
            tile = tilemanhinh;
            gstoado = new Vector2((ktmanhinh.Width - ktmanhinh.X * tile.X) / 2, (ktmanhinh.Height - ktmanhinh.Y * tile.Y) / 2);
            anhnen = anhnenbang;
            tdhtdiemmau = gstoado;
            tlhtdiemmau = new Vector2(ktmanhinh.X * tile.X, ktmanhinh.Y * tile.Y);
            bovien = borongvien;//padding cua 4 duong vien: left-top-right-bottom
            nut[0] = new MChayAnhThuong(1, anhxacnhanthuong, anhxacnhankichhoat, Vector2.Zero, amkichhoat, ktmanhinh, tile);
            nut[1] = new MChayAnhThuong(1, anhhuybothuong, anhhuybokichhoat, Vector2.Zero, amkichhoat, ktmanhinh, tile);
        }
        public void CapNhatAnhNut(Texture2D anhxacnhanthuong, Texture2D anhxacnhankichhoat, Texture2D anhhuybothuong, Texture2D anhhuybokichhoat)
        {
            nut[0].NapHinhAnh(anhxacnhanthuong, anhxacnhankichhoat);
            nut[1].NapHinhAnh(anhhuybothuong, anhhuybokichhoat);
        }
        public void CapNhatToaDoNut(Vector2 toadonutxacnhanmoi, Vector2 toadonuthuybomoi)
        {
            //toa do cac nut tinh theo toa do bang da bo vien
            Vector2 tdvien = new Vector2(bovien.X, bovien.Y);
            nut[0].DiChuyen(toadonutxacnhanmoi + toado + tdvien);
            nut[1].DiChuyen(toadonuthuybomoi + toado + tdvien);
        }
        public void NapNoiDung(Vector2 toadobang, Texture2D[] manganh, Rectangle[] mangboanh, Vector2 toadonutxacnhan, Vector2 toadonuthuybo, Boolean thongbaohoi)
        {
            XoaKhungCuaSo();
            toado = toadobang;
            baohoi = thongbaohoi;
            danhsachanh = manganh;
            slanh = danhsachanh.Length;
            Array.Resize(ref bodanhsachanh, slanh);
            Array.Resize(ref toadodanhsachanh, slanh);
            for (int i = 0; i < slanh; i++)
            {
                //toa do cac hinh anh tinh theo toa do bang da bo vien
                toadodanhsachanh[i] = new Vector2(mangboanh[i].X + toado.X + bovien.X, mangboanh[i].Y + toado.Y + bovien.Y);
                bodanhsachanh[i].X = Convert.ToInt32((toadodanhsachanh[i].X) * tile.X + gstoado.X);
                bodanhsachanh[i].Y = Convert.ToInt32((toadodanhsachanh[i].Y) * tile.Y + gstoado.Y);
                if (mangboanh[i].Width <= 0) bodanhsachanh[i].Width = Convert.ToInt32(danhsachanh[i].Width * tile.X);
                else bodanhsachanh[i].Width = Convert.ToInt32(mangboanh[i].Width * tile.X);
                if (mangboanh[i].Height <= 0) bodanhsachanh[i].Height = Convert.ToInt32(danhsachanh[i].Height * tile.Y);
                else bodanhsachanh[i].Height = Convert.ToInt32(mangboanh[i].Height * tile.Y);
            }
            CapNhatToaDoNut(toadonutxacnhan, toadonuthuybo);
            tdhtbang = new Vector2(toado.X * tile.X + gstoado.X, toado.Y * tile.Y + gstoado.Y);
            CoDanBang();
        }
        public void NapKhungCuaSo(MKhungCuaSo[] dskhungcuaso, Vector2[] dstoadokhungcuaso)
        {
            XoaKhungCuaSo();
            khungcuaso = dskhungcuaso;
            slkhung = khungcuaso.Length;
            Array.Resize(ref toadokhungcuaso, slkhung);
            Vector2 tdvien = new Vector2(bovien.X, bovien.Y);
            for (int i = 0; i < slkhung; i++)
            {
                //toa do cac khung cua so tinh theo toa do bang da bo vien
                toadokhungcuaso[i] = dstoadokhungcuaso[i] + toado + tdvien;
                khungcuaso[i].DiChuyen(toadokhungcuaso[i]);
            }
        }
        private void CoDanBang()
        {
            int minx, maxx, miny, maxy;
            minx = Convert.ToInt32((bodanhsachanh[0].X - gstoado.X) / tile.X);
            maxx = Convert.ToInt32((bodanhsachanh[0].X - gstoado.X + bodanhsachanh[0].Width) / tile.X);
            miny = Convert.ToInt32((bodanhsachanh[0].Y - gstoado.Y) / tile.Y);
            maxy = Convert.ToInt32((bodanhsachanh[0].Y - gstoado.Y + bodanhsachanh[0].Height) / tile.Y);
            for (int i = 0; i < slanh; i++)
            {
                if (minx > Convert.ToInt32((bodanhsachanh[i].X - gstoado.X) / tile.X)) minx = Convert.ToInt32((bodanhsachanh[i].X - gstoado.X) / tile.X);
                if (maxx < Convert.ToInt32((bodanhsachanh[i].X - gstoado.X + bodanhsachanh[i].Width) / tile.X)) maxx = Convert.ToInt32((bodanhsachanh[i].X - gstoado.X + bodanhsachanh[i].Width) / tile.X);
                if (miny > Convert.ToInt32((bodanhsachanh[i].Y - gstoado.Y) / tile.Y)) miny = Convert.ToInt32((bodanhsachanh[i].Y - gstoado.Y) / tile.Y);
                if (maxy < Convert.ToInt32((bodanhsachanh[i].Y - gstoado.Y + bodanhsachanh[i].Height) / tile.Y)) maxy = Convert.ToInt32((bodanhsachanh[i].Y - gstoado.Y + bodanhsachanh[i].Height) / tile.Y);
            }
            for (int i = 0; i < 2; i++)
            {
                if (minx > Convert.ToInt32(nut[i].LayToaDo().X)) minx = Convert.ToInt32(nut[i].LayToaDo().X);
                if (maxx < Convert.ToInt32(nut[i].LayToaDo().X) + nut[i].LayChieuRong()) maxx = Convert.ToInt32(nut[i].LayToaDo().X + nut[i].LayChieuRong());
                if (miny > Convert.ToInt32(nut[i].LayToaDo().Y)) miny = Convert.ToInt32(nut[i].LayToaDo().Y);
                if (maxy < Convert.ToInt32(nut[i].LayToaDo().Y) + nut[i].LayChieuDai()) maxy = Convert.ToInt32(nut[i].LayToaDo().Y + nut[i].LayChieuDai());
            }
            rongbang = (maxx - minx + bovien.X + bovien.Width);
            daibang = (maxy - miny + bovien.Y + bovien.Height);
            tlhtbang = new Vector2((float)rongbang / (float)anhnen.Width * tile.X, (float)daibang / (float)anhnen.Height * tile.Y);
        }
        public void XoaKhungCuaSo()
        {
            slkhung = 0;
            Array.Resize(ref khungcuaso, slkhung);
            Array.Resize(ref toadokhungcuaso, slkhung);
        }
        public string LayMaBang()
        {
            return ma;
        }
        public void BatBang(string mabang, Vector2 toadomoi, ref int chiso, int kieuchaybang, int tocdochay, Boolean kichhoatnhanh)
        {
            if (htbangthongbao == false)
            {
                ma = mabang;
                giatri = "null";
                chiso++;
                NapChiSoThanhPhan(chiso);
                LamMoiHieuUng();
                DuLieuChayBang(toadomoi, kieuchaybang, tocdochay);
                if ((kieuchay == 0) & (toadomoi != toado)) DiChuyen(toadomoi);
                chaytoi = true;
                khnhanh = kichhoatnhanh;
                htbangthongbao = true;
            }
        }
        private void LamMoiHieuUng()
        {
            bienchay = 0;
            chaysangtoi = 0;
            chaybangvao = false;
            chaybangra = false;
            chaytoi = false;
            chaysang = false;
            ktchaybang = false;
            ktchaysangtoi = false;
        }
        public void NapChiSoThanhPhan(int chisomoi)
        {
            nut[0].NapChiSoLop(chisomoi);
            nut[1].NapChiSoLop(chisomoi);
            for (int i = 0; i < slkhung; i++) khungcuaso[i].NapChiSoLop(chisomoi);
        }
        private void DuLieuChayBang(Vector2 toadoketthuc, int kieuchaybang, int tocdochay)
        {
            toadocuoi = toadoketthuc;
            kieuchay = kieuchaybang;
            tocdo = tocdochay;
            chaybangvao = true;
            if (kieuchay == 1)
            {
                slchay = (int)((toadocuoi.X + rongbang) / tocdo);
                DiChuyen(new Vector2(-rongbang, toadocuoi.Y));
            }
            else if (kieuchay == 2)
            {
                slchay = (int)((toadocuoi.Y + daibang) / tocdo);
                DiChuyen(new Vector2(toadocuoi.X, -daibang));
            }
            else if (kieuchay == 3)
            {
                slchay = (int)((toadocuoi.X + rongbang) / tocdo);
                DiChuyen(new Vector2(ktmanhinh.X, toadocuoi.Y));
            }
            else if (kieuchay == 4)
            {
                slchay = (int)((toadocuoi.Y + daibang) / tocdo);
                DiChuyen(new Vector2(toadocuoi.X, ktmanhinh.Y));
            }
            else chaybangvao = false;
        }
        public void TatBang()
        {
            if ((htbangthongbao) & (chaybangvao == false) & (chaybangra == false) & (chaytoi == false) & (chaysang == false))
            {
                Boolean baotat = true;
                foreach (MChayAnhThuong i in nut)
                {
                    if (i.DangHoatDong()) baotat = false;
                    break;
                }
                if (baotat)
                {
                    chaysang = true;
                    chaybangra = true;
                }
            }
        }
        public void DiChuyen(Vector2 toadomoi)
        {
            Vector2 giaso = toadomoi - toado;
            toado = toadomoi;
            tdhtbang = new Vector2(toado.X * tile.X + gstoado.X, toado.Y * tile.Y + gstoado.Y);
            for (int i = 0; i < slanh; i++)
            {
                toadodanhsachanh[i] += giaso;
                bodanhsachanh[i].X = Convert.ToInt32((toadodanhsachanh[i].X) * tile.X + gstoado.X);
                bodanhsachanh[i].Y = Convert.ToInt32((toadodanhsachanh[i].Y) * tile.Y + gstoado.Y);
            }
            for (int i = 0; i < slkhung; i++)
            {
                toadokhungcuaso[i] += giaso;
                khungcuaso[i].DiChuyen(toadokhungcuaso[i]);
            }
            nut[0].DiChuyen(new Vector2(nut[0].LayToaDo().X, nut[0].LayToaDo().Y) + giaso);
            nut[1].DiChuyen(new Vector2(nut[1].LayToaDo().X, nut[1].LayToaDo().Y) + giaso);
        }
        public Boolean TinhTrangHienThi()
        {
            return htbangthongbao;
        }
        private void HieuUngSangToi()
        {
            if (chaytoi)
            {
                if (chaysangtoi == 10)
                {
                    chaytoi = false;
                }
                else
                {
                    chaysangtoi++;
                    CapNhatDiemMau(chaysangtoi);
                }
            }
            else if (chaysang)
            {
                if (chaysangtoi == 0)
                {
                    chaysang = false;
                    ktchaysangtoi = true;
                }
                else
                {
                    chaysangtoi--;
                    CapNhatDiemMau(chaysangtoi);
                }
            }
        }
        private void CapNhatDiemMau(int chiso)
        {
            if (diemmau != null) diemmau.Dispose();
            diemmau = new Texture2D(thietbidohoa, 1, 1, false, SurfaceFormat.Color);
            diemmau.SetData<Color>(new Color[] { new Color(0, 0, 0, chiso * 15) });
        }
        public void KhoiPhucDiemMau()
        {
            CapNhatDiemMau(chaysangtoi);
        }
        private void HieuUngChayBang()
        {
            if (chaybangvao)
            {
                if (bienchay == slchay)
                {
                    chaybangvao = false;
                    DiChuyen(toadocuoi);
                }
                else
                {
                    bienchay++;
                    CapNhatChayBang();
                }
            }
            else if (chaybangra)
            {
                if (bienchay == 0)
                {
                    chaybangra = false;
                    ktchaybang = true;
                }
                else
                {
                    bienchay--;
                    CapNhatChayBang();
                }
            }
        }
        private void CapNhatChayBang()
        {
            if (kieuchay == 1) DiChuyen(new Vector2(bienchay * tocdo - rongbang, toadocuoi.Y));
            else if (kieuchay == 2) DiChuyen(new Vector2(toadocuoi.X, bienchay * tocdo - daibang));
            else if (kieuchay == 3) DiChuyen(new Vector2(ktmanhinh.X - bienchay * tocdo, toadocuoi.Y));
            else if (kieuchay == 4) DiChuyen(new Vector2(toadocuoi.X, ktmanhinh.Y - bienchay * tocdo));
        }
        public void HoatDong(ref int chiso, MouseState chuothientai, MouseState chuottruocdo, ref string giatrilay, float kichthuocamthanh)
        {
            HieuUngSangToi();
            if (htbangthongbao)
            {
                HieuUngChayBang();
                if ((ktchaybang) & (ktchaysangtoi))
                {
                    chiso--;
                    htbangthongbao = false;
                    if (!khnhanh) giatrilay = giatri;
                }
                if ((chaybangvao == false) & (chaybangra == false) & (chaytoi == false) & (chaysang == false))
                {
                    for (int i = 0; i < slkhung; i++)
                    {
                        khungcuaso[i].HoatDong(ref chiso, chuothientai, chuottruocdo);
                    }
                    nut[1].KichHoatThuong(ref chiso, chuothientai, chuottruocdo, "nuthuybo", ref luachon, kichthuocamthanh);
                    if (baohoi) nut[0].KichHoatThuong(ref chiso, chuothientai, chuottruocdo, "nutxacnhan", ref luachon, kichthuocamthanh);
                    if (luachon == "nuthuybo")
                    {
                        giatri = ma + "-" + "0";
                        if (khnhanh) giatrilay = giatri;
                        TatBang();
                    }
                    else if (luachon == "nutxacnhan")
                    {
                        giatri = ma + "-" + "1";
                        if (khnhanh) giatrilay = giatri;
                        TatBang();
                    }
                    luachon = "null";
                }
            }
        }
        public void HienThi(SpriteBatch nenve)
        {
            if (chaysangtoi > 0) nenve.Draw(diemmau, tdhtdiemmau, null, Color.White, 0, Vector2.Zero, tlhtdiemmau, SpriteEffects.None, 0f);
            if (htbangthongbao)
            {
                nenve.Draw(anhnen, tdhtbang, null, Color.White, 0, Vector2.Zero, tlhtbang, SpriteEffects.None, 0f);
                for (int i = 0; i < slanh; i++)
                {
                    nenve.Draw(danhsachanh[i], bodanhsachanh[i], Color.White);
                }
                for (int i = 0; i < slkhung; i++)
                {
                    khungcuaso[i].HienThi(nenve);
                }
                nut[1].HienThiThuong(nenve);
                if (baohoi) nut[0].HienThiThuong(nenve);
            }
        }
    }
}