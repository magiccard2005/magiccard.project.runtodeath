using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MCChayAnhDong;

namespace MCGRunToDeath.HieuUng
{
    class HUDanChim
    {
        private RunToDeath trochoi;
        private Texture2D[] hachimbayxa;
        private int soluongchimbayxa = 5;
        private MChayAnhDong[] madchimbayxa = new MChayAnhDong[5];
        private int[] tocdochimbayxa = new int[5];
        public HUDanChim(RunToDeath trochoichinh, Texture2D[] anhdanchim)
        {
            trochoi = trochoichinh;
            hachimbayxa = anhdanchim;
            DSNgauNhien();
        }
        public void HoatDong()
        {
            for (int i = 0; i < soluongchimbayxa; i++)
            {
                madchimbayxa[i].ChayAnhDong(2);
                madchimbayxa[i].DiChuyen(new Vector2(madchimbayxa[i].LayToaDo().X + tocdochimbayxa[i], madchimbayxa[i].LayToaDo().Y));
                if (madchimbayxa[i].LayToaDo().X >= trochoi.ktmanhinh.X) NgauNhien(i, 0);
            }
        }
        public void HienThi()
        {
            for (int i = 0; i < soluongchimbayxa; i++) madchimbayxa[i].HienThiThuongBT(trochoi.spriteBatch);
        }
        private void DSNgauNhien()
        {
            for (int i = 0; i < soluongchimbayxa; i++) NgauNhien(i, 1);
        }
        private void NgauNhien(int vitri, int landau)
        {
            int rongchim = Convert.ToInt32(trochoi.ngaunhien.Next(10, 30));
            int daichim = rongchim;
            int toadoxchim = Convert.ToInt32(trochoi.ngaunhien.Next(10, trochoi.ktmanhinh.X));
            if (landau == 0) toadoxchim = -rongchim;
            int toadoychim = Convert.ToInt32(trochoi.ngaunhien.Next(0, 250 - daichim));
            if (madchimbayxa[vitri] == null)
            {
                madchimbayxa[vitri] = new MChayAnhDong(1, hachimbayxa, null, new Vector2(toadoxchim, toadoychim), true, null, trochoi.ktmanhinh, trochoi.tile);
            }
            else
            {
                madchimbayxa[vitri].DiChuyen(new Vector2(toadoxchim, toadoychim));
            }
            madchimbayxa[vitri].DoiKichThuoc(rongchim, daichim);
            tocdochimbayxa[vitri] = Convert.ToInt32(trochoi.ngaunhien.Next(1, 2 + (int)(rongchim / 10)));
        }
    }
}