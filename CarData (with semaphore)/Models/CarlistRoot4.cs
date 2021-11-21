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
    public class CarlistRoot4 : INotifyPropertyChanged
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


        private ObservableCollection<Car> _Cars4 { get; set; }

        public ObservableCollection<Car> Cars4
        {
            get { return _Cars4; }
            set { _Cars4 = value; OnpropertyChanged(); }
        }


        public CarlistRoot4()
        {
            Cars4 = new ObservableCollection<Car>();
        }

        public void Add(string carmodel, string carvendor, long caryear)
        {

            Cars4.Add(new Car
            {

                CarModel = carmodel,
                CarVendor = carvendor,
                CarYear = caryear


            });

        }

    }
}
