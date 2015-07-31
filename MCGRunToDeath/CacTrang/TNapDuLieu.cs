using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MCGioiThieuHang;
using MCChayAnhDong;
using MCChuoiKyTuAnh;
using MCChayAnhThuong;
using MCHieuUngSangToi;
using MCBangThongBao;

namespace MCGRunToDeath.CacTrang
{
    class TNapDuLieu
    {
        private RunToDeath trochoi;
        public MChayAnhThuong atnen, atnentientrinh, attientrinh;
        private int rongtientrinh = 600, daitientrinh = 5;
        private int tdx = 20, tdy = 400;
        private int slthietlap = 10;//tang giam so luong thiet lap thi phai cap nhat
        private int napthietlap = 0;
        private int sltainguyen = 10;//tang giam so luong trang co tai nguyen thi phai cap nhat
        private int naptainguyen = 0;
        private Vector2 tdhtbkt;

        public TNapDuLieu(RunToDeath trochoichinh)
        {
            trochoi = trochoichinh;
            atnen = new MChayAnhThuong(1, trochoi.Content.Load<Texture2D>("HinhAnh/GioiThieuHang/NenGioiThieu"), null, new Vector2(0, 0), null, trochoi.ktmanhinh, trochoi.tile);
            atnen.DoiKichThuoc(trochoi.ktmanhinh.X, trochoi.ktmanhinh.Y);
            atnentientrinh = new MChayAnhThuong(1, trochoi.Content.Load<Texture2D>("HinhAnh/DiemTrang"), null, new Vector2(tdx, tdy), null, trochoi.ktmanhinh, trochoi.tile);
            atnentientrinh.DoiKichThuoc(rongtientrinh, daitientrinh);
            attientrinh = new MChayAnhThuong(1, trochoi.Content.Load<Texture2D>("HinhAnh/DiemDo"), null, new Vector2(tdx, tdy), null, trochoi.ktmanhinh, trochoi.tile);
            attientrinh.DoiKichThuoc(rongtientrinh, daitientrinh);
            string noidungnap = "Loading datas ...";
            if (trochoi.ngonngu != "english") noidungnap = "Nạp các dữ liệu ...";
            trochoi.anhbokytu[0] = trochoi.bokytu[0].XuatAnhKyTu(noidungnap, 0, 0, 1, 1, 1, 1, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.White);
            tdhtbkt = new Vector2(tdx * trochoi.tile.X, (tdy - trochoi.anhbokytu[0].Height) * trochoi.tile.Y);
        }
        private Boolean NapCacThietLap()
        {
            Boolean ketqua = false;
            if (napthietlap < slthietlap)
            {
                napthietlap++;
                float phantram = napthietlap / (float)slthietlap;
                attientrinh.DoiKichThuoc(phantram * rongtientrinh, 0);
                string noidungnap = "Loading setting ";
                if (trochoi.ngonngu != "english")
                {
                    noidungnap = "Nạp các thiết lập ";
                }
                trochoi.anhbokytu[0] = trochoi.bokytu[0].XuatAnhKyTu(noidungnap + (phantram * 100) + "%", 0, 0, 1, 1, 1, 1, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.White);
            }
            else
            {
                ketqua = true;
            }
            return ketqua;
        }
        private Boolean NapCacTaiNguyen()
        {
            Boolean ketqua = false;
            if (naptainguyen < sltainguyen)
            {
                if (naptainguyen == 0)
                {
                    trochoi.sdttrochoi.NapTaiNguyen();
                }
                naptainguyen++;
                float phantram = naptainguyen / (float)sltainguyen;
                attientrinh.DoiKichThuoc(phantram * rongtientrinh, 0);
                string noidungnap = "Loading resources ";
                if (trochoi.ngonngu != "english")
                {
                    noidungnap = "Nạp các tài nguyên ";
                }
                trochoi.anhbokytu[0] = trochoi.bokytu[0].XuatAnhKyTu(noidungnap + (phantram * 100) + "%", 0, 0, 1, 1, 1, 1, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.White);
            }
            else
            {
                ketqua = true;
            }
            return ketqua;
        }
        public void HoatDong()
        {
            if (NapCacThietLap())
            {
                if (NapCacTaiNguyen())
                {
                    trochoi.NapAmNhacNen("AmThanh/AmNen");
                    trochoi.tranghientai = "trangtrochoi";
                }
            }
        }
        public void HienThi()
        {
            atnen.HienThiThuongBT(trochoi.spriteBatch);
            trochoi.spriteBatch.Draw(trochoi.anhbokytu[0], tdhtbkt, null, Color.White, 0, Vector2.Zero, trochoi.tile, SpriteEffects.None, 0f);
            atnentientrinh.HienThiThuongBT(trochoi.spriteBatch);
            attientrinh.HienThiThuongBT(trochoi.spriteBatch);
        }
    }
}