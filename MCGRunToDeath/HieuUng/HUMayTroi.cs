using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MCChayAnhThuong;

namespace MCGRunToDeath.HieuUng
{
    class HUMayTroi
    {
        private RunToDeath trochoi;
        private Texture2D hamaytroi;
        private int soluongdammay = 5;
        private MChayAnhThuong[] matmaytroi = new MChayAnhThuong[5];
        private int[] tocdomaytroi = new int[5];
        private float tilemaytroi = 1;
        public HUMayTroi(RunToDeath trochoichinh, Texture2D anhmaytroi)
        {
            trochoi = trochoichinh;
            hamaytroi = anhmaytroi;
            DSNgauNhien();
        }
        public void HoatDong()
        {
            for (int i = 0; i < soluongdammay; i++)
            {
                matmaytroi[i].DiChuyen(new Vector2(matmaytroi[i].LayToaDo().X - tocdomaytroi[i], matmaytroi[i].LayToaDo().Y));
                if (matmaytroi[i].LayToaDo().X + matmaytroi[i].LayChieuRong() <= 0) NgauNhien(i, 0);
            }
        }
        public void HienThi()
        {
            for (int i = 0; i < soluongdammay; i++) matmaytroi[i].HienThiThuongBT(trochoi.spriteBatch);
        }
        private void DSNgauNhien()
        {
            tilemaytroi = hamaytroi.Width / (float)hamaytroi.Height;
            for (int i = 0; i < soluongdammay; i++) NgauNhien(i, 1);
        }
        private void NgauNhien(int vitri, int landau)
        {
            int rongmay = Convert.ToInt32(trochoi.ngaunhien.Next(200, 600));
            int daimay = Convert.ToInt32(rongmay / tilemaytroi);
            int toadoxmay = Convert.ToInt32(trochoi.ngaunhien.Next(10, trochoi.ktmanhinh.X));
            if (landau == 0) toadoxmay = trochoi.ktmanhinh.X;
            int toadoymay = Convert.ToInt32(trochoi.ngaunhien.Next(0, 200) - daimay / 2);
            if (matmaytroi[vitri] == null)
            {
                matmaytroi[vitri] = new MChayAnhThuong(1, hamaytroi, null, new Vector2(toadoxmay, toadoymay), null, trochoi.ktmanhinh, trochoi.tile);
            }
            else
            {
                matmaytroi[vitri].DiChuyen(new Vector2(toadoxmay, toadoymay));
                matmaytroi[vitri].DoiKichThuoc(rongmay, daimay);
            }
            tocdomaytroi[vitri] = Convert.ToInt32(trochoi.ngaunhien.Next(1, 5));
        }
    }
}