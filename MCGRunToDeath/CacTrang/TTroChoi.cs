using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MCChayAnhDong;
using MCChayAnhThuong;
using Microsoft.Phone.Tasks;

namespace MCGRunToDeath.CacTrang
{
    class TTroChoi
    {
        private RunToDeath trochoi;
        private MChayAnhDong adtentrochoi, nvcchay, nvcnhay, adbui;
        private MChayAnhThuong atchubatdau, atchudiemcao, atchucaidat, atchuthongtin, atchuthoat, atnoinho, atnoito, atmattroi, atmattrang, atbautroisao, atthanhtoi;
        private int chaythucdon = 0, kieuchaythucdon = 0;
        //bien dac trung cua game
        private int tocdochay = 20;
        private string tinhtrang = "thucdon";
        private Boolean dangnhay = false;
        private int nuatgnhay = 10;
        private int dnhay = 0;
        private Boolean dangroi = false;
        private int tgroi = 30;
        private int droi = 0;
        private int donhaycao = 200;
        private Boolean htnoinho = false;
        private int tghtnoinho = 20;
        private int dhtnoinho = 0;
        private Boolean htnoito = false;
        private int tghtnoito = 30;
        private int dhtnoito = 0;
        private Rectangle boanhnoinho, boanhnoito;
        private int diemchoi = 0;
        private Texture2D anhtongdiem;
        private Rectangle boanhtongdiem;
        private int tgnoinham = 100;
        private int dnoinham = 0;
        private Boolean dangnoinham = false;
        private int dotnoinham = 0;
        private int tgngat = 10;
        private int dngat = 0;
        //cac bien dia hinh gach
        private Texture2D hagach01, hagach02, hagach03, hagach04, hagach05, hagach06, hagach07, hagach08;
        private Boolean[] mkieugach = new Boolean[8];//neu la true thi la gach thuong
        private int slgach = 8;
        private MChayAnhThuong[] matgach = new MChayAnhThuong[8];
        //bien hieu ung bau troi
        private int tocdotrangtroi = 1;
        private int ngattrangtroi = 1;//cu sau 10 don vi moi thuc hien di chuyen theo toc do
        private int dngattrangtroi = 0;
        private Boolean banngay = true;
        private int songay = 0;
        private HieuUng.HUMayTroi hieuungmaytroi;
        private HieuUng.HUDanChim hieuungdanchim;
        private HieuUng.HUSamSet hieuungsamset;
        //bien trong trang thai cai dat
        private MChayAnhThuong atamthanh, atthanhamthanh, attroamthanh, atchuen, atchuvn, atdautich, atdanhgiaen, atdanhgiavn;
        //cac bien am thanh
        private SoundEffect amchay, amnhay, amchet;
        private int dtgamchay = 0;
        private int maunentroi;//co 3 loai mau nen troi 0, 1, 2
        private Texture2D[] mangchuthucdon = new Texture2D[20];

        public TTroChoi(RunToDeath trochoichinh)
        {
            trochoi = trochoichinh;
            maunentroi = Convert.ToInt32(trochoi.ngaunhien.Next(0, 3));
        }
        public void NapTaiNguyen()
        {
            Texture2D[] anhtentrochoi = new Texture2D[18];
            for (int i = 0; i < 18; i++) anhtentrochoi[i] = trochoi.Content.Load<Texture2D>("HinhAnh/TenTroChoi/" + (i + 1).ToString());
            Texture2D[] anhnvcchay = new Texture2D[14];
            for (int i = 0; i < 14; i++) anhnvcchay[i] = trochoi.Content.Load<Texture2D>("HinhAnh/NVCChay/" + (i + 1).ToString());
            Texture2D[] anhnvcnhay = new Texture2D[10];
            for (int i = 0; i < 10; i++) anhnvcnhay[i] = trochoi.Content.Load<Texture2D>("HinhAnh/NVCNhay/" + (i + 1).ToString());
            Texture2D[] anhbui = new Texture2D[7];
            for (int i = 0; i < 7; i++) anhbui[i] = trochoi.Content.Load<Texture2D>("HinhAnh/Bui/" + (i + 1).ToString());
            Texture2D anhnennoinho = trochoi.Content.Load<Texture2D>("HinhAnh/NenNoiNho");
            Texture2D anhnennoito = trochoi.Content.Load<Texture2D>("HinhAnh/NenNoiTo");
            Texture2D anhmattroi = trochoi.Content.Load<Texture2D>("HinhAnh/MatTroi");
            Texture2D anhmattrang = trochoi.Content.Load<Texture2D>("HinhAnh/MatTrang");
            amchay = trochoi.Content.Load<SoundEffect>("AmThanh/AmChay");
            amnhay = trochoi.Content.Load<SoundEffect>("AmThanh/AmNhay");
            amchet = trochoi.Content.Load<SoundEffect>("AmThanh/AmChet");
            adtentrochoi = new MChayAnhDong(1, anhtentrochoi, null, new Vector2(170, 0), true, null, trochoi.ktmanhinh, trochoi.tile);
            nvcchay = new MChayAnhDong(1, anhnvcchay, null, new Vector2(90, 198), true, null, trochoi.ktmanhinh, trochoi.tile);
            nvcnhay = new MChayAnhDong(1, anhnvcnhay, null, new Vector2(90, 198), false, null, trochoi.ktmanhinh, trochoi.tile);
            adbui = new MChayAnhDong(1, anhbui, null, new Vector2(120, 337), false, null, trochoi.ktmanhinh, trochoi.tile);
            adbui.DoiKichThuoc(26, 42);
            atbautroisao = new MChayAnhThuong(1, trochoi.Content.Load<Texture2D>("HinhAnh/BauTroiSao"), null, new Vector2(0, 0), null, trochoi.ktmanhinh, trochoi.tile);
            atbautroisao.DoiKichThuoc(trochoi.ktmanhinh.X, 300);
            atthanhtoi = new MChayAnhThuong(1, trochoi.Content.Load<Texture2D>("HinhAnh/ThanhToi"), null, new Vector2(0, 0), null, trochoi.ktmanhinh, trochoi.tile);
            atthanhtoi.DoiKichThuoc(trochoi.ktmanhinh.X, 200);
            atnoinho = new MChayAnhThuong(1, anhnennoinho, null, new Vector2(100, 188), null, trochoi.ktmanhinh, trochoi.tile);
            atnoito = new MChayAnhThuong(1, anhnennoito, null, new Vector2(20, 158), null, trochoi.ktmanhinh, trochoi.tile);
            atmattroi = new MChayAnhThuong(1, anhmattroi, null, new Vector2(480, 50), null, trochoi.ktmanhinh, trochoi.tile);
            atmattrang = new MChayAnhThuong(1, anhmattrang, null, new Vector2(640, 50), null, trochoi.ktmanhinh, trochoi.tile);
            trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu("0", 50, 32, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
            boanhnoinho = new Rectangle(Convert.ToInt32(100 * trochoi.tile.X), Convert.ToInt32(188 * trochoi.tile.Y), Convert.ToInt32(50 * trochoi.tile.X), Convert.ToInt32(32 * trochoi.tile.Y));
            trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu("Hello Death!", 120, 75, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
            boanhnoito = new Rectangle(Convert.ToInt32(35 * trochoi.tile.X), Convert.ToInt32(173 * trochoi.tile.Y), Convert.ToInt32(120 * trochoi.tile.X), Convert.ToInt32(75 * trochoi.tile.Y));
            //nap tai nguyen hieu ung
            Texture2D anhmaytroi = trochoi.Content.Load<Texture2D>("HinhAnh/MayTroi");
            Texture2D[] anhchimbayxa = new Texture2D[8];
            for (int i = 0; i < 8; i++) anhchimbayxa[i] = trochoi.Content.Load<Texture2D>("HinhAnh/ChimBayXa/" + (i + 1).ToString());
            Texture2D[] anhsamset = new Texture2D[8];
            for (int i = 0; i < 8; i++) anhsamset[i] = trochoi.Content.Load<Texture2D>("HinhAnh/SamSet/" + (i + 1).ToString());
            SoundEffect amsamset1 = trochoi.Content.Load<SoundEffect>("AmThanh/AmSamSet01");
            SoundEffect amsamset2 = trochoi.Content.Load<SoundEffect>("AmThanh/AmSamSet02");
            SoundEffect amsamset3 = trochoi.Content.Load<SoundEffect>("AmThanh/AmSamSet03");
            hieuungmaytroi = new HieuUng.HUMayTroi(trochoi, anhmaytroi);
            hieuungdanchim = new HieuUng.HUDanChim(trochoi, anhchimbayxa);
            hieuungsamset = new HieuUng.HUSamSet(trochoi, anhsamset, amsamset1, amsamset2, amsamset3);
            //anh thuc don
            for (int i = 0; i < 20; i++) mangchuthucdon[i] = trochoi.Content.Load<Texture2D>("HinhAnh/ThucDon/" + (i + 1).ToString());
            //cac ham nap khac
            NapAnhTuongGach();
            NapNgonNgu(trochoi.ngonngu);
            NapTNCaiDat();
            LamMoiThucDon();
            DamGachNgauNhien();
            CapNhatDiemChoi();
        }
        public void NapTNCaiDat()
        {
            //hinh anh trong trang cai dat
            Texture2D anhamthanh = trochoi.Content.Load<Texture2D>("HinhAnh/AnhAmThanh");
            atamthanh = new MChayAnhThuong(1, anhamthanh, null, new Vector2(85, 75), null, trochoi.ktmanhinh, trochoi.tile);
            Texture2D anhthanhamthanh = trochoi.Content.Load<Texture2D>("HinhAnh/ThanhAmThanh");
            atthanhamthanh = new MChayAnhThuong(1, anhthanhamthanh, null, new Vector2(210, 100), null, trochoi.ktmanhinh, trochoi.tile);
            Texture2D anhtroamthanh = trochoi.Content.Load<Texture2D>("HinhAnh/TroAmThanh");
            attroamthanh = new MChayAnhThuong(1, anhtroamthanh, null, new Vector2(204, 133), null, trochoi.ktmanhinh, trochoi.tile);
            attroamthanh.DiChuyen(new Vector2(204 + (Convert.ToInt32(trochoi.kichthuocam * 10) * 30), 133));

            Texture2D anhchungonnguen = trochoi.Content.Load<Texture2D>("HinhAnh/HinhAnhChu/CNgonNgu/1");
            Texture2D anhchungonnguvn = trochoi.Content.Load<Texture2D>("HinhAnh/HinhAnhChu/CNgonNgu/2");
            atchuen = new MChayAnhThuong(1, anhchungonnguen, anhchungonnguen, new Vector2(267, 200), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            atchuvn = new MChayAnhThuong(1, anhchungonnguvn, anhchungonnguvn, new Vector2(267, 270), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);

            Texture2D anhnutdanhgiaen = trochoi.Content.Load<Texture2D>("HinhAnh/HinhAnhChu/CDanhGia/1");
            Texture2D anhnutdanhgiavn = trochoi.Content.Load<Texture2D>("HinhAnh/HinhAnhChu/CDanhGia/2");
            atdanhgiaen = new MChayAnhThuong(1, anhnutdanhgiaen, anhnutdanhgiaen, new Vector2(330, 380), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            atdanhgiavn = new MChayAnhThuong(1, anhnutdanhgiavn, anhnutdanhgiavn, new Vector2(330, 380), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            //dau tich trong trang cai dat
            Texture2D anhdautich = trochoi.Content.Load<Texture2D>("HinhAnh/DauTich");
            int vitritich = 196;
            if (trochoi.ngonngu != "english") vitritich = 266;
            atdautich = new MChayAnhThuong(1, anhdautich, null, new Vector2(200, vitritich), null, trochoi.ktmanhinh, trochoi.tile);
        }
        public void NapAnhTuongGach()
        {
            hagach01 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/1");
            hagach02 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/2");
            hagach03 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/3");
            hagach04 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/4");
            hagach05 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/5");
            hagach06 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/6");
            hagach07 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/7");
            hagach08 = trochoi.Content.Load<Texture2D>("HinhAnh/TuongGach/8");
        }
        public void NapNgonNgu(string ngonnguthucdon)
        {
            int gsnn = 0;
            if (ngonnguthucdon != "english") gsnn = 1;
            atchubatdau = new MChayAnhThuong(2, mangchuthucdon[0 + gsnn], mangchuthucdon[10 + gsnn], new Vector2(-270, 210), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            atchudiemcao = new MChayAnhThuong(2, mangchuthucdon[2 + gsnn], mangchuthucdon[12 + gsnn], new Vector2(-270, 262), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            atchucaidat = new MChayAnhThuong(2, mangchuthucdon[4 + gsnn], mangchuthucdon[14 + gsnn], new Vector2(-270, 314), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            atchuthongtin = new MChayAnhThuong(2, mangchuthucdon[6 + gsnn], mangchuthucdon[16 + gsnn], new Vector2(-270, 366), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            atchuthoat = new MChayAnhThuong(2, mangchuthucdon[8 + gsnn], mangchuthucdon[18 + gsnn], new Vector2(-270, 418), trochoi.amchon, trochoi.ktmanhinh, trochoi.tile);
            trochoi.NapBangThongBao();
        }
        public void LamMoiThucDon()
        {
            chaythucdon = 0;
            kieuchaythucdon = 0;
            trochoi.chisolop = 0;
            NapViTriTD(chaythucdon);
        }
        public void HoatDong()
        {
            if ((trochoi.trochuothientai.LeftButton == ButtonState.Pressed && trochoi.trochuottruocdo.LeftButton == ButtonState.Released) & (dangroi == false) & (tinhtrang == "batdau"))
            {
                if (dangnhay == false)
                {
                    nvcnhay.ChayMoi();
                    adbui.ChayMoi();
                    adbui.DiChuyen(new Vector2(120, 337));
                    dangnhay = true;
                    amnhay.Play(trochoi.kichthuocam, 0, 0);
                }
            }
            if (dangnhay)
            {
                if (nvcnhay.KetThuc())
                {
                    dnhay = 0;
                    dangnhay = false;
                }
                else
                {
                    nvcnhay.ChayAnhDong(2);
                    dnhay++;
                    int giaso = dnhay;
                    int tdynhay = 198;
                    if (dnhay > nuatgnhay)
                    {
                        giaso = -dnhay + nuatgnhay;
                        tdynhay = donhaycao - 198;
                    }
                    nvcnhay.DiChuyen(new Vector2(90, tdynhay - (giaso * (donhaycao / nuatgnhay))));
                    atnoinho.DiChuyen(new Vector2(100, nvcnhay.LayToaDo().Y - 10));
                    adbui.DiChuyen(new Vector2(adbui.LayToaDo().X - 10, 337));
                    boanhnoinho.Y = Convert.ToInt32((nvcnhay.LayToaDo().Y - 10) * trochoi.tile.Y);
                }
            }
            else if (dangroi)
            {
                nvcnhay.ChayAnhDong(2);
                if (droi < tgroi)
                {
                    droi++;
                    nvcnhay.DiChuyen(new Vector2(90, 198 + droi * 10));
                    atnoito.DiChuyen(new Vector2(20, nvcnhay.LayToaDo().Y - 40));
                    boanhnoito.Y = Convert.ToInt32((nvcnhay.LayToaDo().Y - 25) * trochoi.tile.Y);
                }
                else
                {
                    droi = 0;
                    dangroi = false;
                    LamMoiTroChoi();
                }
            }
            else
            {
                if (dtgamchay == 6)
                {
                    dtgamchay = 0;
                    amchay.Play(trochoi.kichthuocam, 0, 0);
                }
                else dtgamchay++;
                nvcchay.ChayAnhDong(1);
            }
            adbui.ChayAnhDong(2);
            //hien thi thuc don
            if (tinhtrang == "thucdon")
            {
                trochoi.HoatDongShareGames();
                adtentrochoi.ChayAnhDong(2);
                atchubatdau.KichHoatThuong(ref trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "nutbatdau", ref trochoi.luachon, trochoi.kichthuocam);
                atchudiemcao.KichHoatThuong(ref trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "nutdiemcao", ref trochoi.luachon, trochoi.kichthuocam);
                atchucaidat.KichHoatThuong(ref trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "nutcaidat", ref trochoi.luachon, trochoi.kichthuocam);
                atchuthongtin.KichHoatThuong(ref trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "nutthongtin", ref trochoi.luachon, trochoi.kichthuocam);
                atchuthoat.KichHoatThuong(ref trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "nutthoat", ref trochoi.luachon, trochoi.kichthuocam);
            }
            //xu ly su kien thuc don
            if ((trochoi.luachon == "nutbatdau") & (tinhtrang != "batdau"))
            {
                CapNhatDiemChoi();
                trochoi.chisolop = 0;
                kieuchaythucdon = 1;
                tinhtrang = "batdau";
            }
            else if (trochoi.luachon == "nutdiemcao")
            {
                int chisonn = 0;
                if (trochoi.ngonngu == "english") chisonn = 1;
                trochoi.BatThongBao("btbdiemcao", trochoi.tieudebtb[4 * chisonn], trochoi.noidungbtb[4 * chisonn], false, false, 0);
            }
            else if ((trochoi.luachon == "nutcaidat") & (tinhtrang != "caidat"))
            {
                trochoi.chisolop = 0;
                kieuchaythucdon = 1;
                tinhtrang = "caidat";
            }
            else if (trochoi.luachon == "nutthongtin")
            {
                int chisonn = 0;
                if (trochoi.ngonngu == "english") chisonn = 1;
                trochoi.BatThongBao("btbthongtin", trochoi.tieudebtb[4 * chisonn + 1], trochoi.noidungbtb[4 * chisonn + 1], false, false, 270);
            }
            else if (trochoi.luachon == "nutthoat")
            {
                int chisonn = 0;
                if (trochoi.ngonngu == "english") chisonn = 1;
                trochoi.BatThongBao("btbthoat", trochoi.tieudebtb[4 * chisonn + 2], trochoi.noidungbtb[4 * chisonn + 2], true, false, 270);
            }
            else if (trochoi.luachon == "btbdanhgia-1")
            {
                QuangBaSanPham();
            }
            else if (trochoi.luachon == "btbthoat-1")
            {
                trochoi.LuuDuLieuLuuTru(true);
                trochoi.Exit();
            }
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) & ((chaythucdon == 0) | (chaythucdon == 20)))
            {
                if (tinhtrang != "thucdon")
                {
                    if (trochoi.bangthongbao.TinhTrangHienThi())
                    {
                        trochoi.bangthongbao.TatBang();
                    }
                    else
                    {
                        LamMoiTroChoi();
                    }
                }
                else
                {
                    if (trochoi.bangthongbao.TinhTrangHienThi())
                    {
                        trochoi.bangthongbao.TatBang();
                    }
                    else
                    {
                        if ((trochoi.nhacquangcao == 1) & (trochoi.solansudung >= trochoi.solandungdenhac) & (trochoi.mang))
                        {
                            trochoi.nhacquangcao = 0;
                            int chisonn = 0;
                            if (trochoi.ngonngu == "english") chisonn = 1;
                            trochoi.BatThongBao("btbdanhgia", trochoi.tieudebtb[4 * chisonn + 3], trochoi.noidungbtb[4 * chisonn + 3], true, false, 300);
                        }
                        else
                        {
                            int chisonn = 0;
                            if (trochoi.ngonngu == "english") chisonn = 1;
                            trochoi.BatThongBao("btbthoat", trochoi.tieudebtb[4 * chisonn + 2], trochoi.noidungbtb[4 * chisonn + 2], true, false, 250);
                        }
                    }
                }
            }
            HoatDongBauTroi();
            hieuungsamset.HoatDong(trochoi.kichthuocam);
            hieuungmaytroi.HoatDong();
            hieuungdanchim.HoatDong();
            ChayThucDon();
            ChayCaiDat();
            ChayGachNgauNhien();
            if (tinhtrang != "batdau") CapNhatNoiNham();
        }
        public void HienThi()
        {
            //hieu ung nen
            HienThiBauTroi();
            hieuungsamset.HienThi();
            hieuungmaytroi.HienThi();
            hieuungdanchim.HienThi();
            //ve noi dung tro choi
            if ((dangnhay) | (dangroi)) nvcnhay.HienThiThuong(trochoi.spriteBatch);
            else nvcchay.HienThiThuong(trochoi.spriteBatch);
            adbui.HienThiThuong(trochoi.spriteBatch);
            HTGachNgauNhien(trochoi.spriteBatch);
            if (htnoito)
            {
                atnoito.HienThiThuong(trochoi.spriteBatch);
                if (trochoi.anhbokytu[3] != null) trochoi.spriteBatch.Draw(trochoi.anhbokytu[3], boanhnoito, Color.White);
            }
            if (htnoinho)
            {
                atnoinho.HienThiThuong(trochoi.spriteBatch);
                if (trochoi.anhbokytu[3] != null) trochoi.spriteBatch.Draw(trochoi.anhbokytu[3], boanhnoinho, Color.White);
            }
            if ((tinhtrang == "batdau") & (anhtongdiem != null)) trochoi.spriteBatch.Draw(anhtongdiem, boanhtongdiem, Color.White);
            //ve cac tac vu thuc don
            adtentrochoi.HienThiThuong(trochoi.spriteBatch);
            atchubatdau.HienThiThuong(trochoi.spriteBatch);
            atchudiemcao.HienThiThuong(trochoi.spriteBatch);
            atchucaidat.HienThiThuong(trochoi.spriteBatch);
            atchuthongtin.HienThiThuong(trochoi.spriteBatch);
            atchuthoat.HienThiThuong(trochoi.spriteBatch);
            //ve tac vu cai dat
            if ((tinhtrang == "caidat") & (chaythucdon == 0))
            {
                atamthanh.HienThiThuong(trochoi.spriteBatch);
                atthanhamthanh.HienThiThuong(trochoi.spriteBatch);
                attroamthanh.HienThiThuong(trochoi.spriteBatch);
                atchuen.HienThiThuong(trochoi.spriteBatch);
                atchuvn.HienThiThuong(trochoi.spriteBatch);
                atdautich.HienThiThuong(trochoi.spriteBatch);
                if ((trochoi.solansudung >= trochoi.solandungdenhac) & (trochoi.mang))
                {
                    if (trochoi.ngonngu == "english")
                    {
                        atdanhgiaen.HienThiThuong(trochoi.spriteBatch);
                    }
                    else
                    {
                        atdanhgiavn.HienThiThuong(trochoi.spriteBatch);
                    }
                }
            }
            if (tinhtrang == "thucdon") trochoi.HienThiShareGames();
        }
        private void ChayCaiDat()
        {
            if ((tinhtrang == "caidat") & (chaythucdon == 0))
            {
                atchuen.KichHoatNhanh(trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "chontienganh", ref trochoi.luachon, trochoi.kichthuocam);
                atchuvn.KichHoatNhanh(trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "chontiengviet", ref trochoi.luachon, trochoi.kichthuocam);
                if ((trochoi.solansudung >= trochoi.solandungdenhac) & (trochoi.mang))
                {
                    if (trochoi.ngonngu == "english")
                    {
                        atdanhgiaen.KichHoatNhanh(trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "nutdanhgia", ref trochoi.luachon, trochoi.kichthuocam);
                    }
                    else
                    {
                        atdanhgiavn.KichHoatNhanh(trochoi.chisolop, trochoi.trochuothientai, trochoi.trochuottruocdo, "nutdanhgia", ref trochoi.luachon, trochoi.kichthuocam);
                    }
                }
                if (!trochoi.bangthongbao.TinhTrangHienThi())
                {
                    if (trochoi.trochuothientai.LeftButton == ButtonState.Pressed && trochoi.trochuottruocdo.LeftButton == ButtonState.Released)
                    {
                        for (int i = 0; i <= 10; i++)
                        {
                            if (trochoi.trochuothientai.X > (210 + 30 * i) * trochoi.tile.X && trochoi.trochuothientai.X < (210 + 30 * (i + 1)) * trochoi.tile.X && trochoi.trochuothientai.Y > 100 * trochoi.tile.Y && trochoi.trochuothientai.Y < (100 + 63) * trochoi.tile.Y)
                            {
                                if (i < 10) trochoi.kichthuocam = float.Parse("0." + i, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                else trochoi.kichthuocam = 1.0f;
                                trochoi.amchon.Play(trochoi.kichthuocam, 0, 0);
                            }
                        }
                        attroamthanh.DiChuyen(new Vector2(204 + (Convert.ToInt32(trochoi.kichthuocam * 10) * 30), 133));
                        trochoi.amnhacnen.Volume = trochoi.kichthuocam;
                    }
                }
                if (trochoi.luachon == "chontienganh")
                {
                    atdautich.DiChuyen(new Vector2(200, 196));
                    trochoi.ngonngu = "english";
                    NapNgonNgu(trochoi.ngonngu);
                }
                else if (trochoi.luachon == "chontiengviet")
                {
                    atdautich.DiChuyen(new Vector2(200, 266));
                    trochoi.ngonngu = "tiengviet";
                    NapNgonNgu(trochoi.ngonngu);
                }
                else if (trochoi.luachon == "nutdanhgia")
                {
                    trochoi.nhacquangcao = 0;
                    int chisonn = 0;
                    if (trochoi.ngonngu == "english") chisonn = 1;
                    trochoi.BatThongBao("btbdanhgia", trochoi.tieudebtb[4 * chisonn + 3], trochoi.noidungbtb[4 * chisonn + 3], true, false, 300);
                }
                else if (trochoi.luachon == "btbdanhgia-1")
                {
                    QuangBaSanPham();
                }
            }
        }
        private void ChayThucDon()
        {
            if (trochoi.chisolop == 0)
            {
                if (kieuchaythucdon == 0)//chay vao thi tang chi so mac dinh la 2
                {
                    if (chaythucdon < 20) chaythucdon++;
                    else trochoi.chisolop = 2;
                }
                else//chay ra thi giam chi so mac dinh la 1
                {
                    if (chaythucdon > 0) chaythucdon--;
                    else trochoi.chisolop = 1;
                }
                NapViTriTD(chaythucdon);
            }
        }
        private void NapViTriTD(int vitri)
        {
            adtentrochoi.DiChuyen(new Vector2(170, vitri * 10 - 200));
            atchubatdau.DiChuyen(new Vector2(vitri * 23 - 270, 210));
            atchudiemcao.DiChuyen(new Vector2(650 - vitri * 23, 262));
            atchucaidat.DiChuyen(new Vector2(vitri * 23 - 270, 314));
            atchuthongtin.DiChuyen(new Vector2(650 - vitri * 23, 366));
            atchuthoat.DiChuyen(new Vector2(vitri * 23 - 270, 418));
        }
        private void LamMoiTroChoi()
        {
            //diem ky luc duoc sap xep trong mang theo thu tu nho dan
            int vt = -1;
            for (int i = 0; i < 10; i++)
            {
                if (diemchoi >= trochoi.LayDiemKyLuc(trochoi.diemkyluc[i]))
                {
                    vt = i;
                    break;
                }
            }
            //day cac diem cu lai
            if (vt != -1)
            {
                for (int i = 9; i > vt; i--)
                {
                    trochoi.diemkyluc[i] = trochoi.diemkyluc[i - 1];
                }
                DateTime homnay = DateTime.Now;
                string ktdancach = ".........";
                trochoi.diemkyluc[vt] = homnay.ToString("dd-MM-yyyy hh:mm:ss") + ktdancach + diemchoi.ToString();
            }
            //thiet lap khac
            string hienthidiem = "";
            for (int i = 0; i < 10; i++)
            {
                hienthidiem += trochoi.diemkyluc[i];
                if (i < 9) hienthidiem += "|";
            }
            trochoi.LuuDuLieuLuuTru(false);
            trochoi.noidungbtb[0] = hienthidiem;
            trochoi.noidungbtb[4] = hienthidiem;
            diemchoi = 0;
            songay = 0;
            htnoinho = false;
            dhtnoinho = 0;
            htnoito = false;
            dhtnoito = 0;
            dangnoinham = false;
            dnoinham = 0;
            dotnoinham = 0;
            dngat = 0;
            dangroi = false;
            dangnhay = false;
            nvcchay.DiChuyen(new Vector2(90, 198));
            atnoinho.DiChuyen(new Vector2(100, 188));
            boanhnoinho.Y = Convert.ToInt32(188 * trochoi.tile.Y);
            nvcnhay.DiChuyen(new Vector2(90, 198));
            atnoito.DiChuyen(new Vector2(20, 158));
            boanhnoito.Y = Convert.ToInt32(173 * trochoi.tile.Y);
            trochoi.chisolop = 0;
            kieuchaythucdon = 0;
            tinhtrang = "thucdon";
        }
        public void ChayGachNgauNhien()
        {
            if (dangroi == false)
            {
                for (int i = 0; i < slgach; i++)
                {
                    matgach[i].DiChuyen(new Vector2(matgach[i].LayToaDo().X - tocdochay, 380));
                    if (matgach[i].LayToaDo().X <= -100) GachNgauNhien(i, 0);
                    if (tinhtrang == "batdau")
                    {
                        if ((matgach[i].LayToaDo().X >= 100) & (matgach[i].LayToaDo().X <= 120))//tu 100 den 120
                        {
                            if (mkieugach[i] == false)//neu gap cai lo
                            {
                                if (dangnhay == false)//neu khong nhay
                                {
                                    htnoinho = false;
                                    dhtnoinho = 0;
                                    nvcnhay.ChayMoi();
                                    int noichetnn = Convert.ToInt32(trochoi.ngaunhien.Next(0, 7));
                                    int chisonn = 0;
                                    if (trochoi.ngonngu == "english") chisonn = 1;
                                    trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu(trochoi.noilucchet[noichetnn + 7 * chisonn], 120, 75, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
                                    htnoito = true;
                                    dhtnoito = 0;
                                    dangroi = true;
                                    amchet.Play(trochoi.kichthuocam, 0, 0);
                                }
                                else//neu nhay thi an diem
                                {
                                    if (matgach[i].LayToaDo().X == 100)
                                    {
                                        diemchoi++;
                                        trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu(diemchoi.ToString(), 50, 32, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
                                        htnoinho = true;
                                        dhtnoinho = 0;
                                        CapNhatDiemChoi();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (tinhtrang == "batdau")
            {
                //hien thi noi nho
                if (htnoinho)
                {
                    if (dhtnoinho < tghtnoinho)
                    {
                        dhtnoinho++;
                    }
                    else
                    {
                        dhtnoinho = 0;
                        htnoinho = false;
                    }
                }
                //hien thi noi to
                if (htnoito)
                {
                    if (dhtnoito < tghtnoito)
                    {
                        dhtnoito++;
                    }
                    else
                    {
                        dhtnoito = 0;
                        htnoito = false;
                    }
                }
            }
        }
        private void CapNhatNoiNham()
        {
            if (dangnoinham)
            {
                if (htnoito)
                {
                    if (dhtnoito < tghtnoito)
                    {
                        dhtnoito++;
                    }
                    else
                    {
                        dhtnoito = 0;
                        htnoito = false;
                    }
                }
                else
                {
                    if (dngat < tgngat)
                    {
                        dngat++;
                    }
                    else
                    {
                        dngat = 0;
                        dotnoinham++;
                        //noi lam nham luc chua choi
                        CauNoiNhamMoMan();
                    }
                }
            }
            else
            {
                if (dnoinham < tgnoinham)
                {
                    dnoinham++;
                }
                else
                {
                    dnoinham = 0;
                    dangnoinham = true;
                }
            }
        }
        private void CapNhatDiemChoi()
        {
            string chudiem = "Score: ";
            if (trochoi.ngonngu != "english") chudiem = "Điểm: ";
            Color mauchuso = Color.White;
            if (diemchoi < 50) mauchuso = Color.White;
            else if (diemchoi < 100) mauchuso = Color.Lime;
            else mauchuso = Color.Red;
            anhtongdiem = trochoi.bokytu[4].XuatAnhKyTu(chudiem + diemchoi.ToString(), 0, 0, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, mauchuso);
            boanhtongdiem = new Rectangle(Convert.ToInt32(10 * trochoi.tile.X), Convert.ToInt32(10 * trochoi.tile.Y), Convert.ToInt32(anhtongdiem.Width * trochoi.tile.X), Convert.ToInt32(anhtongdiem.Height * trochoi.tile.Y));
        }
        public void HTGachNgauNhien(SpriteBatch nenve)
        {
            for (int i = 0; i < slgach; i++) matgach[i].HienThiThuong(nenve);
        }
        private void DamGachNgauNhien()
        {
            for (int i = 0; i < slgach; i++) GachNgauNhien(i, 1);
        }
        private void GachNgauNhien(int vitri, int landau)
        {
            Texture2D anhgach = hagach01;
            int anhgachdaynn = Convert.ToInt32(trochoi.ngaunhien.Next(0, 4));
            if (anhgachdaynn == 0) anhgach = hagach01;
            else if (anhgachdaynn == 1) anhgach = hagach02;
            else if (anhgachdaynn == 2) anhgach = hagach03;
            else if (anhgachdaynn == 3) anhgach = hagach04;
            mkieugach[vitri] = true;
            if (tinhtrang == "batdau")
            {
                if (vitri > 0)
                {
                    if (mkieugach[vitri - 1] == true)
                    {
                        int gtmax = 5 - songay;//moi dau dia hinh de hon vi xac suat gap lo la it
                        if (gtmax < 2) gtmax = 2;
                        int kieugach = Convert.ToInt32(trochoi.ngaunhien.Next(0, gtmax));
                        if (kieugach == 0)
                        {
                            int anhgachnn = Convert.ToInt32(trochoi.ngaunhien.Next(0, 4));
                            if (anhgachnn == 0) anhgach = hagach05;
                            else if (anhgachnn == 1) anhgach = hagach06;
                            else if (anhgachnn == 2) anhgach = hagach07;
                            else if (anhgachnn == 3) anhgach = hagach08;
                            mkieugach[vitri] = false;
                        }
                    }
                }
            }
            int toadox = 0;
            if (landau == 1) toadox = vitri * 100;
            else toadox = 700;
            if (matgach[vitri] == null) matgach[vitri] = new MChayAnhThuong(1, anhgach, null, new Vector2(toadox, 380), null, trochoi.ktmanhinh, trochoi.tile);
            else
            {
                matgach[vitri].NapHinhAnh(anhgach, null);
                matgach[vitri].DiChuyen(new Vector2(toadox, 380));
            }
        }
        private void CauNoiNhamMoMan()
        {
            int giasonn = 4;
            if (trochoi.ngonngu == "english") giasonn = 0;
            if (dotnoinham == 1)
            {
                trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu(trochoi.noilamnham[giasonn], 120, 75, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
                htnoito = true;
            }
            else if (dotnoinham == 2)
            {
                trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu(trochoi.noilamnham[giasonn + 1], 120, 75, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
                htnoito = true;
            }
            else if (dotnoinham == 3)
            {
                trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu(trochoi.noilamnham[giasonn + 2], 120, 75, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
                htnoito = true;
            }
            else if (dotnoinham == 4)
            {
                trochoi.anhbokytu[3] = trochoi.bokytu[3].XuatAnhKyTu(trochoi.noilamnham[giasonn + 3], 120, 75, 1, 1, 2, 2, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
                htnoito = true;
            }
            else
            {
                dotnoinham = 0;
                dangnoinham = false;
            }
        }
        private void HoatDongBauTroi()
        {
            if (dngattrangtroi < ngattrangtroi)
            {
                dngattrangtroi++;
            }
            else
            {
                dngattrangtroi = 0;
                if (banngay)
                {
                    float tdxmoitt = atmattroi.LayToaDo().X - tocdotrangtroi;
                    if (tdxmoitt <= -150)
                    {
                        banngay = false;
                        tdxmoitt = trochoi.ktmanhinh.X;
                        maunentroi = Convert.ToInt32(trochoi.ngaunhien.Next(0, 3));
                    }
                    atmattroi.DiChuyen(new Vector2(tdxmoitt, 50));
                }
                else
                {
                    float tdxmoitt = atmattrang.LayToaDo().X - tocdotrangtroi;
                    if (tdxmoitt <= -150)
                    {
                        banngay = true;
                        tdxmoitt = trochoi.ktmanhinh.X;
                        if (tinhtrang == "batdau") songay++;
                    }
                    atmattrang.DiChuyen(new Vector2(tdxmoitt, 50));
                }
            }
        }
        private void HienThiBauTroi()
        {
            float giatrixet = atmattroi.LayToaDo().X;
            if (banngay == false) giatrixet = 490 - atmattrang.LayToaDo().X;
            int sotruthem = 0;
            int gt = Convert.ToInt32(giatrixet / 5);
            if (gt < 0)
            {
                gt = 0;
                if (giatrixet > -127) sotruthem = Convert.ToInt32(giatrixet);
                else sotruthem = -127;
            }
            if (maunentroi == 0) trochoi.GraphicsDevice.Clear(new Color(gt, gt, gt + 127 + sotruthem));
            else if (maunentroi == 1) trochoi.GraphicsDevice.Clear(new Color(gt, gt + 127 + sotruthem, gt));
            else if (maunentroi == 2) trochoi.GraphicsDevice.Clear(new Color(gt + 127 + sotruthem, gt, gt));
            atbautroisao.HienThiThuongBT(trochoi.spriteBatch);
            atthanhtoi.HienThiThuongBT(trochoi.spriteBatch);
            //hieu ung bau troi
            atmattroi.HienThiThuongBT(trochoi.spriteBatch);
            atmattrang.HienThiThuongBT(trochoi.spriteBatch);
        }
        public Boolean QuangBaSanPham()
        {
            Boolean ketqua = false;
            try
            {
                MarketplaceReviewTask trangdanhgia = new MarketplaceReviewTask();
                trangdanhgia.Show();
                ketqua = true;
            }
            catch
            {
                ketqua = false;
            }
            return ketqua;
        }
    }
}