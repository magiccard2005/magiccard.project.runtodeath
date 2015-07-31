//Copyright (c) 2014 MagicCard
using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace MCLuuLayDuLieu
{
    public class MLuuLayDuLieu
    {
        //chi cho phep luu du lieu la cac ky tu trong bankytu
        private string khoabimat;
        private string[] bankytu = new string[]
        {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c",
         "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
         "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", ";", "-"};
        public MLuuLayDuLieu(string khoamahoa)
        {
            khoabimat = khoamahoa;
        }
        public void CapNhatKhoa(string khoamoi)
        {
            khoabimat = khoamoi;
        }
        private int[] ViTriPhanTuKhoa(string chuoi, string khoa)
        {
            int slkytu = bankytu.Length;
            string khoamoi = khoa;
            int ddkhoa = khoa.Length;
            int ddchuoi = chuoi.Length;
            if (ddkhoa > ddchuoi)
            {
                khoamoi = khoa.Substring(0, ddchuoi);
            }
            else if (ddkhoa < ddchuoi)
            {
                int solanlap = (int)(ddchuoi / ddkhoa) - 1;
                int sokytudu = ddchuoi % ddkhoa;
                for (int i = 0; i < solanlap; i++)
                {
                    khoamoi += khoa;
                }
                if (sokytudu > 0)
                {
                    khoamoi += khoa.Substring(0, sokytudu);
                }
            }
            int[] mvtktkhoa = new int[ddchuoi];
            int d = 0;
            for (int i = 0; i < ddchuoi; i++)
            {
                for (int j = 0; j < slkytu; j++)
                {
                    if (khoamoi.Substring(i, 1) == bankytu[j])
                    {
                        mvtktkhoa[d] = j;
                        d++;
                        break;
                    }
                }
            }
            return mvtktkhoa;
        }
        private string MaHoaVigenere(string banro, string khoa)
        {
            string chuoimahoa = "";
            int slkytu = bankytu.Length;
            int[] vtkhoa = ViTriPhanTuKhoa(banro, khoa);
            //ma hoa theo khoa moi vua tim
            for (int i = 0; i < banro.Length; i++)
            {
                for (int j = 0; j < slkytu; j++)
                {
                    if (banro.Substring(i, 1) == bankytu[j])
                    {
                        int vtktmahoa = (j + vtkhoa[i]) % slkytu;
                        chuoimahoa += bankytu[vtktmahoa];
                        break;
                    }
                }
            }
            return chuoimahoa;
        }
        private string GiaiMaVigenere(string banma, string khoa)
        {
            string chuoigiaima = "";
            int slkytu = bankytu.Length;
            int[] vtkhoa = ViTriPhanTuKhoa(banma, khoa);
            //ma hoa theo khoa moi vua tim
            for (int i = 0; i < banma.Length; i++)
            {
                for (int j = 0; j < slkytu; j++)
                {
                    if (banma.Substring(i, 1) == bankytu[j])
                    {
                        int vtktbanro = j - vtkhoa[i];
                        if (vtktbanro < 0) vtktbanro += slkytu;
                        chuoigiaima += bankytu[vtktbanro];
                        break;
                    }
                }
            }
            return chuoigiaima;
        }
        public Boolean LuuDuLieu(string fileluu, string dulieu)
        {
            Boolean tinhtrang = true;
            string filebanro = fileluu;
            string filebanma = "mh" + filebanro;
            string dulieubanro = dulieu;
            string dulieubanma = MaHoaVigenere(dulieubanro, khoabimat);
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (StreamWriter ghibanro = new StreamWriter(new IsolatedStorageFileStream(filebanro, FileMode.Create, FileAccess.Write, myIsolatedStorage)))
                {
                    ghibanro.WriteLine(dulieubanro);
                    ghibanro.Close();
                }
                using (StreamWriter ghimahoa = new StreamWriter(new IsolatedStorageFileStream(filebanma, FileMode.Create, FileAccess.Write, myIsolatedStorage)))
                {
                    ghimahoa.WriteLine(dulieubanma);
                    ghimahoa.Close();
                }
            }
            catch
            {
                tinhtrang = false;
            }
            return tinhtrang;
        }
        public string LayDuLieu(string fileluu)
        {
            string filebanro = fileluu;
            string filebanma = "mh" + filebanro;
            string dulieubanro = "0";
            string dulieubanma = "0";
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                IsolatedStorageFileStream mobanro = myIsolatedStorage.OpenFile(filebanro, FileMode.Open, FileAccess.Read);
                using (StreamReader docbanro = new StreamReader(mobanro))
                {
                    dulieubanro = docbanro.ReadLine();
                }
                IsolatedStorageFileStream mobanma = myIsolatedStorage.OpenFile(filebanma, FileMode.Open, FileAccess.Read);
                using (StreamReader docbanma = new StreamReader(mobanma))
                {
                    dulieubanma = docbanma.ReadLine();
                }
            }
            catch
            {
                dulieubanro = "0";
                dulieubanma = "0";
            }
            if (dulieubanro != GiaiMaVigenere(dulieubanma, khoabimat))
            {
                dulieubanro = "0";
            }
            return dulieubanro;
        }
    }
}