using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MCGioiThieuHang;

namespace MCGRunToDeath.CacTrang
{
    class TGioiThieu
    {
        private RunToDeath trochoi;
        private MGioiThieuHang gthgioithieu;
        public TGioiThieu(RunToDeath trochoichinh)
        {
            trochoi = trochoichinh;
            NapTaiNguyen();
        }
        private void NapTaiNguyen()
        {
            Texture2D[] anhhangsanxuat = new Texture2D[20];
            for (int i = 0; i < 20; i++) anhhangsanxuat[i] = trochoi.Content.Load<Texture2D>("HinhAnh/GioiThieuHang/HangSanXuat/" + (i + 1).ToString());
            Texture2D anhnengioithieu = trochoi.Content.Load<Texture2D>("HinhAnh/GioiThieuHang/NenGioiThieu");
            SoundEffect amluachay = trochoi.Content.Load<SoundEffect>("AmThanh/AmLuaChay");
            gthgioithieu = new MGioiThieuHang(anhhangsanxuat, anhnengioithieu, amluachay, trochoi.ktmanhinh, trochoi.tile);
        }
        public void HoatDong()
        {
            if (gthgioithieu.HoatDong(2, 30, trochoi.kichthuocam))
            {
                trochoi.tranghientai = "trangnapdulieu";
            }
        }
        public void HienThi()
        {
            gthgioithieu.HienThi(trochoi.spriteBatch);
        }
    }
}