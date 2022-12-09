using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System;
using System.Threading;


namespace MainProgram
{
    public class Global
    {
        public static List<string> nodeip = new List<string>();
        public static List<string> usedeip = new List<string>();
        public static List<string> nosnmpip = new List<string>();
        public static string rename;
        public static List<string> x = new List<string>();
        public static List<string> icon = new List<string>();
        public static List<string> routerip = new List<string>();
        public static List<string> switchip = new List<string>();
    }


    class MyProcess             //用於下cmd指令
    {
        // 當找不到檔案或者拒絕訪問時出現的Win32錯誤碼
        const int ERROR_FILE_NOT_FOUND = 2;
        const int ERROR_ACCESS_DENIED = 5;

        // 通過命令列獲取help顯示資訊
        void PrintDoc(string input_ip, int i)
        {
            Process process = new Process();
            try
            {



                string[] oid = new string[] { ".1.3.6.1.2.1.4.22.1.2", ".1.3.6.1.2.1.1.1", ".1.3.6.1.4.1.9.2.1.56", ".1.3.6.1.2.1.2.2.1.8" };                                                                       //固定的OID
                string path1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\sharpsnmplib-samples-master\\Samples\\CSharpCore\\snmpwalk\\bin\\Debug\\netcoreapp3.1\\snmpwalk ";    //snmpwalk的路徑



                string c = " -c=123";
                string s = "Variable: Id: ";

                string str = "";
                LinkedList<string> node = new LinkedList<string>();

                string instruction = path1 + input_ip + " " + oid[i] + c;                                                                   //把path1,ip,oid以及c組合起來

                process.StartInfo.UseShellExecute = false;   //是否使用作業系統shell啟動 
                process.StartInfo.CreateNoWindow = true;   //是否在新視窗中啟動該程序的值 (不顯示程式視窗)
                process.StartInfo.RedirectStandardInput = true;  // 接受來自呼叫程式的輸入資訊 
                process.StartInfo.RedirectStandardOutput = true;  // 由呼叫程式獲取輸出資訊
                process.StartInfo.RedirectStandardError = true;  //重定向標準錯誤輸出
                process.StartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";  //使用cmd
                process.Start();                         // 啟動程式
                process.StandardInput.WriteLine(instruction); //向cmd視窗傳送輸入指令
                process.StandardInput.AutoFlush = true;
                // 前面一個命令不管是否執行成功都執行後面(exit)命令，如果不執行exit命令，後面呼叫ReadToEnd()方法會假死
                process.StandardInput.WriteLine("exit");
                StreamReader reader = process.StandardOutput;//獲取exe處理之後的輸出資訊
                string get_info = reader.ReadToEnd();           //讀取輸出資訊

                reader.Close(); //close程序
                process.WaitForExit();  //等待程式執行完退出程序
                process.Close();

                string s0 = "遠端主機已強制關閉一個現存的連線";
                bool a1 = get_info.Contains(s0);                                               //判別需要的資料
                if (a1)
                {
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + ".txt";
                    StreamWriter sw = new StreamWriter(fullfilename1);
                    sw.WriteLine(input_ip + ",NoSnmp/pc");
                    sw.Close();
                    Global.nosnmpip.Add(input_ip);


                    i = 1000;


                }                                            //判別需要的資料


                if (i == 0)
                {
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "-arpfull.txt";
                    string fullfilename2 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "-arp.txt";
                    StreamWriter sw = new StreamWriter(fullfilename1);
                    sw.WriteLine(get_info);                                                 //將所有資料寫入檔案
                    sw.Close();
                    StreamReader sr1 = new StreamReader(fullfilename1);                             //讀取full.txt內容
                    StreamWriter sw1 = new StreamWriter(fullfilename2);                             //輸入nexthop.txt內容
                    string IP;
                    string Data;
                    string allip = "";
                    while ((str = sr1.ReadLine()) != null)                                         //用迴圈控制讀取資料
                    {
                        bool a = str.Contains(s);                                               //判別需要的資料
                        if (a)
                        {
                            IP = str.Remove(0, 37);                                               //去除多餘的資料
                            int founds1 = IP.IndexOf("; Data: ");                                 //去除多餘的資料
                            IP = IP.Remove(founds1);                                              //去除多餘的資料
                            Data = str;
                            int founds2 = Data.IndexOf("; Data: ");
                            Data = Data.Remove(0, founds2 + 8);
                            sw1.WriteLine(IP + "," + Data);                                   //將整理的資料寫入文字檔

                            Global.nodeip.Add(IP);
                        }
                    }
                    sr1.Close();
                    sw1.Close();
                    Global.nodeip = Global.nodeip.Distinct().ToList();
                }
                else if (i == 1)
                {
                    string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "namefull.txt";
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "name.txt";

                    string Data;

                    StreamWriter sw = new StreamWriter(fullfilename);
                    sw.WriteLine(get_info);                                                 //將所有資料寫入檔案
                    sw.Close();

                    StreamReader sr = new StreamReader(fullfilename);                             //讀取full.txt內容
                    StreamWriter sw1 = new StreamWriter(fullfilename1);
                    string allname = "";


                    while ((str = sr.ReadLine()) != null)                                         //用迴圈控制讀取資料
                    {
                        bool a = str.Contains(s);                                               //判別需要的資料
                        if (a)
                        {
                            bool b = str.Contains("DGS-3120");
                            if (b)
                            {
                                sw1.WriteLine(input_ip + ",DGS-3120");                                   //將整理的資料寫入文字檔
                                Global.rename = "DGS-3120";
                                Global.icon.Add(input_ip + ",nodeIcon2");
                            }
                            else
                            {
                                Data = str;
                                int founds2 = Data.IndexOf("; Data: ");
                                Data = Data.Remove(0, founds2 + 8);
                                string[] hex = Data.Split("-");

                                foreach (string hex2 in hex)
                                {
                                    int v = Convert.ToInt32(hex2, 16);
                                    string strsdf = char.ConvertFromUtf32(v);
                                    allname = allname + strsdf;

                                }
                                bool sws = allname.Contains("C3560");
                                if (sws)
                                {
                                    sw1.WriteLine(input_ip + ",C3560");                                   //將整理的資料寫入文字檔
                                    Global.rename = "C3560";
                                    Global.icon.Add(input_ip + ",null");
                                    Global.routerip.Add(input_ip);
                                }
                                bool sws1 = allname.Contains("C2960");
                                if (sws1)
                                {
                                    sw1.WriteLine(input_ip + ",C2960");                                   //將整理的資料寫入文字檔
                                    Global.rename = "C2960";
                                    Global.icon.Add(input_ip + ",nodeIcon2");
                                    Global.switchip.Add(input_ip);
                                }
                            }
                            sw1.Close();

                        }
                    }
                    sr.Close();
                }
                else if (i == 2) //cpu
                {
                    string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "cpufull.txt";
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "cpu.txt";
                    string Data;

                    StreamWriter sw = new StreamWriter(fullfilename);
                    sw.WriteLine(get_info);
                    sw.Close();
                    StreamReader sr = new StreamReader(fullfilename);                             //讀取full.txt內容
                    StreamWriter sw1 = new StreamWriter(fullfilename1);
                    while ((str = sr.ReadLine()) != null)                                         //用迴圈控制讀取資料
                    {
                        bool a = str.Contains(s);                                               //判別需要的資料
                        if (a)
                        {

                            Data = str;
                            int founds2 = Data.IndexOf("; Data: ");
                            Data = Data.Remove(0, founds2 + 8);
                            sw1.WriteLine(input_ip + "," + Data);                                   //將整理的資料寫入文字檔
                        }
                    }
                    sr.Close();
                    sw1.Close();
                }
                else if (i == 3) //port
                {
                    string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "portfull.txt";
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + input_ip + "_interface_port.csv";
                    string Data;
                    int p = 1;
                    int n = 0;
                    string level = "";
                    StreamWriter sw = new StreamWriter(fullfilename);
                    sw.WriteLine(get_info);
                    sw.Close();
                    StreamReader sr = new StreamReader(fullfilename);                             //讀取full.txt內容
                    StreamWriter sw1 = new StreamWriter(fullfilename1);
                    sw1.WriteLine("port_name,on_off");
                    while ((str = sr.ReadLine()) != null)                                         //用迴圈控 制讀取資料
                    {
                        if (Global.rename == "C3560")
                        {
                            string s1 = "Variable: Id: 1.3.6.1.2.1.2.2.1.8.101";
                            bool a = str.Contains(s1);                                               //判別需要的資料
                            if (a)
                            {
                                Data = str;
                                int founds2 = Data.IndexOf("; Data: ");
                                Data = Data.Remove(0, founds2 + 8);
                                sw1.WriteLine(p + "," + Data);                                   //將整理的資料寫入文字檔
                                if (Data == "1")
                                {
                                    n++;
                                }
                                p++;
                            }
                            level = "L3";
                        }
                        else if (Global.rename == "DGS-3120")
                        {
                            string s1 = "Variable: Id: 1.3.6.1.2.1.2.2.1.8";
                            bool a = str.Contains(s1);                                               //判別需要的資料                        
                            if (a)
                            {
                                int founds1 = str.IndexOf(".8.");
                                string ip;
                                ip = str.Remove(0, founds1 + 3);
                                if (ip.Length > 11)
                                {
                                    continue;
                                }
                                Data = str;
                                int founds2 = Data.IndexOf("; Data: ");
                                Data = Data.Remove(0, founds2 + 8);
                                sw1.WriteLine(p + "," + Data);                                   //將整理的資料寫入文字檔
                                if (Data == "1")
                                {
                                    n++;
                                }
                                p++;
                            }
                            level = "L2";
                        }
                        else if (Global.rename == "C2960")
                        {
                            string s1 = "Variable: Id: 1.3.6.1.2.1.2.2.1.8.100";
                            string s2 = "Variable: Id: 1.3.6.1.2.1.2.2.1.8.101";
                            bool a = str.Contains(s1);                                               //判別需要的資料
                            bool b = str.Contains(s2);
                            if (a || b)
                            {
                                Data = str;
                                int founds2 = Data.IndexOf("; Data: ");
                                Data = Data.Remove(0, founds2 + 8);
                                sw1.WriteLine(p + "," + Data);                                   //將整理的資料寫入文字檔
                                if (Data == "1")
                                {
                                    n++;
                                }
                                p++;
                            }
                            level = "L2";
                        }
                    }
                    Global.x.Add(n.ToString() + "," + input_ip);
                    sr.Close();
                    sw1.Close();
                }








            }
            catch (Win32Exception e)                                       //處理錯誤訊息
            {
                if (e.NativeErrorCode == ERROR_FILE_NOT_FOUND)
                {
                    Console.WriteLine(e.Message + ". 檢查檔案路徑.");
                }

                else if (e.NativeErrorCode == ERROR_ACCESS_DENIED)
                {
                    Console.WriteLine(e.Message + ". 你沒有許可權操作檔案.");
                }
            }
        }
       
        public static void Main()
        {




            string path = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public";                                      //將路徑指定到C槽                 
            try
            {
                if (!Directory.Exists(path)) //創建資料夾
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            try
            {

                string csv = "*.csv";

                string[] csvfiles = Directory.GetFiles(path, csv);
                foreach (string currentfile in csvfiles)
                {
                    File.Delete(currentfile);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            MyProcess myProcess = new MyProcess();
            string ip = "192.168.10.254";
            myProcess.PrintDoc(ip, 0);
            Global.usedeip.Add(ip);

            for (int i = 0; i < Global.nodeip.Count; i++)
            {
                myProcess.PrintDoc(Global.nodeip[i], 0);
            }
            for (int i = 0; i < Global.nodeip.Count; i++)
            {
                myProcess.PrintDoc(Global.nodeip[i], 1);
            }
            for (int i = 0; i < Global.nodeip.Count; i++)
            {
                myProcess.PrintDoc(Global.nodeip[i], 2);
            }
            for (int i = 0; i < Global.nodeip.Count; i++)
            {
                myProcess.PrintDoc(Global.nodeip[i], 3);
            }
            Global.nosnmpip = Global.nosnmpip.Distinct().ToList();
            int k = 0;

            while (k < 1)
            {
                string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\node-ip-all.csv";
                string ipnode;
                StreamWriter sw = new StreamWriter(fullfilename);
                sw.WriteLine("name,parent,icon");
                for (int i = 0; i < Global.nosnmpip.Count; i++)
                {
                    sw.WriteLine(Global.nosnmpip[i] + "," + Global.nosnmpip[i] + "," + "nodeIcon1");
                }
                for (int i = 0; i < Global.switchip.Count; i++)
                {
                    sw.WriteLine(Global.switchip[i] + "," + Global.switchip[i] + "," + "nodeIcon2");
                }
                for (int i = 0; i < Global.routerip.Count; i++)
                {
                    sw.WriteLine(Global.routerip[i] + "," + Global.routerip[i] + "," + "null");
                }

                sw.Close();
                k = 1;
            }
            k = 0;
            while (k < 1)
            {

                string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\prop.csv";
                StreamWriter sw = new StreamWriter(fullfilename);

                string str = "";
                string Data = "";
                sw.WriteLine("installation,ip,mac,memory_used,memory_total,memory,cpu,port");
                for (int i = 0; i < Global.nosnmpip.Count; i++)
                {
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + ip + "-arp.txt";
                    StreamReader sr = new StreamReader(fullfilename1);
                    sw.Write("nosnmp/pc,");
                    sw.Write(Global.nosnmpip[i] + ",");
                    while ((str = sr.ReadLine()) != null)
                    {
                        bool a = str.Contains(Global.nosnmpip[i] + ",");
                        if (a)
                        {
                            int f1 = Global.nosnmpip[i].Length;
                            Data = str.Remove(0, f1 + 1);
                            sw.Write(Data + ",");
                        }
                    }
                    sw.Write("null,null,null,null,null");
                    sw.WriteLine();
                    sr.Close();
                }
                for (int i = 0; i < Global.switchip.Count; i++)
                {
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.switchip[i] + "-arp.txt";
                    string fullfilename2 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.switchip[i] + "cpu.txt";
                    string fullfilename3 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.switchip[i] + "_interface_port.csv";
                    StreamReader sr = new StreamReader(fullfilename1);
                    StreamReader sr1 = new StreamReader(fullfilename2);
                    sw.Write("switch,");
                    sw.Write(Global.switchip[i] + ",");
                    while ((str = sr.ReadLine()) != null)
                    {
                        bool a = str.Contains(Global.switchip[i] + ",");
                        if (a)
                        {
                            int f1 = Global.switchip[i].Length;
                            Data = str.Remove(0, f1 + 1);
                            sw.Write(Data + ",");
                        }
                    }
                    sr.Close();
                    sw.Write("null,null,null,");
                    while ((str = sr1.ReadLine()) != null)
                    {
                        bool a = str.Contains(Global.switchip[i] + ",");
                        if (a)
                        {
                            int f1 = Global.switchip[i].Length;
                            Data = str.Remove(0, f1 + 1);
                            sw.Write(Data + ",");
                        }
                    }
                    sr1.Close();
                    string[] lastport = System.IO.File.ReadAllLines(fullfilename3);
                    string numport = lastport[lastport.Length - 1];
                    numport = numport.Substring(0, numport.Length - 2);
                    sw.Write(numport);
                    sw.WriteLine();
                }
                for (int i = 0; i < Global.routerip.Count; i++)
                {
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.routerip[i] + "-arp.txt";
                    string fullfilename2 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.routerip[i] + "cpu.txt";
                    string fullfilename3 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.routerip[i] + "_interface_port.csv";
                    StreamReader sr = new StreamReader(fullfilename1);
                    StreamReader sr1 = new StreamReader(fullfilename2);
                    sw.Write("router,");
                    sw.Write(Global.routerip[i] + ",");
                    while ((str = sr.ReadLine()) != null)
                    {
                        bool a = str.Contains(Global.routerip[i] + ",");
                        if (a)
                        {
                            int f1 = Global.routerip[i].Length;
                            Data = str.Remove(0, f1 + 1);
                            sw.Write(Data + ",");
                        }
                    }
                    sr.Close();
                    sw.Write("null,null,null,");
                    while ((str = sr1.ReadLine()) != null)
                    {
                        bool a = str.Contains(Global.routerip[i] + ",");
                        if (a)
                        {
                            int f1 = Global.routerip[i].Length;
                            Data = str.Remove(0, f1 + 1);
                            sw.Write(Data + ",");
                        }
                    }
                    sr1.Close();
                    string[] lastport = System.IO.File.ReadAllLines(fullfilename3);
                    string numport = lastport[lastport.Length - 1];
                    numport = numport.Substring(0, numport.Length - 2);
                    sw.Write(numport);
                    sw.WriteLine();
                }


                sw.Close();
                k++;
            }

            k = 0;
            while (k < 1)
            {
                string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\gateway_hexthop.csv";
                StreamWriter sw = new StreamWriter(fullfilename);                             //讀取full.txt內容
                string str = "";
                string Data = "";
                List<string> switchip = new List<string>();
                sw.WriteLine("name,parent");
                sw.WriteLine(ip);
                Global.routerip.Remove(ip);
                for (int i = 0; i < Global.routerip.Count; i++)
                {
                    sw.WriteLine(ip + "," + Global.routerip[i]);
                }
                for (int i = 0; i < Global.routerip.Count; i++)
                {
                    string fullfilename1 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.routerip[i] + "-arp.txt";
                    List<string> data = new List<string>();
                    StreamReader sr = new StreamReader(fullfilename1);
                    while ((str = sr.ReadLine()) != null)                                         //用迴圈控 制讀取資料
                    {


                        int founds2 = str.IndexOf(",");
                        Data = str.Substring(0, founds2);

                        data.Add(Data);


                    }
                    for (int l = 0; l < Global.nosnmpip.Count; l++)
                    {
                        data.Remove(Global.nosnmpip[l]);
                    }
                    data.Remove(ip);
                    data.Remove(Global.routerip[k]);
                    for (int l = 0; l < data.Count; l++)
                    {
                        sw.WriteLine(Global.routerip[k] + "," + data[l]);
                        Global.switchip.Remove(data[l]);
                    }
                    data.Clear();
                    sr.Close();
                    for (int l = 0; l < Global.switchip.Count; l++)
                    {
                        string fullfilename2 = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\" + Global.switchip[l] + "-arp.txt";
                        StreamReader sr1 = new StreamReader(fullfilename2);
                        while ((str = sr1.ReadLine()) != null)                                         //用迴圈控 制讀取資料
                        {
                            int founds2 = str.IndexOf(",");
                            Data = str.Substring(0, founds2);
                            data.Add(Data);
                            data.Remove(ip);
                            for (int j = 0; j < Global.nosnmpip.Count; j++)
                            {
                                data.Remove(Global.nosnmpip[j]);
                            }
                        }
                        if (Global.switchip[l] == "192.168.10.253")
                        {
                            sw.WriteLine(ip + "," + Global.switchip[l]);
                            for (int n = 0; n < Global.nosnmpip.Count; n++)
                            {
                                sw.WriteLine(Global.switchip[l] + "," + Global.nosnmpip[n]);
                            }
                            Global.switchip.Remove(Global.switchip[l]);
                        }
                        data.Clear();
                        sr1.Close();
                    }
                    string b = "";
                    for (int l = 0; l < Global.switchip.Count; l++)
                    {
                       
                        for (int n = 0; n < Global.x.Count; n++)
                        {
                            bool a = Global.x[n].Contains(Global.switchip[l]);

                            if (a)
                            {
                                string m;

                                m = Global.x[n].Substring(0, 1);
                                if (m == "2")
                                {
                                    sw.WriteLine(ip + "," + Global.switchip[l]);
                                    sw.WriteLine(b + "," + Global.switchip[l]);
                                }
                                else
                                {
                                    b = Global.switchip[l];
                                    
                                }
                            }
                        }
                    }
                }
                sw.Close();
                k++;
            }
            k = 0;
            while (k < 1)
            {
                string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\地球上最浪漫的一首歌.黃鴻升.csv";
                StreamWriter sw = new StreamWriter(fullfilename);
                sw.WriteLine("csv");
                sw.WriteLine("node-ip-all.csv");
                sw.WriteLine("gateway_hexthop.csv");



                sw.Close();
                k++;
            }
            k = 0;

            while (k < 1)
            {
                string fullfilename = "C:\\Users\\MUST_CSIE\\Desktop\\vueproject2\\vueproject2\\public\\山丘.李宗盛.csv";
                StreamWriter sw = new StreamWriter(fullfilename);
                sw.WriteLine("csv");
                for(int i = 0; i < Global.nosnmpip.Count; i++)
                {
                    Global.nodeip.Remove(Global.nosnmpip[i]);
                }
                for(int i = 0; i < Global.nodeip.Count; i++)
                {
                    sw.WriteLine(Global.nodeip[i]+ "_interface_port.csv");
                }
                sw.Close();
                k++;
            }





        }

    }
}