using CarData__with_semaphore_.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CarData__with_semaphore_.View_Models
{


    public class MainViewModel : BaseViewModel
    {
        public MainWindow MainWindows { get; set; }
        public RelayCommand WriteFileCommand { get; set; }
        public RelayCommand StartCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public RelayCommand OnlyNumberCommand { get; set; }

        private ObservableCollection<Car> _Cars { get; set; }

        public ObservableCollection<Car> Cars
        {
            get { return _Cars; }
            set { _Cars = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CarlistRoot> _rootObjectlist { get; set; }

        public ObservableCollection<CarlistRoot> rootObjectlist
        {
            get { return _rootObjectlist; }
            set { _rootObjectlist = value; OnPropertyChanged(); }
        }

        public CarlistRoot _rootObject { get; set; }

        public CarlistRoot rootObject
        {
            get { return _rootObject; }
            set { _rootObject = value; OnPropertyChanged(); }
        }


        public CarlistRoot1 _rootObject1 { get; set; }

        public CarlistRoot1 rootObject1
        {
            get { return _rootObject1; }
            set { _rootObject1 = value; OnPropertyChanged(); }
        }

        public CarlistRoot2 _rootObject2 { get; set; }

        public CarlistRoot2 rootObject2
        {
            get { return _rootObject2; }
            set { _rootObject2 = value; OnPropertyChanged(); }
        }

        public CarlistRoot3 _rootObject3 { get; set; }

        public CarlistRoot3 rootObject3
        {
            get { return _rootObject3; }
            set { _rootObject3 = value; OnPropertyChanged(); }
        }

        public CarlistRoot4 _rootObject4 { get; set; }

        public CarlistRoot4 rootObject4
        {
            get { return _rootObject4; }
            set { _rootObject4 = value; OnPropertyChanged(); }
        }

        public CarlistRoot5 _rootObject5 { get; set; }

        public CarlistRoot5 rootObject5
        {
            get { return _rootObject5; }
            set { _rootObject5 = value; OnPropertyChanged(); }
        }

        public Car _car { get; set; }

        public Car Car
        {
            get { return _car; }
            set { _car = value; OnPropertyChanged(); }
        }


        bool ca = false;
        bool cc = false;
        bool cd = false;
        bool ce = false;
        bool s = false;
        bool multi = false;

        string carm = string.Empty;
        string carv = string.Empty;
        long cary = 0;

        long second = 0;
        long minute = 0;
        long hour = 0;

        long length =  0;
        long length1 = 0;
        long length3 = 0;
        long length4 = 0;
        long length5 = 0;

        string Timertext = string.Empty;


        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer timer3 = new DispatcherTimer();
        DispatcherTimer timer3_2 = new DispatcherTimer();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        Semaphore semaphore = new Semaphore(1, 5, "UploadSingle");
        Semaphore semaphore2 = new Semaphore(1, 5, "UploadMulti");

        DispatcherTimer timer_rb0= new DispatcherTimer();
        DispatcherTimer timer_rb1 = new DispatcherTimer();
        DispatcherTimer timer_rb3 = new DispatcherTimer();
        DispatcherTimer timer_rb4 = new DispatcherTimer();
        DispatcherTimer timer_rb5 = new DispatcherTimer();


        public MainViewModel()
        {
            if (!File.Exists($"../../../CatalogA.json"))
            {
                File.Create($"../../../CatalogA.json").Close();
            }

            //if (!File.Exists($"../../../CataloB.json"))
            //{
            //    File.Create($"../../../CatalogB.json").Close();
            //}

            if (!File.Exists($"../../../CatalogC.json"))
            {
                File.Create($"../../../CatalogC.json").Close();
            }

            if (!File.Exists($"../../../CatalogD.json"))
            {
                File.Create($"../../../CatalogD.json").Close();
            }

            if (!File.Exists($"../../../CatalogE.json"))
            {
                File.Create($"../../../CatalogE.json").Close();
            }

            if (!File.Exists($"../../../Catalog.json"))
            {
                File.Create($"../../../Catalog.json").Close();
            }







            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
           // timer.Start();

            timer2.Tick += Timer2_Tick;
            timer2.Start();

            timer3.Interval = new TimeSpan(0, 0, 0, 1,0);
            timer3.Tick += Timer3_Tick;

            timer_rb0.Tick += Timer_rb0_Tick;
            timer_rb0.Interval = new TimeSpan(0, 0, 0, 1, 0);
                                                      
            timer_rb1.Tick += Timer_rb1_Tick;         
            timer_rb1.Interval = new TimeSpan(0, 0, 0, 1, 0);
                                                       
            timer_rb3.Tick += Timer_rb3_Tick;          
            timer_rb3.Interval = new TimeSpan(0, 0, 0, 1, 0);
                                                       
            timer_rb4.Tick += Timer_rb4_Tick;          
            timer_rb4.Interval = new TimeSpan(0, 0, 0, 1, 0);
                                                       
            timer_rb5.Tick += Timer_rb5_Tick;          
            timer_rb5.Interval = new TimeSpan(0, 0, 0, 1, 0);

            timer3_2.Tick += Timer3_2_Tick;
            timer3_2.Interval = new TimeSpan(0, 0, 0, 1, 0);

            Thread thread = new Thread(() =>
            {

                
                Thread.Sleep(10000);
                UploadSingleMode(semaphore);
                
            });
            






           // timer3.Start();

            WriteFileCommand = new RelayCommand((sender) =>
            {
                Add();

                MainWindows.CarmodelTbox.Text = string.Empty;
                MainWindows.CarVendorTbox.Text = string.Empty;
                MainWindows.CarYearTbox.Text = string.Empty;
            });

            StartCommand = new RelayCommand((sender) =>
            {

                try
                {


                    length = new System.IO.FileInfo($"../../../Catalog.json").Length;
                    length1 = new System.IO.FileInfo($"../../../CatalogA.json").Length;
                    length3 = new System.IO.FileInfo($"../../../CatalogC.json").Length;
                    length4 = new System.IO.FileInfo($"../../../CatalogD.json").Length;
                    length5 = new System.IO.FileInfo($"../../../CatalogE.json").Length;

                    if (File.Exists($"../../../Catalog.json") || File.Exists($"../../../CatalogA.json") || File.Exists($"../../../CatalogC.json") || File.Exists($"../../../CatalogD.json") || File.Exists($"../../../Catalog.json"))
                    {

                        
                        if (s == true)
                        {

                            if (length >= 0.0005 || length1 >= 0.0005 || length3 >= 0.0005 || length4 >= 0.0005 || length5 >= 0.0005)
                            {


                                                timer3.Start();
                                                timer2.Start();

                                                thread.Start();
                        
                            }
                            else
                            {
                                MessageBox.Show("Files are empty.");
                            }
                        }

                        if (multi==true) 
                        {

                            if (length >= 0.0005 || length1 >= 0.0005 || length3 >= 0.0005 || length4 >= 0.0005 || length5 >= 0.0005)
                            {


                                try
                                {
                                    timer3.Stop();
                                    timer2.Start();

                                    Thread.Sleep(2000);
                                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_1(cancellationTokenSource, semaphore2); });
                                    Thread.Sleep(2000);
                                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_3(cancellationTokenSource, semaphore2); });
                                    Thread.Sleep(2000);
                                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_4(cancellationTokenSource, semaphore2); });
                                    Thread.Sleep(2000);
                                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_5(cancellationTokenSource, semaphore2); });

                                  
                                    timer3_2.Start();


                                }
                                catch (Exception)
                                {


                                }
                            }
                            else
                            {
                                MessageBox.Show("Files are empty.");
                            }

                        }

                        if(multi == false && s == false)
                        {
                            MessageBox.Show($"Select mode.");
                        }

                    }


                    else
                    {
                        MessageBox.Show("Thread dead.");
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Thread dead.");
                }
            });

            CancelCommand = new RelayCommand((sender) =>
            {

                if (s == true)
                {
                    timer3.Stop();
                    thread.Abort();
                    if (thread.IsAlive == false)
                    {


                        if (File.Exists($"../../../CatalogA.json"))
                        {
                            File.Delete($"../../../CatalogA.json");
                        }

                        //if (!File.Exists($"../../../CataloB.json"))
                        //{
                        //    File.Create($"../../../CatalogB.json").Close();
                        //}

                        if (File.Exists($"../../../CatalogC.json"))
                        {
                            File.Delete($"../../../CatalogC.json");
                        }

                        if (File.Exists($"../../../CatalogD.json"))
                        {
                            File.Delete($"../../../CatalogD.json");
                        }

                        if (File.Exists($"../../../CatalogE.json"))
                        {
                            File.Delete($"../../../CatalogE.json");
                        }

                        if (File.Exists($"../../../Catalog.json"))
                        {
                            File.Delete($"../../../Catalog.json");
                        }


                        timer3.Stop();
                    }

                    MessageBox.Show($"Thread killed.");
                }

                if (multi == true)
                {
                    Thread.Sleep(2000);
                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_1(cancellationTokenSource, semaphore2); });
                    Thread.Sleep(2000);
                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_3(cancellationTokenSource, semaphore2); });
                    Thread.Sleep(2000);
                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_4(cancellationTokenSource, semaphore2); });
                    Thread.Sleep(2000);
                    ThreadPool.QueueUserWorkItem((j) => { UploadMultiModeWhichusedasSingle_5(cancellationTokenSource, semaphore2); });

                    if (File.Exists($"../../../CatalogA.json"))
                    {
                        File.Delete($"../../../CatalogA.json");
                    }

                    //if (!File.Exists($"../../../CataloB.json"))
                    //{
                    //    File.Create($"../../../CatalogB.json").Close();
                    //}

                    if (File.Exists($"../../../CatalogC.json"))
                    {
                        File.Delete($"../../../CatalogC.json");
                    }

                    if (File.Exists($"../../../CatalogD.json"))
                    {
                        File.Delete($"../../../CatalogD.json");
                    }

                    if (File.Exists($"../../../CatalogE.json"))
                    {
                        File.Delete($"../../../CatalogE.json");
                    }

                    if (File.Exists($"../../../Catalog.json"))
                    {
                        File.Delete($"../../../Catalog.json");
                    }

                    timer3_2.Stop();

                    MessageBox.Show($"Threadpool killed.");
                }



            });


            OnlyNumberCommand = new RelayCommand((sender) =>
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(MainWindows.CarYearTbox.Text, @"[^0-9.]"))
                {
                    MessageBox.Show("Please enter only number.");
                    MainWindows.CarYearTbox.Text = MainWindows.CarYearTbox.Text.Remove(MainWindows.CarYearTbox.Text.Length - 1);
                }

            });

        }

        private void Timer3_2_Tick(object sender, EventArgs e)
        {
            MainWindows.Dispatcher.BeginInvoke(new Action(() =>
            {

                second += 1;

                if (second == 60)
                {
                    second = 0;
                    minute += 1;
                }
                if (minute == 60)
                {
                    minute = 0;
                    hour += 1;
                }

                MainWindows.TimerTextblock.Text = string.Format("{0}:{1}:{2}", hour, minute, second);
            }));
        }

        private void Timer_rb4_Tick(object sender, EventArgs e)
        {
            rootObject4 = DeserializeFile4();


            foreach (var item in rootObject4.Cars4)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }
        }

        private void Timer_rb5_Tick(object sender, EventArgs e)
        {
            rootObject5 = DeserializeFile5();


            foreach (var item in rootObject5.Cars5)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }
        }

        private void Timer_rb3_Tick(object sender, EventArgs e)
        {
            rootObject3 = DeserializeFile3();


            foreach (var item in rootObject3.Cars3)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }
        }

        private void Timer_rb1_Tick(object sender, EventArgs e)
        {
            rootObject1 = DeserializeFile1();


            foreach (var item in rootObject1.Cars1)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }
        }

        private void Timer_rb0_Tick(object sender, EventArgs e)
        {
            rootObject = DeserializeFile();


            foreach (var item in rootObject.Cars)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }

        }

        private void Timer3_Tick(object sender, EventArgs e)
        {

        

            MainWindows.Dispatcher.BeginInvoke(new Action(() =>
            {

                second += 1;
                
                if (second == 60)
                {
                    second = 0;
                    minute += 1;
                }
                if(minute==60)
                {
                    minute = 0;
                    hour += 1;
                }

                MainWindows.TimerTextblock.Text = string.Format("{0}:{1}:{2}",hour,minute,second);
            }));
        }


        private void Timer2_Tick(object sender, EventArgs e)
        {



            MainWindows.Dispatcher.BeginInvoke(new Action(() =>
            {


                ca = (bool)MainWindows.CatalogARadioButton.IsChecked;
                cc = (bool)MainWindows.CatalogCRadioButton.IsChecked;
                cd = (bool)MainWindows.CatalogDRadioButton.IsChecked;
                ce = (bool)MainWindows.CatalogERadioButton.IsChecked;
                s = (bool)MainWindows.SingleRadioButton.IsChecked;
                multi= (bool)MainWindows.MultiRadioButton.IsChecked;
                carm = MainWindows.CarmodelTbox.Text;
                carv = MainWindows.CarVendorTbox.Text;
         
                if (long.TryParse(MainWindows.CarYearTbox.Text, out cary)) { }

                Timertext = MainWindows.TimerTextblock.Text;
            }));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            rootObject = DeserializeFile();


            rootObject1 = DeserializeFile1();
            //rootObject2 = DeserializeFile2();


            rootObject3 = DeserializeFile3();


            rootObject4 = DeserializeFile4();


            rootObject5 = DeserializeFile5();


            foreach (var item in rootObject1.Cars1)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }

            foreach (var item in rootObject3.Cars3)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }

            foreach (var item in rootObject4.Cars4)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }


            foreach (var item in rootObject5.Cars5)
            {
                if (!MainWindows.listbox1.Items.Contains(item.FullData))
                {

                    MainWindows.listbox1.Items.Add(item.FullData);
                }
            }
        }

        private void UploadSingleMode(object state)
        {
          


            try
            {

                var s = state as Semaphore;


                bool st = false;

                while (!st)
                {
                    if (s.WaitOne(500))
                    {
                        try
                        {
                            Thread.Sleep(1000);

                            timer.Start();

                            timer3.Stop();

                            MessageBox.Show($"Thread to enter the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");

                        }
                        finally
                        {

                            st = true;
                            MessageBox.Show($"Thread get out of the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");
                        }
                    }

                    else
                    {
                        try
                        {
                            s.Release();
                        }
                        catch (Exception)
                        {


                        }
                    }
                }

            }
            catch (Exception)
            {


            }
        }

        private void UploadMultiModeWhichusedasSingle_1(CancellationTokenSource cancellationTokensource, object state )
        {
            if (!cancellationTokensource.IsCancellationRequested)
            {
                var s = state as Semaphore;


                bool st = false;

                while (!st)
                {
                    if (s.WaitOne(500))
                    {
                        try
                        {

                            timer2.Start();

                            timer_rb1.Start();



                            Thread.Sleep(1000);


                            MessageBox.Show($"Thread to enter the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");

                        }
                        finally
                        {

                            st = true;
                            MessageBox.Show($"Thread get out of the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");
                        }
                    }

                    else
                    {
                        try
                        {
                            s.Release();
                        }
                        catch (Exception)
                        {


                        }
                    }
                }

                Thread.Sleep(2000);
            }

            if (cancellationTokensource.IsCancellationRequested)
            {
                cancellationTokensource.Cancel();
            }
        }

        private void UploadMultiModeWhichusedasSingle_3(CancellationTokenSource cancellationTokensource, object state )
        {

            if (!cancellationTokensource.IsCancellationRequested)
            {
                var s = state as Semaphore;


                bool st = false;

                while (!st)
                {
                    if (s.WaitOne(500))
                    {
                        try
                        {




                            timer2.Start();

                            timer_rb3.Start();



                            Thread.Sleep(1000);

                            MessageBox.Show($"Thread to enter the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");

                        }
                        finally
                        {

                            st = true;
                            MessageBox.Show($"Thread get out of the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");
                        }
                    }

                    else
                    {
                        try
                        {
                            s.Release();
                        }
                        catch (Exception)
                        {


                        }
                    }
                }

                Thread.Sleep(2000);
            }

            if (cancellationTokensource.IsCancellationRequested)
            {
                cancellationTokensource.Cancel();
            }

        }
        private void UploadMultiModeWhichusedasSingle_4(CancellationTokenSource cancellationTokensource, object state )
        {

            if (!cancellationTokensource.IsCancellationRequested)
            {
                var s = state as Semaphore;


                bool st = false;

                while (!st)
                {
                    if (s.WaitOne(500))
                    {
                        try
                        {




                            timer2.Start();

                            timer_rb4.Start();



                            Thread.Sleep(1000);

                            MessageBox.Show($"Thread to enter the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");

                        }
                        finally
                        {

                            st = true;
                            MessageBox.Show($"Thread get out of the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");
                        }
                    }

                    else
                    {
                        try
                        {
                            s.Release();
                        }
                        catch (Exception)
                        {


                        }
                    }
                }

                Thread.Sleep(2000);

            }
            if (cancellationTokensource.IsCancellationRequested)
            {
                cancellationTokensource.Cancel();
            }
        }
                                                                                   
        private void UploadMultiModeWhichusedasSingle_5(CancellationTokenSource cancellationTokensource, object state)
        {
            if (!cancellationTokensource.IsCancellationRequested)
            {

                var s = state as Semaphore;


                bool st = false;

                while (!st)
                {
                    if (s.WaitOne(500))
                    {
                        try
                        {



                            timer2.Start();

                            timer_rb5.Start();



                            Thread.Sleep(1000);


                            MessageBox.Show($"Thread to enter the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");

                        }
                        finally
                        {

                            st = true;
                            MessageBox.Show($"Thread get out of the Critical section, which is Id {Thread.CurrentThread.ManagedThreadId}");
                        }
                    }

                    else
                    {
                        try
                        {
                            s.Release();
                        }
                        catch (Exception)
                        {


                        }
                    }
                }

                Thread.Sleep(2000);
                timer3_2.Stop();

            }
            if (cancellationTokensource.IsCancellationRequested)
            {
                cancellationTokensource.Cancel();
            }
        }

        private void Add()
        {
            try
            {

                rootObject = DeserializeFile();
                if (ca == true /*|| CatalogBRadioButton.IsChecked == true*/ || cc == true || cd == true || ce == true)
                {
                    if (rootObject.Cars.Any(p => p.FullData == $" {carm}  {carv}  {cary} ") == false)
                    {

                        if (String.IsNullOrEmpty(carm) || String.IsNullOrEmpty(carv) || cary==0)
                        {
                            MessageBox.Show($"Empty field.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            if (ca == true)
                            {
                                rootObject = DeserializeFile();
                                rootObject1 = DeserializeFile1();

                                rootObject.Add(carm, carv, cary);
                                rootObject1.Add(carm, carv, cary);

                                SerializeList(rootObject);
                                SerializeList1(rootObject1);
                            }

                            //if (CatalogBRadioButton.IsChecked == true)
                            //{
                            //    rootObject = DeserializeFile();
                            //    rootObject2 = DeserializeFile2();

                            //    rootObject.Add(CarmodelTbox.Text, CarVendorTbox.Text, long.Parse(CarYearTbox.Text));
                            //    rootObject2.Add(CarmodelTbox.Text, CarVendorTbox.Text, long.Parse(CarYearTbox.Text));

                            //    SerializeList(rootObject);
                            //    SerializeList2(rootObject2);
                            //}

                            if (cc == true)
                            {
                                rootObject = DeserializeFile();
                                rootObject3 = DeserializeFile3();

                                rootObject.Add(carm, carv, cary);
                               rootObject3.Add(carm, carv, cary);

                                SerializeList(rootObject);
                                SerializeList3(rootObject3);
                            }

                            if (cd == true)
                            {
                                rootObject = DeserializeFile();
                                rootObject4 = DeserializeFile4();

                                rootObject.Add(carm,carv,cary);
                               rootObject4.Add(carm,carv,cary);

                                SerializeList(rootObject);
                                SerializeList4(rootObject4);
                            }

                            if (ce == true)
                            {
                                rootObject = DeserializeFile();
                                rootObject5 = DeserializeFile5();

                                rootObject.Add(carm,carv,cary);
                               rootObject5.Add(carm,carv,cary);

                                SerializeList(rootObject);
                                SerializeList5(rootObject5);
                            }
                        }

                        //foreach (var item in rootObject.Cars)
                        //{
                        //    if (!MainWindows.listbox1.Items.Contains(item.FullData))
                        //    {

                        //        MainWindows.listbox1.Items.Add(item.FullData);
                        //    }
                        //}

                    }
                    else
                    {
                        MessageBox.Show($"Same data.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }


                if (ca == false /*&& CatalogBRadioButton.IsChecked == false*/ && cc == false && cd == false && ce == false)
                {
                    MessageBox.Show($"Select catalog");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("This file can not find. May be thread dead.");
            }
           
        }


        private void SerializeList(CarlistRoot carlist)
        {
            var f2 = Newtonsoft.Json.Formatting.Indented;
            string data2 = JsonConvert.SerializeObject(carlist, f2);

            File.WriteAllText($"../../../Catalog.json", data2);

        }

        private void SerializeList1(CarlistRoot1 carlist1)
        {
            var f = Newtonsoft.Json.Formatting.Indented;
            string data = JsonConvert.SerializeObject(carlist1, f);

            File.WriteAllText($"../../../CatalogA.json", data);

        }

        //private void SerializeList2(CarlistRoot2 carlist2)
        //{
        //    var f = Newtonsoft.Json.Formatting.Indented;
        //    string data = JsonConvert.SerializeObject(carlist2, f);

        //    File.WriteAllText($"../../../CatalogB.json", data);

        //}

        private void SerializeList3(CarlistRoot3 carlist3)
        {
            var f = Newtonsoft.Json.Formatting.Indented;
            string data = JsonConvert.SerializeObject(carlist3, f);

            File.WriteAllText($"../../../CatalogC.json", data);

        }

        private void SerializeList4(CarlistRoot4 carlist4)
        {
            var f = Newtonsoft.Json.Formatting.Indented;
            string data = JsonConvert.SerializeObject(carlist4, f);

            File.WriteAllText($"../../../CatalogD.json", data);

        }


        private void SerializeList5(CarlistRoot5 carlist5)
        {
            var f = Newtonsoft.Json.Formatting.Indented;
            string data = JsonConvert.SerializeObject(carlist5, f);

            File.WriteAllText($"../../../CatalogE.json", data);

        }

        public CarlistRoot DeserializeFile()
        {
            if (File.Exists($"../../../Catalog.json") )
            {

                string data2 = File.ReadAllText($"../../../Catalog.json");

                rootObject = JsonConvert.DeserializeObject<CarlistRoot>(data2);

                if (rootObject != null)
                {
                    return rootObject;
                }

            }
                return new CarlistRoot();
        }

        public CarlistRoot1 DeserializeFile1()
        {
            if (File.Exists($"../../../Catalog.json"))
            {
                string data = File.ReadAllText($"../../../CatalogA.json");

                rootObject1 = JsonConvert.DeserializeObject<CarlistRoot1>(data);

                if (rootObject1 != null)
                {
                    return rootObject1;
                }
            }
            return new CarlistRoot1();
        }

        //public CarlistRoot2 DeserializeFile2()
        //{
        //    string data = File.ReadAllText($"../../../CatalogB.json");

        //    rootObject2 = JsonConvert.DeserializeObject<CarlistRoot2>(data);

        //    if (rootObject2 != null)
        //    {
        //        return rootObject2;
        //    }

        //    return new CarlistRoot2();
        //}

        public CarlistRoot3 DeserializeFile3()
        {
            if (File.Exists($"../../../CatalogC.json"))
            {
                string data = File.ReadAllText($"../../../CatalogC.json");

                rootObject3 = JsonConvert.DeserializeObject<CarlistRoot3>(data);

                if (rootObject3 != null)
                {
                    return rootObject3;
                }
            }
            return new CarlistRoot3();
        }

        public CarlistRoot4 DeserializeFile4()
        {
            if (File.Exists($"../../../CatalogD.json"))
            {
                string data = File.ReadAllText($"../../../CatalogD.json");

                rootObject4 = JsonConvert.DeserializeObject<CarlistRoot4>(data);

                if (rootObject4 != null)
                {
                    return rootObject4;
                }
            }

            return new CarlistRoot4();
        }

        public CarlistRoot5 DeserializeFile5()
        {
            if (File.Exists($"../../../CatalogE.json"))
            {
                string data = File.ReadAllText($"../../../CatalogE.json");

                rootObject5 = JsonConvert.DeserializeObject<CarlistRoot5>(data);

                if (rootObject5 != null)
                {
                    return rootObject5;
                }
            }

            return new CarlistRoot5();
        }

    }
}

