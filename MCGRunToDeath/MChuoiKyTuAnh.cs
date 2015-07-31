//Copyright (c) 2014 MagicCard
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MCChuoiKyTuAnh
{
    public class MChuoiKyTuAnh
    {
        private GraphicsDevice thietbidohoa;
        private SpriteBatch nenve;
        private RenderTarget2D bonenvunganh;
        private string[] giatrikytu;
        private Texture2D[] anhkytu;
        private Texture2D anhkytucham;
        private int dorongkytucham;
        private int[] dorongkytu;
        private int dodaikytu;
        private Boolean baotran;
        private int cmax, dmax;//so luong ky tu tren dong toi da uoc tinh, so luong dong toi da uoc tinh
        //can le can dong theo thu tu 1-2-3=>Trai(Tren)-Giua-Phai(Duoi)
        //cac bien sao chep de khoi phuc lai anh chuoi ky tu khi can
        private string scchuoi;
        private int scrongkhung, scdaikhung, scdancachchu, scdancachdong, sccanle, sccandong;
        private Rectangle scbovien;
        private Color scmaunen, scmauchu;

        public MChuoiKyTuAnh(GraphicsDevice dohoa, Texture2D[] anhcackytu, int[] mangdorongkytu, string[] manggiatrikytu, Texture2D anhkytudaucham, int dorongdaucham, int cotmax, int dongmax)
        {
            thietbidohoa = dohoa;
            nenve = new SpriteBatch(thietbidohoa);
            anhkytu = anhcackytu;
            anhkytucham = anhkytudaucham;
            dorongkytucham = dorongdaucham;
            dorongkytu = mangdorongkytu;
            dodaikytu = Convert.ToInt32(anhcackytu[0].Height);
            giatrikytu = manggiatrikytu;
            cmax = cotmax;
            dmax = dongmax;
        }
        public Texture2D XuatAnhKyTu(string chuoi, int rongkhung, int daikhung, int dancachchu, int dancachdong, int canle, int candong, Rectangle bovien, Color maunen, Color mauchu)
        {
            //sao chep lai cac thong tin truoc khi xu ly
            scchuoi = chuoi;
            scrongkhung = rongkhung;
            scdaikhung = daikhung;
            scdancachchu = dancachchu;
            scdancachdong = dancachdong;
            sccanle = canle;
            sccandong = candong;
            scbovien = bovien;
            scmaunen = maunen;
            scmauchu = mauchu;
            baotran = false;
            //bat dau tien xu ly
            if (bonenvunganh != null) bonenvunganh.Dispose();
            int kieutudongrong = 0, kieutudongdai = 0;
            if (rongkhung <= 0)
            {
                rongkhung = 640;
                kieutudongrong = 1;
            }
            if (daikhung <= 0)
            {
                daikhung = 480;
                kieutudongdai = 1;
            }
            //bat dau thuc hien ghep chuoi ky tu
            int vitrighepx = 0, vitrighepy = 0, demdong = 0;
            int sldong = (int)(daikhung / (dodaikytu + dancachdong));
            if (sldong == 0) sldong = 1;
            int[] mangdorongdong = new int[sldong];//khai bao mang do rong cua cac dong
            int[] demkytu = new int[sldong];//dem so luong ky tu tren moi dong
            int[,] matranvanban = new int[cmax, dmax];//ma tran van ban co do lon co the chua cmax x dmax ky tu anh con
            for (int i = 0; i < chuoi.Length; i++)
            {
                for (int j = 0; j < giatrikytu.Length; j++)
                {
                    if (chuoi.Substring(i, 1) == giatrikytu[j])
                    {
                        if (chuoi.Substring(i, 1) == " ")
                        {
                            int vitricach = i;
                            for (int l = i + 1; l < chuoi.Length; l++)
                            {
                                vitricach = l;
                                if ((chuoi.Substring(l, 1) == " ") | (chuoi.Substring(l, 1) == "|")) break;
                            }
                            int tucuoi = vitricach;
                            if ((chuoi.Substring(vitricach, 1) == " ") | (chuoi.Substring(vitricach, 1) == " ")) tucuoi = vitricach - 1;
                            int dodaitumoi = 0;
                            for (int l = i + 1; l <= tucuoi; l++)
                            {
                                for (int k = 0; k < giatrikytu.Length; k++)
                                {
                                    if (chuoi.Substring(l, 1) == giatrikytu[k])
                                    {
                                        dodaitumoi += dorongkytu[k] + dancachchu;
                                        break;
                                    }
                                }
                            }
                            if (vitrighepx + dodaitumoi + 3 * dorongkytucham > rongkhung)//neu la tu cuoi
                            {
                                if (vitrighepy + 2 * dodaikytu + dancachdong > daikhung)//neu la dong cuoi
                                {
                                    int dorongdongcuoi = 0;
                                    int dorongdongcuoimax = vitrighepx + dorongkytu[j] + 3 * dorongkytucham;
                                    if (dorongdongcuoimax < rongkhung) dorongdongcuoi = dorongdongcuoimax;
                                    else dorongdongcuoi = rongkhung;
                                    mangdorongdong[demdong] = dorongdongcuoi;
                                    matranvanban[demkytu[demdong], demdong] = -1;
                                    demkytu[demdong]++;
                                    matranvanban[demkytu[demdong], demdong] = -1;
                                    demkytu[demdong]++;
                                    matranvanban[demkytu[demdong], demdong] = -1;
                                    demkytu[demdong]++;
                                    demdong++;
                                    baotran = true;
                                }
                                else//neu khong phai dong cuoi
                                {
                                    mangdorongdong[demdong] = vitrighepx;
                                    demdong = demdong + 1;
                                    vitrighepx = 0;
                                    vitrighepy += dodaikytu + dancachdong;
                                    matranvanban[demkytu[demdong], demdong] = j;
                                    demkytu[demdong]++;
                                }
                            }
                            else//neu khong phai tu cuoi
                            {
                                vitrighepx += dorongkytu[j] + dancachchu;
                                mangdorongdong[demdong] = vitrighepx;
                                matranvanban[demkytu[demdong], demdong] = j;
                                demkytu[demdong]++;
                            }
                        }
                        else if (chuoi.Substring(i, 1) == "|")//ky tu xuong dong
                        {
                            if (vitrighepy + 2 * dodaikytu + dancachdong > daikhung)//neu la dong cuoi
                            {
                                int dorongdongcuoi = 0;
                                int dorongdongcuoimax = vitrighepx + dorongkytu[j] + 3 * dorongkytucham;
                                if (dorongdongcuoimax < rongkhung) dorongdongcuoi = dorongdongcuoimax;
                                else dorongdongcuoi = rongkhung;
                                mangdorongdong[demdong] = dorongdongcuoi;
                                matranvanban[demkytu[demdong], demdong] = -1;
                                demkytu[demdong]++;
                                matranvanban[demkytu[demdong], demdong] = -1;
                                demkytu[demdong]++;
                                matranvanban[demkytu[demdong], demdong] = -1;
                                demkytu[demdong]++;
                                demdong++;
                                baotran = true;
                            }
                            else//neu khong phai dong cuoi
                            {
                                mangdorongdong[demdong] = vitrighepx;
                                demdong = demdong + 1;
                                vitrighepx = 0;
                                vitrighepy += dodaikytu + dancachdong;
                                matranvanban[demkytu[demdong], demdong] = j;
                                demkytu[demdong]++;
                            }
                        }
                        else//neu la cac ky tu binh thuong
                        {
                            vitrighepx += dorongkytu[j] + dancachchu;
                            mangdorongdong[demdong] = vitrighepx;
                            matranvanban[demkytu[demdong], demdong] = j;
                            demkytu[demdong]++;
                        }
                        break;
                    }
                }
                if (baotran) break;
            }
            if (baotran == false)
            {
                demdong = demdong + 1;
                if (kieutudongdai == 1) daikhung = demdong * (dodaikytu + dancachdong) + Math.Abs(dancachdong);
                if (kieutudongrong == 1)
                {
                    rongkhung = mangdorongdong[0];
                    for (int i = 0; i < mangdorongdong.Length; i++)
                    {
                        if (mangdorongdong[i] > rongkhung) rongkhung = mangdorongdong[i];
                    }
                }
            }
            //thuc hien can le va can dong
            bonenvunganh = new RenderTarget2D(thietbidohoa, rongkhung + bovien.X + bovien.Width, daikhung + bovien.Y + bovien.Height);
            thietbidohoa.SetRenderTarget(bonenvunganh);
            thietbidohoa.Clear(maunen);
            nenve.Begin();
            int dodaitoanbochu = demdong * (dodaikytu + dancachdong) - dancachdong;
            int giasocandong = 0;
            if (candong == 3) giasocandong = daikhung - dodaitoanbochu;
            else if (candong == 2) giasocandong = (int)((daikhung - dodaitoanbochu) / 2);
            vitrighepx = bovien.X; vitrighepy = giasocandong + bovien.Y;
            if (canle == 3)
            {
                for (int i = 0; i < demdong; i++)
                {
                    vitrighepx = rongkhung - mangdorongdong[i] + bovien.X;
                    vitrighepy = i * (dodaikytu + dancachdong) + giasocandong + bovien.Y;
                    for (int j = 0; j < demkytu[i]; j++)
                    {
                        if (matranvanban[j, i] == -1)
                        {
                            nenve.Draw(anhkytucham, new Vector2(vitrighepx, vitrighepy), Color.White);
                            vitrighepx += dorongkytucham + dancachchu;
                        }
                        else
                        {
                            if ((j == 0) & ((giatrikytu[matranvanban[j, i]] == " ") | (giatrikytu[matranvanban[j, i]] == "|"))) continue;
                            nenve.Draw(anhkytu[matranvanban[j, i]], new Vector2(vitrighepx, vitrighepy), Color.White);
                            vitrighepx += dorongkytu[matranvanban[j, i]] + dancachchu;
                        }
                    }
                }
            }
            else if (canle == 2)
            {
                for (int i = 0; i < demdong; i++)
                {
                    vitrighepx = (int)((rongkhung - mangdorongdong[i]) / 2) + bovien.X;
                    vitrighepy = i * (dodaikytu + dancachdong) + giasocandong + bovien.Y;
                    for (int j = 0; j < demkytu[i]; j++)
                    {
                        if (matranvanban[j, i] == -1)
                        {
                            nenve.Draw(anhkytucham, new Vector2(vitrighepx, vitrighepy), Color.White);
                            vitrighepx += dorongkytucham + dancachchu;
                        }
                        else
                        {
                            if ((j == 0) & ((giatrikytu[matranvanban[j, i]] == " ") | (giatrikytu[matranvanban[j, i]] == "|"))) continue;
                            nenve.Draw(anhkytu[matranvanban[j, i]], new Vector2(vitrighepx, vitrighepy), Color.White);
                            vitrighepx += dorongkytu[matranvanban[j, i]] + dancachchu;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < demdong; i++)
                {
                    vitrighepx = bovien.X;
                    vitrighepy = i * (dodaikytu + dancachdong) + giasocandong + bovien.Y;
                    for (int j = 0; j < demkytu[i]; j++)
                    {
                        if (matranvanban[j, i] == -1)
                        {
                            nenve.Draw(anhkytucham, new Vector2(vitrighepx, vitrighepy), Color.White);
                            vitrighepx += dorongkytucham + dancachchu;
                        }
                        else
                        {
                            if ((j == 0) & ((giatrikytu[matranvanban[j, i]] == " ") | (giatrikytu[matranvanban[j, i]] == "|"))) continue;
                            nenve.Draw(anhkytu[matranvanban[j, i]], new Vector2(vitrighepx, vitrighepy), Color.White);
                            vitrighepx += dorongkytu[matranvanban[j, i]] + dancachchu;
                        }
                    }
                }
            }
            nenve.End();
            thietbidohoa.SetRenderTarget(null);
            if (mauchu != Color.White)
            {
                Color[] dulieumau = new Color[bonenvunganh.Width * bonenvunganh.Height];
                bonenvunganh.GetData(dulieumau);
                for (int x = 0; x < bonenvunganh.Width; x++)
                {
                    for (int y = 0; y < bonenvunganh.Height; y++)
                    {
                        int vitri = x + y * bonenvunganh.Width;
                        if (dulieumau[vitri].R > 126)
                        {
                            dulieumau[vitri] = mauchu;
                        }
                    }
                }
                bonenvunganh.SetData(dulieumau);
            }
            return bonenvunganh;
        }
        public Texture2D KhoiPhuc()
        {
            return XuatAnhKyTu(scchuoi, scrongkhung, scdaikhung, scdancachchu, scdancachdong, sccanle, sccandong, scbovien, scmaunen, scmauchu);
        }
        public Boolean TranKhung()
        {
            return baotran;//tra ve co bi tran ra khoi khung chua hay khong
        }
    }
}