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
    public class CarlistRoot5 : INotifyPropertyChanged
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


        private ObservableCollection<Car> _Cars5 { get; set; }

        public ObservableCollection<Car> Cars5
        {
            get { return _Cars5; }
            set { _Cars5 = value; OnpropertyChanged(); }
        }


        public CarlistRoot5()
        {
            Cars5 = new ObservableCollection<Car>();
        }

        public void Add(string carmodel, string carvendor, long caryear)
        {

            Cars5.Add(new Car
            {

                CarModel = carmodel,
                CarVendor = carvendor,
                CarYear = caryear


            });

        }

    }
}
