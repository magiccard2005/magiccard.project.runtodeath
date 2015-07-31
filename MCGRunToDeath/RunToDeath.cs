using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MCChayAnhDong;
using MCChayAnhThuong;
using MCChuoiKyTuAnh;
using MCGioiThieuHang;
using MCBangThongBao;
using MCHieuUngSangToi;
using MCLuuLayDuLieu;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.Tasks;

namespace MCGRunToDeath
{
    public class RunToDeath : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        //cac bien thiet lap co ban
        public string phienban = "RunToDeath 1.0.0.3";
        public string dccaidat = "caidat.bmt";
        public string dcdiemcao = "diemcao.bmt";
        public MLuuLayDuLieu luulaydulieu;
        public Random ngaunhien = new Random();
        public MouseState trochuothientai, trochuottruocdo;
        public Rectangle ktmanhinh;//kich thuoc man hinh lan luot la rong chuan, dai chuan, rong dien thoai, dai dien thoai
        public Vector2 tile;//ti le rong, ti le dai
        public MHieuUngSangToi huquaman;
        public MBangThongBao bangthongbao;
        //bien khoi phuc bang thong bao
        public Texture2D[] dsanhbtb;
        public int[] dsdanhdaubtb;
        //bien chuyen doi xuyen suot
        public string tranghientai = "tranggioithieu", trangtruocdo = "tranggioithieu";
        public int chisolop = 0;//lop 0 duoc coi la lop khong kich hoat tat ca vi vay khong su dung lop 0.
        public string luachon = "null", ngonngu = "english";
        public SoundEffectInstance amnhacnen;
        public float kichthuocam = 0.5f;
        //tai nguyen tai su dung
        public SoundEffect amchon;
        public Texture2D nenbtb;
        public Texture2D[] nutbtb = new Texture2D[8];
        //cac trang trong tro choi
        internal CacTrang.TNapDuLieu sdtnapdulieu;
        internal CacTrang.TGioiThieu sdtgioithieu;
        internal CacTrang.TTroChoi sdttrochoi;
        internal TPBoChu laybochu;
        //cac bien dac trung
        public string[] diemkyluc = new string[10];
        public int nhacquangcao = 1;//neu bang 0 thi moi lan thoat khong nhac lai nua
        public int solansudung = 0;//dem so lan su dung de nhac nguoi dung danh gia
        public int solandungdenhac = 5;//sau 5 lan dung dau tien thi se nhac danh gia
        //cac bien text
        public string[] tieudebtb = new string[8];
        public string[] noidungbtb = new string[8];
        public string[] noilucchet = new string[14];
        public string[] noilamnham = new string[8];
        //khai bao cac bo ky tu co the hien thi dong thoi va gia tri
        public int slbokytu = 5;//neu them cac bo ky tu chu thi them o day
        public MChuoiKyTuAnh[] bokytu;
        public Texture2D[] anhbokytu;
        //bo anh ky tu va dau cham
        public Texture2D[] bkttahomas20none = new Texture2D[225];
        public Texture2D[] bkttahomas50black = new Texture2D[225];
        public Boolean mang = true;//tinh trang co mang hay khong
        //phan dang tai tren trang mang xa hoi
        public MChayAnhThuong sharegame, othergames;

        public RunToDeath()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            TargetElapsedTime = TimeSpan.FromSeconds(0.03125);
            mang = App.mang;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ThietLapCoBan();
            LayDuLieuLuuTru();
            sdtgioithieu = new CacTrang.TGioiThieu(this);
            sdtnapdulieu = new CacTrang.TNapDuLieu(this);
            sdttrochoi = new CacTrang.TTroChoi(this);

            tieudebtb[0] = "ĐIỂM KỶ LỤC";
            tieudebtb[1] = "THÔNG TIN TRÒ CHƠI";
            tieudebtb[2] = "THOÁT TRÒ CHƠI";
            tieudebtb[3] = "ĐÁNH GIÁ TRÒ CHƠI";
            tieudebtb[4] = "HIGH SCORES";
            tieudebtb[5] = "GAME INFORMATIONS";
            tieudebtb[6] = "CONFIRM EXIT";
            tieudebtb[7] = "RATE THIS GAME";

            string hienthidiem = "";
            for (int i = 0; i < 10; i++)
            {
                hienthidiem += diemkyluc[i];
                if (i < 9) hienthidiem += "|";
            }
            noidungbtb[0] = hienthidiem;
            noidungbtb[1] = "Có bao giờ bạn cảm thấy chán nản tất cả mọi thứ xung quanh bạn? Bạn không có tiền, không tình yêu, bạn bè, không sự nghiệp, bạn cô đơn và muốn chạy mãi? Run To Death là game nhỏ, đơn giản, vui nhộn, giúp bạn đỡ buồn chán khi cảm thấy bị lạc lõng. Nội dung game chỉ đơn giản là bạn hãy chạy, nhảy, đừng để rơi xuống hố, điểm số thể hiện sự nhạy bén, khéo léo cũng như trình độ của bạn. Run To Death là 1 trò chơi của MagicCard xây dựng. Thể loại nhập vai vui nhộn, tính điểm thành tích. Phiên bản hiện thời " + phienban + "|Xin cảm ơn bạn đã sử dụng trò chơi này, Chúc các bạn luôn vui vẻ!";
            noidungbtb[2] = "Bạn có thật sự muốn thoát khỏi trò chơi này?";
            noidungbtb[3] = "Bạn đã sử dụng trò chơi này ít nhất 5 lần, bạn có muốn đánh giá trò chơi này không?";
            noidungbtb[4] = hienthidiem;
            noidungbtb[5] = "Have you ever felt depressed everything surrounding you? No money, no love, no friends, or career, you are lonely and want to run away? Run To Death is little game, simple, funny and it will help you when you felt lost. All you have to do is very simple like run, jump, do not fall into the hole. Score shown acumen, ingenuity as well as your level. Run To Death is the game is developed by MagicCard, kind of roleplaying/fun. Current version is " + phienban + "|Thank you for playing this game. Play and have fun with it!";
            noidungbtb[6] = "Are you sure you want to exit?";
            noidungbtb[7] = "You have to use this game at least 5 times, do you want to rate this game?";

            noilucchet[0] = "Chào cái chết!";
            noilucchet[1] = "Vĩnh biệt!";
            noilucchet[2] = "Đù má!";
            noilucchet[3] = "Xuống lỗ rồi!";
            noilucchet[4] = "Chết mịa nó rồi!";
            noilucchet[5] = "Xong game!";
            noilucchet[6] = "Cái định mệnh!";
            noilucchet[7] = "Hello death!";
            noilucchet[8] = "Oh no!";
            noilucchet[9] = "Game over!";
            noilucchet[10] = "Goodbye my love!";
            noilucchet[11] = "No no no...";
            noilucchet[12] = "Oh my God!";
            noilucchet[13] = "Very funny!";

            noilamnham[0] = "I haven't got any money!";
            noilamnham[1] = "I haven't got any love!";
            noilamnham[2] = "I am lonely";
            noilamnham[3] = "I want to run until i die!";
            noilamnham[4] = "Tôi đếch có tiền!";
            noilamnham[5] = "Tôi đếch có tình yêu!";
            noilamnham[6] = "Chán vật vã!";
            noilamnham[7] = "Tôi muốn chạy cho đến chết!";

            //tai nguyen tai su dung
            amchon = Content.Load<SoundEffect>("AmThanh/AmChon");
            nenbtb = Content.Load<Texture2D>("HinhAnh/BangThongBao/NenBangThongBao");
            nutbtb[0] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NBTDuoc");
            nutbtb[1] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NBTHuy");
            nutbtb[2] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NKHDuoc");
            nutbtb[3] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NKHHuy");
            nutbtb[4] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NBTOK");
            nutbtb[5] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NBTCancel");
            nutbtb[6] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NKHOK");
            nutbtb[7] = Content.Load<Texture2D>("HinhAnh/BangThongBao/NKHCancel");

            //quang ba game
            sharegame = new MChayAnhThuong(2, Content.Load<Texture2D>("HinhAnh/ShareGames/ShareBT"), Content.Load<Texture2D>("HinhAnh/ShareGames/ShareKH"), new Vector2(487, 3), amchon, ktmanhinh, tile);
            othergames = new MChayAnhThuong(2, Content.Load<Texture2D>("HinhAnh/ShareGames/OtherGamesBT"), Content.Load<Texture2D>("HinhAnh/ShareGames/OtherGamesKH"), new Vector2(487, 36), amchon, ktmanhinh, tile);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (App.naplai)
            {
                NapLaiAnhChuoiKyTu();
                App.naplai = false;
            }
            trochuothientai = GetMouseState();
            if (huquaman.HieuUngVao())
            {
                if ((tranghientai != "tranggioithieu") & (tranghientai != "trangnapdulieu"))
                {
                    bangthongbao.HoatDong(ref chisolop, trochuothientai, trochuottruocdo, ref luachon, kichthuocam);
                }
                if (tranghientai == "tranggioithieu")
                {
                    sdtgioithieu.HoatDong();
                }
                else if (tranghientai == "trangnapdulieu")
                {
                    sdtnapdulieu.HoatDong();
                }
                else if (tranghientai == "trangtrochoi")
                {
                    sdttrochoi.HoatDong();
                }
            }
            huquaman.HieuUngRa(ref tranghientai, ref trangtruocdo);
            trochuottruocdo = trochuothientai;
            luachon = "null";
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (tranghientai == "tranggioithieu")
            {
                sdtgioithieu.HienThi();
            }
            else if (tranghientai == "trangnapdulieu")
            {
                sdtnapdulieu.HienThi();
            }
            else if (tranghientai == "trangtrochoi")
            {
                sdttrochoi.HienThi();
            }
            if ((tranghientai != "tranggioithieu") & (tranghientai != "trangnapdulieu"))
            {
                bangthongbao.HienThi(spriteBatch);
            }
            huquaman.HienThi(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void ThietLapCoBan()
        {
            //tao lop luu lay du lieu
            luulaydulieu = new MLuuLayDuLieu("magiccard");
            //thiet lap ti le man hinh
            //thiet lap ti le man hinh
            ktmanhinh = new Rectangle(640, 480, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            tile = new Vector2(ktmanhinh.Width / (float)ktmanhinh.X, ktmanhinh.Height / (float)ktmanhinh.Y);
            huquaman = new MHieuUngSangToi(GraphicsDevice, ktmanhinh, tile);
            NapCacBoKyTu();
        }
        public void NapAmNhacNen(string diachinhac)
        {
            if (amnhacnen != null) amnhacnen.Stop();
            SoundEffect amnentrangdau = Content.Load<SoundEffect>(diachinhac);
            amnhacnen = amnentrangdau.CreateInstance();
            amnhacnen.Volume = kichthuocam;
            amnhacnen.IsLooped = true;
            amnhacnen.Play();
        }
        public void LayDuLieuLuuTru()
        {
            //du lieu cai dat
            string dlcaidat = luulaydulieu.LayDuLieu(dccaidat);
            if (dlcaidat == "0")
            {
                ngonngu = "english";
                kichthuocam = 0.5f;
                nhacquangcao = 1;
                solansudung = 0;
            }
            else
            {
                string[] manggiatri = new string[4];//ngon ngu, am thanh, nhac qc, so lan su dung
                int dgt = 0;
                for (int i = 0; i < dlcaidat.Length; i++)
                {
                    string s = dlcaidat.Substring(i, 1);
                    if (s != ";") manggiatri[dgt] += s;
                    else dgt++;
                }
                ngonngu = manggiatri[0];
                kichthuocam = Convert.ToInt32(manggiatri[1]) / 10f;
                nhacquangcao = Convert.ToInt32(manggiatri[2]);
                solansudung = Convert.ToInt32(manggiatri[3]);
            }
            //du lieu diem cao
            string dldiemcao = luulaydulieu.LayDuLieu(dcdiemcao);
            if (dldiemcao == "0")
            {
                DateTime homnay = DateTime.Now;
                string ktdancach = ".........";
                for (int i = 0; i < 10; i++)
                {
                    diemkyluc[i] = homnay.ToString("dd-MM-yyyy hh:mm:ss") + ktdancach + "0";
                }
            }
            else
            {
                dldiemcao = dldiemcao.Replace("x", " ").Replace("y", ":").Replace("z", ".");
                string[] manggiatri = new string[10];//danh sach 10 diem ky luc
                int dgt = 0;
                for (int i = 0; i < dldiemcao.Length; i++)
                {
                    string s = dldiemcao.Substring(i, 1);
                    if (s != ";") manggiatri[dgt] += s;
                    else dgt++;
                }
                diemkyluc = manggiatri;
            }
        }
        public int LayDiemKyLuc(string chuoidiem)
        {
            string diemso = "";
            for (int i = chuoidiem.Length - 1; i >= 0; i--)
            {
                string s = chuoidiem.Substring(i, 1);
                if (s != ".") diemso += s;
                else break;
            }
            int diem = 0;
            for (int i = diemso.Length - 1; i >= 0; i--)
            {
                string s = diemso.Substring(i, 1);
                diem = diem * 10 + Convert.ToInt32(s);
            }
            return diem;
        }
        public void LuuDuLieuLuuTru(Boolean tanglansudung)
        {
            if (tanglansudung)
            {
                solansudung++;
                if (solansudung > 1000) solansudung = 1000;
            }
            string dlcaidat = ngonngu + ";" + (kichthuocam * 10).ToString() + ";" + nhacquangcao.ToString() + ";" + solansudung.ToString() + ";";
            luulaydulieu.LuuDuLieu(dccaidat, dlcaidat);
            //luu du lieu diem cao
            string dldiemcao = "";
            for (int i = 0; i < 10; i++)
            {
                dldiemcao += diemkyluc[i] + ";";
            }
            dldiemcao = dldiemcao.Replace(" ", "x").Replace(":", "y").Replace(".", "z");
            luulaydulieu.LuuDuLieu(dcdiemcao, dldiemcao.ToLower());
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
        private void NapCacBoKyTu()
        {
            for (int i = 0; i < 225; i++)
            {
                bkttahomas20none[i] = Content.Load<Texture2D>("HinhAnh/BKTTahomaS20None" + "/" + (i + 1));
                bkttahomas50black[i] = Content.Load<Texture2D>("HinhAnh/BKTTahomaS50Black" + "/" + (i + 1));
            }
            laybochu = new TPBoChu(this);
            //thiet lap cac mang cua bo ky tu
            Array.Resize(ref bokytu, slbokytu);
            Array.Resize(ref anhbokytu, slbokytu);
            //bo chu chay load du lieu
            bokytu[0] = laybochu.BKTTahomaS20None();
            //bo chu tieu de bang thong bao
            bokytu[1] = laybochu.BKTTahomaS20None();
            //bo chu noi dung bang thong bao
            bokytu[2] = laybochu.BKTTahomaS20None();
            //bo chu nhan vat noi nham
            bokytu[3] = laybochu.BKTTahomaS20None();
            //bo chu diem so to trong san choi
            bokytu[4] = laybochu.BKTTahomaS50Black();
        }
        public void NapBangThongBao()
        {
            int gsnn = 0;
            if (ngonngu == "english") gsnn = 4;
            bangthongbao = new MBangThongBao(GraphicsDevice, nenbtb, new Rectangle(20, 20, 20, 20), nutbtb[0 + gsnn], nutbtb[2 + gsnn], nutbtb[1 + gsnn], nutbtb[3 + gsnn], amchon, ktmanhinh, tile);
        }
        public void BatThongBao(string mabang, string tieude, string noidung, Boolean laygiatri, Boolean kichhoatnhanh, int rongbang)
        {
            anhbokytu[1] = bokytu[1].XuatAnhKyTu(tieude, rongbang, 25, 1, 1, 1, 1, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Maroon);
            anhbokytu[2] = bokytu[2].XuatAnhKyTu(noidung, rongbang, 0, 1, 1, 1, 1, new Rectangle(0, 0, 0, 0), Color.Transparent, Color.Black);
            dsanhbtb = new Texture2D[2] { anhbokytu[1], anhbokytu[2] };
            dsdanhdaubtb = new int[2] { 1, 2 };//danh dau lai vi tri bokytu duoc su dung, anh nao khong can khoi phuc thi danh so 0
            Rectangle[] dsboanh = { new Rectangle(0, 0, 0, 0), new Rectangle(0, 20, 0, 0) };
            int tdynutbang = 30 + anhbokytu[2].Height;
            float tdybang = (ktmanhinh.Y - (tdynutbang + 60)) / 2;
            int rongbangmoi = rongbang;
            if (rongbangmoi == 0) rongbangmoi = anhbokytu[2].Width;
            bangthongbao.NapNoiDung(new Vector2(165, tdybang), dsanhbtb, dsboanh, new Vector2(rongbangmoi - 150, tdynutbang), new Vector2(rongbangmoi - 75, tdynutbang), laygiatri);
            bangthongbao.BatBang(mabang, new Vector2((640 - (rongbangmoi + 40)) / 2, tdybang), ref chisolop, ngaunhien.Next(1, 5), 50, kichhoatnhanh);
        }
        public void HoatDongShareGames()
        {
            if (mang)
            {
                sharegame.KichHoatThuong(ref chisolop, trochuothientai, trochuottruocdo, "ttdsharegame", ref luachon, kichthuocam);
                othergames.KichHoatThuong(ref chisolop, trochuothientai, trochuottruocdo, "ttdothergames", ref luachon, kichthuocam);
                if (luachon == "ttdsharegame")
                {
                    ShareLinkTask shareLinkTask = new ShareLinkTask();
                    shareLinkTask.LinkUri = new Uri("https://www.windowsphone.com/en-us/store/app/run-or-death/77469eea-677b-4661-96ed-2273d50673e2", UriKind.Absolute);
                    shareLinkTask.Message = "I'm playing this game, invite you to play with me. Thank!";
                    shareLinkTask.Show();
                }
                else if (luachon == "ttdothergames")
                {
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = new Uri("http://www.windowsphone.com/en-US/store/publishers?publisherId=Magic%2BCard&appId=070ee79d-01c5-4708-a504-c54ecb576e1e", UriKind.Absolute);
                    webBrowserTask.Show();
                }
            }
        }
        public void HienThiShareGames()
        {
            if (mang)
            {
                sharegame.HienThiThuong(spriteBatch);
                othergames.HienThiThuong(spriteBatch);
            }
        }
        public void NapLaiAnhChuoiKyTu()//danh cho truong hop giai phong ram thi phai goi lai
        {
            try
            {
                for (int i = 0; i < slbokytu; i++)
                {
                    anhbokytu[i] = bokytu[i].KhoiPhuc();
                }
            }
            catch { }
            //khoi phuc bang thong bao
            if (dsdanhdaubtb != null)
            {
                for (int i = 0; i < dsdanhdaubtb.Length; i++)
                {
                    if (dsdanhdaubtb[i] != 0)
                    {
                        dsanhbtb[i] = anhbokytu[dsdanhdaubtb[i]];
                    }
                }
            }
            try
            {
                bangthongbao.KhoiPhucDiemMau();
            }
            catch { }
        }
    }
}