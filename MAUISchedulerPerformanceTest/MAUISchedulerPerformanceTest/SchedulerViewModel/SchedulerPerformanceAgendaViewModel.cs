using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MAUISchedulerPerformanceTest
{
    public class SchedulerPerformanceAgendaViewModel : INotifyPropertyChanged
    {
        private readonly Random random = new();

        private ObservableCollection<Meeting> appointments = new ObservableCollection<Meeting>();
        private ObservableCollection<Meeting> allAppointments = new ObservableCollection<Meeting>();

        private bool showBusyIndicator;

        public ObservableCollection<Meeting> Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }

        public bool ShowBusyIndicator
        {
            get => showBusyIndicator;
            set
            {
                showBusyIndicator = value;
                OnPropertyChanged(nameof(ShowBusyIndicator));
            }
        }

        public ICommand QueryAppointmentsCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SchedulerPerformanceAgendaViewModel()
        {
            GenerateAppointments(50000);

            Appointments = new ObservableCollection<Meeting>();

            QueryAppointmentsCommand = new Command<object>(ExecuteQueryAppointments,CanExecuteQueryAppointments);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecuteQueryAppointments(object arg)
        {
            return true;
        }

        private async void ExecuteQueryAppointments(object arg)
        {
            if (arg is not SchedulerQueryAppointmentsEventArgs queryArgs)
                return;

            ShowBusyIndicator = true;

            await Task.Delay(100);

            var visibleAppointments = GetAppointmentsInVisibleDates(queryArgs.VisibleDates);

            Appointments = visibleAppointments;

            ShowBusyIndicator = false;
        }

        private ObservableCollection<Meeting> GetAppointmentsInVisibleDates(IList<DateTime> visibleDates)
        {
            if (allAppointments == null || allAppointments.Count == 0)
            {
                return new ObservableCollection<Meeting>();
            }

            DateTime startDate = visibleDates.First().Date;
            DateTime endDate = visibleDates.Last().Date;

            var filteredAppointments =
                allAppointments.Where(app =>
                    (app.StartTime.Date >= startDate && app.StartTime.Date <= endDate) ||

                    (app.EndTime.Date >= startDate && app.EndTime.Date <= endDate) ||

                    (app.StartTime.Date <= startDate && app.EndTime.Date >= endDate))
                .ToList();

            return new ObservableCollection<Meeting>(filteredAppointments);
        }

        private void GenerateAppointments(int count)
        {
            allAppointments = new ObservableCollection<Meeting>();

            DateTime startDate = new DateTime(2023, 1, 1);

            for (int i = 0; i < count; i++)
            {
                int monthOffset = i % 48;

                DateTime monthStart = startDate.AddMonths(monthOffset);

                DateTime appointmentStart = monthStart.AddDays(random.Next(DateTime.DaysInMonth(monthStart.Year,monthStart.Month)))
                                                      .AddHours(random.Next(24))
                                                      .AddMinutes(random.Next(60));

                allAppointments.Add(new Meeting
                {
                    Id = i,
                    Subject = $"Meeting {i + 1}",
                    StartTime = appointmentStart,
                    EndTime = appointmentStart.AddHours(random.Next(1, 4)),
                    Notes = $"Performance appointment {i + 1}",
                    Location = $"Room {random.Next(1, 100)}",
                    IsAllDay = false,
                    BackgroundColor = new SolidColorBrush(GetRandomColor())
                });
            }
        }

        private Color GetRandomColor()
        {
            return Color.FromRgb(random.Next(40, 255),
                                 random.Next(40, 255),
                                 random.Next(40, 255));
        }
    }
}
