using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace FrmOtomatikYedek
{
    public partial class FrmOtoYedek : DevExpress.XtraEditors.XtraForm
    {
        string programadi = "Otomatik Yedekleme";
        public static SqlConnection conn;
        public static SqlCommand command;
        public static SqlDataReader reader;
        public static string sql = "";
        public static string connectionString = "";
        public static string[] seciligunler = new string[7];
        public static string guncelgun = "";
        public string zamanlamasecili = "";
        public static DataTable dt = new DataTable();
        public static Boolean yedeklerArsivlensin = false;
        public static DataTable ayardt = new DataTable();
        public static string YedekKonum = "";
        public static Boolean zamandaYedekle = false;
        public static string seciliSaat = "";
        public static string yedeklenecekVeritabanlari;
        public static Boolean otomatikYedekle = false;
        public static DataTable dt2 = new DataTable();

       
        private static FrmOtoYedek OrneginiGetir;
        public static FrmOtoYedek Olustur
        {
            get
            {
                if (OrneginiGetir == null)
                    OrneginiGetir = new FrmOtoYedek();
                return OrneginiGetir;
            }
            set
            {
                OrneginiGetir = value;
            }
        }
        class Gorev : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                
                if (zamandaYedekle == false  )
                {
                    guncelgun = DateTime.Now.DayOfWeek.ToString();
                    for (int i = 0; i < 7; i++)
                    {
                        string gunkotrol = seciligunler[i];
                        if (gunkotrol == guncelgun)
                        {
                            
                            try
                            {
                                FrmOtoYedek form = new FrmOtoYedek();
                                conn = new SqlConnection(connectionString);
                                conn.Open();
                                string value = "";
                                string name = "";
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    value = dt.Rows[j]["SEÇİM"].ToString();
                                    if (value == "True")
                                    {
                                        name = dt.Rows[j]["DATABASE_NAME"].ToString();
                                        if (yedeklerArsivlensin == true)
                                        {
                                            sql = "BACKUP DATABASE " + name + " TO DISK = '" + YedekKonum.ToString() + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak' WITH COMPRESSION";
                                        }
                                        else
                                        {
                                            sql = "BACKUP DATABASE " + name + " TO DISK = '" + YedekKonum.ToString() + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
                                        }
                                        command = new SqlCommand(sql, conn);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("Veritabanı Yedeklemesi Başarılı Bir Şekilde Tamamlandı");
                                form.RestoreGridGetir();
                                name = "";
                              
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                                conn.Dispose();
                            }
                        }                                       
                    }
                }
                else
                {
                    int sayac = 0; 
                    guncelgun = DateTime.Now.DayOfWeek.ToString();
                    for (int i = 0; i < 7; i++)
                    {
                        if ((seciligunler[i]==guncelgun) && (seciliSaat == DateTime.Now.ToLongTimeString()))
                        {
                            try
                            {
                                FrmOtoYedek form = new FrmOtoYedek();
                                conn = new SqlConnection(connectionString);
                                conn.Open();
                                string value = "";
                                string name = "";
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    value = dt.Rows[j]["SEÇİM"].ToString();
                                    if (value == "True")
                                    {
                                        name = dt.Rows[j]["DATABASE_NAME"].ToString();
                                        if (yedeklerArsivlensin == true)
                                        {
                                            sql = "BACKUP DATABASE " + name + " TO DISK = '" + YedekKonum.ToString() + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak' WITH COMPRESSION";
                                        }
                                        else
                                        {
                                            sql = "BACKUP DATABASE " + name + " TO DISK = '" + YedekKonum.ToString() + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
                                        }
                                        command = new SqlCommand(sql, conn);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("Veritabanı Yedeklemesi Başarılı Bir Şekilde Tamamlandı");
                                form.RestoreGridGetir();
                                name = "";                                
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                conn.Close();
                                conn.Dispose();
                            }
                        }
                    }                    
                    for (int i = 0; i < 7; i++)
                    {
                        if (seciligunler[i]==null)
                        {
                            sayac++;
                        }                        
                    }
                    if ((sayac == 7) && (seciliSaat == DateTime.Now.ToLongTimeString()))
                    {
                        try
                        {
                            FrmOtoYedek form = new FrmOtoYedek();
                            conn = new SqlConnection(connectionString);
                            conn.Open();
                            string value = "";
                            string name = "";
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                value = dt.Rows[j]["SEÇİM"].ToString();
                                if (value == "True")
                                {
                                    name = dt.Rows[j]["DATABASE_NAME"].ToString();
                                    if (yedeklerArsivlensin == true)
                                    {
                                        sql = "BACKUP DATABASE " + name + " TO DISK = '" + YedekKonum.ToString() + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak' WITH COMPRESSION";
                                    }
                                    else
                                    {
                                        sql = "BACKUP DATABASE " + name + " TO DISK = '" + YedekKonum.ToString() + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
                                    }
                                    command = new SqlCommand(sql, conn);
                                    command.ExecuteNonQuery();
                                }
                            }
                            MessageBox.Show("Veritabanı Yedeklemesi Başarılı Bir Şekilde Tamamlandı");
                            form.RestoreGridGetir();
                            name = "";
                              
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }                      
                    }   
                    
                }
            }
        }
        class Tetikleyici
        {
            private IScheduler Baslat()
            {         
                ISchedulerFactory schedFact = new StdSchedulerFactory();
                IScheduler sched = schedFact.GetScheduler();
                if (!sched.IsStarted)
                {
                    sched.Start();
                }
                else
                {
                    sched.Clear();
                }
                
                return sched;
            }
            public void GoreviTetikle()
            {
                
                if (zamandaYedekle == false)
                {
                    IScheduler sched = Baslat();
                    IJobDetail Gorev = JobBuilder.Create<Gorev>().WithIdentity("Gorev", null).Build();
                    ICronTrigger TriggerGorev = (ICronTrigger)TriggerBuilder.Create().WithIdentity("Gorev").StartAt(DateTime.UtcNow).WithCronSchedule(" 0 0 12 1/1 * ? * ").Build();
                    sched.ScheduleJob(Gorev, TriggerGorev);
                }
                else
                {
                    IScheduler sched = Baslat();
                    IJobDetail Gorev = JobBuilder.Create<Gorev>().WithIdentity("Gorev", null).Build();
                    ISimpleTrigger TriggerGorev = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("Gorev").StartAt(DateTime.UtcNow).WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever()).Build();
                    sched.ScheduleJob(Gorev, TriggerGorev);
                    
                }
                
            }
        }       
            public FrmOtoYedek()
        {
            InitializeComponent();

            
            try
            {

                connectionString = "Data Source=localhost;Initial Catalog=ayarlar;Integrated Security=True";
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "SELECT * From ayar";
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();
                ayardt.Clear();
                ayardt.Load(reader);
                txtServerAdi.Text = ayardt.Rows[0][1].ToString();
                txtKullaniciAdi.Text = ayardt.Rows[0][2].ToString();
                txtSifre.Text = ayardt.Rows[0][11].ToString();
                if (ayardt.Rows[0][3].ToString() == "True")
                {
                    chckOtomatikYedekle.Checked = true;
                }
                else
                {
                    chckOtomatikYedekle.Checked = false;
                }
                if (ayardt.Rows[0][9].ToString() == "True")
                {
                    chckZamandaYedekle.Checked = true;
                }
                else
                {
                    chckZamandaYedekle.Checked = false;
                }
                if (ayardt.Rows[0][10].ToString() == "True")
                {
                    chckYedekleriArsivle.Checked = true;
                }
                else
                {
                    chckYedekleriArsivle.Checked = false;
                }
                timeOtoYedekSaati.EditValue = ayardt.Rows[0][5].ToString();
                numGunYedekSil.Value = Convert.ToInt32(ayardt.Rows[0][6].ToString());
                txtYedekKonum1.Text = ayardt.Rows[0][7].ToString();
                txtYedekKonum2.Text = ayardt.Rows[0][8].ToString();
                string gunler = ayardt.Rows[0][4].ToString();
                string[] parcala;
                parcala = gunler.Split(',');

                foreach (var item in parcala)
                {
                    if (item == "Monday")
                    {
                        chckPazartesi.Checked = true;
                    }
                    if (item == "Tuesday")
                    {
                        chckSali.Checked = true;
                    }
                    if (item == "Wednesday")
                    {
                        chckCarsamba.Checked = true;
                    }
                    if (item == "Thursday")
                    {
                        chckPersembe.Checked = true;
                    }
                    if (item == "Friday")
                    {
                        chckCuma.Checked = true;
                    }
                    if (item == "Saturday")
                    {
                        chckCumartesi.Checked = true;
                    }
                    if (item == "Sunday")
                    {
                        chckPazar.Checked = true;
                    }
                }
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (key.GetValue(programadi).ToString() == "\"" + Application.ExecutablePath + "\"")
                {
                    chckBaslangictaCalis.Checked = true;
                    if (txtKullaniciAdi.Text != "" && txtServerAdi.Text != "" && txtSifre.Text != "")
                    {
                        btnBaglan.Enabled = false;
                        btnTestEt.Enabled = false;
                        btnBaglantiKes.Enabled = true;
                        try
                        {
                            connectionString = "Data Source=" + txtServerAdi.Text + ";User ID=" + txtKullaniciAdi.Text + ";Password=" + txtSifre.Text + "";
                            conn = new SqlConnection(connectionString);
                            conn.Open();
                            sql = "EXEC sp_databases";
                            command = new SqlCommand(sql, conn);
                            reader = command.ExecuteReader();
                            dt.Clear();
                            dt.Load(reader);
                            string s = "BOŞ";
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (dt.Columns[i].Caption.ToString() == "SEÇİM")
                                {
                                    s = "Var";
                                    break;
                                }
                                else
                                {
                                    s = "Yok";
                                }
                            }
                            if (s == "Yok")
                            {
                                dt.Columns.Add(new DataColumn("SEÇİM", Type.GetType("System.Boolean")));
                            }
                            gridYedek.DataSource = dt;
                            labelControl10.Text = dt.Rows.Count.ToString();

                            reader.Dispose();
                            conn.Close();
                            conn.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }


                        YedekKonum = txtYedekKonum1.Text;
                        if (chckYedekleriArsivle.Checked)
                        {
                            yedeklerArsivlensin = true;
                        }
                        if (chckZamandaYedekle.Checked)
                        {
                            seciliSaat = timeOtoYedekSaati.EditValue.ToString();
                            zamandaYedekle = true;
                            string[] parcalazaman = new string[2];
                            parcalazaman = seciliSaat.Split();
                            seciliSaat = parcalazaman[0];
                        }
                        
                        connectionString += "; Initial Catalog=ayarlar";
                        conn = new SqlConnection(connectionString);
                        conn.Open();
                        sql = "SELECT veritabanlari FROM ayar where ayar_id=0";
                        command = new SqlCommand(sql, conn);
                        reader = command.ExecuteReader();
                        dt2.Clear();
                        dt2.Load(reader);
                        string veritabanlari = dt2.Rows[0]["veritabanlari"].ToString();
                        string[] vt = new string[gridView1.RowCount];
                        vt = veritabanlari.Split(',');

                        for (int i = 0; i < vt.Length - 1; i++)
                        {
                            if (gridView1.GetDataRow(i)["DATABASE_NAME"].ToString() == vt[i].ToString())
                            {
                                gridView1.GetDataRow(i)["SEÇİM"] = "True";
                            }
                        }

                        string value = "";
                        int sayac = 0;


                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            value = gridView1.GetDataRow(i)["SEÇİM"].ToString();
                            if (value == "True")
                            {
                                sayac++;
                            }
                        }
                        if (sayac == 0)
                        {
                            MessageBox.Show("Lütfen Yedeklemek İstediğiniz Veritabanlarını Seçiniz");
                            sayac = 0;
                        }
                        else
                        {
                            if (chckPazartesi.Checked)
                            {
                                seciligunler[0] = "Monday";
                            }
                            if (chckSali.Checked)
                            {
                                seciligunler[1] = "Tuesday";
                            }
                            if (chckCarsamba.Checked)
                            {
                                seciligunler[2] = "Wednesday";
                            }
                            if (chckPersembe.Checked)
                            {
                                seciligunler[3] = "Thursday";
                            }
                            if (chckCuma.Checked)
                            {
                                seciligunler[4] = "Friday";
                            }
                            if (chckCumartesi.Checked)
                            {
                                seciligunler[5] = "Saturday";
                            }
                            if (chckPazar.Checked)
                            {
                                seciligunler[6] = "Sunday";
                            }
                            if (chckOtomatikYedekle.Checked)
                            {
                                otomatikYedekle = true;
                            }
                            else
                            {
                                otomatikYedekle = false;
                            }
                            if (chckYedekleriArsivle.Checked)
                            {
                                yedeklerArsivlensin = true;
                            }
                            else
                            {
                                yedeklerArsivlensin = false;
                            }
                            if (chckZamandaYedekle.Checked)
                            {
                                zamandaYedekle = true;
                            }
                            else
                            {
                                zamandaYedekle = false;
                            }

                            string dbseciligunler = "";
                            int say = 0;
                            foreach (var item in seciligunler)
                            {
                                if ((item != null && item != "") && say == 0)
                                {
                                    dbseciligunler = item;
                                    say++;
                                    continue;
                                }
                                if ((item != null && item != "") && say > 0)
                                {
                                    dbseciligunler += "," + item;
                                }
                            }

                            command = new SqlCommand(sql, conn);

                            command.ExecuteNonQuery();
                            conn.Close();
                            dbseciligunler = "";
                            MessageBox.Show("Veritabanı Yedeklemesi Başlatıldı..");
                            Tetikleyici tetikle = new Tetikleyici();
                            tetikle.GoreviTetikle();
                            
                        }

                    }
                    else
                    {
                        btnBaglan.Enabled = true;
                        btnTestEt.Enabled = true;
                        btnBaglantiKes.Enabled = true;

                        groupBox2.Enabled = false;
                        txtYedekKonum1.Enabled = false;
                        txtYedekKonum2.Enabled = false;
                        btnYedekAl.Enabled = false;
                    }
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTestEt_Click(object sender, EventArgs e)
        {           
            try
            {
                connectionString = "Data Source=" + txtServerAdi.Text + ";User ID=" + txtKullaniciAdi.Text + ";Password=" + txtSifre.Text + "";
                conn = new SqlConnection(connectionString);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Başarılı");
                    conn.Close();
                    conn.Dispose();
                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı Başarısız! Lütfen Bilgileri Kontrol Ediniz");
            }
        }
        private void btnBaglan_Click(object sender, EventArgs e)
        {
            
            try
            {
                connectionString = "Data Source=" + txtServerAdi.Text + ";User ID=" + txtKullaniciAdi.Text + ";Password=" + txtSifre.Text + "";
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "EXEC sp_databases";
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();                
                dt.Clear();
                dt.Load(reader);
                dt.Columns.Add(new DataColumn("SEÇİM", Type.GetType("System.Boolean")));
                gridYedek.DataSource = dt;
                labelControl10.Text = dt.Rows.Count.ToString();

                reader.Dispose();
                conn.Close();
                conn.Dispose();

                btnBaglantiKes.Enabled = true;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;

                panel2.Enabled = false;
                chckOtomatikYedekle.Enabled = false;
                chckZamandaYedekle.Enabled = false;
                timeOtoYedekSaati.Enabled = false;

                txtYedekKonum1.Enabled = true;
                txtYedekKonum2.Enabled = true;

                txtServerAdi.Enabled = false;
                txtKullaniciAdi.Enabled = false;
                txtSifre.Enabled = false;
                btnBaglan.Enabled = false;
                btnTestEt.Enabled = false;

                btnYedekAl.Enabled = true;
                
                if (chckBaslangictaCalis.Checked)
                {
                    chckOtomatikYedekle.Enabled = true;                    
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key.SetValue(programadi, "\"" + Application.ExecutablePath + "\"");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "SEÇİM", "True");
                
            }
        }
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "SEÇİM", "False");
            }
        }
        private void gridYedek_MouseClick(object sender, MouseEventArgs e)
        {
            string value = "";
            int say=0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                value = gridView1.GetDataRow(i)["SEÇİM"].ToString();
                if (value == "True")
                {
                    say++;
                   
                }
            }
            labelControl12.Text = say.ToString();
        }     
        private void FrmOtoYedek_Load(object sender, EventArgs e)
        {
            
        }
        private void btnBaglantiKes_Click(object sender, EventArgs e)
        {
            txtServerAdi.Text = "";
            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
            txtServerAdi.Enabled = true;
            txtKullaniciAdi.Enabled = true;
            txtSifre.Enabled = true;
            btnBaglan.Enabled = true;
            btnTestEt.Enabled = true;
            txtYedekKonum1.Text = "";
            txtYedekKonum2.Text = "";            

            btnYedekAl.Enabled = false;

            btnBaglantiKes.Enabled = false;
            groupBox2.Enabled = false;
            txtYedekKonum1.Enabled = false;
            txtYedekKonum2.Enabled = false;

           
            dt.Clear();
           
            dt.Columns.Clear();

                   

            chckPazartesi.Checked = false;
            chckSali.Checked = false;
            chckCarsamba.Checked = false;
            chckPersembe.Checked = false;
            chckCuma.Checked = false;
            chckCumartesi.Checked = false;
            chckPazar.Checked = false;

            chckOtomatikYedekle.Checked = false;
            chckZamandaYedekle.Checked = false;
            chckYedekleriArsivle.Checked = false;

            gridYedek.DataSource = "";
            labelControl10.Text = "0";

            conn.Close();
            conn.Dispose();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (txtYedekKonum1.Text=="")
            {
                MessageBox.Show("Lütfen Veritabanlarının Yedekleneceği Dizini Seçiniz");
            }
            else
            {
                YedekAl();
            }            
        }
        private void btnYedekKonum1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                txtYedekKonum1.Text = dlg.SelectedPath;
                txtYedekKonum2.Text = dlg.SelectedPath;
            }
        }
        private void txtYedekKonum2_TextChanged(object sender, EventArgs e)
        {
            RestoreGridGetir();
        }
        private void btnYedekKonum2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
             txtYedekKonum2.Text = dlg.SelectedPath;
             
            } 
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            Restore();
        }      
        private void chckBaslangictaCalis_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBaslangictaCalis.Checked)
            {
                chckOtomatikYedekle.Enabled = true;
                chckZamandaYedekle.Enabled = true;
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue(programadi, "\"" + Application.ExecutablePath + "\"");
            }
            else
            {
                chckOtomatikYedekle.Enabled = false;
                chckZamandaYedekle.Enabled = false;
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue(programadi);
            }
        }       
        private void chckOtomatikYedekle_CheckedChanged(object sender, EventArgs e)
        {
            if (chckOtomatikYedekle.Checked)
            {
                panel2.Enabled = true;
                chckZamandaYedekle.Enabled = true;               
            }          
            else
            {
                panel2.Enabled = false;
                chckZamandaYedekle.Enabled = false;
                chckZamandaYedekle.Checked = false;
            }
        }
        private void chckZamandaYedekle_CheckedChanged(object sender, EventArgs e)
        {
            if (chckZamandaYedekle.Checked)
            {
                timeOtoYedekSaati.Enabled = true;
            }
            else
            {
                timeOtoYedekSaati.Enabled = false;
            }
        }
        private void btnAyarKaydet_Click(object sender, EventArgs e)
        {           
            if (txtYedekKonum1.Text == "")
            {
                MessageBox.Show("Lütfen Yedeklemek İstediğiniz Konumu Seçiniz");
            }
            else
            {
                yedeklenecekVeritabanlari = "";
                YedekKonum = txtYedekKonum1.Text;
                if (chckYedekleriArsivle.Checked)
                {
                    yedeklerArsivlensin = true;
                }
                if (chckZamandaYedekle.Checked)
                {
                    timeOtoYedekSaati.Enabled = true;
                    seciliSaat = timeOtoYedekSaati.EditValue.ToString();
                    zamandaYedekle = true;
                    string[] parcalazaman = new string[2];
                    parcalazaman = seciliSaat.Split();
                    if (parcalazaman.Length == 1)
                    {
                        MessageBox.Show("Lütfen Otomatik Yedek Saatini Tekrar Seçerek Güncelleyiniz");
                        return;
                    }
                    else
                    {
                        parcalazaman[0] = DateTime.Now.ToShortDateString();
                        seciliSaat = parcalazaman[1];
                    }
                    
                }

                string value = "";
                int sayac = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    value = gridView1.GetDataRow(i)["SEÇİM"].ToString();
                    if (value == "True")
                    {
                        yedeklenecekVeritabanlari += gridView1.GetDataRow(i)["DATABASE_NAME"].ToString();
                        yedeklenecekVeritabanlari += ",";
                        sayac++;
                    }
                }
                if (sayac == 0)
                {
                    MessageBox.Show("Lütfen Yedeklemek İstediğiniz Veritabanlarını Seçiniz");
                    sayac = 0;
                }
                else
                {
                    if (chckPazartesi.Checked==true)
                    {
                        seciligunler[0] = "Monday";
                    }
                    else
                    {
                        seciligunler[0] = null;
                    }
                    if (chckSali.Checked==true)
                    {
                        seciligunler[1] = "Tuesday";
                    }
                    else
                    {
                        seciligunler[1] = null;
                    }
                    if (chckCarsamba.Checked==true)
                    {
                        seciligunler[2] = "Wednesday";
                    }
                    else
                    {
                        seciligunler[2] = null;
                    }
                    if (chckPersembe.Checked==true)
                    {
                        seciligunler[3] = "Thursday";
                    }
                    else
                    {
                        seciligunler[3] = null;
                    }
                    if (chckCuma.Checked==true)
                    {
                        seciligunler[4] = "Friday";
                    }
                    else
                    {
                        seciligunler[4] = null;
                    }
                    if (chckCumartesi.Checked==true)
                    {
                        seciligunler[5] = "Saturday";
                    }
                    else
                    {
                        seciligunler[5] = null;
                    }
                    if (chckPazar.Checked==true)
                    {
                        seciligunler[6] = "Sunday";
                    }
                    else
                    {
                        seciligunler[6] = null;
                    }
                    if (chckOtomatikYedekle.Checked)
                    {
                        otomatikYedekle = true;
                    }
                    else
                    {
                        otomatikYedekle = false;
                    }
                    if (chckYedekleriArsivle.Checked)
                    {
                        yedeklerArsivlensin = true;
                    }
                    else
                    {
                        yedeklerArsivlensin = false;
                    }
                    if (chckZamandaYedekle.Checked)
                    {
                        zamandaYedekle = true;
                    }
                    else
                    {
                        zamandaYedekle = false;
                    }

                    string dbseciligunler = "";
                    int say = 0;
                    int secilmeyengunsay = 0;
                    foreach (var item in seciligunler)
                    {
                        if (item == null)
                        {
                            secilmeyengunsay++;
                        }
                        if ( (chckZamandaYedekle.Checked=true) && (secilmeyengunsay == 7))
                        {
                            MessageBox.Show("Lütfen En Az Bir Otomatik Yedek Günü Seçiniz ");
                            return;
                        }
                        if ((item != null && item != "") && say == 0)
                        {
                            dbseciligunler = item;
                            say++;
                            continue;
                        }
                        if ((item != null && item != "") && say > 0)
                        {
                            dbseciligunler += "," + item;
                        }
                    }
                    connectionString += "; Initial Catalog=ayarlar";
                    conn = new SqlConnection(connectionString);
                    conn.Open();
                    sql = "UPDATE ayar set sunucu_adi='" + txtServerAdi.Text + "', kullanici_adi='" + txtKullaniciAdi.Text + "', kullanici_sifre='" + txtSifre.Text + "', otomatik_yedek='" + otomatikYedekle + "', yedek_gun='" + dbseciligunler + "', yedek_saati='" + seciliSaat + "', yedek_sil='" + numGunYedekSil.Value + "', yedek_konum1='" + txtYedekKonum1.Text + "', yedek_konum2='" + txtYedekKonum2.Text + "', zamanda_yedekle='" + zamandaYedekle + "', yedekleri_arsivle='" + yedeklerArsivlensin + "', veritabanlari='" + yedeklenecekVeritabanlari + "' where ayar_id=0";

                    command = new SqlCommand(sql, conn);

                    command.ExecuteNonQuery();
                    conn.Close();

                    dbseciligunler = "";
                    MessageBox.Show("Ayarlar Kaydedildi");

                    Tetikleyici tetikle = new Tetikleyici();
                    tetikle.GoreviTetikle();

                }
            }
           
        }        
        public void RestoreGridGetir()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Load(reader);
            dt.Columns.Add(new DataColumn("VERİTABANI ADI", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("YEDEK ALINMA TARİHİ", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("SEÇİM", Type.GetType("System.Boolean")));

            if (txtYedekKonum2.Text != "")
            {
                string[] databases = Directory.GetFiles(txtYedekKonum2.Text);
                foreach (string item in databases)
                {
                    if (item.EndsWith(".bak"))
                    {
                        dt.Rows.Add(Path.GetFileName(item), File.GetCreationTime(item).ToString());
                    }
                }
            }
            gridRestore.DataSource = dt;
            labelControl15.Text = dt.Rows.Count.ToString();
        }
        public void YedekAl()
        {                    
                try
                {
                    conn = new SqlConnection(connectionString);
                    conn.Open();
                    string value = "";
                    string name = "";
                    int sayac = 0;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        value = gridView1.GetDataRow(i)["SEÇİM"].ToString();
                        if (value == "True")
                        {
                            name = gridView1.GetDataRow(i)["DATABASE_NAME"].ToString();
                            if (chckYedekleriArsivle.Checked)
                            {
                                sql = "BACKUP DATABASE " + name + " TO DISK = '" + txtYedekKonum1.Text + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak' WITH COMPRESSION";
                            }
                            else
                            {
                                sql = "BACKUP DATABASE " + name + " TO DISK = '" + txtYedekKonum1.Text + "\\" + name + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
                            }
                            command = new SqlCommand(sql, conn);
                            sayac++;
                            command.ExecuteNonQuery();
                        }
                   
                }
                    
                 
                    MessageBox.Show("Veritabanı Yedeklemesi Başarılı Bir Şekilde Tamamlandı");
                    RestoreGridGetir();
                    name = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }            
            }
        
        public void Restore()
        {
            try
            {

                conn = new SqlConnection(connectionString);
                conn.Open();

                string value = "";
                string[] name;
                string yeniad = "";
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    value = gridView2.GetDataRow(i)["SEÇİM"].ToString();
                    if (value == "True")
                    {

                        name = gridView2.GetDataRow(i)["VERİTABANI ADI"].ToString().Split('-');
                        yeniad = name[0];
                        sql = "Alter Database " + yeniad + " Set SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                        sql += "Restore Database " + yeniad + " FROM Disk = '" + txtYedekKonum2.Text  + "\\" + gridView2.GetDataRow(i)["VERİTABANI ADI"].ToString() + "' WITH REPLACE;";
                        command = new SqlCommand(sql, conn);
                        command.ExecuteNonQuery();
                    }
                }               
                conn.Close();
                conn.Dispose();
                MessageBox.Show("Veritabanı Geri Yükleme İşlemi Başarılı Bir Şekilde Tamamlandı");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtYedekKonum2.Text != "")
            {
                string[] dosyalar = Directory.GetFiles(txtYedekKonum2.Text);
                int sayac = 0;
                foreach (var dosya in dosyalar)
                {
                    if (dosya.EndsWith(".bak"))
                    {
                        
                        int cikarilacakgun = Convert.ToInt32(numGunYedekSil.Value);
                        DateTime olusturmaZamani = Directory.GetCreationTime(dosya);
                        string[] olusturmaTarihi = new string[2];
                        olusturmaTarihi = olusturmaZamani.ToString().Split();
                        string gunceltarih = DateTime.Now.AddDays(-cikarilacakgun).ToShortDateString();
                        sayac++;
                        if (olusturmaTarihi[0]==gunceltarih)
                        {
                            File.Delete(dosya);
                        }
                    }                       
                }
                if (sayac == 0)
                {
                    MessageBox.Show("Silinecek Veritabanı bulunamadı!");
                }
                else
                {
                    MessageBox.Show("Veritabanları Başarıyla Silindi");
                    RestoreGridGetir();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Dosya Yolunu Belirtiniz ");
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            RestoreGridGetir();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                gridView2.SetRowCellValue(i, "SEÇİM", "False");
            }
        }
    }
}
