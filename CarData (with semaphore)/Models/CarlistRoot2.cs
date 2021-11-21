using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarData__with_semaphore_
{
    public class CarlistRoot2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnpropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private ObservableCollection<Car> _Cars2 { get; set; }

        public ObservableCollection<Car> Cars2
        {
            get { return _Cars2; }
            set { _Cars2 = value; OnpropertyChanged(); }
        }


        public CarlistRoot2()
        {
            Cars2 = new ObservableCollection<Car>();
        }

        public void Add(string carmodel, string carvendor, long caryear)
        {

            Cars2.Add(new Car
            {

                CarModel = carmodel,
                CarVendor = carvendor,
                CarYear = caryear


            });

        }

    }
}
