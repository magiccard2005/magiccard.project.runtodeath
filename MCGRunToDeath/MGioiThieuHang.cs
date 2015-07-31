//Copyright (c) 2014 MagicCard
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace MCGioiThieuHang
{
    public class MGioiThieuHang
    {
        private Texture2D[] manganh;
        private Texture2D anhhienthi, anhnen;
        private int tongsoanh;
        private Rectangle ktmanhinh;//kich thuoc man hinh lan luot la rong chuan, dai chuan, rong dien thoai, dai dien thoai
        private Vector2 tile, gstoado;//ti le rong, ti le dai, gia so toa do x, toa do y
        private MouseState trochuothientai, trochuottruocdo;
        private int bienchay, hoatcanh, thoigianchay, thoigianbatdau = 20;
        private Vector2 tdhtnen, tlhtnen;
        private Vector2 tdhtanh, tlhtanh;
        private SoundEffect amchay;
        public MGioiThieuHang(Texture2D[] danhsachanh, Texture2D anhnengioithieu, SoundEffect amthanh, Rectangle kichthuocmanhinh, Vector2 tilemanhinh)
        {
            ktmanhinh = kichthuocmanhinh;
            tile = tilemanhinh;
            gstoado = new Vector2((ktmanhinh.Width - ktmanhinh.X * tile.X) / 2, (ktmanhinh.Height - ktmanhinh.Y * tile.Y) / 2);
            manganh = danhsachanh;
            tongsoanh = manganh.Length;
            anhnen = anhnengioithieu;
            tdhtnen = gstoado;
            tlhtnen = new Vector2(ktmanhinh.X / (float)anhnengioithieu.Width * tile.X, ktmanhinh.Y / (float)anhnengioithieu.Height * tile.Y);
            anhhienthi = manganh[0];
            tdhtanh = new Vector2((ktmanhinh.X - anhhienthi.Width) / 2 * tile.X + gstoado.X, (ktmanhinh.Y - anhhienthi.Height) / 2 * tile.Y + gstoado.Y);
            tlhtanh = tile;
            amchay = amthanh;
        }
        public Boolean HoatDong(int tocdo, int thoigian, float kichthuocamthanh)
        {
            Boolean ketqua = false;
            if (thoigian < tongsoanh + thoigianbatdau) thoigian = tongsoanh + thoigianbatdau;
            bienchay++;
            if (bienchay == tocdo)
            {
                if (thoigianchay >= thoigianbatdau)
                {
                    if (hoatcanh == 0) amchay.Play(kichthuocamthanh, 0, 0);
                    if (hoatcanh < tongsoanh - 1) hoatcanh++;
                    else hoatcanh = tongsoanh - 1;
                    anhhienthi = manganh[hoatcanh];
                }
                thoigianchay++;
                bienchay = 0;
            }
            trochuothientai = GetMouseState();
            int baoboqua = 0;
            if (trochuothientai.LeftButton == ButtonState.Pressed && trochuottruocdo.LeftButton == ButtonState.Released)
            {
                baoboqua = 1;
            }
            trochuottruocdo = trochuothientai;
            if ((thoigianchay == thoigian + thoigianbatdau) | (baoboqua == 1))
            {
                bienchay = 0;
                hoatcanh = 0;
                thoigianchay = 0;
                ketqua = true;
            }
            return ketqua;
        }
        public void HienThi(SpriteBatch nenve)
        {
            nenve.Draw(anhnen, tdhtnen, null, Color.White, 0, Vector2.Zero, tlhtnen, SpriteEffects.None, 0f);
            nenve.Draw(anhhienthi, tdhtanh, null, Color.White, 0, Vector2.Zero, tlhtanh, SpriteEffects.None, 0f);
        }
        private MouseState GetMouseState()
        {
            TouchCollection TouchState = TouchPanel.GetState();
            MouseState CurrentMouseState = new MouseState();
            foreach (TouchLocation tl in TouchState)
            {
                CurrentMouseState = new MouseState((int)tl.Position.X, (int)tl.Position.Y, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            }
            return CurrentMouseState;
        }
    }
}