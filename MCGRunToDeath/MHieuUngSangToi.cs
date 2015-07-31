//Copyright (c) 2014 MagicCard
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MCHieuUngSangToi
{
    public class MHieuUngSangToi
    {
        private GraphicsDevice thietbidohoa;
        private Rectangle ktmanhinh;//kich thuoc man hinh lan luot la rong chuan, dai chuan, rong dien thoai, dai dien thoai
        private Vector2 tile, gstoado;//ti le rong, ti le dai, gia so toa do x, toa do y
        private int chay;
        private Texture2D diemmau;
        private Vector2 tdhtdiemmau, tlhtdiemmau;
        private Boolean husangdan, hutoidan = true;
        public MHieuUngSangToi(GraphicsDevice dohoa, Rectangle kichthuocmanhinh, Vector2 tilemanhinh)
        {
            thietbidohoa = dohoa;
            ktmanhinh = kichthuocmanhinh;
            tile = tilemanhinh;
            gstoado = new Vector2((ktmanhinh.Width - ktmanhinh.X * tile.X) / 2, (ktmanhinh.Height - ktmanhinh.Y * tile.Y) / 2);
            tdhtdiemmau = gstoado;
            tlhtdiemmau = new Vector2(ktmanhinh.X * tile.X, ktmanhinh.Y * tile.Y);
        }
        public Boolean ChayToiDan()
        {
            Boolean ketqua = false;
            if (chay < 16)
            {
                chay++;
                CapNhatDiemMau(chay);
            }
            else ketqua = true;
            return ketqua;
        }
        public Boolean ChaySangDan()
        {
            Boolean ketqua = false;
            if (chay > 0)
            {
                chay--;
                CapNhatDiemMau(chay);
            }
            else ketqua = true;
            return ketqua;
        }
        private void CapNhatDiemMau(int giatri)
        {
            if (diemmau != null) diemmau.Dispose();
            diemmau = new Texture2D(thietbidohoa, 1, 1, false, SurfaceFormat.Color);
            diemmau.SetData<Color>(new Color[] { new Color(0, 0, 0, giatri * 17) });
        }
        public void HienThi(SpriteBatch nenve)
        {
            if (chay != 0) nenve.Draw(diemmau, tdhtdiemmau, null, Color.White, 0, Vector2.Zero, tlhtdiemmau, SpriteEffects.None, 0f);
        }
        public Boolean HieuUngVao()
        {
            Boolean ketqua = false;
            if (husangdan == false) husangdan = ChaySangDan();
            if ((husangdan) & (hutoidan)) ketqua = true;
            return ketqua;
        }
        public void HieuUngRa(ref string tranghienthoi, ref string tranglichsu)
        {
            if ((tranghienthoi != tranglichsu) & (husangdan))
            {
                if (hutoidan)
                {
                    string trangtg = tranglichsu;
                    tranglichsu = tranghienthoi;
                    tranghienthoi = trangtg;
                    hutoidan = false;
                }
                if (ChayToiDan())
                {
                    husangdan = false;
                    hutoidan = true;
                    tranghienthoi = tranglichsu;
                    tranglichsu = tranghienthoi;
                }
            }
        }
    }
}