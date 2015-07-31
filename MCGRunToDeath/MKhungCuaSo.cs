//Copyright (c) 2014 MagicCard
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MCChayAnhThuong;

namespace MCKhungCuaSo
{
    public class MKhungCuaSo
    {
        private GraphicsDevice thietbidohoa;
        private SpriteBatch nenve;
        private Rectangle ktmanhinh;//kich thuoc man hinh lan luot la rong chuan, dai chuan, rong dien thoai, dai dien thoai
        private Vector2 tile, gstoado;//ti le rong, ti le dai, gia so toa do x, toa do y
        private RenderTarget2D bonenvunganh;
        private Vector2 tdhtkhung, tlhtkhung;
        private Texture2D[] athanhphan;//0 topbt, 1 topkh, 2 downbt, 3 downkh, 4 leftbt, 5 leftkh, 6 rightbt, 7 rightkh, 8 nen dung, 9 nen ngang, 10 cuon dung, 11 cuon ngang
        private MChayAnhThuong[] nut = new MChayAnhThuong[8];//0 top, 1 down, 2 left, 3 right, 4 nen dung, 5 nen ngang, 6 cuon dung, 7 cuon ngang
        private Vector2[] tdthanhphan = new Vector2[8];//0 top, 1 down, 2 left, 3 right, 4 nen dung, 5 nen ngang, 6 cuon dung, 7 cuon ngang
        private Texture2D anoidunggoc;
        private Vector2 toadokhung, toadocuon;
        private int buoccuon = 10;//so pixel cuon di trong 1 lan
        private Boolean cuondung, cuonngang;
        private int hieudung, hieungang, daitruotdung, daitruotngang, dichuyendung, dichuyenngang;
        private int chiso;
        private string luachon = "null";
        public MKhungCuaSo(GraphicsDevice dohoa, int chisolop, Texture2D[] anhthanhphan, Texture2D anhnoidung, Rectangle bokhungcuaso, Rectangle kichthuocmanhinh, Vector2 tilemanhinh)
        {
            thietbidohoa = dohoa;
            nenve = new SpriteBatch(thietbidohoa);
            ktmanhinh = kichthuocmanhinh;
            tile = tilemanhinh;
            gstoado = new Vector2((ktmanhinh.Width - ktmanhinh.X * tile.X) / 2, (ktmanhinh.Height - ktmanhinh.Y * tile.Y) / 2);
            athanhphan = anhthanhphan;
            toadokhung = new Vector2(bokhungcuaso.X, bokhungcuaso.Y);
            bonenvunganh = new RenderTarget2D(thietbidohoa, bokhungcuaso.Width, bokhungcuaso.Height);
            for (int i = 0; i < 8; i++) tdthanhphan[i] = Vector2.Zero;//cac toa do anh mac dinh bang 0 het
            nut[0] = new MChayAnhThuong(1, athanhphan[0], athanhphan[1], tdthanhphan[0], null, ktmanhinh, tile);//nut top
            nut[1] = new MChayAnhThuong(1, athanhphan[2], athanhphan[3], tdthanhphan[1], null, ktmanhinh, tile);//nut down
            nut[2] = new MChayAnhThuong(1, athanhphan[4], athanhphan[5], tdthanhphan[2], null, ktmanhinh, tile);//nut left
            nut[3] = new MChayAnhThuong(1, athanhphan[6], athanhphan[7], tdthanhphan[3], null, ktmanhinh, tile);//nut right
            nut[4] = new MChayAnhThuong(1, athanhphan[8], null, tdthanhphan[4], null, ktmanhinh, tile);//nen dung
            nut[5] = new MChayAnhThuong(1, athanhphan[9], null, tdthanhphan[5], null, ktmanhinh, tile);//nen ngang
            nut[6] = new MChayAnhThuong(1, athanhphan[10], null, tdthanhphan[6], null, ktmanhinh, tile);//cuon dung
            nut[7] = new MChayAnhThuong(1, athanhphan[11], null, tdthanhphan[7], null, ktmanhinh, tile);//cuon ngang
            NapChiSoLop(chisolop);
            NapNoiDung(anhnoidung);//ve anh da cat theo khung
            tdhtkhung = new Vector2(toadokhung.X * tile.X + gstoado.X, toadokhung.Y * tile.Y + gstoado.Y);
            tlhtkhung = tile;
        }
        public void NapNoiDung(Texture2D anhnoidungmoi)//neu hinh anh duoc nap o phuong thuc khoi tao bi thay doi thi cung phai goi lai phuong thuc nay
        {
            anoidunggoc = anhnoidungmoi;
            //thiet lap cac thanh phan
            if (anoidunggoc.Width > bonenvunganh.Width) cuonngang = true;
            else cuonngang = false;
            if (anoidunggoc.Height > bonenvunganh.Height) cuondung = true;
            else cuondung = false;
            //thiet lap ti so cho thanh truot dung
            daitruotdung = bonenvunganh.Height - athanhphan[0].Height - athanhphan[2].Height - athanhphan[10].Height;
            if (cuondung)
            {
                hieudung = bonenvunganh.Height - anoidunggoc.Height;
                if (cuonngang) hieudung -= athanhphan[9].Height;
                dichuyendung = Convert.ToInt32((buoccuon / (float)Math.Abs(hieudung)) * daitruotdung);
            }
            //thiet lap ti so cho thanh truot ngang
            daitruotngang = bonenvunganh.Width - athanhphan[4].Width - athanhphan[6].Width - athanhphan[11].Width;
            if (cuonngang)
            {
                hieungang = bonenvunganh.Width - anoidunggoc.Width;
                if (cuondung) hieungang -= athanhphan[8].Width;
                dichuyenngang = Convert.ToInt32((buoccuon / (float)Math.Abs(hieungang)) * daitruotngang);
            }
            //tao lai hinh anh hien thi
            NapViTriPhuTung();
            toadocuon = Vector2.Zero;
            VeLai();
        }
        private void NapViTriPhuTung()
        {
            if (cuondung)//cuon dung
            {
                tdthanhphan[0] = new Vector2(toadokhung.X + bonenvunganh.Width - athanhphan[0].Width, toadokhung.Y);//nut top
                tdthanhphan[1] = new Vector2(toadokhung.X + bonenvunganh.Width - athanhphan[2].Width, toadokhung.Y + bonenvunganh.Height - athanhphan[2].Height);//nut down
                tdthanhphan[4] = new Vector2(toadokhung.X + bonenvunganh.Width - athanhphan[8].Width, toadokhung.Y + athanhphan[0].Height);//nen dung
                tdthanhphan[6] = new Vector2(toadokhung.X + bonenvunganh.Width - athanhphan[10].Width, toadokhung.Y + athanhphan[0].Height);//cuon dung
                nut[4].DoiKichThuoc(0, bonenvunganh.Height - athanhphan[0].Height - athanhphan[2].Height);//nen dung
            }
            if (cuonngang)//cuon ngang
            {
                tdthanhphan[2] = new Vector2(toadokhung.X, toadokhung.Y + bonenvunganh.Height - athanhphan[4].Height);//nut left
                tdthanhphan[3] = new Vector2(toadokhung.X + bonenvunganh.Width - athanhphan[6].Width, toadokhung.Y + bonenvunganh.Height - athanhphan[6].Height);//nut right
                tdthanhphan[5] = new Vector2(toadokhung.X + athanhphan[4].Width, toadokhung.Y + bonenvunganh.Height - athanhphan[9].Height);//nen ngang
                tdthanhphan[7] = new Vector2(toadokhung.X + athanhphan[4].Width, toadokhung.Y + bonenvunganh.Height - athanhphan[11].Height);//cuon ngang
                nut[5].DoiKichThuoc(bonenvunganh.Width - athanhphan[4].Width - athanhphan[6].Width, 0);//nen ngang
            }
            if ((cuondung) & (cuonngang))//cuon ca dung lan ngang
            {
                tdthanhphan[3] = new Vector2(toadokhung.X + bonenvunganh.Width - athanhphan[2].Width - athanhphan[6].Width, toadokhung.Y + bonenvunganh.Height - athanhphan[6].Height);//nut right
                nut[5].DoiKichThuoc(bonenvunganh.Width - athanhphan[2].Width - athanhphan[4].Width - athanhphan[6].Width, 0);//nen ngang
            }
            for (int i = 0; i < 8; i++) nut[i].DiChuyen(tdthanhphan[i]);
        }
        public void NapChiSoLop(int chisomoi)
        {
            chiso = chisomoi;
            for (int i = 0; i < 4; i++)
            {
                nut[i].NapChiSoLop(chiso);
            }
        }
        public void DiChuyen(Vector2 toadomoi)
        {
            Vector2 giaso = toadomoi - toadokhung;
            toadokhung = toadomoi;
            tdhtkhung = new Vector2(toadokhung.X * tile.X + gstoado.X, toadokhung.Y * tile.Y + gstoado.Y);
            for (int i = 0; i < 8; i++)
            {
                tdthanhphan[i] += giaso;
                nut[i].DiChuyen(tdthanhphan[i]);
            }
        }
        public void HoatDong(ref int chisolop, MouseState chuothientai, MouseState chuottruocdo)
        {
            if (cuondung)
            {
                nut[0].KichHoatThuong(ref chisolop, chuothientai, chuottruocdo, "top", ref luachon, 0);
                nut[1].KichHoatThuong(ref chisolop, chuothientai, chuottruocdo, "down", ref luachon, 0);
                if ((chuothientai.LeftButton == ButtonState.Pressed) & (chisolop == chiso))
                {
                    //chuyen the toa do kich theo man hinh chuan
                    int toadokichx = Convert.ToInt32((chuothientai.X - gstoado.X) / tile.X);
                    int toadokichy = Convert.ToInt32((chuothientai.Y - gstoado.Y) / tile.Y);
                    if (KiemTraChua(new Vector2(toadokichx, toadokichy), new Rectangle(Convert.ToInt32(nut[4].LayToaDo().X), Convert.ToInt32(nut[4].LayToaDo().Y), Convert.ToInt32(nut[4].LayChieuRong()), Convert.ToInt32(nut[4].LayChieuDai()))))
                    {
                        float tdycuon = toadokichy - nut[6].LayChieuDai() / 2f;
                        if (tdycuon < toadokhung.Y + athanhphan[0].Height) tdycuon = toadokhung.Y + athanhphan[0].Height;
                        else if (tdycuon > tdthanhphan[1].Y - athanhphan[10].Height) tdycuon = tdthanhphan[1].Y - athanhphan[10].Height;
                        nut[6].DiChuyen(new Vector2(nut[6].LayToaDo().X, tdycuon));
                        int slcuon = Convert.ToInt32((toadokichy - (toadokhung.Y + athanhphan[0].Height)) / (float)dichuyendung);
                        int cuonmax = (int)(Math.Abs(hieudung) / buoccuon);
                        if (slcuon <= cuonmax) toadocuon.Y = -(buoccuon * slcuon);
                        else toadocuon.Y = hieudung;
                        VeLai();
                    }
                }
            }
            if (cuonngang)
            {
                nut[2].KichHoatThuong(ref chisolop, chuothientai, chuottruocdo, "left", ref luachon, 0);
                nut[3].KichHoatThuong(ref chisolop, chuothientai, chuottruocdo, "right", ref luachon, 0);
                if ((chuothientai.LeftButton == ButtonState.Pressed) & (chisolop == chiso))
                {
                    //chuyen the toa do kich theo man hinh chuan
                    int toadokichx = Convert.ToInt32((chuothientai.X - gstoado.X) / tile.X);
                    int toadokichy = Convert.ToInt32((chuothientai.Y - gstoado.Y) / tile.Y);
                    if (KiemTraChua(new Vector2(toadokichx, toadokichy), new Rectangle(Convert.ToInt32(nut[5].LayToaDo().X), Convert.ToInt32(nut[5].LayToaDo().Y), Convert.ToInt32(nut[5].LayChieuRong()), Convert.ToInt32(nut[5].LayChieuDai()))))
                    {
                        float tdxcuon = toadokichx - nut[7].LayChieuRong() / 2f;
                        if (tdxcuon < toadokhung.X + athanhphan[4].Width) tdxcuon = toadokhung.X + athanhphan[4].Width;
                        else if (tdxcuon > tdthanhphan[3].X - athanhphan[11].Width) tdxcuon = tdthanhphan[3].X - athanhphan[11].Width;
                        nut[7].DiChuyen(new Vector2(tdxcuon, nut[7].LayToaDo().Y));
                        int slcuon = Convert.ToInt32((toadokichx - (toadokhung.X + athanhphan[4].Width)) / (float)dichuyenngang);
                        int cuonmax = (int)(Math.Abs(hieungang) / buoccuon);
                        if (slcuon <= cuonmax) toadocuon.X = -(buoccuon * slcuon);
                        else toadocuon.X = hieungang;
                        VeLai();
                    }
                }
            }
            if (luachon == "top")
            {
                if (toadocuon.Y < 0)
                {
                    toadocuon.Y += buoccuon;
                    if (toadocuon.Y > 0) toadocuon.Y = 0;
                    VeLai();
                }
                float tdycuon = nut[6].LayToaDo().Y - dichuyendung;
                if (tdycuon < toadokhung.Y + athanhphan[0].Height) tdycuon = toadokhung.Y + athanhphan[0].Height;
                nut[6].DiChuyen(new Vector2(nut[6].LayToaDo().X, tdycuon));
                luachon = "null";
            }
            else if (luachon == "down")
            {
                if (toadocuon.Y > hieudung)
                {
                    toadocuon.Y -= buoccuon;
                    if (toadocuon.Y < hieudung) toadocuon.Y = hieudung;
                    VeLai();
                }
                float tdycuon = nut[6].LayToaDo().Y + dichuyendung;
                if (tdycuon > tdthanhphan[1].Y - athanhphan[10].Height) tdycuon = tdthanhphan[1].Y - athanhphan[10].Height;
                nut[6].DiChuyen(new Vector2(nut[6].LayToaDo().X, tdycuon));
                luachon = "null";
            }
            else if (luachon == "left")
            {
                if (toadocuon.X < 0)
                {
                    toadocuon.X += buoccuon;
                    if (toadocuon.X > 0) toadocuon.X = 0;
                    VeLai();
                }
                float tdxcuon = nut[7].LayToaDo().X - dichuyenngang;
                if (tdxcuon < toadokhung.X + athanhphan[4].Width) tdxcuon = toadokhung.X + athanhphan[4].Width;
                nut[7].DiChuyen(new Vector2(tdxcuon, nut[7].LayToaDo().Y));
                luachon = "null";
            }
            else if (luachon == "right")
            {
                if (toadocuon.X > hieungang)
                {
                    toadocuon.X -= buoccuon;
                    if (toadocuon.X < hieungang) toadocuon.X = hieungang;
                    VeLai();
                }
                float tdxcuon = nut[7].LayToaDo().X + dichuyenngang;
                if (tdxcuon > tdthanhphan[3].X - athanhphan[11].Width) tdxcuon = tdthanhphan[3].X - athanhphan[11].Width;
                nut[7].DiChuyen(new Vector2(tdxcuon, nut[7].LayToaDo().Y));
                luachon = "null";
            }
        }
        public void HienThi(SpriteBatch nenvemoi)
        {
            nenvemoi.Draw(bonenvunganh, tdhtkhung, null, Color.White, 0, Vector2.Zero, tlhtkhung, SpriteEffects.None, 0f);
            if (cuondung)
            {
                nut[0].HienThiThuong(nenvemoi);
                nut[1].HienThiThuong(nenvemoi);
                nut[4].HienThiThuong(nenvemoi);
                nut[6].HienThiThuong(nenvemoi);
            }
            if (cuonngang)
            {
                nut[2].HienThiThuong(nenvemoi);
                nut[3].HienThiThuong(nenvemoi);
                nut[5].HienThiThuong(nenvemoi);
                nut[7].HienThiThuong(nenvemoi);
            }
        }
        public void KhoiPhuc(Texture2D anhnoidungmoi)
        {
            anoidunggoc = anhnoidungmoi;
            VeLai();
        }
        private void VeLai()
        {
            thietbidohoa.SetRenderTarget(bonenvunganh);
            thietbidohoa.Clear(Color.Transparent);
            nenve.Begin();
            nenve.Draw(anoidunggoc, new Rectangle(Convert.ToInt32(toadocuon.X), Convert.ToInt32(toadocuon.Y), anoidunggoc.Width, anoidunggoc.Height), Color.White);
            nenve.End();
            thietbidohoa.SetRenderTarget(null);
        }
        private Boolean KiemTraChua(Vector2 diem, Rectangle vungchua)
        {
            Boolean ketqua = false;
            if ((diem.X >= vungchua.X) & (diem.X < vungchua.X + vungchua.Width) & (diem.Y >= vungchua.Y) & (diem.Y < vungchua.Y + vungchua.Height))
                ketqua = true;
            return ketqua;
        }
    }
}