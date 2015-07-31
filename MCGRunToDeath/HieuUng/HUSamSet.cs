using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MCChayAnhDong;

namespace MCGRunToDeath.HieuUng
{
    class HUSamSet
    {
        private RunToDeath trochoi;
        private Texture2D[] hasamset;
        private int soluongsamset = 5;
        private MChayAnhDong[] madsamset = new MChayAnhDong[5];
        private int[] xuathiensamset = new int[5];
        private int[] tgchayset = new int[5];
        private SoundEffect[] mamsamset = new SoundEffect[5];
        private int[] mbaochayamsamset = new int[5];
        private SoundEffect amsamset1, amsamset2, amsamset3;
        public HUSamSet(RunToDeath trochoichinh, Texture2D[] anhsamset, SoundEffect tiengsamset1, SoundEffect tiengsamset2, SoundEffect tiengsamset3)
        {
            trochoi = trochoichinh;
            hasamset = anhsamset;
            amsamset1 = tiengsamset1;
            amsamset2 = tiengsamset2;
            amsamset3 = tiengsamset3;
            DSNgauNhien();
        }
        public void HoatDong(float kichthuocamthanh)
        {
            for (int i = 0; i < soluongsamset; i++)
            {
                if (tgchayset[i] == xuathiensamset[i])
                {
                    madsamset[i].ChayAnhDong(2);
                    if (mbaochayamsamset[i] == 0)
                    {
                        mamsamset[i].Play(kichthuocamthanh, 0, 0);
                        mbaochayamsamset[i] = 1;
                    }
                    if (madsamset[i].KetThuc()) NgauNhien(i);
                }
                else tgchayset[i]++;
            }
        }
        public void HienThi()
        {
            for (int i = 0; i < soluongsamset; i++) if (tgchayset[i] == xuathiensamset[i]) madsamset[i].HienThiXoayBT(trochoi.spriteBatch);
        }
        private void DSNgauNhien()
        {
            for (int i = 0; i < soluongsamset; i++) NgauNhien(i);
        }
        private void NgauNhien(int vitri)
        {
            int rongset = Convert.ToInt32(trochoi.ngaunhien.Next(100, 250));
            int daiset = rongset;
            int toadoxset = Convert.ToInt32(trochoi.ngaunhien.Next(0, trochoi.ktmanhinh.X)) - Convert.ToInt32(rongset / 2);
            int toadoyset = Convert.ToInt32(trochoi.ngaunhien.Next(0, 380 - Convert.ToInt32(daiset / 2))) - Convert.ToInt32(daiset / 2);
            if (madsamset[vitri] == null)
            {
                madsamset[vitri] = new MChayAnhDong(1, hasamset, null, new Vector2(toadoxset, toadoyset), false, null, trochoi.ktmanhinh, trochoi.tile);
            }
            else
            {
                madsamset[vitri].DiChuyen(new Vector2(toadoxset, toadoyset));
                madsamset[vitri].ChayMoi();
            }
            madsamset[vitri].DoiKichThuoc(rongset, daiset);
            madsamset[vitri].XoayAnh(Convert.ToInt32(trochoi.ngaunhien.Next(0, 360)), new Vector2(100, 100));
            tgchayset[vitri] = 0;
            xuathiensamset[vitri] = Convert.ToInt32(trochoi.ngaunhien.Next(300, 500));
            if (daiset < 150) mamsamset[vitri] = amsamset1;
            else if (daiset < 200) mamsamset[vitri] = amsamset2;
            else mamsamset[vitri] = amsamset3;
            mbaochayamsamset[vitri] = 0;
        }
    }
}